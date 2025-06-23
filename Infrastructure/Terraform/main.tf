terraform {
  required_version = ">= 1.3.0"

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = ">= 5.0"
    }
  }
}

provider "aws" {
  region = var.aws_region
}

module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "5.1.1"

  name                    = "three-tier-vpc"
  cidr                    = var.vpc_cidr
  azs                     = var.availability_zones
  public_subnets          = ["10.0.1.0/24", "10.0.2.0/24"]
  private_subnets         = ["10.0.3.0/24", "10.0.4.0/24"]
  enable_nat_gateway      = true
  single_nat_gateway      = true
  map_public_ip_on_launch = true

  tags = {
    Name = "three-tier-vpc"
  }
}

# After the VPC module, tag each subnet individually
locals {
  public_subnet_ids  = module.vpc.public_subnets
  private_subnet_ids = module.vpc.private_subnets
}

resource "aws_ec2_tag" "public_subnet_names" {
  for_each    = { for idx, id in local.public_subnet_ids  : id => idx }
  resource_id = each.key
  key         = "Name"
  value       = "three-tier-vpc-public-${each.value + 1}"
}

resource "aws_ec2_tag" "private_subnet_names" {
  for_each    = { for idx, id in local.private_subnet_ids : id => idx }
  resource_id = each.key
  key         = "Name"
  value       = "three-tier-vpc-private-${each.value + 1}"
}

resource "aws_security_group" "eks_sg" {
  name        = "eks-sg"
  description = "Allow all traffic"
  vpc_id      = module.vpc.vpc_id

  ingress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

module "eks" {
  source          = "terraform-aws-modules/eks/aws"
  version         = "20.8.4"

  cluster_name    = var.eks_cluster_name
  cluster_version = "1.32"
  subnet_ids      = module.vpc.public_subnets
  vpc_id          = module.vpc.vpc_id

  eks_managed_node_groups = {
    eks_nodes = {
      desired_size   = 2
      max_size       = 3
      min_size       = 1
      instance_types = ["t3.medium"]
      disk_size      = 20
    }
  }

  cluster_endpoint_public_access = true

  tags = {
    Environment = "dev"
  }
}

resource "aws_security_group" "rds_sg" {
  name        = "rds-sg"
  description = "Allow SQL Server"
  vpc_id      = module.vpc.vpc_id

  ingress {
    from_port   = 1433
    to_port     = 1433
    protocol    = "tcp"
    cidr_blocks = [var.vpc_cidr]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_db_subnet_group" "rds_subnet_group" {
  name       = "rds-subnet-group"
  subnet_ids = module.vpc.private_subnets

  tags = {
    Name = "rds-subnet-group"
  }
}

resource "aws_db_instance" "rds_instance" {
  identifier             = var.db_instance_identifier
  engine                 = "sqlserver-ex"
  instance_class         = "db.t3.micro"
  allocated_storage      = 20
  storage_type           = "gp2"
  username               = var.db_username
  password               = var.db_password
  db_subnet_group_name   = aws_db_subnet_group.rds_subnet_group.name
  vpc_security_group_ids = [aws_security_group.rds_sg.id]
  publicly_accessible    = false
  multi_az               = false
  deletion_protection    = false
  skip_final_snapshot    = true
}

resource "aws_ecr_repository" "product_backend" {
  name = "product-backend"
}

resource "aws_ecr_lifecycle_policy" "product_backend_policy" {
  repository = aws_ecr_repository.product_backend.name
  policy     = file("ecr-lifecycle-policy.json")
}

resource "aws_ecr_repository" "product_frontend" {
  name = "product-frontend"
}

resource "aws_ecr_lifecycle_policy" "product_frontend_policy" {
  repository = aws_ecr_repository.product_frontend.name
  policy     = file("ecr-lifecycle-policy.json")
}

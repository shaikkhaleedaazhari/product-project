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

  # One tag-map per public subnet (in same order as public_subnets)
  public_subnet_tags = [
    { Name = "three-tier-vpc-public-1" },
    { Name = "three-tier-vpc-public-2" },
  ]

  # One tag-map per private subnet (in same order as private_subnets)
  private_subnet_tags = [
    { Name = "three-tier-vpc-private-1" },
    { Name = "three-tier-vpc-private-2" },
  ]

  tags = {
    Name = "three-tier-vpc"
  }
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

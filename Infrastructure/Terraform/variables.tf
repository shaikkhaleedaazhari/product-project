variable "aws_region" {
  description = "AWS region for resource provisioning"
  default     = "us-east-2"
}

variable "vpc_cidr" {
  description = "CIDR block for the VPC"
  default     = "10.0.0.0/16"
}

variable "availability_zones" {
  description = "List of AZs for subnets"
  type        = list(string)
  default     = ["us-east-2a", "us-east-2b"]
}

variable "eks_cluster_name" {
  description = "Name of the EKS cluster"
  default     = "MyEKSCluster"
}

variable "db_instance_identifier" {
  description = "Identifier for the RDS instance"
  default     = "sqlserver-express-ft"
}

variable "db_username" {
  description = "Master username for RDS"
}

variable "db_password" {
  description = "Master password for RDS"
  sensitive   = true
}

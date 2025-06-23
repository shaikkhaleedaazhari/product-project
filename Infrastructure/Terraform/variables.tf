variable "aws_region" {
  default = "us-east-2"
}

variable "vpc_cidr" {
  default = "10.0.0.0/16"
}

variable "availability_zones" {
  default = ["us-east-2a", "us-east-2b"]
}

variable "eks_cluster_name" {
  default = "MyEKSCluster"
}

variable "db_username" {}

variable "db_password" {
  sensitive = true
}

variable "db_instance_identifier" {
  default = "sqlserver-express-ft"
}

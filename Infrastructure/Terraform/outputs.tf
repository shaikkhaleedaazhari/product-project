output "vpc_id" {
  value = module.vpc.vpc_id
}

output "eks_cluster_name" {
  value = module.eks.cluster_name
}

output "backend_ecr_uri" {
  value = aws_ecr_repository.product_backend.repository_url
}

output "frontend_ecr_uri" {
  value = aws_ecr_repository.product_frontend.repository_url
}

output "rds_endpoint" {
  value = aws_db_instance.rds_instance.address
}

output "rds_port" {
  value = aws_db_instance.rds_instance.port
}

output "rds_security_group_id" {
  value = aws_security_group.rds_sg.id
}

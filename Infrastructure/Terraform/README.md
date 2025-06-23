# Terraform EKS + RDS + ECR Infrastructure Setup

This project provisions the following AWS infrastructure using Terraform:

- VPC with public/private subnets, NAT Gateway, and route tables
- EKS Cluster with managed Node Group
- RDS SQL Server Express instance (Free Tier eligible)
- ECR repositories for backend and frontend applications

## 🧰 Prerequisites

1. **Terraform**
   - [Install Terraform](https://developer.hashicorp.com/terraform/downloads)
   - On Ubuntu:
     ```bash
     sudo apt-get update && sudo apt-get install -y gnupg software-properties-common curl
     curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg
     echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list
     sudo apt-get update && sudo apt-get install terraform
     ```

2. **AWS CLI**
   - [Install AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)
   - Configure credentials:
     ```bash
     aws configure
     ```
     Provide:
     - AWS Access Key ID
     - AWS Secret Access Key
     - Region (e.g., `us-east-1`)

## 📁 File Structure


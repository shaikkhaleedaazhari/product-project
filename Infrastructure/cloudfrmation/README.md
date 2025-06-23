# 🚀 AWS Infrastructure: EKS, RDS (SQL Server), and ECR via CloudFormation

This repository provides an AWS CloudFormation template that sets up:

- A VPC with public/private subnets  
- Internet Gateway and NAT Gateway  
- Route tables  
- Amazon EKS Cluster with Node Group  
- Amazon RDS (SQL Server Express - Free Tier eligible)  
- Amazon ECR Repositories for Frontend and Backend  
- IAM roles and security groups

---

## 📦 Prerequisites

Make sure you have the following installed and configured:

- **AWS CLI** (v2+): [Install Guide](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)  
- AWS CLI configured via `aws configure`  
- Your CloudFormation template saved as `infrastructure.yaml`

---

## 🛠️ Installation Steps

### 1. ✅ Install AWS CLI

#### For Ubuntu:

```bash
curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
unzip awscliv2.zip
sudo ./aws/install
aws --version

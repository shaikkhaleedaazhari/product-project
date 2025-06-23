# Terraform EKS + RDS + ECR Infrastructure Setup

This project provisions the following AWS infrastructure using Terraform:

- VPC with public/private subnets, NAT Gateway, and route tables
- EKS Cluster with managed Node Group
- RDS SQL Server Express instance (Free Tier eligible)
- ECR repositories for backend and frontend applications

---

## 🧰 Prerequisites

### 1. Install Terraform

**Ubuntu:**
```bash
sudo apt-get update && sudo apt-get install -y gnupg software-properties-common curl
curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list
sudo apt-get update && sudo apt-get install terraform
```

**Other OS:** [Terraform Installation Guide](https://developer.hashicorp.com/terraform/downloads)

### 2. Install and Configure AWS CLI

Install AWS CLI: [AWS CLI Installation Guide](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)

Configure credentials:
```bash
aws configure
```
Enter:
- AWS Access Key ID
- AWS Secret Access Key
- Region (e.g., `us-east-1`)

---

## 📁 File Structure

```
.
├── main.tf                  # Core infrastructure: VPC, EKS, RDS, ECR
├── variables.tf             # Input variables
├── outputs.tf               # Outputs after apply
├── terraform.tfvars         # (Optional) Secrets/variables
└── README.md                # This file
```

---

## ⚙️ Terraform Usage

### Step 1: Clone the Repository
```bash
git clone <your-repo-url>
cd <repo-directory>
```

### Step 2: Initialize Terraform
```bash
terraform init
```

### Step 3: Preview the Changes
```bash
terraform plan
```

### Step 4: Apply the Infrastructure
You will be prompted to confirm:
```bash
terraform apply
```
Or auto-approve:
```bash
terraform apply -auto-approve
```

---

## 🔐 Secrets Handling

Use `terraform.tfvars` or pass variables inline:

**terraform.tfvars example:**
```hcl
db_username = "admin"
db_password = "yourStrongPassword123"
```

**Apply using tfvars file:**
```bash
terraform apply -var-file="terraform.tfvars"
```

Or use inline flags:
```bash
terraform apply \
  -var="db_username=admin" \
  -var="db_password=yourStrongPassword123"
```

---

## ✅ Outputs

After a successful apply, Terraform will output:

- `vpc_id`: VPC ID
- `eks_cluster_name`: EKS cluster name
- `backend_ecr_uri`: URI of backend ECR repo
- `frontend_ecr_uri`: URI of frontend ECR repo
- `rds_endpoint`: SQL Server RDS endpoint address
- `rds_port`: SQL Server RDS port
- `rds_security_group_id`: RDS security group ID

---

## 🧹 Cleanup

To destroy all provisioned infrastructure:
```bash
terraform destroy
```

---

## 📌 Notes

- EKS provisioning may take up to 15 minutes.
- Ensure selected region supports EKS and RDS SQL Server Express.
- RDS SQL Server Express is eligible for the AWS Free Tier.
- Tag your resources appropriately for cost tracking.

---

## 🔗 References
- [Terraform Docs](https://developer.hashicorp.com/terraform/docs)
- [Amazon EKS Documentation](https://docs.aws.amazon.com/eks/latest/userguide/what-is-eks.html)
- [Amazon RDS Documentation](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/Welcome.html)
- [Amazon ECR Documentation](https://docs.aws.amazon.com/AmazonECR/latest/userguide/what-is-ecr.html)

# AKS parameters
variable "location" {
  type = string
  description = "Azure Region where all these resources will be provisioned"
  default = "West Europe"
}

variable "resource_group_name" {
  type = string
  description = "This variable defines the Resource Group"
  default = "ms-tech-rg"
}

variable "aks_name" {
  type        = string
  description = "The AKS cluster name"
  default     = "ms-tech-cluster"
}

# SSH Public Key for Linux VMs
variable "ssh_public_key" {
  default = "~/.ssh/aks-prod-sshkeys-terraform/aksprodsshkey.pub"
  description = "This variable defines the SSH Public Key for Linux k8s Worker nodes"  
}

# Windows Admin Username for k8s worker nodes
variable "windows_admin_username" {
  type = string
  default = "azureuser"
  description = "This variable defines the Windows admin username k8s Worker nodes"  
}

# Windows Admin Password for k8s worker nodes
variable "windows_admin_password" {
  type = string
  default = "xxx"  # Can be set up inside a local terraform.tfvars file
  description = "This variable defines the Windows admin password k8s Worker nodes"  
}
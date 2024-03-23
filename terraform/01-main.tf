terraform {
  required_version = ">= 1.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.0"
    }
  }

  backend "azurerm" {
    resource_group_name   = "ms-tech-terraform-rg"
    storage_account_name  = "mstechterraformstate"
    container_name        = "tfstatefiles"
    key                   = "terraform.tfstate"
    subscription_id       = "XXXXXXX"
  }  
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

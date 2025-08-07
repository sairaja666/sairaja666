variable "resource_group_name" {
  description = "Name of the resource group"
  default     = "rg-aks-demo"
}

variable "location" {
  description = "Azure location"
  default     = "East US"
}

variable "cluster_name" {
  description = "AKS cluster name"
  default     = "aks-demo-cluster"
}

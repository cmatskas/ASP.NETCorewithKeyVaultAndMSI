terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 2.26"
    }
  }
   backend "azurerm" {
    resource_group_name  = "ndclondon"
    storage_account_name = "cmterraformdeployment"
    container_name       = "tfstate"
    key                  = "prod.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

data "azurerm_key_vault" "myKeyVault" {
  name                = "cm-identity-kv"
  resource_group_name = "identity"
}

data "azurerm_key_vault_secret" "admin_username" {
  name = "sqlAdministratorLogin"
  key_vault_id = data.azurerm_key_vault.myKeyVault.id
}

data "azurerm_key_vault_secret" "admin_password" {
  name = "sqlAdministratorLoginPassword"
  key_vault_id = data.azurerm_key_vault.myKeyVault.id
}

resource "azurerm_sql_server" "cmsqlserver" {
  name                           = "cmndcconfsqlsrv"
  resource_group_name            = var.resource_group_name
  location                       = var.location
  version                        = "12.0"
  administrator_login            = data.azurerm_key_vault_secret.admin_username.value
  administrator_login_password   = data.azurerm_key_vault_secret.admin_password.value
}

resource "azurerm_mssql_database" "test" {
  name           = "cmndcconfsqldb"
  server_id      = azurerm_sql_server.cmsqlserver.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 4
  read_scale     = true
  sku_name       = "BC_Gen5_2"
  zone_redundant = false
}
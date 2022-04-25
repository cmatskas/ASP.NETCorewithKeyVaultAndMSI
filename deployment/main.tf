resource "azurerm_sql_database" "res-12" {
  create_mode         = null
  location            = "westus"
  name                = "dm-devops-tf-demo-db"
  read_scale          = true
  resource_group_name = "terraform-demo"
  server_name         = "cm-devops-tf-demo-sqlserver"
  depends_on = [
    # Depending on "/subscriptions/e42acc2d-8462-4fb5-bf0d-d983c0017584/resourceGroups/terraform-demo/providers/Microsoft.Sql/servers/cm-devops-tf-demo-sqlserver", which is not imported by Terraform.
  ]
}
resource "azurerm_mssql_server_vulnerability_assessment" "res-26" {
  server_security_alert_policy_id = "/subscriptions/e42acc2d-8462-4fb5-bf0d-d983c0017584/resourceGroups/terraform-demo/providers/Microsoft.Sql/servers/cm-devops-tf-demo-sqlserver/securityAlertPolicies/Default"
  storage_account_access_key      = null # sensitive
  storage_container_path          = ""
  storage_container_sas_key       = null # sensitive
  depends_on = [
    # Depending on "/subscriptions/e42acc2d-8462-4fb5-bf0d-d983c0017584/resourceGroups/terraform-demo/providers/Microsoft.Sql/servers/cm-devops-tf-demo-sqlserver", which is not imported by Terraform.
  ]
}
resource "azurerm_resource_group" "res-47" {
  location = "westus"
  name     = "terraform-demo"
}
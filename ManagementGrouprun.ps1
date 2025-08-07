param($Request)

# Input: {
#   "managementGroupId": "myNewGroup",
#   "displayName": "My New Management Group",
#   "parentGroupId": "root"
# }

$body = Get-Content -Raw -InputObject $Request.Body | ConvertFrom-Json
$mgmtGroupId = $body.managementGroupId
$displayName = $body.displayName
$parentGroupId = $body.parentGroupId

# Acquire Azure access token
$tokenResponse = Invoke-RestMethod -Method POST -Uri "http://localhost:8081/msi/token/?resource=https://management.azure.com/" -Headers @{
    Metadata = "true"
}
$accessToken = $tokenResponse.access_token

# Build URI for Management Group creation
$uri = "https://management.azure.com/providers/Microsoft.Management/managementGroups/$mgmtGroupId?api-version=2021-04-01"

# Request body
$payload = @{
    name = $mgmtGroupId
    properties = @{
        displayName = $displayName
        parent = @{
            id = "/providers/Microsoft.Management/managementGroups/$parentGroupId"
        }
    }
} | ConvertTo-Json -Depth 10

# Make API call
$response = Invoke-RestMethod -Method PUT -Uri $uri -Headers @{
    Authorization = "Bearer $accessToken"
    "Content-Type" = "application/json"
} -Body $payload

# Output response
$response | ConvertTo-Json -Depth 10

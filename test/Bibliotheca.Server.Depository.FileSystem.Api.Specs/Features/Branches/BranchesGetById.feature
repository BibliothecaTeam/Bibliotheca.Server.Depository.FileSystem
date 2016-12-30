Feature: Branch details

    As a user I want to download specific branch details
    so that I can easly view information about branch.

Scenario: Branch details must be available
Given system contains project "project-a" with branch "Latest"
When user wants to see details of branch "Latest" from project "project-a"
Then system returns status code Ok
    And branch name is equal to "Latest"
    And configuration information is available

Scenario: System have to return proper status code during getting branch details when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user wants to see details of branch "branch-not-exists" from project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code during getting branch details when project not exists
Given system not contains project "project-not-exists"
When user wants to see details of branch "branch-not-exists" from project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code during getting branch details when project id not specified
Given system contains project "project-a" with branch "Latest"
When user wants to see details of branch "Latest" from project ""
Then system returns status code BadRequest

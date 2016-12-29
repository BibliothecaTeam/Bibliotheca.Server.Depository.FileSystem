Feature: Creating a new branch

    As a user I want to create new branch in project
    so that I can easly add new branch to the system.

Scenario: Branch should be successfully added
Given system not contains branch "new-branch" in project "project-a"
When user adds branch "new-branch" to project "project-a"
Then system returns status code Ok
    And branch "new-branch" exists in project "project-a"

Scenario: System have to return proper status code when project not exists
Given system not contains project "project-not-exists"
When user adds branch "new-branch" to project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code when branch name not specified
Given system contains project "project-a"
When user adds branch "" to project "project-a"
Then system returns status code BadRequest

Scenario: System have to return proper status code when project id not specified
Given system not contains branch "new-branch" in project "project-a"
When user adds branch "new-branch" to project ""
Then system returns status code BadRequest

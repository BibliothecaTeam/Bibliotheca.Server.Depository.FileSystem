Feature: Updating existing branch

    As a user I want to update existing branch
    so that I can easly modify the branch configuration.

Scenario: Branch should be successfully modified
Given system contains branch "updated-branch" in project "project-a"
When user updates branch "updated-branch" in project "project-a"
Then system returns status code Ok

Scenario: System have to return proper status code when project not exists
Given system not contains project "project-not-exists"
When user updates branch "updated-branch" in project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code when branch name not specified
Given system contains branch "updated-branch" in project "project-a"
When user updates branch "" in project "project-a"
Then system returns status code BadRequest

Scenario: System have to return proper status code when project id not specified
Given system not contains branch "not-exists-branch" in project "project-a"
When user updates branch "not-exists-branch" in project ""
Then system returns status code BadRequest

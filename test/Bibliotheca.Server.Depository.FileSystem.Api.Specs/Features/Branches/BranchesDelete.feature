Feature: Deleting branch

    As a user I want to delete specific branch
    so that I can easly remove unecessary branches.

Scenario: Branch should be successfully deleted
Given system contains project "project-a" with branch "branch-to-delete"
When user deletes branch "branch-to-delete" from project "project-a"
Then system returns status code Ok
    And brach "branch-to-delete" in project "project-a" not exists

Scenario: System have to return proper status code durng deleting branch when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user deletes branch "branch-not-exists" from project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code during deleting branch when project not exists
Given system not contains project "project-not-exists"
When user deletes branch "branch-not-exists" from project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code during deleting branch when branch name not specified
Given system contains project "project-a"
When user deletes branch "" from project "project-a"
Then system returns status code BadRequest

Scenario: System have to return proper status code during deleting branch when project id not specified
Given system contains project "project-a" with branch "branch-to-delete"
When user deletes branch "branch-to-delete" from project ""
Then system returns status code BadRequest
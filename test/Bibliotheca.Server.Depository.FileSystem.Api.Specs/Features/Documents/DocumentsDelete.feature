Feature: Deleting document

    As a user I want to delete specific document
    so that I can easly remove unecessary documents.

Scenario: Document should be successfully deleted
Given system contains document "docs/to-delete.md" in branch "Latest" in project "project-a"
When user deletes document "docs/to-delete.md" from branch "Latest" in project "project-a"
Then system returns status code Ok
    And document "docs/to-delete.md" not exists in branch "Latest" in project "project-a"

Scenario: System have to return proper status code when project not exists
Given system not contains project "project-not-exists"
When user deletes document "docs/index.md" from branch "Latest" in project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user deletes document "docs/index.md" from branch "branch-not-exists" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when document not exists
Given system not contains document "docs/index-not-exists.md" in branch "Latest" in project "project-a"
When user deletes document "docs/index-not-exists.md" from branch "Latest" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when project id not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user deletes document "docs/index.md" from branch "Latest" in project ""
Then system returns status code BadRequest

Scenario: System have to return proper status code when branch name not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user deletes document "docs/index.md" from branch "" in project "project-a"
Then system returns status code BadRequest

Scenario: System have to return proper status code when document uri not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user deletes document "" from branch "Latest" in project "project-a"
Then system returns status code BadRequest

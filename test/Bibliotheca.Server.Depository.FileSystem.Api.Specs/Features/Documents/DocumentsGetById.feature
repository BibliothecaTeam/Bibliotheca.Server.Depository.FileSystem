Feature: Documents details

    As a user I want to download specific document details
    so that I can easly view information about document.

Scenario: Documents details must be available
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user wants to see details of document "docs/index.md" in branch "Latest" in project "project-a"
Then system returns status code Ok
    And document uri is equal to "docs/index.md"
    And file content is available
    And file type is equal "file/markdown"

Scenario: System have to return proper status code when project not exists
Given system not contains project "project-not-exists"
When user wants to see details of document "docs/index.md" in branch "Latest" in project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user wants to see details of document "docs/index.md" in branch "branch-not-exists" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when document not exists
Given system not contains document "docs/index-not-exists.md" in branch "Latest" in project "project-a"
When user wants to see details of document "docs/index-not-exists.md" in branch "Latest" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when project id not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user wants to see details of document "docs/index.md" in branch "Latest" in project ""
Then system returns status code BadRequest

Scenario: System have to return proper status code when branch name not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user wants to see details of document "docs/index.md" in branch "" in project "project-a"
Then system returns status code BadRequest

Scenario: System have to return proper status code when document uri not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user wants to see details of document "" in branch "Latest" in project "project-a"
Then system returns status code BadRequest

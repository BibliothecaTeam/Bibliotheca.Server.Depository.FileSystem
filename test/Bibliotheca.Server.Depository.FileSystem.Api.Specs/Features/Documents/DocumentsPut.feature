Feature: Updating existing document

    As a user I want to update existing document
    so that I can easly modify the content of the document.

Scenario: Document should be successfully modified
Given system contains document "docs/updated-index.md" in branch "Latest" in project "project-a"
When user updates document "docs/updated-index.md" in branch "Latest" in project "project-a"
Then system returns status code Ok

Scenario: System have to return proper status code when project not exists
Given system not contains project "project-not-exists"
When user updates document "docs/index.md" in branch "Latest" in project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user updates document "docs/index.md" in branch "branch-not-exists" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when document not exists
Given system not contains document "docs/index-not-exists.md" in branch "Latest" in project "project-a"
When user updates document "docs/index-not-exists.md" in branch "Latest" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when project id not specified
Given system contains document "updated-index.md" in branch "Latest" in project "project-a"
When user updates document "updated-index.md" in branch "Latest" in project ""
Then system returns status code NotFound

Scenario: System have to return proper status code when branch name not specified
Given system contains document "updated-index.md" in branch "Latest" in project "project-a"
When user updates document "updated-index.md" in branch "" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code when document uri not specified
Given system contains document "docs/index.md" in branch "Latest" in project "project-a"
When user updates document "" in branch "Latest" in project "project-a"
Then system returns status code BadRequest

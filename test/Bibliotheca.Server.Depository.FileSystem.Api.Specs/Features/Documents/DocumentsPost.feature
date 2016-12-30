Feature: Creating a new documents

    As a user I want to add new document to branch in project
    so that I can add a new documents to the system.

Scenario: Documents should be successfully added
Given system not contains document "docs/new-document.md" in branch "Latest" in project "project-a"
When user adds document "docs/new-document.md" to branch "Latest" in project "project-a"
Then system returns status code Created
    And new document "docs/new-document.md" exists in branch "Latest" in project "project-a"

Scenario: System have to return proper status code during adding document when project not exists
Given system not contains project "project-not-exists"
When user adds document "docs/new-document.md" to branch "Latest" in project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code during adding document when branch not exists
Given system not contains branch "branch-not-exists" in project "project-a"
When user adds document "docs/new-document.md" to branch "branch-not-exists" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code during adding document when project id not specified
Given system not contains document "docs/new-document.md" in branch "Latest" in project "project-a"
When user adds document "docs/new-document.md" to branch "Latest" in project ""
Then system returns status code NotFound

Scenario: System have to return proper status code during adding document when branch name not specified
Given system not contains document "docs/new-document.md" in branch "Latest" in project "project-a"
When user adds document "docs/new-document.md" to branch "" in project "project-a"
Then system returns status code NotFound

Scenario: System have to return proper status code during adding document when document uri not specified
Given system not contains document "docs/new-document.md" in branch "Latest" in project "project-a"
When user adds document "" to branch "Latest" in project "project-a"
Then system returns status code BadRequest

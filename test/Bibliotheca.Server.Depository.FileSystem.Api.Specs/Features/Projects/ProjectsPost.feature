Feature: Creating a new project

    As a user I want to create new project
    so that I can easly add new projects to system.

Scenario: Project should be successfully added
Given system does not contains project "new-project-a"
When user adds project "new-project-a"
Then system returns status code Created
    And project "new-project-a" exists

Scenario: System have to return proper status code when project exists
Given system contains projects "project-a"
When user adds project "project-a"
Then system returns status code BadRequest

Scenario: Project without id cannot be added
Given system does not contains project "new-project-b"
When user adds project ""
Then system returns status code BadRequest
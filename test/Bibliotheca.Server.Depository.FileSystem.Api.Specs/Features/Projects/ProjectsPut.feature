Feature: Updating existing project

    As a user I want to update existing project
    so that I can easly modify the project configuration.

Scenario: Project should be successfully modified
Given system contains project "updated-project-a" with name "Project Name"
When user updates project "updated-project-a" with new name "New Project Name"
Then system returns status code Ok
    And project "updated-project-a" has name "New Project Name"

Scenario: System have to return proper status code during updating project when project not exists
Given system does not contains project "updated-project-b"
When user updates project "updated-project-b" with new name "New Project Name"
Then system returns status code NotFound

Scenario: System have to return proper status code during updating project when project id was not specified
Given system does not contains project "updated-project-c"
When user updates project "" with new name "New Project Name"
Then system returns status code BadRequest
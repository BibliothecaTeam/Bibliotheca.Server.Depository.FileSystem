Feature: Projects list

    As a user I want to have list of projects
    so that I can easly view all existing projects.

Scenario: List of projects must be available
Given system contains projects
When user wants to see all projects
Then system returns status code Ok
    And system returns all available projects
    And projects are sorted alpabetically

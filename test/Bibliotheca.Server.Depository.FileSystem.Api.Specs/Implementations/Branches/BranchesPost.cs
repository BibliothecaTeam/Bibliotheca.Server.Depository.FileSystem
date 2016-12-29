using FluentBehave;
using System;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesPost", "Creating a new branch")]
    public class BranchesPost
    {
        [Scenario("Branch should be successfully added")]
        public void BranchShouldBeSuccessfullyAdded()
        {
            GivenSystemNotContainsBranchInProject("new-branch", "project-a");
            WhenUserAddsBranchToProject("new-branch", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenBranchExistsInProject("new-branch", "project-a");
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserAddsBranchToProject("new-branch", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsProject("project-a");
            WhenUserAddsBranchToProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemNotContainsBranchInProject("new-branch", "project-a");
            WhenUserAddsBranchToProject("new-branch", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System not contains branch in project")]
        private void GivenSystemNotContainsBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User adds branch to project")]
        private void WhenUserAddsBranchToProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Branch exists in project")]
        private void ThenBranchExistsInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains project")]
        private void GivenSystemNotContainsProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System contains project")]
        private void GivenSystemContainsProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            throw new NotImplementedException("Implement me!");
        }

    }
}
using FluentBehave;
using System;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesPut", "Updating existing branch")]
    public class BranchesPut
    {
        [Scenario("Branch should be successfully modified")]
        public void BranchShouldBeSuccessfullyModified()
        {
            GivenSystemContainsBranchInProject("updated-branch", "project-a");
            WhenUserUpdatesBranchInProject("updated-branch", "project-a");
            ThenSystemReturnsStatusCodeOk();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserUpdatesBranchInProject("updated-branch", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsBranchInProject("updated-branch", "project-a");
            WhenUserUpdatesBranchInProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemNotContainsBranchInProject("not-exists-branch", "project-a");
            WhenUserUpdatesBranchInProject("not-exists-branch", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains branch in project")]
        private void GivenSystemContainsBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User updates branch in project")]
        private void WhenUserUpdatesBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
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

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains branch in project")]
        private void GivenSystemNotContainsBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

    }
}
using FluentBehave;
using System;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesGet", "Branches list")]
    public class BranchesGet
    {
        [Scenario("List of branches must be available")]
        public void ListOfBranchesMustBeAvailable()
        {
            GivenSystemContainsProjectWithBranches("project-a");
            WhenUserWantsToSeeAllBranchesFromProject("project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenSystemReturnsAllAvailableBranches();
            ThenBranchesAreSortedAlpabetically();
        }

        [Scenario("List must contains only branches with correct configuration")]
        public void ListMustContainsOnlyBranchesWithCorrectConfiguration()
        {
            GivenSystemContainsProjectWithBranches("project-a");
            GivenBranchDoesNotHaveConfigurationFile("empty-branch");
            WhenUserWantsToSeeAllBranchesFromProject("project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenSystemReturnsBranchesWithout("empty-branch");
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserWantsToSeeAllBranchesFromProject("project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemContainsProjectWithBranches("project-a");
            WhenUserWantsToSeeAllBranchesFromProject("");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains project with branches")]
        private void GivenSystemContainsProjectWithBranches(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User wants to see all branches from project")]
        private void WhenUserWantsToSeeAllBranchesFromProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns all available branches")]
        private void ThenSystemReturnsAllAvailableBranches()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Branches are sorted alpabetically")]
        private void ThenBranchesAreSortedAlpabetically()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("Branch does not have configuration file")]
        private void GivenBranchDoesNotHaveConfigurationFile(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns branches without")]
        private void ThenSystemReturnsBranchesWithout(string p0)
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

    }
}
using FluentBehave;
using System;

namespace test
{
    [Feature("DocumentsGetById", "Documents details")]
    public class DocumentsGetById
    {
        [Scenario("Documents details must be available")]
        public void DocumentsDetailsMustBeAvailable()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenDocumentUriIsEqualTo("docs/index.md");
            ThenFileContentIsAvailable();
            ThenFileTypeIsEqual("file/markdown");
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when document not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenDocumentNotExists()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when document uri not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenDocumentUriNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("", "Latest", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains document in branch in project")]
        private void GivenSystemContainsDocumentInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User wants to see details of document in branch in project")]
        private void WhenUserWantsToSeeDetailsOfDocumentInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Document uri is equal to")]
        private void ThenDocumentUriIsEqualTo(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("File content is available")]
        private void ThenFileContentIsAvailable()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("File type is equal")]
        private void ThenFileTypeIsEqual(string p0)
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

        [Given("System not contains branch in project")]
        private void GivenSystemNotContainsBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains document in branch in project")]
        private void GivenSystemNotContainsDocumentInBranchInProject(string p0, string p1, string p2)
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
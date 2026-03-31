using Reqnroll;

namespace ApiAndUiProject.UI.Helpers
{
    public static class StepArgumentTransformations
    {
        [StepArgumentTransformation(@"should( not)? ")]
        public static bool TransformAppear(string not)
        {
            return string.IsNullOrEmpty(not);
        }
    }
}
using System.Linq;
using Reqnroll;

namespace PlaywrightProject.Transformations
{
    [Binding]
    public static class ArgumentTransformer
    {
        [StepArgumentTransformation(@"(be present|not be present)")]
        [StepArgumentTransformation(@"(be 0|not be 0)")]
        [StepArgumentTransformation(@"(shown|not shown)")]
        [StepArgumentTransformation(@"(true|false)")]
        public static bool ToBoolTransformer(string value)
        {
            string[] positives = { "be present", "not be 0", "present", "shown", "true" };
            return positives.Any(v => v == value);
        }
    }
}
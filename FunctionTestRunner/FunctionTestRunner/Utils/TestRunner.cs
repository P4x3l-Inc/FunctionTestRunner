using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTestRunner.Utils
{
    public static class TestRunner
    {
        public static void Run(Action<ScenarioPropertyBag> testMethod)
        {
            var bag = ScenarioPropertyBag.Create();
            try
            {
                OutputTestProperties();
                testMethod(bag);
            }
            finally
            {
                CleanUpBag(bag);
            }
        }

        public static async Task RunAsync(Func<ScenarioPropertyBag, Task> testMethod)
        {
            var bag = ScenarioPropertyBag.Create();
            try
            {
                OutputTestProperties();
                await testMethod(bag);
            }
            finally
            {
                CleanUpBag(bag);
            }
        }

        private static void CleanUpBag(ScenarioPropertyBag bag)
        {
            /* RestBase.IgnoreApiStatusCodes = true;

            AchievementScenarios.DeleteAllAchievements();
            SqlTestActivityScenarios.ClearTest(bag);
            AutomationsScenarios.RemoveAutomation(bag);
            TransactionsScenarios.RemoveTransactionData(bag);
            CustomTriggersScenarios.RemoveCustomTrigger(bag);
            EmailScenarios.DeleteEmailTemplate(bag);
            EmailScenarios.DeleteEmail(bag);
            LabelScenarios.RemoveGeneratedLabels(bag);
            ScheduledJobsScenarios.RemoveScheduledJob(bag);
            StoreScenarios.RemoveStores();
            PromotionScenarios.RemovePromotions(bag);
            ContactsScenarios.RemoveContactConsents(bag);
            ContactsScenarios.RemoveGeneratedContacts(bag);
            ContactExportScenarios.RemovedGeneratedContactExport(bag);
            BonusCheckScenarios.RemoveGeneratedBonusChecks(bag);
            ApplicationUserScenarios.DeleteUser(bag);
            BonusScenarios.RemoveGeneratedBonusTransactions(bag);
            PointCardScenarios.DeletePointCards(bag);
            RoomReservationsScenarios.RemoveRoomReservations(bag);
            ProductRecommendationsScenarios.RemoveAllProductRecommendations();
            BiExportScenarios.ClearFtpFiles(bag);
            ContactAttributeScenarios.DeleteContactAttributes(bag);
            SegmentationScenarios.DeleteSegmentations(bag);
            SegmentationScenarios.DeleteTargetAudience(bag);
            SegmentationScenarios.DeleteTags(bag);*/
        }

        private static void OutputTestProperties()
        {
            /*TestContext.WriteLine("Test Configuration:");
            TestContext.WriteLine($"Environment: {TestConfiguration.GetEnvironment()}");
            TestContext.WriteLine($"Api base url: {TestConfiguration.GetApiBaseUrl()}");
            TestContext.WriteLine($"Internal api base url: {TestConfiguration.GetInternalClientBaseUrl()}");
            TestContext.WriteLine($"Tenant api base url: {TestConfiguration.GetTenantsClientBaseUrl()}");

            var stackTrace = new StackTrace();
            var callingClassType = stackTrace.GetFrame(2).GetMethod().DeclaringType;

            var testCategories = new List<string>();
            var classCategories = callingClassType?.GetCustomAttributes(typeof(TeamAttribute), false)
                .Select(x => (x as TeamAttribute)?.Name).Where(x => !string.IsNullOrEmpty(x));
            testCategories.AddRange(classCategories);

            var methodCategories = TestContext.CurrentContext.Test.Properties["Category"];
            testCategories.AddRange(methodCategories.Cast<string>());

            var categoriesString = testCategories.Count > 0
                ? string.Join(", ", testCategories)
                : string.Empty;
            TestContext.WriteLine($"Categories: {categoriesString}");*/
        }
    }
}

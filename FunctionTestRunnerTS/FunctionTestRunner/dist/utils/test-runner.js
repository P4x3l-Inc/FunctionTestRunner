"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const scenario_property_bag_1 = __importDefault(require("./scenario-property-bag"));
class TestRunner {
    static run(testMethod) {
        const bag = scenario_property_bag_1.default.create();
        try {
            this.outputTestProperties();
            testMethod(bag);
        }
        finally {
            this.cleanUpBag(bag);
        }
    }
    static runAsync(testMethod) {
        return __awaiter(this, void 0, void 0, function* () {
            const bag = scenario_property_bag_1.default.create();
            try {
                this.outputTestProperties();
                yield testMethod(bag);
            }
            finally {
                this.cleanUpBag(bag);
            }
        });
    }
    static cleanUpBag(bag) {
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
    static outputTestProperties() {
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
exports.default = TestRunner;
//# sourceMappingURL=test-runner.js.map
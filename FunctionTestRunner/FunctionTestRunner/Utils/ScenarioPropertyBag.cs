using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTestRunner.Utils
{
    public class ScenarioPropertyBag
    {
        private readonly Dictionary<string, Tuple<bool, string>> _bag;

        public static ScenarioPropertyBag Create()
        {
            return new ScenarioPropertyBag();
        }

        private ScenarioPropertyBag()
        {
            _bag = new Dictionary<string, Tuple<bool, string>>();
        }

        public enum DataBagKey
        {/*
            /// <summary>
            /// Name of Email domain. Used for example to sort out users during a specific test or scenario. Type is <see cref="string"/>.
            /// </summary>
            EmailDomainName,

            /// <summary>
            /// Guid of created email. Type is <see cref="Guid"/>
            /// </summary>
            EmailId,

            /// <summary>
            /// List of Contacts. For example will be populated when Contacts are generated. Type is <see cref="IEnumerable{Contact}"/>
            /// </summary>
            Contacts,

            /// <summary>
            /// List of Consents. For example will be populated when Contacts are generated. Type is <see cref="List<Consent>>"/>
            /// </summary>
            ContactConsents,

            /// <summary>
            /// List of SourceTypes. Used to store selected options for PreRegisterMember trigger setup. Type is <see cref="List<string>>"/>
            /// </summary>
            SourceTypes,

            /// <summary>
            /// Id of Trigger in Automation. Can be used to map Advanced Search Trigger in Automatiom with corresponding Scheduled Job. Type is <see cref="Guid"/>.
            /// </summary>
            AutomationTriggerId,

            /// <summary>
            /// Id of Automation
            /// </summary>
            AutomationId,

            /// <summary>
            /// Id of Contact Export file.
            /// </summary>
            ContactExportGuid,

            /// <summary>
            /// Name of Custom Trigger
            /// </summary>
            CustomTrigger,

            /// <summary>
            /// Contains collection of mappings between trigger fields and contacts as <see cref="Tuple{Dictonary, List}"/>
            /// </summary>
            CustomTriggerData,

            /// <summary>
            /// Contains transaction import data that can be used to populate xml import file.
            /// </summary>
            TransactionImportXmlData,
            TransactionImportApiData,
            TransactionsNumberOfReceiptsPerContact,
            TransactionsNumberOfArticlesPerReceipt,
            TransactionsImportNumberOfApiCalls,

            /// <summary>
            /// Contains receipts data that can be used to validate reeipts import.
            /// </summary>
            ReceiptsImportNumberOfApiCallsPerContact,
            ReceiptsImportNumberOfItemsPerReceipt,
            ReceiptsImportApiData,

            /// <summary>
            /// Article filter in new purchase trigger automation.
            /// </summary>
            ArticleFilterProperty,
            ArticleFilterPropertyValue,
            ReceiptsToIncludeInTransactionImport,
            NumberOfReceiptsInArticleFilter,

            #region Contact generator constraints
            /// <summary>
            /// Total number of contacts to generate
            /// </summary>
            ContactGeneratorTotalCount,

            /// <summary>
            /// When defined, the number of contacts defined in <see cref="ContactsThatShouldPassCount"/> will have this birthday. The remaining ones will not.
            /// </summary>
            ContactBirthdayValue,

            /// <summary>
            /// Number of contacts that will have the birthday defined under <see cref="ContactBirthdayValue"/>
            /// </summary>
            ContactsThatShouldPassCount,

            /// <summary>
            /// Mobile phone is not generated when creating a contact. Type is <see cref="bool"/>.
            /// </summary>
            ContactGeneratorNoMobilePhone,

            /// <summary>
            /// Contact will be created as unapproved. Used for example to set. Type is <see cref="bool"/>
            /// </summary>
            ContactCreatedAsUnapproved,
            #endregion

            #region Activity SQL Test Values

            /// <summary>
            /// This value is stored in column ActivityConfig in table SqlTestActivity. Type is <see cref="string"/>.
            /// </summary>
            SqlTestActivityValueInfo,

            /// <summary>
            /// This value is stored in column ActivityConfig in table SqlTestActivity for proceeding activities when setting up a workflow using the method <see cref="AutomationsScenarios.SetupAdvancedSearchTriggerWithWaitForActivity"/>. Type is <see cref="string"/>.
            /// </summary>
            SqlTestActivityValueYes,

            /// <summary>
            /// This value is stored in column ActivityConfig in table SqlTestActivity for activities where timeout is reached when setting up a workflow using the method <see cref="AutomationsScenarios.SetupAdvancedSearchTriggerWithWaitForActivity"/>. Type is <see cref="string"/>.
            /// </summary>
            SqlTestActivityValueNo,
            #endregion

            /// <summary>
            /// Id of the Activity WaitFor as well as it's configured Trigger. Can be used to map Advanced Search Trigger in Automatiom with corresponding Scheduled Job. Type is <see cref="Guid"/>.
            /// </summary>
            WaitForActivityId,

            /// <summary>
            /// E-mail domain different from the one used when creating contacts (see <see cref="EmailDomainName"/>). Type is <see cref="string"/>.
            /// </summary>
            OtherEmailDomainName,

            Stores,
            /// <summary>
            /// Contains alias for achievment
            /// </summary>
            AchievmentAlias,

            /// <summary>
            /// Dummy url
            /// </summary>
            TestUrl,

            /// <summary>
            /// Contatains info about scheduled job
            /// </summary>
            ScheduledJob,

            /// <summary>
            /// Contains guid for Label
            /// </summary>
            LabelId,

            /// <summary>
            /// Id of current mail template
            /// </summary>
            EmailTemplateId,

            /// <summary>
            /// Email address of email sender
            /// </summary>
            EmailAddressSender,

            /// <summary>
            /// Content in main email module. Type is string
            /// </summary>
            EmailMainContent,

            /// <summary>
            /// CustomAttributes in EmailMessage. Type is Dictionary(string,string) 
            /// </summary>
            EmailCustomAttributes,
            EmailJsonData,
            /// <summary>
            /// Subject of email
            /// </summary>
            EmailSubject,
            /// <summary>
            /// Indicates whether sent email is A/B test. Type is bool.
            /// </summary>
            EmailIsABTest,
            AutomationEmailAsTransactional,
            /// <summary>
            /// Latest generated promotion
            /// </summary>
            Promotion,
            PromotionCodes,
            BonusCheckPromotion,
            /// <summary>
            /// All generated promotions
            /// </summary>
            Promotions,
            BonusPointTransactions,
            /// <summary>
            /// <see cref="string"/>
            /// </summary>
            PromotionInputCondition,
            /// <summary>
            /// </summary>
            DateTime,
            /// <summary>
            /// <see cref="RecurrencySettings"/>
            /// </summary>
            RecurrencySettings,
            /// <summary>
            /// <see cref="EmailExternalDataSettings"/>
            /// </summary>
            ExternalDataSettings,

            /// <summary>
            /// Username of ApplicationUser. Type is string
            /// </summary>
            ApplicationUser,

            /// <summary>
            /// <see cref="decimal"/>
            /// </summary>
            BonusValue,
            /// <summary>
            /// <see cref="string"/>
            /// </summary>
            BonusDescription,
            /// <summary>
            /// <see cref="string"/>
            /// </summary>
            Reason,
            /// <summary>
            /// <see cref="pointCardDefinitionModel"/>
            /// </summary>
            pointCardDefinition,
            /// <summary>
            /// <see cref="List<pointCardModel>"/>
            /// </summary>
            pointCards,
            /// <summary>
            /// <see cref="List<RoomReservationModel>"/>
            /// </summary>
            RoomReservations,
            /// <summary>
            /// <see cref="int"/>
            /// </summary>
            BiExportVersion,
            BiExportType,
            ContactScoringData,
            ScoringImportJob,
            /// <summary>
            /// <see cref="string"/>
            /// </summary>
            ProductRecommendations,
            /// <summary>
            /// <see cref="string"/>
            /// </summary>
            ProductRecommendationImportJob,
            ContactAttributeData,
            ContactAttributesImportJob,
            ContactAttributesBeforeImportTime,
            AbandonedCart,
            CreatedContactAttributes,
            Tags,
            Segmentations,
            TargetAudiences*/
        }

        public void Require(params string[] keys)
        {
            var missingKeys = new List<string>();
            foreach (var key in keys)
            {
                if (!_bag.ContainsKey(key))
                {
                    missingKeys.Add(key);
                }
            }

            if (missingKeys.Count > 0)
            {
                throw new Exception($"The following data item(s) are not set in data bag: {string.Join(", ", missingKeys)}. Scenario is aborted.");
            }
        }

        public void Require(params DataBagKey[] keys)
        {
            Require(keys.Select(x => x.ToString()).ToArray());
        }

        public bool ContainsKeys(params string[] keys)
        {
            var missingKeys = new List<string>();
            foreach (var key in keys)
            {
                if (!_bag.ContainsKey(key))
                {
                    missingKeys.Add(key);
                }
            }

            return missingKeys.Count == 0;
        }

        public bool ContainsKeys(params DataBagKey[] keys)
        {
            return ContainsKeys(keys.Select(x => x.ToString()).ToArray());
        }

        public T Get<T>(string key)
        {
            if (!_bag.ContainsKey(key) || !_bag[key].Item1)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(_bag[key].Item2);
        }

        public T Get<T>(DataBagKey key)
        {
            return Get<T>(key.ToString());
        }

        public void Set(string key, object value)
        {
            var insert = new Tuple<bool, string>(value != null, value == null ? string.Empty : JsonConvert.SerializeObject(value));
            if (_bag.ContainsKey(key))
            {
                _bag[key] = insert;
            }
            else
            {
                _bag.Add(key, insert);
            }
        }

        public void Set(DataBagKey key, object value)
        {
            Set(key.ToString(), value);
        }

        public void RemoveKey(DataBagKey key)
        {
            if (_bag.ContainsKey(key.ToString()))
            {
                _bag.Remove(key.ToString());
            }
        }

        internal object Get<T>(object labelId)
        {
            throw new NotImplementedException();
        }
    }
}

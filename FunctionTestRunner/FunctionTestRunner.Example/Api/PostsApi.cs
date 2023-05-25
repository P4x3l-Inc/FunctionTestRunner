using FunctionTestRunner.Example.Configuration;
using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Utils;
using FunctionTestRunner.Wrappers.Api;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Configuration;
using System.Net;
using System.Text.Json;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace FunctionTestRunner.Example.Api;

public class PostsApi : ApiBase
{
    private readonly string basePath;
    private readonly Settings settings;

    protected override ITestConfiguration Config => settings;

    public PostsApi(ITestOutputHelper? testOutputHelper = null)
    {
        TestOutputHelper = testOutputHelper;
        basePath = "posts";
        settings = new Settings();

        DefaultHeaders = new Dictionary<string, string>
        {
            { "apikey", settings.GetApiKey() }
        };
    }

    public async Task<object?> Test()
    {
        var text = "{\r\n  \"schoolYear\": \"3626b462-c8d0-1195-7450-ad4305050505\",\r\n  \"nationalId\": \"860803-0097\",\r\n  \"firstname\": \"David\",\r\n  \"surname\": \"Bandemo\",\r\n  \"school\": \"a026b462-c8d0-1195-7450-aeb405050505\",\r\n  \"otherSchool\": \"string\",\r\n  \"grade\": 3,\r\n  \"class\": \"string\",\r\n  \"specialSchool\": true,\r\n  \"secondaryTravelApplication\": true,\r\n  \"freeBuscardApplication\": true,\r\n  \"domicileAddress\": \"string\",\r\n  \"domicileZip\": \"12345\",\r\n  \"domicileCity\": \"string\",\r\n  \"isDomicileFutureAdress\": true,\r\n  \"alternateDomicileHome\": false,\r\n  \"secondaryAddress\": null,\r\n  \"secondaryZip\": null,\r\n  \"secondaryCity\": null,\r\n  \"alternateSecondaryHome\": true,\r\n  \"isSecondaryFutureAdress\": true,\r\n  \"secondaryPeriodInPercentage\": 0,\r\n  \"isYouthHome\": true,\r\n  \"isRelieveHome\": true,\r\n  \"isOtherHome\": true,\r\n  \"secondaryHomeInformation\": \"string\",\r\n  \"applyFromDomicile\": true,\r\n  \"applyDomicileBuscard\": true,\r\n  \"applyDomicileTrafficFeed\": true,\r\n  \"applyDomicileTaxi\": true,\r\n  \"applyDomicileSchoolBus\": true,\r\n  \"applyDomicileOtherTransport\": true,\r\n  \"applyDomicileForFreeSeat\": true,\r\n  \"applyDomicileForFreeBuscard\": true,\r\n  \"applyDomicileEconomicCompensation\": true,\r\n  \"applyDomicileTransportPriority\": [\r\n    1\r\n  ],\r\n  \"applyDomicileOtherText\": \"string\",\r\n  \"applyDomicilePeriod\": \"string\",\r\n  \"applyDomicileSchoolbusPeriod\": \"string\",\r\n  \"applyDomicileBuscardPeriod\": \"string\",\r\n  \"applyDomicileTaxiPeriod\": \"string\",\r\n  \"applyDomicileOtherPeriod\": \"string\",\r\n  \"applyDomicilePeriodStartDate\": \"2023-05-17T22:02:32.933Z\",\r\n  \"applyDomicilePeriodEndDate\": \"2023-05-17T22:02:32.933Z\",\r\n  \"applyFromSecondary\": true,\r\n  \"applySecondaryBuscard\": true,\r\n  \"applySecondaryTrafficFeed\": true,\r\n  \"applySecondaryTaxi\": true,\r\n  \"applySecondarySchoolBus\": true,\r\n  \"applySecondaryOtherTransport\": true,\r\n  \"applySecondaryForFreeSeat\": true,\r\n  \"applySecondaryForFreeBuscard\": true,\r\n  \"applySecondaryEconomicCompensation\": true,\r\n  \"applySecondaryTransportPriority\": [\r\n    1\r\n  ],\r\n  \"applySecondaryOtherText\": \"string\",\r\n  \"hasWheelChair\": true,\r\n  \"hasFoldingWheelChair\": true,\r\n  \"hasMotorizedWheelChair\": true,\r\n  \"usingWalker\": true,\r\n  \"hasAssistent\": true,\r\n  \"usingChildCushion\": true,\r\n  \"needStairClimber\": true,\r\n  \"needHandOver\": true,\r\n  \"singlePassenger\": true,\r\n  \"needSameDriver\": true,\r\n  \"needToBeSeatedInTheFrontSeat\": true,\r\n  \"needSpecialVehicle\": true,\r\n  \"applySecondaryPeriod\": \"string\",\r\n  \"applySecondarySchoolbusPeriod\": \"string\",\r\n  \"applySecondaryBuscardPeriod\": \"string\",\r\n  \"applySecondaryTaxiPeriod\": \"string\",\r\n  \"applySecondaryOtherPeriod\": \"string\",\r\n  \"applySecondaryPeriodStartDate\": \"2023-05-17T22:02:32.933Z\",\r\n  \"applySecondaryPeriodEndDate\": \"2023-05-17T22:02:32.933Z\",\r\n  \"domicileWholeSchoolYear\": true,\r\n  \"domicileWholeSchoolStage\": true,\r\n  \"domicileSchoolbusOnHolidays\": true,\r\n  \"domicileDistanceToSchool\": true,\r\n  \"domicileDistanceToBusstop\": true,\r\n  \"domicileTrafficSecurity\": true,\r\n  \"domicileOtherReason\": true,\r\n  \"domicileHasDisability\": true,\r\n  \"domicileWinterSchoolbus\": true,\r\n  \"domicileTrafficSecurityDescription\": \"string\",\r\n  \"domicileDisabilityDescription\": \"string\",\r\n  \"domicileOtherReasonDescription\": \"string\",\r\n  \"secondaryWholeSchoolYear\": true,\r\n  \"secondaryWholeSchoolStage\": true,\r\n  \"secondarySchoolbusOnHolidays\": true,\r\n  \"secondaryDistanceToSchool\": true,\r\n  \"secondaryDistanceToBusstop\": true,\r\n  \"secondaryTrafficSecurity\": true,\r\n  \"secondaryOtherReason\": true,\r\n  \"secondaryHasDisability\": true,\r\n  \"secondaryWinterSchoolbus\": true,\r\n  \"secondaryTrafficSecurityDescription\": \"string\",\r\n  \"secondaryDisabilityDescription\": \"string\",\r\n  \"secondaryOtherReasonDescription\": \"string\",\r\n  \"noTransportInformation\": true,\r\n  \"additionalInformation\": \"string\",\r\n  \"notUsingDomicileDayCare\": true,\r\n  \"usingPartialDomicileDayCare\": true,\r\n  \"usingDomicileDayCare\": true,\r\n  \"domicileDayCareDeviation\": \"string\",\r\n  \"notUsingSecondaryDayCare\": true,\r\n  \"usingPartialSecondaryDayCare\": true,\r\n  \"usingSecondaryDayCare\": true,\r\n  \"secondaryDayCareDeviation\": \"string\",\r\n  \"transportationScheduleWeeks\": [\r\n    {\r\n      \"frequency\": 1,\r\n      \"mondayMorning\": 0,\r\n      \"tuesdayMorning\": 0,\r\n      \"wednesdayMorning\": 0,\r\n      \"thursdayMorning\": 0,\r\n      \"fridayMorning\": 0,\r\n      \"mondayAfternoon\": 0,\r\n      \"tuesdayAfternoon\": 0,\r\n      \"wednesdayAfternoon\": 0,\r\n      \"thursdayAfternoon\": 0,\r\n      \"fridayAfternoon\": 0\r\n    }\r\n  ],\r\n  \"usingDomicileTaxiFridayAfternoon\": true,\r\n  \"domicileTaxiInformation\": \"string\",\r\n  \"domicileInformationToTaxi\": \"string\",\r\n  \"secondaryTaxiInformation\": \"string\",\r\n  \"secondaryInformationToTaxi\": \"string\",\r\n  \"guardian1NationalId\": \"string\",\r\n  \"guardian1Firstname\": \"string\",\r\n  \"guardian1Surname\": \"string\",\r\n  \"guardian1Adress\": \"string\",\r\n  \"guardian1Zip\": \"string\",\r\n  \"guardian1City\": \"string\",\r\n  \"guardian1Phone\": \"string\",\r\n  \"guardian1PhoneWork\": \"string\",\r\n  \"guardian1Cellphone\": \"string\",\r\n  \"guardian1Email\": \"string\",\r\n  \"guardian1NoticeByPortal\": true,\r\n  \"guardian1NoticeByEmail\": true,\r\n  \"guardian1NoticeByLetter\": true,\r\n  \"singleGuardian\": true,\r\n  \"guardian1IsApplicant\": true,\r\n  \"guardian2NationalId\": \"string\",\r\n  \"guardian2Firstname\": \"string\",\r\n  \"guardian2Surname\": \"string\",\r\n  \"guardian2Adress\": \"string\",\r\n  \"guardian2Zip\": \"string\",\r\n  \"guardian2City\": \"string\",\r\n  \"guardian2Phone\": \"string\",\r\n  \"guardian2Cellphone\": \"string\",\r\n  \"guardian2PhoneWork\": \"string\",\r\n  \"guardian2Email\": \"string\",\r\n  \"guardian2NoticeByPortal\": true,\r\n  \"guardian2NoticeByEmail\": true,\r\n  \"guardian2NoticeByLetter\": true,\r\n  \"guardian2IsApplicant\": true\r\n}\r\n";

        //var application = JsonSerializer.Deserialize<JObject>(text);

        var response = await PostWithBody<object>($"Application?api_key={settings.GetApiKey()}", new { }).ConfigureAwait(false);

        return response;
    }

    public async Task<Post> Create(Post post)
    {
        var response = await PostWithBody<Post>(basePath, post, HttpStatusCode.Created).ConfigureAwait(false);

        return response;
    }

    public async Task<Post?> Get(string id)
    {
        var response = await Get<Post>($"{basePath}/{id}", null).ConfigureAwait(false);

        return response;
    }

    public async Task<Post?> GetWithHttpStatus(string id, HttpStatusCode httpStatus)
    {
        var response = await Get<Post>($"{basePath}/{id}", null, httpStatus).ConfigureAwait(false);

        return response;
    }

    public async Task<Post> Update(string id, Post post)
    {
        var response = await PutWithBody<Post>($"{basePath}/{id}", post).ConfigureAwait(false);

        return response;
    }

    public async Task Delete(string id)
    {
        await Delete<Post>($"{basePath}/{id}").ConfigureAwait(false);
    }
}

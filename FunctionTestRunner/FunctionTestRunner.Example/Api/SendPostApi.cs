using FunctionTestRunner.Example.Configuration;
using FunctionTestRunner.Utils;
using FunctionTestRunner.Wrappers.Api;

namespace FunctionTestRunner.Example.Api;

public class SendPostApi : ApiBase
{
    private readonly string basePath;
    private readonly Settings settings;

    protected override ITestConfiguration Config => settings;

    public SendPostApi()
    {
        basePath = "send";
        settings = new Settings();
    }
}

import config from 'config';

export class TestConfiguration {
    static getEnvironment(): string {
        return this.getSetting("environmentToTest");
    }

    static getApiKey(): string {
        return this.getSetting("apiKey");
    }

    static getApiBaseUrl(): string
    {
        return this.getSetting("apiBaseUrl");
    }

    // Add more internalAPi info

    static getInternalClientBaseUrl(): string
    {
        return this.getSetting("internalApiBaseUrl");
    }

    private static getSetting(settingKey: string): string
    {
        var value: string = config.get(settingKey);

        if (value == null)
        {
            throw new Error("AppSetting testEnvironment not defined");
        }

        return value;
    }
}
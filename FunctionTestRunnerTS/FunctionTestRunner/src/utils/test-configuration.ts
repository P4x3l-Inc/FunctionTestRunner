import config from 'config';

export default class TestConfiguration {
    static getEnvironment(): string {
        return this.getSetting('testSetup.environment');
    }

    static getApiKey(): string {
        return this.getSetting('apiKey');
    }

    static getApiBaseUrl(): string {
        return this.getSetting('apiBaseUrl');
    }

    static getApiTimeout(): number {
        return this.getNumberSetting('apiTimeout');
    }

    static getInternalClientBaseUrl(): string
    {
        return this.getSetting('internalApiBaseUrl');
    }

    private static getSetting(settingKey: string): string
    {
        if (!config.has(settingKey)) {
            throw new Error(`AppSetting ${settingKey} not defined`);
        }

        const value: string = config.get(settingKey);

        return value;
    }

    private static getNumberSetting(settingKey: string): number
    {
        var value = this.getSetting(settingKey);

        return Number.parseInt(value);
    }
}
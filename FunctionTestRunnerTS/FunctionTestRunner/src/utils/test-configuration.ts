import config from 'config';

export class TestConfiguration {
  getEnvironment(): string {
    return this.getSetting('testSetup.environment');
  }

  getApiKey(): string {
    return this.getSetting('apiKey');
  }

  getApiBaseUrl(): string {
    return this.getSetting('apiBaseUrl');
  }

  getApiTimeout(): number {
    return this.getNumberSetting('apiTimeout');
  }

  getInternalClientBaseUrl(): string {
    return this.getSetting('internalApiBaseUrl');
  }

  getSetting(settingKey: string): string {
    if (!config.has(settingKey)) {
      throw new Error(`AppSetting ${settingKey} not defined`);
    }

    const value: string = config.get(settingKey);

    return value;
  }

  getNumberSetting(settingKey: string): number {
    const value = this.getSetting(settingKey);

    return Number.parseInt(value, 10);
  }
}

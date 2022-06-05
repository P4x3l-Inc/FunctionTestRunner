"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const config_1 = __importDefault(require("config"));
class TestConfiguration {
    static getEnvironment() {
        return this.getSetting('testSetup.environment');
    }
    static getApiKey() {
        return this.getSetting('apiKey');
    }
    static getApiBaseUrl() {
        return this.getSetting('apiBaseUrl');
    }
    static getApiTimeout() {
        return this.getNumberSetting('apiTimeout');
    }
    static getInternalClientBaseUrl() {
        return this.getSetting('internalApiBaseUrl');
    }
    static getSetting(settingKey) {
        if (!config_1.default.has(settingKey)) {
            throw new Error(`AppSetting ${settingKey} not defined`);
        }
        const value = config_1.default.get(settingKey);
        return value;
    }
    static getNumberSetting(settingKey) {
        const value = this.getSetting(settingKey);
        return Number.parseInt(value, 10);
    }
}
exports.default = TestConfiguration;
//# sourceMappingURL=test-configuration.js.map
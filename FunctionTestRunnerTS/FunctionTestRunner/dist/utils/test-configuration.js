"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.TestConfiguration = void 0;
const config_1 = __importDefault(require("config"));
class TestConfiguration {
    getEnvironment() {
        return this.getSetting('testSetup.environment');
    }
    getApiKey() {
        return this.getSetting('apiKey');
    }
    getApiBaseUrl() {
        return this.getSetting('apiBaseUrl');
    }
    getApiTimeout() {
        return this.getNumberSetting('apiTimeout');
    }
    getInternalClientBaseUrl() {
        return this.getSetting('internalApiBaseUrl');
    }
    getSetting(settingKey) {
        if (!config_1.default.has(settingKey)) {
            throw new Error(`AppSetting ${settingKey} not defined`);
        }
        const value = config_1.default.get(settingKey);
        return value;
    }
    getNumberSetting(settingKey) {
        const value = this.getSetting(settingKey);
        return Number.parseInt(value, 10);
    }
}
exports.TestConfiguration = TestConfiguration;
//# sourceMappingURL=test-configuration.js.map
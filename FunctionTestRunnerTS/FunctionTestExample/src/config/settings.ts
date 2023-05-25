import { TestConfiguration } from "functiontestrunner";

export default class Settings extends TestConfiguration {
    constructor() {
        super();
    }

    getApiKey(): string {
        return super.getApiKey();
    }
}
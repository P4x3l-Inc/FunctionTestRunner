import ScenarioPropertyBag from './scenario-property-bag';
export default class TestRunner {
    static run(testMethod: (bag: ScenarioPropertyBag) => void): void;
    static runAsync(testMethod: (bag: ScenarioPropertyBag) => Promise<void>): Promise<void>;
    private static cleanUpBag;
    private static outputTestProperties;
}
//# sourceMappingURL=test-runner.d.ts.map
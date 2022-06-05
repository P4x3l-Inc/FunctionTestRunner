import ScenarioPropertyBag from '../utils/scenario-property-bag';
export interface Scenario {
    scenarioType(): string;
    setupScenarios(bag: ScenarioPropertyBag): string;
    cleanupScenarios(bag: ScenarioPropertyBag): void;
}
//# sourceMappingURL=scenario.d.ts.map
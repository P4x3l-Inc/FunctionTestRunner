import ScenarioPropertyBag from '../utils/scenario-property-bag';
export interface Scenario {
    scenarioType(): string;
    setupScenarios(bag: ScenarioPropertyBag): string;
    cleanupScenarios(bag: ScenarioPropertyBag): void;
}
export declare namespace Scenario {
    type Constructor<T> = {
        new (...args: any[]): T;
        readonly prototype: T;
    };
    export function GetImplementations(): Constructor<Scenario>[];
    export function register<T extends Constructor<Scenario>>(ctor: T): T;
    export {};
}
//# sourceMappingURL=scenario.d.ts.map
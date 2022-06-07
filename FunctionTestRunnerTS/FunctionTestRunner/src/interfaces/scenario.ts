import ScenarioPropertyBag from '../utils/scenario-property-bag';

export interface Scenario {
  scenarioType(): string;
  setupScenarios(bag: ScenarioPropertyBag): string;
  cleanupScenarios(bag: ScenarioPropertyBag): void;
}

export namespace Scenario {
  type Constructor<T> = {
    new(...args: any[]): T;
    readonly prototype: T;
  }
  const implementations: Constructor<Scenario>[] = [];
  export function GetImplementations(): Constructor<Scenario>[] {
    return implementations;
  }
  export function register<T extends Constructor<Scenario>>(ctor: T) {
    implementations.push(ctor);
    return ctor;
  }
}

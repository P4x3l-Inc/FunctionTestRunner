export default class ScenarioPropertyBag {
    private bag;
    static create(): ScenarioPropertyBag;
    constructor();
    require(keys: string[]): void;
    containsKeys(keys: string[]): boolean;
    get<T>(key: string): T | undefined;
    set(key: string, value: unknown): void;
    removeKey(key: string): void;
}
//# sourceMappingURL=scenario-property-bag.d.ts.map
export class ScenarioPropertyBag {
    private bag: { [id: string] : [boolean, string]; };

    static create(): ScenarioPropertyBag {
        return new ScenarioPropertyBag();
    }

    constructor() {
        this.bag = {};
    }

    require(keys: string[]) {
        const missingKeys: string[] = [];

        keys.forEach(key => {
            if (!(key in this.bag)) {
                missingKeys.push(key);
            }
        });

        if (missingKeys.length > 0) {
            throw new Error(`The following data item(s) are not set in data bag: ${missingKeys.join(', ')}. Scenario is aborted.`);
        }
    }

    containsKeys(keys: string[]): boolean {
        var missingKeys: string[] = [];

        keys.forEach(key => {
            if (!(key in this.bag)) {
                missingKeys.push(key);
            }
        });

        return missingKeys.length == 0;
    }

    get<T>(key: string): T | undefined
    {
        if (!(key in this.bag) || !this.bag[key][0]) {
            return undefined;
        }

        return JSON.parse(this.bag[key][1]);
    }

    set(key: string, value: unknown) {
        const insert: [ boolean, string] = [ value !== null, value == null ? '' : JSON.stringify(value) ]

        this.bag[key] = insert;
    }

    removeKey(key: string) {
        if (key in this.bag) {
            delete this.bag[key]
        }
    }
}
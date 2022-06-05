"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class ScenarioPropertyBag {
    constructor() {
        this.bag = {};
    }
    static create() {
        return new ScenarioPropertyBag();
    }
    require(keys) {
        const missingKeys = [];
        keys.forEach((key) => {
            if (!(key in this.bag)) {
                missingKeys.push(key);
            }
        });
        if (missingKeys.length > 0) {
            throw new Error(`The following data item(s) are not set in data bag: ${missingKeys.join(', ')}. Scenario is aborted.`);
        }
    }
    containsKeys(keys) {
        const missingKeys = [];
        keys.forEach((key) => {
            if (!(key in this.bag)) {
                missingKeys.push(key);
            }
        });
        return missingKeys.length === 0;
    }
    get(key) {
        if (!(key in this.bag) || !this.bag[key][0]) {
            return undefined;
        }
        return JSON.parse(this.bag[key][1]);
    }
    set(key, value) {
        const insert = [value !== null, value == null ? '' : JSON.stringify(value)];
        this.bag[key] = insert;
    }
    removeKey(key) {
        if (key in this.bag) {
            delete this.bag[key];
        }
    }
}
exports.default = ScenarioPropertyBag;
//# sourceMappingURL=scenario-property-bag.js.map
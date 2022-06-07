"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Scenario = void 0;
var Scenario;
(function (Scenario) {
    const implementations = [];
    function GetImplementations() {
        return implementations;
    }
    Scenario.GetImplementations = GetImplementations;
    function register(ctor) {
        implementations.push(ctor);
        return ctor;
    }
    Scenario.register = register;
})(Scenario = exports.Scenario || (exports.Scenario = {}));
//# sourceMappingURL=scenario.js.map
"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = __importDefault(require("axios"));
const wait_1 = __importDefault(require("../../utils/wait"));
class ApiBase {
    constructor(apiBaseUrl, apiTimeout) {
        const axiosConfig = {
            baseURL: apiBaseUrl,
            timeout: apiTimeout,
        };
        this.axiosClient = axios_1.default.create(axiosConfig);
    }
    get(path) {
        return __awaiter(this, void 0, void 0, function* () {
            const request = {
                method: HttpMethod.Get,
                url: path,
            };
            const data = yield this.execute(request, 200);
            return data;
        });
    }
    getWithQueryParams(path, params) {
        return __awaiter(this, void 0, void 0, function* () {
            const request = {
                method: HttpMethod.Get,
                url: path,
                params: params,
            };
            const data = yield this.execute(request, 200);
            return data;
        });
    }
    delete(path) {
        return __awaiter(this, void 0, void 0, function* () {
            const request = {
                method: HttpMethod.Delete,
                url: path,
            };
            const data = yield this.execute(request, 200);
        });
    }
    postWithBody(path, body, expectedResponse = 200) {
        return __awaiter(this, void 0, void 0, function* () {
            const request = {
                method: HttpMethod.Post,
                url: path,
                data: body,
            };
            const data = yield this.execute(request, expectedResponse);
            return data;
        });
    }
    putWithBody(path, body, expectedResponse = 200) {
        return __awaiter(this, void 0, void 0, function* () {
            const request = {
                method: HttpMethod.Put,
                url: path,
                data: body,
            };
            const data = yield this.execute(request, expectedResponse);
            return data;
        });
    }
    execute(request, expectedResponse) {
        return __awaiter(this, void 0, void 0, function* () {
            let response = yield this.axiosClient(request);
            if (!this.ignoreApiStatusCodes) {
                if (response.status !== expectedResponse) {
                    wait_1.default.forSeconds(15);
                    response = yield this.axiosClient(request);
                }
                // Validate response
                // expect(response.status).to.be(expectedResponse);
            }
            return response.data;
        });
    }
}
exports.default = ApiBase;
var HttpMethod;
(function (HttpMethod) {
    HttpMethod["Get"] = "get";
    HttpMethod["Post"] = "post";
    HttpMethod["Delete"] = "delete";
    HttpMethod["Put"] = "put";
})(HttpMethod || (HttpMethod = {}));
//# sourceMappingURL=api-base.js.map
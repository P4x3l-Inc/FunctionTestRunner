import { AxiosInstance } from 'axios';
export default abstract class ApiBase {
    abstract ignoreApiStatusCodes: boolean;
    axiosClient: AxiosInstance;
    constructor(apiBaseUrl: string, apiTimeout: number);
    get<T>(path: string): Promise<T>;
    getWithQueryParams<T>(path: string, params: {
        [id: string]: string;
    }): Promise<T>;
    delete<T>(path: string): Promise<void>;
    postWithBody<T>(path: string, body: unknown, expectedResponse?: number): Promise<T>;
    putWithBody<T>(path: string, body: unknown, expectedResponse?: number): Promise<T>;
    private execute;
}
//# sourceMappingURL=api-base.d.ts.map
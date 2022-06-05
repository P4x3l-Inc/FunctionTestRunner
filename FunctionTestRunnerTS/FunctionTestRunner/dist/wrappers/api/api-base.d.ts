import { AxiosInstance } from 'axios';
export default abstract class ApiBase {
    abstract ignoreApiStatusCodes: boolean;
    axiosClient: AxiosInstance;
    constructor();
    get<T>(path: string): Promise<T>;
    PostWithBody<T>(path: string, body: unknown, expectedResponse?: number): Promise<T>;
    private execute;
}
//# sourceMappingURL=api-base.d.ts.map
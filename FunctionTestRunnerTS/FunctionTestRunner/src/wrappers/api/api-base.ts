import axios, { AxiosInstance } from 'axios'
import TestConfiguration from '../../utils/test-configuration';
import Wait from '../../utils/wait';

export default abstract class ApiBase {
    abstract ignoreApiStatusCodes: boolean;
    axiosClient: AxiosInstance | undefined;

    constructor() {
        const axiosConfig = {
            baseURL: TestConfiguration.getApiBaseUrl(),
            timeout: TestConfiguration.getApiTimeout(),
        };

        this.axiosClient = axios.create(axiosConfig);
    }

    async get<T>(path: string): Promise<T>
    {
        var response = await execute(HttpMethod.Get, path, );
        var result = DeserializeResponse<T>(response);
        return result;
    }

    private async execute(method: HttpMethod, url: string, expectedResponse: number) {
        let response = await axios({
            method,
            url,
        });

        if (!this.ignoreApiStatusCodes) {
            if (response.status !== expectedResponse) {
                Wait.forSeconds(15);
                response = await axios({
                    method,
                    url,
                });
            }
            // Validate response
            // expect(response.status).to.be(expectedResponse);
        }

        return response;
    }
}

enum HttpMethod {
    Get = 'get',
    Post = 'post',
    Delete = 'delete',
    Update = 'update',
}
import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import TestConfiguration from '../../utils/test-configuration';
import Wait from '../../utils/wait';

export default abstract class ApiBase {
  abstract ignoreApiStatusCodes: boolean;
  axiosClient: AxiosInstance;

  constructor() {
    const axiosConfig = {
      baseURL: TestConfiguration.getApiBaseUrl(),
      timeout: TestConfiguration.getApiTimeout(),
    };

    this.axiosClient = axios.create(axiosConfig);
  }

  async get<T>(path: string): Promise<T> {
    const request: AxiosRequestConfig = {
      method: HttpMethod.Get,
      url: path,
    };

    const data = await this.execute<T>(request, 200);

    return data;
  }

  async delete<T>(path: string): Promise<void> {
    const request: AxiosRequestConfig = {
      method: HttpMethod.Delete,
      url: path,
    };

    const data = await this.execute<T>(request, 200);
  }

  async postWithBody<T>(path: string, body: unknown, expectedResponse: number = 200): Promise<T> {
    const request: AxiosRequestConfig = {
      method: HttpMethod.Post,
      url: path,
      data: body,
    };

    const data = await this.execute<T>(request, expectedResponse);

    return data;
  }

  async putWithBody<T>(path: string, body: unknown, expectedResponse: number = 200): Promise<T> {
    const request: AxiosRequestConfig = {
      method: HttpMethod.Put,
      url: path,
      data: body,
    };

    const data = await this.execute<T>(request, expectedResponse);

    return data;
  }

  private async execute<T>(request: AxiosRequestConfig, expectedResponse: number): Promise<T> {
    let response = await this.axiosClient(request);

    if (!this.ignoreApiStatusCodes) {
      if (response.status !== expectedResponse) {
        Wait.forSeconds(15);
        response = await this.axiosClient(request);
      }
      // Validate response
      // expect(response.status).to.be(expectedResponse);
    }

    return response.data as T;
  }
}

enum HttpMethod {
  Get = 'get',
  Post = 'post',
  Delete = 'delete',
  Put = 'put',
}

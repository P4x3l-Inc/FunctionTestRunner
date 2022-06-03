import axios from 'axios'

export default abstract class RestBase {
    abstract ignoreApiStatusCodes: boolean;

    protected async Promise<T> Get<T>(string path)
    {
        var request = new RestRequest(path, Method.Get);

        var response = await Execute(request);
        var result = DeserializeResponse<T>(response);
        return result;
    }
}
import ApiBase from 'functiontestrunner/dist/wrappers/api/api-base';
import Settings from '../config/settings';
import { Post } from '../models/post';

export default class PostsApi extends ApiBase {
    ignoreApiStatusCodes: boolean = false;
    constructor() {
        var settings = new Settings();
        super(settings.getApiBaseUrl(), settings.getApiTimeout())
    }

    public async createPost(post: Post): Promise<Post> {
        const response = await this.postWithBody<Post>('posts', post);
        return response;
    }

    public async getPost(id: string): Promise<Post> {
        const response = await this.get<Post>(`posts/${id}`);
        return response;
    }

    public async updatePost(id: string, post: Post): Promise<Post> {
        const response = await this.putWithBody<Post>(`posts/${id}`, post);
        return response;
    }

    public async deletePost(id: string): Promise<void> {
        await this.delete<Post>(`posts/${id}`);
    }
}
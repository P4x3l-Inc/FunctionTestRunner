import ApiBase from 'functiontestrunner/src/wrappers/api/api-base.js';
import { Post } from '../models/post';

export default class PostsApi extends ApiBase {
    ignoreApiStatusCodes: boolean = false;;

    public async createPost(post: Post): Promise<Post> {
        const response = await this.PostWithBody<Post>('posts', post);

        return response;
    }
}
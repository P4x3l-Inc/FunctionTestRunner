import ApiBase from 'functiontestrunner/dist/wrappers/api/api-base';
import { Post } from '../models/post';

export default class PostsApi extends ApiBase {
    ignoreApiStatusCodes: boolean = false;;

    public async createPost(post: Post): Promise<Post> {
        const response = await this.PostWithBody<Post>('posts', post);
        return response;
    }
}
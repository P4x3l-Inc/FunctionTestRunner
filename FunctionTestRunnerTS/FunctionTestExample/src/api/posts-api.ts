import { ApiBase } from 'functiontestrunner';
import { Post } from '../models/post';

export default class PostsApi extends ApiBase {
    public createPost(post: Post): Post {
        this.PostWithBody<Post>(post);

    }
}
import { Post } from "../models/post";
import { v4 as uuidv4 } from 'uuid';

export class PostService {
    private posts: Post[];
    private static instance: PostService;

    public static getInstance(): PostService {
        if (!PostService.instance) {
            PostService.instance = new PostService();
        }

        return PostService.instance;
    }

    private constructor() {
        this.posts = []
    }

    getAll(): Post[] {
        return this.posts;
    }

    get(id: string): Post | undefined {
        return this.posts.find(x => x.id === id);
    }

    create(post: Post): Post {
        post.id = uuidv4();
        post.hasBeenSent = false;
        this.posts.push(post);

        return post;
    }

    update(id: string, post: Post) {
        const index = this.posts.findIndex(x => x.id === id);
        if (index > -1) {
            this.posts[index].body = post.body;
            this.posts[index].title = post.title;
        }

        return this.get(id);
    }

    delete(id: string) {
        const index = this.posts.findIndex(x => x.id === id);
        if (index > -1) {
            this.posts.splice(index, 1); // 2nd parameter means remove one item only
        }
    }

    async sendPost(id: string) {
        setTimeout(() => {
            const index = this.posts.findIndex(x => x.id === id);
        
            if (index > -1) {
                this.posts[index].hasBeenSent = true;
            }
        }, 10000);
    }

    async sendPosts() {
        
        this.posts.forEach(post => {
            setTimeout(() => {
                if (!post.hasBeenSent) {
                    post.hasBeenSent = true;
                }
            }, 1000);
        });
    }
}
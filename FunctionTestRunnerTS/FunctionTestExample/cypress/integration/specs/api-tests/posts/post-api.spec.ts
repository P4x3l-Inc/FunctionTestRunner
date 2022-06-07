import TestRunner from "functiontestrunner/dist/utils/test-runner";
import PostsApi from "../../../../../src/api/posts-api";
import { Post } from "../../../../../src/models/post";

let api: PostsApi;

describe('Post Api', () => {
    before(() => {
        api = new PostsApi();
    });

    it('Should be able to perform CRUD operations', async () => {
        TestRunner.runAsync(async (bag) => {
            let post: Post = {
                title: 'test title',
                body: 'test body'
            };

            const postId = validateCreate(await api.createPost(post));
            
            post = validateGet(postId, await api.getPost(postId));

            post.body = 'updated body';
            post.title = 'updated title';
            validateUpdate(postId, await api.updatePost(postId, post));

            await api.deletePost(postId);
            validateDelete(await api.getPost(postId));
        });
    });
});

function validateCreate(post: Post): string {
    expect(post).not.to.be.null;
    expect(post.id).not.to.be.null;
    expect(post.title).to.equal('test title');
    expect(post.body).to.equal('test body');
    expect(post.hasBeenSent).to.be.false;

    return post.id;
}

function validateGet(expectedId: string, post: Post) {
    expect(post).not.to.be.null;
    expect(post.id).to.equal(expectedId);
    expect(post.title).to.equal('test title');
    expect(post.body).to.equal('test body');
    expect(post.hasBeenSent).to.be.false;

    return post;
}

function validateUpdate(expectedId: string, post: Post) {
    expect(post).not.to.be.null;
    expect(post.id).to.equal(expectedId);
    expect(post.title).to.equal('updated title');
    expect(post.body).to.equal('updated body');
    expect(post.hasBeenSent).to.be.false;

    return post;
}

function validateDelete(post: Post) {
    expect(post).to.be.null;
}
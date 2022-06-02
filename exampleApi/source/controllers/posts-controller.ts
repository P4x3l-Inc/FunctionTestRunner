import { Request, Response, NextFunction } from 'express';
import { Post } from '../models/post';
import { PostService } from '../services/posts-service';

const service = PostService.getInstance();

const getPosts = (req: Request, res: Response, next: NextFunction) => {
    const posts = service.getAll();
    return res.status(200).json({
        data: posts
    });
};

const getPost = (req: Request, res: Response, next: NextFunction) => {
    const id: string = req.params.id;
    
    const post = service.get(id);

    if (!post) {
        return res.status(404).json({
            message: `Post with id ${id} not found`
        });
    }

    return res.status(200).json({
        data: post
    });
};

const updatePost = (req: Request, res: Response, next: NextFunction) => {
    const id: string = req.params.id;
    const body: Post = req.body ?? null;
    
    const post = service.update(id, body);

    if (!post) {
        return res.status(404).json({
            message: `Post with id ${id} not found`
        });
    }
    
    return res.status(200).json({
        data: post,
    });
};

const deletePost = (req: Request, res: Response, next: NextFunction) => {
    const id: string = req.params.id;
    
    service.delete(id);
    
    return res.status(200).json({
        message: 'post deleted successfully'
    });
};

const addPost = (req: Request, res: Response, next: NextFunction) => {
    const body: Post = req.body;

    const post = service.create(body);
    // return response
    return res.status(201).json({
        data: post
    });
};

export default { getPosts, getPost, updatePost, deletePost, addPost };
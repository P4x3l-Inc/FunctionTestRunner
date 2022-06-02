import { Request, Response, NextFunction } from 'express';
import { Post } from '../models/post';
import { PostService } from '../services/posts-service';

const service = PostService.getInstance();

const sendPost = (req: Request, res: Response, next: NextFunction) => {
    const id: string = req.params.id;

    const post = service.get(id);

    if (!post) {
        return res.status(404).json({
            message: `Post with id ${id} not found`
        });
    }

    service.sendPost(id);
    
    return res.status(202).json({
        message: 'Request received',
    });
};

const sendPosts = (req: Request, res: Response, next: NextFunction) => {
    service.sendPosts();
    
    return res.status(202).json({
        message: 'Request received',
    });
};

export default { sendPost, sendPosts };
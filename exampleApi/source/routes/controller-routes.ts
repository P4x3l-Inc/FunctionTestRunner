import express from 'express';
import postController from '../controllers/posts-controller';
import sendController from '../controllers/send-controller';
const router = express.Router();

router.get('/posts', postController.getPosts);
router.get('/posts/:id', postController.getPost);
router.put('/posts/:id', postController.updatePost);
router.delete('/posts/:id', postController.deletePost);
router.post('/posts', postController.addPost);
router.post('/send', sendController.sendPosts);
router.post('/send/:id', sendController.sendPost);

export = router;
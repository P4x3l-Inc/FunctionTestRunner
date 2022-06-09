import http from 'http';
import express, { Express } from 'express';
import morgan from 'morgan';
import routes from './routes/controller-routes';
const apiKeyAuth = require('api-key-auth');

const router: Express = express();

const apiKeys = new Map();
apiKeys.set('047ede7a-875f-48fd-9e39-4e5ff6d80338', {
  id: 1,
  name: 'function-test'
});

function getSecret(keyId: string, done: any) {
    if (!apiKeys.has(keyId)) {
        return done(new Error('Unknown api key'));
    }
    const clientApp = apiKeys.get(keyId);
    done(null, clientApp.secret, {
        id: clientApp.id,
        name: clientApp.name
    });
}

//router.use(apiKeyAuth({ getSecret }));

/** Logging */
router.use(morgan('dev'));
/** Parse the request */
router.use(express.urlencoded({ extended: false }));
/** Takes care of JSON data */
router.use(express.json());

/** RULES OF OUR API */
router.use((req, res, next) => {
    // set the CORS policy
    res.header('Access-Control-Allow-Origin', '*');
    // set the CORS headers
    res.header('Access-Control-Allow-Headers', 'origin, X-Requested-With,Content-Type,Accept, Authorization');
    // set the CORS method headers
    if (req.method === 'OPTIONS') {
        res.header('Access-Control-Allow-Methods', 'GET PATCH DELETE POST');
        return res.status(200).json({});
    }
    next();
});

/** Routes */
router.use('/', routes);

/** Error handling */
router.use((req, res, next) => {
    const error = new Error('not found');
    return res.status(404).json({
        message: error.message
    });
});

// Auth by api-key
router.use((req, res, next) => {
    const providedApiKey = req.header('apikey');
    if (!apiKeys.has(providedApiKey)) {
        return res.status(401).json({
            message: 'Unauthorized'
        });
    }
    next();
});

/** Server */
const httpServer = http.createServer(router);
const PORT: any = process.env.PORT ?? 6060;
httpServer.listen(PORT, () => console.log(`The server is running on port ${PORT}`));
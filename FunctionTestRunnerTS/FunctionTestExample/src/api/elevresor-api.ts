import ScenarioPropertyBag from "functiontestrunner/dist/utils/scenario-property-bag";
import ApiBase from "functiontestrunner/dist/wrappers/api/api-base"
import Settings from "../config/settings";
import { Municipality } from "../models/municipality";

export class ElevresorApi extends ApiBase {
    ignoreApiStatusCodes: boolean = false;
    settings: Settings;
    
    constructor() {
        var settings = new Settings();
        super(settings.getApiBaseUrl(), 3000);
        this.settings = settings;
    }

    public async getMunicipality(bag: ScenarioPropertyBag): Promise<Municipality> {
        var queryParams = { api_key: this.settings.getApiKey() };

        var response = await super.getWithQueryParams<Municipality>("Municipality", queryParams);

        bag.set("schoolYearId", response.schoolYear);

        return response;
    }
}
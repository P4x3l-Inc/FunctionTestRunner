import TestRunner from "functiontestrunner/dist/utils/test-runner";
import { ElevresorApi } from "../api/elevresor-api";

describe('elevresor-api', () => {
    let service: ElevresorApi;

    before(() => {
        service = new ElevresorApi();
    });

    it('test1', async () => {
        await TestRunner.runAsync(async bag => {
            const result = await service.getMunicipality(bag as any);

            expect(result).is.not.null;
        });
    });
});

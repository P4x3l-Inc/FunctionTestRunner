export default class Wait {
    static async forMilliseconds(milliseconds: number)
    {
        await this.waitInternal(milliseconds);
    }

    static async forSeconds(seconds: number)
    {
        await this.waitInternal(seconds * 1000);
    }

    static async forMinutes(minutes: number)
    {
        await this.waitInternal((minutes * 60) * 1000);
    }

    private static async waitInternal(milliseconds: number)
    {
        return new Promise(resolve => setTimeout(resolve, milliseconds));
    }
}
export default class Wait {
    static forMilliseconds(milliseconds: number): Promise<void>;
    static forSeconds(seconds: number): Promise<void>;
    static forMinutes(minutes: number): Promise<void>;
    private static waitInternal;
}
//# sourceMappingURL=wait.d.ts.map
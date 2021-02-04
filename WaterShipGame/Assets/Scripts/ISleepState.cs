public interface ISleepState
{
    bool isSleeping();
    void enableSleeping();
    void disableSleeping();
    void sleepForSeconds(float secondsToSleepFor);
}
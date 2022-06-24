namespace PanDataCollector.DataCollectorController
{
    public enum CollectorState
    {
        Init, Connect, Reconnect, WaitForConnection, Start, Read, WaitForPhenotypeCalculation, End, Exit
    }
}
public class PlayerHealth : Health<AttackerData, EnemyCollisionHandler> 
{
    public void ResetValue()
    {
        CurrentValue = MaximumValue;
        InvokeValueChangedEvent();
    }
}
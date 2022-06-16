
public class Timer 
{
    float timer;
    public float MaxTime;

    public Timer(  float maxTime )
    {
        timer = 0.0f;
        MaxTime = maxTime;
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }

    public bool CheckTimer( float deltaTime)
    {
        timer += deltaTime;
        return timer >= MaxTime;
    }

}

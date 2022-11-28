public class Timer
{
    //Текущее свойство. 
    private float m_CurrentTime;
    // Завершился ли таймер или нет ( меньше или = 0 )
    public bool IsFinished => m_CurrentTime <= 0;

    //коструктор класса. Можно вызвать коструктор в другом классе.
    public Timer(float startTime)
    {
        Start(startTime);
    }
    // принимает значемние startTime

    public void Start(float startTime)
    {
        m_CurrentTime = startTime;
    }
    // Возвращяем время ( если текущее время <0 то выход, так минусуем время )
    public void RemoveTime(float deltatime)
    {
        if (m_CurrentTime <= 0) return;

        m_CurrentTime -= deltatime;
    }

}
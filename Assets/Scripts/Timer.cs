public class Timer
{
    //������� ��������. 
    private float m_CurrentTime;
    // ���������� �� ������ ��� ��� ( ������ ��� = 0 )
    public bool IsFinished => m_CurrentTime <= 0;

    //���������� ������. ����� ������� ���������� � ������ ������.
    public Timer(float startTime)
    {
        Start(startTime);
    }
    // ��������� ��������� startTime

    public void Start(float startTime)
    {
        m_CurrentTime = startTime;
    }
    // ���������� ����� ( ���� ������� ����� <0 �� �����, ��� �������� ����� )
    public void RemoveTime(float deltatime)
    {
        if (m_CurrentTime <= 0) return;

        m_CurrentTime -= deltatime;
    }

}
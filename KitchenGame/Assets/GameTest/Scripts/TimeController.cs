using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : Singleton<TimeController>
{
    public Text _timeText;

    private float timeSpend;//= 50395;
    private float m_speedupTime = 0;

    public int TimeScale = 1;

    public bool IsTimeOut = false;
    public bool IsSpeedUp = false;


    [Header("自定义时间")]
    public int hours;
    public int minutes;
    public int seconds;

    protected override void Awake()
    {
        base.Awake();
        timeSpend = hours * 3600 + minutes * 60 + seconds;
    }

    private void FixedUpdate()
    {
        if (!IsTimeOut)
        {
            TimeUpdate();
            TimeAcceleration();
            SpeedUpTime();
        }
    }

    private void TimeUpdate()
    {
        timeSpend += Time.fixedDeltaTime * TimeScale;
        hours = (int)timeSpend / 3600;
        minutes = ((int)timeSpend - hours * 3600) / 60;
        seconds = (int)timeSpend - hours * 3600 - minutes * 60;
        _timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

        /*seconds += Time.fixedDeltaTime;       
        seconds = (int)timeSpend;
        minutes += (int)seconds / 60;       
        hours+= (int)minutes / 60;
        minutes = (int)minutes % 60;
        _timeText.text = hours.ToString("00")+": "+minutes.ToString("00")+":"+seconds.ToString("00");*/

    }

    private void TimeAcceleration()
    {
        if (Input.GetKeyDown(KeyCode.T)&&!IsSpeedUp)
        {
            IsSpeedUp = true;
            TimeScale = 60;
            m_speedupTime = 60;
            //StartCoroutine(SpeedTime());
        }
    }

    private void SpeedUpTime()
    {
        if (IsSpeedUp)
        {
            m_speedupTime -= Time.fixedDeltaTime;
            Debug.Log(m_speedupTime);
            if(m_speedupTime<=0)
            {
                IsSpeedUp = false;
                TimeScale = 1;
            }
        }
    }

    #region 协程倒计时
    /*IEnumerator SpeedTime()
    {
        while(m_speedupTime>0)
        {
            Debug.Log(m_speedupTime);
            yield return new WaitForSeconds(1);
            m_speedupTime--;
        }
        TimeScale = 1;
        IsSpeedUp = false;
    }*/
    #endregion

    public void TimeOut()
    {
        IsTimeOut = true;
    }

    public void TimeIn()
    {
        IsTimeOut = false;
    }

}

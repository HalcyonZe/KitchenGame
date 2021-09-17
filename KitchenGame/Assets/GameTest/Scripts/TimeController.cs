using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    //public Image image;
    public Text hoursTex, minutesTex;
    public Text _timeText;
    private int hours = 13, minutes = 30;
    private int seconds;
    private float timeSpend = 50395;

    public int TimeScale = 1;

    private void FixedUpdate()
    {
        TimeUpdate();
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

}

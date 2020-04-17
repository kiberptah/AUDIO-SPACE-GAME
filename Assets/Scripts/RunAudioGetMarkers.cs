using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class RunAudioGetMarkers : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }


    public TimelineInfo timelineInfo;
    GCHandle timelineHandle = new GCHandle();

    public FMOD.Studio.EventInstance audioInstance;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    

    // Я понятия не имею как это работает
    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type,
        FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        //Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }

        return FMOD.RESULT.OK;
    }

    private void Start()
    {

    }

    public void Execute()
    {
        timelineInfo = new TimelineInfo();
        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used;
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);


        audioInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
     
        // Pass the object through the userdata of the instance
        audioInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        audioInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        audioInstance.start();
        //Debug.Log(audioInstance);
    }
    public void Stop(FMOD.Studio.STOP_MODE stopmode)
    {
        //FMOD.Studio.STOP_MODE.ALLOWFADEOUT = 0
        //FMOD.Studio.STOP_MODE.ALLOWFADEOUT = 1

        audioInstance.stop(stopmode);


    }

    public void Destroy(FMOD.Studio.STOP_MODE stopmode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT, float time = 0f)
    {
        audioInstance.stop(stopmode);

        Destroy(gameObject, time);
    }

    void OnDestroy()
    {
        audioInstance.setUserData(IntPtr.Zero);
        //audioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        timelineHandle.Free();
    }






}

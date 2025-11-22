using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{
    protected List<int> _event_list { get; set; }

    public void RemoveAllEvents()
    { 
        EventManager.RemoveListeners(_event_list);
    }
}

using System;
using System.Collections.Generic;

namespace Gamee._Manager
{
    public delegate void SkillCheck(MainStats stats,int check);
    public class EventManager
    {
        private Dictionary<string, Action> _eventDictionary = new Dictionary<string, Action>();
        private Dictionary<string, Action> _checkDictionary = new Dictionary<string, Action>();

        public void Subscribe(string eventName, Action action)
        {
            if (!_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = null;
            }
            _eventDictionary[eventName] += action;
        }
        public void Unsubscribe(string eventName, Action action)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] -= action;
            }
        }
        public void Invoke(string eventName)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName]?.Invoke();
            }
        }
    }
}
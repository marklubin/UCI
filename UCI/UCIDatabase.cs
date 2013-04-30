using System;

namespace UCI
{
    public static class UCIDatabase
    {
        Dictionary<string, Card> cardDict = new Dictionary<string, Card>();
        Dictionary<string, Door> doorDict = new Dictionary<string, Door>();
        Dictionary<string, PersonGroup> personGroupDict = new Dictionary<string, PersonGroup>();
        Dictionary<string, DoorGroup> doorGroupDict = new Dictionary<string, DoorGroup>();
        Dictionary<string, CardReader> cardReadersDict = new Dictionary<string, CardReader>();
        Dictionary<Door, TypicalWeek> doorWeekDictionary = new Dictionary<Door, TypicalWeek>();
    }
}
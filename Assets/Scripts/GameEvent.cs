using System;


public static class Events
{
    public static AddPointEvent addPointEvent = new AddPointEvent();
    public static AddDiamondEvent addDiamondEvent = new AddDiamondEvent();
    public static BonusPoint bonusPoint = new BonusPoint();
}

public class GameEvent { }

public class AddPointEvent : GameEvent { }

public class AddDiamondEvent : GameEvent { }

public class BonusPoint : GameEvent { }

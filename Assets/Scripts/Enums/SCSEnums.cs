namespace SCS.Enums
{
    public enum EnumSpaceObjectSize
    { 
        NA = -1,

        XS = 10,
        S = 20,
        M = 30,
        L = 40,
        XL = 50,

        G = 100,
    }

    public enum EnumSpaceshipSystems
    { 
        Main,
        Shields,
        Navigation,
        Weapons,
        Energy,
        Radar,
        Oxygen,
    }

    public enum EnumSpaceshipSystemActions
    {
        //Main,

        //Shields,

        //Navigation,
        MoveTo = 300,

        //Weapons,

        //Energy,

        //Radar,

        //Oxygen,
    }

    public enum EnumSpaceshipSystemActionStates
    { 
        NA,

        Waiting,

        Started,
        Paused,
        Completed
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEffectsLibrary
{
    public abstract class CommandState
    {
        public static CommandState<T> Create<T>()
            where T : Enum
            => new CommandState<T>();
    }

    public class CommandState<T> : CommandState
        where T: Enum
    {
        public T State;
    }

    public class Command
    {
        public Command(CommandState stateKeeperObject)
        {
            State = stateKeeperObject;
        }

        public object[] Parameters;
        public float PercentComplete = 0;
        public float Rate;

        public Func<GameObject, Command, object[], bool> Action;
        public CommandState State;
    }
}

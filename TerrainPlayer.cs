using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return @"Terrain.png";
        }
    }

    class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case Keys.Left:
                    if (x != 0)
                    	command.DeltaX = -1;
                    command.DeltaY = 0;
                    break;
                case Keys.Right:
                    if (x != Game.Map.GetLength(0) - 1)
                    	command.DeltaX = 1;
                    command.DeltaY = 0;
                    break;
                case Keys.Down:
                    if (y != Game.Map.GetLength(1) - 1)
                    	command.DeltaY = 1;
                    command.DeltaX = 0;
                    break;
                case Keys.Up:
                    if (y != 0)
                        command.DeltaY = -1;
                    command.DeltaX = 0;
                    break;
            }
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return !(conflictedObject is Terrain);
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return @"Digger.png";
        }
    }
}

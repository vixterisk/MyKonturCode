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
        CreatureCommand DefineDirection(int x, int y)
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

        public CreatureCommand Act(int x, int y)
        {
            var command = DefineDirection(x, y);
            bool cellIsEmpty = Game.Map[x + command.DeltaX, y + command.DeltaY] == null;
            if (!cellIsEmpty)
            {
                bool sackInCell = Game.Map[x + command.DeltaX, y + command.DeltaY] is Sack;
                if (sackInCell)
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            }
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
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

    class Sack : ICreature
    {
        bool isFalling;
        int amountOfMovesWhileFalling;

        bool SackBreakerInCell(ICreature creature)
        {
            return creature is Terrain || creature is Sack || creature is Gold;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var sackIsAtTheBottom = y == Game.Map.GetLength(1) - 1;
            var nextCellIsEmpty = !sackIsAtTheBottom && Game.Map[x, y + 1] == null;
			if (isFalling && amountOfMovesWhileFalling > 1 && 
			(sackIsAtTheBottom || !nextCellIsEmpty && SackBreakerInCell(Game.Map[x, y + 1])))
				command.TransformTo = new Gold();
            isFalling = !sackIsAtTheBottom && (nextCellIsEmpty || (Game.Map[x, y + 1] is Player  
                        || Game.Map[x, y + 1] is Monster) && amountOfMovesWhileFalling > 0);
            if (isFalling)
            {
                amountOfMovesWhileFalling++;
                command.DeltaY = 1;
                command.DeltaX = 0;
            }
            else
                amountOfMovesWhileFalling = 0;
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return @"Sack.png";
        }
    }

    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
                Game.Scores += 10;
            return conflictedObject is Player || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return @"Gold.png";
        }
    }

    class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int playerX = -1, playerY = -1;
            for (int i = 0; i < Game.Map.GetLength(0); i++)
                for (int j = 0; j < Game.Map.GetLength(1); j++)
                    if (Game.Map[i, j] != null && Game.Map[i, j] is Player)
                    {
                        playerX = i;
                        playerY = j;
                    }
            if (playerX == -1)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            var upDirection = y != 0 && CanMonsterMoveInCell(x, y - 1) && 
			Math.Abs(playerY - y) > Math.Abs(playerY - (y - 1));
            var downDirection = y != Game.Map.GetLength(1) - 1 && 
			CanMonsterMoveInCell(x, y + 1) && Math.Abs(playerY - y) > Math.Abs(playerY - (y + 1));
            var leftDirection = x != 0 && CanMonsterMoveInCell(x - 1, y) && 
			Math.Abs(playerX - x) > Math.Abs(playerX - (x - 1));
            var rightDirection = x != Game.Map.GetLength(0) - 1 && 
			CanMonsterMoveInCell(x + 1, y) && Math.Abs(playerX - x) > Math.Abs(playerX - (x + 1));
            if (upDirection)
                return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
            if (downDirection)
                return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
            if (leftDirection)
                return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
            if (rightDirection)
                return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        bool CanMonsterMoveInCell(int x, int y)
        {
            return Game.Map[x, y] == null || Game.Map[x, y] is Player || Game.Map[x, y] is Gold;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return @"Monster.png";
        }
    }
}

using Robocode;

namespace dotnet_bots.Snippets
{
    public class TooCloseToWallCustomEvent : AdvancedRobot
    {
        public override void Run()
        {
            AddCustomEvent(new Condition("CloseToWall", 1, condition =>
            {
                int wallMargin = 60;
                return (
                    // we're too close to the left wall
                    X <= wallMargin ||
                    // or we're too close to the right wall
                    X >= BattleFieldWidth - wallMargin ||
                    // or we're too close to the bottom wall
                    Y <= wallMargin ||
                    // or we're too close to the top wall
                    Y >= BattleFieldHeight - wallMargin
                );
            }));
        }

        public override void OnCustomEvent(CustomEvent e)
        {
            if (e.Condition.Name == "CustomEvent")
            {
                
            }
        }
    }
}
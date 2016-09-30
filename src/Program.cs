
namespace RPGFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MoveBuilder mbuilder = new MoveBuilder("res/moves/");
            CharacterBuilder cbuilder = new CharacterBuilder("res/Characters/");
            cbuilder.MoveBuilder = mbuilder;
            Character[] pside = new Character[] { cbuilder.buildFromXml("milly.xml", new PlayerMoveProvider()) };
            Character[] eside = new Character[] { cbuilder.buildFromXml("gerard.xml", new PlayerMoveProvider()) };
            TurnProvider turnProvider = new TraditionalTurnProvider();
            Battle b = new Battle(pside, eside, turnProvider);
            b.start();
        }
    }
}

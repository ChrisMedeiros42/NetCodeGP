namespace Braxvius.UDCT.Utilities.DeveloperConsole.Commands {
    public interface IConsoleCommand {
        public string CommandWord { get; }
        public abstract bool Process(string[] args);
    }
}

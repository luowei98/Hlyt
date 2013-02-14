using System.Collections.Generic;
using Hlyt.CommandProcessor.Command;
using Hlyt.Core.Common;


namespace Hlyt.CommandProcessor.Dispatcher
{
    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand;
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

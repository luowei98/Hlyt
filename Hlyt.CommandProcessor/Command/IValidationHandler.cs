using System.Collections.Generic;
using Hlyt.Core.Common;


namespace Hlyt.CommandProcessor.Command
{
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}

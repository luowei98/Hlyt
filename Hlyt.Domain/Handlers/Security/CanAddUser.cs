using System.Collections.Generic;
using Hlyt.CommandProcessor.Command;
using Hlyt.Core.Common;
using Hlyt.Data.Repositories;
using Hlyt.Domain.Commands.Security;
using Hlyt.Domain.Properties;
using Hlyt.Model.Security;


namespace Hlyt.Domain.Handlers.Security
{
    public class CanAddUser : IValidationHandler<UserRegisterCommand>
    {
        private readonly IUserRepository userRepository;

        public CanAddUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #region IValidationHandler<UserRegisterCommand> Members

        public IEnumerable<ValidationResult> Validate(UserRegisterCommand command)
        {
            User isUserExists = userRepository.Get(c => c.Email == command.Email);

            if (isUserExists != null)
            {
                yield return new ValidationResult("EMail", Resources.UserExists);
            }
        }

        #endregion
    }
}

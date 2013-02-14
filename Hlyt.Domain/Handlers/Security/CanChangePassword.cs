using System.Collections.Generic;
using Hlyt.CommandProcessor.Command;
using Hlyt.Core.Common;
using Hlyt.Data.Repositories;
using Hlyt.Domain.Commands.Security;
using Hlyt.Domain.Properties;
using Hlyt.Model.Security;


namespace Hlyt.Domain.Handlers.Security
{
    public class CanChangePassword : IValidationHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository userRepository;

        public CanChangePassword(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #region IValidationHandler<ChangePasswordCommand> Members

        public IEnumerable<ValidationResult> Validate(ChangePasswordCommand command)
        {
            User user = userRepository.GetById(command.UserId);
            string encoded = Md5Encrypt.Md5EncryptPassword(command.OldPassword);

            if (!user.PasswordHash.Equals(encoded))
            {
                yield return new ValidationResult("OldPassword", Resources.Password);
            }
        }

        #endregion
    }
}

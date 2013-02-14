using System;
using Hlyt.CommandProcessor.Command;
using Hlyt.Data.Infrastructure;
using Hlyt.Data.Repositories;
using Hlyt.Domain.Commands.Security;
using Hlyt.Model.Security;


namespace Hlyt.Domain.Handlers.Security
{
    public class UserRegisterHandler : ICommandHandler<UserRegisterCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserRegisterHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ICommandHandler<UserRegisterCommand> Members

        public ICommandResult Execute(UserRegisterCommand command)
        {
            var user = new User
                       {
                           FirstName = command.FirstName,
                           LastName = command.LastName,
                           Email = command.Email,
                           Password = command.Password,
                           RoleId = command.RoleId,
                           DateCreated = DateTime.Now,
                           LastLoginTime = DateTime.Now,
                           Activated = command.Activated
                       };
            userRepository.Add(user);
            unitOfWork.Commit();
            return new CommandResult(true);
        }

        #endregion
    }
}

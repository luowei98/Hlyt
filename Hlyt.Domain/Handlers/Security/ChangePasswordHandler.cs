using Hlyt.CommandProcessor.Command;
using Hlyt.Data.Infrastructure;
using Hlyt.Data.Repositories;
using Hlyt.Domain.Commands.Security;
using Hlyt.Model.Security;


namespace Hlyt.Domain.Handlers.Security
{
    public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public ChangePasswordHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ICommandHandler<ChangePasswordCommand> Members

        public ICommandResult Execute(ChangePasswordCommand command)
        {
            User user = userRepository.GetById(command.UserId);
            user.Password = command.NewPassword;
            userRepository.Update(user);
            unitOfWork.Commit();
            return new CommandResult(true);
        }

        #endregion
    }
}

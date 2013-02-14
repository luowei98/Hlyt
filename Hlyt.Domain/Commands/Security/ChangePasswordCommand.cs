using Hlyt.CommandProcessor.Command;


namespace Hlyt.Domain.Commands.Security
{
    public class ChangePasswordCommand : ICommand
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

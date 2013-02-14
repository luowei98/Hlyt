using System;
using System.Security.Principal;
using System.Web.Security;


namespace Hlyt.Web.Core.Models
{
    [Serializable]
    public class HlytUser : IIdentity
    {
        public HlytUser()
        {
        }

        public HlytUser(string name, string displayName, int userId)
        {
            Name = name;
            DisplayName = displayName;
            UserId = userId;
        }

        public HlytUser(string name, string displayName, int userId, string roleName)
        {
            Name = name;
            DisplayName = displayName;
            UserId = userId;
            RoleName = roleName;
        }

        public HlytUser(string name, UserInfo userInfo)
            : this(name, userInfo.DisplayName, userInfo.UserId, userInfo.RoleName)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            UserId = userInfo.UserId;
        }

        public HlytUser(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }

        public string DisplayName { get; private set; }
        public string RoleName { get; private set; }
        public int UserId { get; private set; }

        #region IIdentity Members

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "HlytForms"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        #endregion
    }
}

using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B0 RID: 1968
	[Serializable]
	public sealed class MailboxFolderUserId : ObjectId
	{
		// Token: 0x0600454D RID: 17741 RVA: 0x0011CBC0 File Offset: 0x0011ADC0
		private MailboxFolderUserId(PermissionSecurityPrincipal.SpecialPrincipalType specialPrincipalType)
		{
			switch (specialPrincipalType)
			{
			case PermissionSecurityPrincipal.SpecialPrincipalType.Default:
				this.userType = MailboxFolderUserId.MailboxFolderUserType.Default;
				return;
			}
			this.userType = MailboxFolderUserId.MailboxFolderUserType.Anonymous;
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x0011CBF4 File Offset: 0x0011ADF4
		private MailboxFolderUserId(ADRecipient adRecipient)
		{
			if (adRecipient == null)
			{
				throw new ArgumentNullException("adRecipient");
			}
			this.userType = MailboxFolderUserId.MailboxFolderUserType.Internal;
			this.adRecipient = adRecipient;
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x0011CC18 File Offset: 0x0011AE18
		private MailboxFolderUserId(SmtpAddress smtpAddress)
		{
			this.userType = MailboxFolderUserId.MailboxFolderUserType.External;
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x0011CC2E File Offset: 0x0011AE2E
		private MailboxFolderUserId(ExternalUser externalUser)
		{
			if (externalUser == null)
			{
				throw new ArgumentNullException("externalUser");
			}
			this.userType = MailboxFolderUserId.MailboxFolderUserType.External;
			this.smtpAddress = externalUser.SmtpAddress;
			this.externalUser = externalUser;
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x0011CC5E File Offset: 0x0011AE5E
		private MailboxFolderUserId(string unknownMemberName)
		{
			if (string.IsNullOrEmpty(unknownMemberName))
			{
				throw new ArgumentNullException("unknownMemberName");
			}
			this.userType = MailboxFolderUserId.MailboxFolderUserType.Unknown;
			this.unknownMemberName = unknownMemberName;
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x0011CC88 File Offset: 0x0011AE88
		internal void EnsureExternalUser(MailboxSession mailboxSession)
		{
			if (this.UserType != MailboxFolderUserId.MailboxFolderUserType.External)
			{
				throw new InvalidOperationException("Only support External user type.");
			}
			if (this.externalUser != null)
			{
				return;
			}
			using (ExternalUserCollection externalUsers = mailboxSession.GetExternalUsers())
			{
				ExternalUser externalUser = externalUsers.FindExternalUser(this.smtpAddress);
				if (externalUser == null)
				{
					throw new InvalidExternalUserIdException(this.smtpAddress.ToString());
				}
				this.externalUser = externalUser;
			}
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x0011CD08 File Offset: 0x0011AF08
		internal static MailboxFolderUserId CreateFromSecurityPrincipal(PermissionSecurityPrincipal securityPrincipal)
		{
			if (securityPrincipal == null)
			{
				throw new ArgumentNullException("securityPrincipal");
			}
			switch (securityPrincipal.Type)
			{
			case PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal:
				return new MailboxFolderUserId(securityPrincipal.ADRecipient);
			case PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal:
				return new MailboxFolderUserId(securityPrincipal.ExternalUser);
			case PermissionSecurityPrincipal.SecurityPrincipalType.SpecialPrincipal:
				return new MailboxFolderUserId(securityPrincipal.SpecialType);
			}
			return new MailboxFolderUserId(securityPrincipal.UnknownPrincipalMemberName);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x0011CD74 File Offset: 0x0011AF74
		internal static MailboxFolderUserId TryCreateFromSmtpAddress(string smtpAddress, MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (string.IsNullOrEmpty(smtpAddress))
			{
				throw new ArgumentNullException("smtpAddress");
			}
			if (!SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				throw new ArgumentException("smtpAddress");
			}
			MailboxFolderUserId mailboxFolderUserId = new MailboxFolderUserId(new SmtpAddress(smtpAddress));
			try
			{
				mailboxFolderUserId.EnsureExternalUser(mailboxSession);
			}
			catch (InvalidExternalUserIdException)
			{
				return null;
			}
			return mailboxFolderUserId;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x0011CDE4 File Offset: 0x0011AFE4
		internal static MailboxFolderUserId TryCreateFromADRecipient(ADRecipient recipient, bool allowInvalidSecurityPrincipal)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (allowInvalidSecurityPrincipal || recipient.IsValidSecurityPrincipal)
			{
				return new MailboxFolderUserId(recipient);
			}
			return null;
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x0011CE07 File Offset: 0x0011B007
		internal static MailboxFolderUserId CreateFromUnknownUser(string unknownUser)
		{
			if (string.IsNullOrEmpty(unknownUser))
			{
				throw new ArgumentNullException("unknownUser");
			}
			return new MailboxFolderUserId(unknownUser);
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x0011CE22 File Offset: 0x0011B022
		public override byte[] GetBytes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x0011CE29 File Offset: 0x0011B029
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x0011CE34 File Offset: 0x0011B034
		internal PermissionSecurityPrincipal ToSecurityPrincipal()
		{
			switch (this.UserType)
			{
			case MailboxFolderUserId.MailboxFolderUserType.Internal:
				return new PermissionSecurityPrincipal(this.adRecipient);
			case MailboxFolderUserId.MailboxFolderUserType.External:
				if (this.externalUser == null)
				{
					throw new InvalidOperationException("external user is null");
				}
				return new PermissionSecurityPrincipal(this.externalUser);
			default:
				throw new NotSupportedException("Only support internal or external user");
			}
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x0011CE90 File Offset: 0x0011B090
		internal bool Equals(PermissionSecurityPrincipal securityPrincipal)
		{
			if (securityPrincipal == null)
			{
				return false;
			}
			switch (securityPrincipal.Type)
			{
			case PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal:
				return this.UserType == MailboxFolderUserId.MailboxFolderUserType.Internal && securityPrincipal.ADRecipient.Id.Equals(this.adRecipient.Id);
			case PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal:
				return this.UserType == MailboxFolderUserId.MailboxFolderUserType.External && securityPrincipal.ExternalUser.SmtpAddress.Equals(this.smtpAddress);
			case PermissionSecurityPrincipal.SecurityPrincipalType.SpecialPrincipal:
				return (securityPrincipal.SpecialType == PermissionSecurityPrincipal.SpecialPrincipalType.Default && this.UserType == MailboxFolderUserId.MailboxFolderUserType.Default) || (securityPrincipal.SpecialType == PermissionSecurityPrincipal.SpecialPrincipalType.Anonymous && this.UserType == MailboxFolderUserId.MailboxFolderUserType.Anonymous);
			}
			return this.UserType == MailboxFolderUserId.MailboxFolderUserType.Unknown && StringComparer.InvariantCultureIgnoreCase.Equals(this.unknownMemberName, securityPrincipal.UnknownPrincipalMemberName);
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x0011CF55 File Offset: 0x0011B155
		public MailboxFolderUserId.MailboxFolderUserType UserType
		{
			get
			{
				return this.userType;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x0011CF5D File Offset: 0x0011B15D
		public ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x0011CF68 File Offset: 0x0011B168
		public string DisplayName
		{
			get
			{
				switch (this.UserType)
				{
				case MailboxFolderUserId.MailboxFolderUserType.Default:
					return Strings.DefaultUser;
				case MailboxFolderUserId.MailboxFolderUserType.Anonymous:
					return Strings.AnonymousUser;
				case MailboxFolderUserId.MailboxFolderUserType.Internal:
					return this.adRecipient.DisplayName;
				case MailboxFolderUserId.MailboxFolderUserType.External:
					return this.smtpAddress.ToString();
				}
				return this.unknownMemberName;
			}
		}

		// Token: 0x04002ACC RID: 10956
		internal static readonly MailboxFolderUserId DefaultUserId = new MailboxFolderUserId(PermissionSecurityPrincipal.SpecialPrincipalType.Default);

		// Token: 0x04002ACD RID: 10957
		internal static readonly MailboxFolderUserId AnonymousUserId = new MailboxFolderUserId(PermissionSecurityPrincipal.SpecialPrincipalType.Anonymous);

		// Token: 0x04002ACE RID: 10958
		private readonly MailboxFolderUserId.MailboxFolderUserType userType;

		// Token: 0x04002ACF RID: 10959
		private readonly ADRecipient adRecipient;

		// Token: 0x04002AD0 RID: 10960
		private readonly SmtpAddress smtpAddress;

		// Token: 0x04002AD1 RID: 10961
		private readonly string unknownMemberName;

		// Token: 0x04002AD2 RID: 10962
		[NonSerialized]
		private ExternalUser externalUser;

		// Token: 0x020007B1 RID: 1969
		public enum MailboxFolderUserType
		{
			// Token: 0x04002AD4 RID: 10964
			Default,
			// Token: 0x04002AD5 RID: 10965
			Anonymous,
			// Token: 0x04002AD6 RID: 10966
			Internal,
			// Token: 0x04002AD7 RID: 10967
			External,
			// Token: 0x04002AD8 RID: 10968
			Unknown
		}
	}
}

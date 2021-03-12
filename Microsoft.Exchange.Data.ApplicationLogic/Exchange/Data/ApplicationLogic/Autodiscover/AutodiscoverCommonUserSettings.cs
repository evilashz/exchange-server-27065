using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Autodiscover
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutodiscoverCommonUserSettings
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001CF4F File Offset: 0x0001B14F
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001CF57 File Offset: 0x0001B157
		internal string AccountDisplayName { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001CF60 File Offset: 0x0001B160
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001CF68 File Offset: 0x0001B168
		internal string AccountLegacyDn { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001CF71 File Offset: 0x0001B171
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001CF79 File Offset: 0x0001B179
		internal SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001CF82 File Offset: 0x0001B182
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001CF8A File Offset: 0x0001B18A
		internal string RpcServer { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001CF93 File Offset: 0x0001B193
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001CF9B File Offset: 0x0001B19B
		internal Guid MailboxGuid { get; private set; }

		// Token: 0x06000772 RID: 1906 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		private AutodiscoverCommonUserSettings(string accountDisplayName, string accountLegacyDn, SmtpAddress primarySmtpAddress, string rpcServer, Guid mailboxGuid)
		{
			this.AccountDisplayName = accountDisplayName;
			this.AccountLegacyDn = accountLegacyDn;
			this.PrimarySmtpAddress = primarySmtpAddress;
			this.RpcServer = rpcServer;
			this.MailboxGuid = mailboxGuid;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		internal static AutodiscoverCommonUserSettings GetSettingsFromRecipient(ADUser user, string emailAddress)
		{
			bool flag = AutodiscoverCommonUserSettings.IsArchiveMailUser(user) || AutodiscoverCommonUserSettings.IsEmailAddressTargetingArchive(user, emailAddress);
			string domain = user.PrimarySmtpAddress.Domain;
			Guid mailboxGuid = flag ? user.ArchiveGuid : user.ExchangeGuid;
			return new AutodiscoverCommonUserSettings(flag ? AutodiscoverCommonUserSettings.GetArchiveDisplayName(user) : user.DisplayName, flag ? user.GetAlternateMailboxLegDN(user.ArchiveGuid) : user.LegacyExchangeDN, user.PrimarySmtpAddress, ExchangeRpcClientAccess.CreatePersonalizedServer(mailboxGuid, domain), mailboxGuid);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001D050 File Offset: 0x0001B250
		internal static bool IsArchiveMailUser(ADRecipient recipient)
		{
			return recipient.RecipientType == RecipientType.MailUser && AutodiscoverCommonUserSettings.HasLocalArchive(recipient);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001D063 File Offset: 0x0001B263
		internal static string GetArchiveDisplayName(ADUser user)
		{
			if (user.ArchiveName == null || user.ArchiveName.Count != 1)
			{
				return "Online Archive";
			}
			return user.ArchiveName[0];
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001D090 File Offset: 0x0001B290
		internal static bool HasLocalArchive(ADRecipient recipient)
		{
			ADUser aduser = recipient as ADUser;
			return aduser != null && !aduser.ArchiveGuid.Equals(Guid.Empty) && aduser.ArchiveDatabase != null && !aduser.ArchiveDatabase.ObjectGuid.Equals(Guid.Empty);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001D0E4 File Offset: 0x0001B2E4
		internal static bool IsEmailAddressTargetingArchive(ADUser adUser, string emailAddress)
		{
			if (adUser == null || string.IsNullOrEmpty(emailAddress))
			{
				return false;
			}
			bool result = false;
			Guid empty = Guid.Empty;
			if (AutodiscoverCommonUserSettings.TryGetExchangeGuidFromEmailAddress(emailAddress, out empty))
			{
				result = empty.Equals(adUser.ArchiveGuid);
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001D120 File Offset: 0x0001B320
		internal static bool TryGetExchangeGuidFromEmailAddress(string emailAddress, out Guid exchangeGuid)
		{
			exchangeGuid = Guid.Empty;
			if (string.IsNullOrEmpty(emailAddress) || !SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				return false;
			}
			if (SmtpProxyAddress.TryDeencapsulateExchangeGuid(emailAddress, out exchangeGuid))
			{
				return true;
			}
			SmtpAddress smtpAddress = new SmtpAddress(emailAddress);
			return !string.IsNullOrEmpty(smtpAddress.Local) && Guid.TryParse(smtpAddress.Local, out exchangeGuid);
		}
	}
}

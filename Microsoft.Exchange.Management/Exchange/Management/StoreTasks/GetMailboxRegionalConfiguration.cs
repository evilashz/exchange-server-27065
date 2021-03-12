using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CB RID: 1995
	[Cmdlet("Get", "MailboxRegionalConfiguration")]
	public sealed class GetMailboxRegionalConfiguration : GetMailboxConfigurationTaskBase<MailboxRegionalConfiguration>
	{
		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x0011FD86 File Offset: 0x0011DF86
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x0011FDAC File Offset: 0x0011DFAC
		[Parameter(Mandatory = false)]
		public SwitchParameter VerifyDefaultFolderNameLanguage
		{
			get
			{
				return (SwitchParameter)(base.Fields["VerifyDefaultFolderNameLanguage"] ?? false);
			}
			set
			{
				base.Fields["VerifyDefaultFolderNameLanguage"] = value;
			}
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x0011FDC4 File Offset: 0x0011DFC4
		protected override IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(adUser);
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(base.SessionSettings, adUser, RemotingOptions.AllowCrossSite);
			mailboxStoreTypeProvider.MailboxSession = StoreTasksHelper.OpenMailboxSession(principal, "Get-MailboxConfiguration", this.VerifyDefaultFolderNameLanguage.IsPresent);
			return mailboxStoreTypeProvider;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x0011FE08 File Offset: 0x0011E008
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (this.VerifyDefaultFolderNameLanguage.IsPresent)
			{
				MailboxStoreTypeProvider mailboxStoreTypeProvider = (MailboxStoreTypeProvider)base.DataSession;
				((MailboxRegionalConfiguration)dataObject).DefaultFolderNameMatchingUserLanguage = mailboxStoreTypeProvider.MailboxSession.VerifyDefaultFolderLocalization();
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x0011FE4E File Offset: 0x0011E04E
		protected override bool ReadUserFromDC
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002AFA RID: 11002
		private const string ParameterVerifyDefaultFolderNameLanguage = "VerifyDefaultFolderNameLanguage";
	}
}

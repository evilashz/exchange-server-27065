using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CC RID: 1996
	[Cmdlet("Set", "MailboxRegionalConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxRegionalConfiguration : SetMailboxConfigurationTaskBase<MailboxRegionalConfiguration>
	{
		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x0011FE59 File Offset: 0x0011E059
		// (set) Token: 0x0600460D RID: 17933 RVA: 0x0011FE7F File Offset: 0x0011E07F
		[Parameter(Mandatory = false)]
		public SwitchParameter LocalizeDefaultFolderName
		{
			get
			{
				return (SwitchParameter)(base.Fields["LocalizeDefaultFolderName"] ?? false);
			}
			set
			{
				base.Fields["LocalizeDefaultFolderName"] = value;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x0011FE97 File Offset: 0x0011E097
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxRegionalConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x0011FEAC File Offset: 0x0011E0AC
		protected override IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(adUser);
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(base.SessionSettings, adUser, RemotingOptions.AllowCrossSite);
			mailboxStoreTypeProvider.MailboxSession = StoreTasksHelper.OpenMailboxSession(principal, "Set-MailboxConfiguration", this.LocalizeDefaultFolderName.IsPresent);
			return mailboxStoreTypeProvider;
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x0011FEF0 File Offset: 0x0011E0F0
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			MailboxStoreTypeProvider mailboxStoreTypeProvider = null;
			try
			{
				mailboxStoreTypeProvider = (MailboxStoreTypeProvider)this.CreateSession();
				if (this.LocalizeDefaultFolderName.IsPresent)
				{
					Exception[] array;
					mailboxStoreTypeProvider.MailboxSession.LocalizeDefaultFolders(out array);
					if (array != null && array.Length > 0)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorCannotLocalizeDefaultFolders(this.Identity.ToString(), array[0].Message), array[0]), ErrorCategory.InvalidOperation, this.Identity);
					}
				}
				mailboxStoreTypeProvider.MailboxSession.SetMailboxLocale();
			}
			finally
			{
				if (mailboxStoreTypeProvider != null && mailboxStoreTypeProvider.MailboxSession != null)
				{
					mailboxStoreTypeProvider.MailboxSession.Dispose();
					mailboxStoreTypeProvider.MailboxSession = null;
				}
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x0011FFA8 File Offset: 0x0011E1A8
		protected override bool ReadUserFromDC
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002AFB RID: 11003
		private const string ParameterLocalizeDefaultFolderName = "LocalizeDefaultFolderName";
	}
}

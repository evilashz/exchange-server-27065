using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.ServiceHost.Common.Powershell
{
	// Token: 0x02000018 RID: 24
	internal class MailboxHelper : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003FCB File Offset: 0x000021CB
		public MailboxHelper(string partitionFqdn)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.psProvider = new LocalPowerShellProvider(partitionFqdn);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003FEB File Offset: 0x000021EB
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxHelper>(this);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003FF3 File Offset: 0x000021F3
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000400F File Offset: 0x0000220F
		public void Dispose()
		{
			if (this.psProvider != null)
			{
				this.psProvider.Dispose();
				this.psProvider = null;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004048 File Offset: 0x00002248
		public bool IsMailboxCreated(string name, string tenant)
		{
			this.ValidateParamenter("name", name);
			this.ValidateParamenter("tenant", tenant);
			PSCommand pscommand = new PSCommand();
			pscommand.AddScript(string.Format("Get-Mailbox -Organization {0} -Filter {{Name -eq '{1}'}}", tenant, name));
			Collection<PSObject> collection = this.psProvider.ExecuteCommand(pscommand);
			return collection.Count > 0;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000409C File Offset: 0x0000229C
		public Mailbox CreateMailbox(string name, string tenant, string domain)
		{
			this.ValidateParamenter("name", name);
			this.ValidateParamenter("tenant", tenant);
			this.ValidateParamenter("domain", domain);
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("New-SyncMailbox");
			pscommand.AddParameter("Organization", tenant);
			pscommand.AddParameter("Name", name);
			pscommand.AddParameter("MicrosoftOnlineServicesID", name + "@" + domain);
			string randomPassword = PasswordHelper.GetRandomPassword("a", "a", 128);
			pscommand.AddParameter("Password", randomPassword.ConvertToSecureString());
			pscommand.AddParameter("HiddenFromAddressListsEnabled", true);
			pscommand.AddParameter("OverrideRecipientQuotas", true);
			Collection<PSObject> collection = this.psProvider.ExecuteCommand(pscommand);
			if (collection.Count <= 0)
			{
				throw new Exception("The monitoring account wasn't created successfully through local powershell");
			}
			return (Mailbox)collection[0].BaseObject;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004190 File Offset: 0x00002390
		public void ExcludeMailboxFromBackSync(string name, string tenant)
		{
			this.ValidateParamenter("name", name);
			this.ValidateParamenter("tenant", tenant);
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-SyncMailbox");
			pscommand.AddParameter("Identity", string.Format("{0}\\{1}", tenant, name));
			pscommand.AddParameter("ExcludedFromBackSync", true);
			this.psProvider.ExecuteCommand(pscommand);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004200 File Offset: 0x00002400
		public void SetDefaultRegionConfiguration(string mailbox, string tenant)
		{
			this.ValidateParamenter("mailbox", mailbox);
			this.ValidateParamenter("tenant", tenant);
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-MailboxRegionalConfiguration");
			pscommand.AddParameter("Identity", string.Format("{0}\\{1}", tenant, mailbox));
			pscommand.AddParameter("TimeZone", "Pacific Standard Time");
			pscommand.AddParameter("Language", "EN-US");
			this.psProvider.ExecuteCommand(pscommand);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004280 File Offset: 0x00002480
		public void ResetPassword(string mailbox, string tenant, string password)
		{
			this.ValidateParamenter("mailbox", mailbox);
			this.ValidateParamenter("tenant", tenant);
			this.ValidateParamenter("password", password);
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-Mailbox");
			pscommand.AddParameter("Identity", string.Format("{0}\\{1}", tenant, mailbox));
			pscommand.AddParameter("Password", password.ConvertToSecureString());
			this.psProvider.ExecuteCommand(pscommand);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042FC File Offset: 0x000024FC
		public void RemoveMailbox(string mailbox, string tenant)
		{
			this.ValidateParamenter("mailbox", mailbox);
			this.ValidateParamenter("tenant", tenant);
			PSCommand pscommand = new PSCommand();
			pscommand.AddScript(string.Format("Get-Mailbox -Organization {0} -Identity {1} | Remove-Mailbox -Permanent:$True -Confirm:$False", tenant, mailbox));
			this.psProvider.ExecuteCommand(pscommand);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004348 File Offset: 0x00002548
		public void UpdateMailboxCreateTime(string mailboxDN, string value)
		{
			this.ValidateParamenter("mailbox", mailboxDN);
			this.ValidateParamenter("value", value);
			PSCommand pscommand = new PSCommand();
			pscommand.AddScript(string.Format("Set-ADObject -Identity '{0}' -Replace @{{msExchWhenMailboxCreated = '{1}'}}", mailboxDN, value));
			this.psProvider.ExecuteCommand(pscommand);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004393 File Offset: 0x00002593
		private void ValidateParamenter(string name, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x04000053 RID: 83
		private LocalPowerShellProvider psProvider;

		// Token: 0x04000054 RID: 84
		private DisposeTracker disposeTracker;
	}
}

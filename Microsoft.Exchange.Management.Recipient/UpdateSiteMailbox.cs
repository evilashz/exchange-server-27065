using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000EB RID: 235
	[Cmdlet("Update", "SiteMailbox", SupportsShouldProcess = true)]
	public sealed class UpdateSiteMailbox : TeamMailboxDiagnosticsBase
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004182E File Offset: 0x0003FA2E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateTeamMailbox(base.Identity.ToString());
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00041840 File Offset: 0x0003FA40
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x00041857 File Offset: 0x0003FA57
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[ValidateNotNullOrEmpty]
		public string Server
		{
			get
			{
				return (string)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004186A File Offset: 0x0003FA6A
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00041890 File Offset: 0x0003FA90
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public SwitchParameter FullSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["FullSync"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["FullSync"] = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000418A8 File Offset: 0x0003FAA8
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000418BF File Offset: 0x0003FABF
		[Parameter(Mandatory = false)]
		public TeamMailboxDiagnosticsBase.TargetType? Target
		{
			get
			{
				return (TeamMailboxDiagnosticsBase.TargetType?)base.Fields["Target"];
			}
			set
			{
				base.Fields["Target"] = value;
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000418D8 File Offset: 0x0003FAD8
		protected override void InternalProcessRecord()
		{
			foreach (KeyValuePair<ADUser, ExchangePrincipal> keyValuePair in base.TMPrincipals)
			{
				ADUser key = keyValuePair.Key;
				ExchangePrincipal value = keyValuePair.Value;
				this.Target = new TeamMailboxDiagnosticsBase.TargetType?(this.Target ?? TeamMailboxDiagnosticsBase.TargetType.All);
				if (this.Target == TeamMailboxDiagnosticsBase.TargetType.Document || this.Target == TeamMailboxDiagnosticsBase.TargetType.All)
				{
					EnqueueResult enqueueResult = RpcClientWrapper.EnqueueTeamMailboxSyncRequest((!string.IsNullOrEmpty(this.Server)) ? this.Server : value.MailboxInfo.Location.ServerFqdn, value.MailboxInfo.MailboxGuid, QueueType.TeamMailboxDocumentSync, key.OrganizationId, "UpdateTMCMD_" + base.ExecutingUserIdentityName, base.DomainController, this.FullSync ? SyncOption.FullSync : SyncOption.Default);
					enqueueResult.Type = QueueType.TeamMailboxDocumentSync;
					base.WriteObject(enqueueResult);
				}
				if (this.Target == TeamMailboxDiagnosticsBase.TargetType.Membership || this.Target == TeamMailboxDiagnosticsBase.TargetType.All)
				{
					EnqueueResult enqueueResult2 = RpcClientWrapper.EnqueueTeamMailboxSyncRequest((!string.IsNullOrEmpty(this.Server)) ? this.Server : value.MailboxInfo.Location.ServerFqdn, value.MailboxInfo.MailboxGuid, QueueType.TeamMailboxMembershipSync, key.OrganizationId, "UpdateTMCMD_" + base.ExecutingUserIdentityName, base.DomainController, this.FullSync ? SyncOption.FullSync : SyncOption.Default);
					enqueueResult2.Type = QueueType.TeamMailboxMembershipSync;
					base.WriteObject(enqueueResult2);
				}
				if (this.Target == TeamMailboxDiagnosticsBase.TargetType.Maintenance || this.Target == TeamMailboxDiagnosticsBase.TargetType.All)
				{
					EnqueueResult enqueueResult3 = RpcClientWrapper.EnqueueTeamMailboxSyncRequest((!string.IsNullOrEmpty(this.Server)) ? this.Server : value.MailboxInfo.Location.ServerFqdn, value.MailboxInfo.MailboxGuid, QueueType.TeamMailboxMaintenanceSync, key.OrganizationId, "UpdateTMCMD_" + base.ExecutingUserIdentityName, base.DomainController, this.FullSync ? SyncOption.FullSync : SyncOption.Default);
					enqueueResult3.Type = QueueType.TeamMailboxMaintenanceSync;
					base.WriteObject(enqueueResult3);
				}
			}
		}
	}
}

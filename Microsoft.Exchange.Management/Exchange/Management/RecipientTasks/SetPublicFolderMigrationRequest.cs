using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB3 RID: 3251
	[Cmdlet("Set", "PublicFolderMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPublicFolderMigrationRequest : SetRequest<PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x06007CA0 RID: 31904 RVA: 0x001FE486 File Offset: 0x001FC686
		// (set) Token: 0x06007CA1 RID: 31905 RVA: 0x001FE4A7 File Offset: 0x001FC6A7
		[Parameter(Mandatory = false)]
		public bool PreventCompletion
		{
			get
			{
				return (bool)(base.Fields["PreventCompletion"] ?? true);
			}
			set
			{
				base.Fields["PreventCompletion"] = value;
			}
		}

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x001FE4BF File Offset: 0x001FC6BF
		// (set) Token: 0x06007CA3 RID: 31907 RVA: 0x001FE4D6 File Offset: 0x001FC6D6
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string RemoteMailboxLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxLegacyDN"] = value;
			}
		}

		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x06007CA4 RID: 31908 RVA: 0x001FE4E9 File Offset: 0x001FC6E9
		// (set) Token: 0x06007CA5 RID: 31909 RVA: 0x001FE500 File Offset: 0x001FC700
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string RemoteMailboxServerLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxServerLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxServerLegacyDN"] = value;
			}
		}

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x06007CA6 RID: 31910 RVA: 0x001FE513 File Offset: 0x001FC713
		// (set) Token: 0x06007CA7 RID: 31911 RVA: 0x001FE52A File Offset: 0x001FC72A
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Fqdn OutlookAnywhereHostName
		{
			get
			{
				return (Fqdn)base.Fields["OutlookAnywhereHostName"];
			}
			set
			{
				base.Fields["OutlookAnywhereHostName"] = value;
			}
		}

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x06007CA8 RID: 31912 RVA: 0x001FE53D File Offset: 0x001FC73D
		// (set) Token: 0x06007CA9 RID: 31913 RVA: 0x001FE55E File Offset: 0x001FC75E
		[Parameter(Mandatory = false)]
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)(base.Fields["AuthenticationMethod"] ?? AuthenticationMethod.Basic);
			}
			set
			{
				base.Fields["AuthenticationMethod"] = value;
			}
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x06007CAA RID: 31914 RVA: 0x001FE576 File Offset: 0x001FC776
		// (set) Token: 0x06007CAB RID: 31915 RVA: 0x001FE57E File Offset: 0x001FC77E
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x001FE588 File Offset: 0x001FC788
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			base.ModifyRequest(requestJob);
			if (base.IsFieldSet("RemoteMailboxLegacyDN"))
			{
				requestJob.RemoteMailboxLegacyDN = this.RemoteMailboxLegacyDN;
			}
			if (base.IsFieldSet("RemoteMailboxServerLegacyDN"))
			{
				requestJob.RemoteMailboxServerLegacyDN = this.RemoteMailboxServerLegacyDN;
			}
			if (base.IsFieldSet("OutlookAnywhereHostName"))
			{
				requestJob.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
			}
			if (base.IsFieldSet("AuthenticationMethod"))
			{
				requestJob.AuthenticationMethod = new AuthenticationMethod?(this.AuthenticationMethod);
			}
			if (base.IsFieldSet("RemoteCredential"))
			{
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, requestJob.AuthenticationMethod);
			}
			if (base.IsFieldSet("PreventCompletion"))
			{
				if (!this.PreventCompletion)
				{
					if (requestJob.Status != RequestStatus.AutoSuspended)
					{
						base.WriteError(new PreventCompletionCannotSetException(), ExchangeErrorCategory.Client, this.Identity);
					}
					requestJob.Priority = RequestPriority.High;
				}
				else if (!requestJob.PreventCompletion)
				{
					base.WriteError(new InvalidValueForPreventCompletionException(), ExchangeErrorCategory.Client, this.Identity);
				}
				requestJob.PreventCompletion = this.PreventCompletion;
				requestJob.SuspendWhenReadyToComplete = this.PreventCompletion;
				requestJob.AllowedToFinishMove = !this.PreventCompletion;
			}
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x001FE6B1 File Offset: 0x001FC8B1
		protected override void CheckIndexEntry()
		{
		}

		// Token: 0x04003D8C RID: 15756
		private const string ParameterPreventCompletion = "PreventCompletion";
	}
}

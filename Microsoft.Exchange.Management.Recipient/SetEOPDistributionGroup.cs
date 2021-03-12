using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003D RID: 61
	[Cmdlet("Set", "EOPDistributionGroup", DefaultParameterSetName = "Identity")]
	public sealed class SetEOPDistributionGroup : EOPTask
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		[Parameter(Mandatory = false)]
		public DistributionGroupIdParameter Identity { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000D7E9 File Offset: 0x0000B9E9
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000D7F1 File Offset: 0x0000B9F1
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000D7FA File Offset: 0x0000B9FA
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000D802 File Offset: 0x0000BA02
		[Parameter(Mandatory = false)]
		public string Alias { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000D80B File Offset: 0x0000BA0B
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000D813 File Offset: 0x0000BA13
		[Parameter(Mandatory = false)]
		public string DisplayName { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000D81C File Offset: 0x0000BA1C
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000D824 File Offset: 0x0000BA24
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000D82D File Offset: 0x0000BA2D
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000D835 File Offset: 0x0000BA35
		[Parameter(Mandatory = false)]
		public string[] ManagedBy { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D840 File Offset: 0x0000BA40
		protected override void InternalProcessRecord()
		{
			try
			{
				SetDistributionGroupCmdlet setDistributionGroupCmdlet = new SetDistributionGroupCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				setDistributionGroupCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				setDistributionGroupCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.DisplayName, this.DisplayName);
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.Alias, this.Alias);
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.PrimarySmtpAddress, this.PrimarySmtpAddress);
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.ManagedBy, this.ManagedBy);
				EOPRecipient.SetProperty(setDistributionGroupCmdlet, Parameters.Organization, base.Organization);
				setDistributionGroupCmdlet.Run();
				EOPRecipient.CheckForError(this, setDistributionGroupCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}

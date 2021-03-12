using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000038 RID: 56
	[Cmdlet("New", "EOPDistributionGroup", DefaultParameterSetName = "Identity")]
	public sealed class NewEOPDistributionGroup : EOPTask
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000D131 File Offset: 0x0000B331
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000D139 File Offset: 0x0000B339
		[Parameter(Mandatory = false)]
		public string Alias { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000D142 File Offset: 0x0000B342
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000D14A File Offset: 0x0000B34A
		[Parameter(Mandatory = false)]
		public string DisplayName { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000D153 File Offset: 0x0000B353
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000D15B File Offset: 0x0000B35B
		[Parameter(Mandatory = false)]
		public string[] ManagedBy { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000D164 File Offset: 0x0000B364
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000D16C File Offset: 0x0000B36C
		[Parameter(Mandatory = false)]
		public string[] Members { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000D175 File Offset: 0x0000B375
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000D17D File Offset: 0x0000B37D
		[Parameter(Mandatory = true)]
		public string Name { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000D186 File Offset: 0x0000B386
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000D18E File Offset: 0x0000B38E
		[Parameter(Mandatory = false)]
		public string Notes { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000D197 File Offset: 0x0000B397
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000D19F File Offset: 0x0000B39F
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		[Parameter(Mandatory = false)]
		public GroupType Type { get; set; }

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		protected override void InternalProcessRecord()
		{
			try
			{
				NewDistributionGroupCmdlet newDistributionGroupCmdlet = new NewDistributionGroupCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				newDistributionGroupCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				newDistributionGroupCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Name, this.Name);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.DisplayName, this.DisplayName);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Alias, this.Alias);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.PrimarySmtpAddress, this.PrimarySmtpAddress);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Notes, this.Notes);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.ManagedByForInput, this.ManagedBy);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Members, this.Members);
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Type, this.Type.ToString());
				EOPRecipient.SetProperty(newDistributionGroupCmdlet, Parameters.Organization, base.Organization);
				newDistributionGroupCmdlet.Run();
				EOPRecipient.CheckForError(this, newDistributionGroupCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}

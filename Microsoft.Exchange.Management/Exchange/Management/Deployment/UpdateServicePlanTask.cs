using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000284 RID: 644
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Update", "ServicePlan", SupportsShouldProcess = true, DefaultParameterSetName = "IdentityParameterSet")]
	public sealed class UpdateServicePlanTask : ManageServicePlanMigrationBase
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00063D6C File Offset: 0x00061F6C
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UpdateServicePlanDescription;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00063D73 File Offset: 0x00061F73
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateServicePlan(this.Identity.ToString());
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00063D85 File Offset: 0x00061F85
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x00063D8D File Offset: 0x00061F8D
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00063D96 File Offset: 0x00061F96
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x00063DAD File Offset: 0x00061FAD
		[Parameter(Mandatory = true, ParameterSetName = "MigrateServicePlanParameterSet")]
		public string ProgramId
		{
			get
			{
				return (string)base.Fields["TenantProgramId"];
			}
			set
			{
				base.Fields["TenantProgramId"] = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00063DC0 File Offset: 0x00061FC0
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x00063DD7 File Offset: 0x00061FD7
		[Parameter(Mandatory = true, ParameterSetName = "MigrateServicePlanParameterSet")]
		public string OfferId
		{
			get
			{
				return (string)base.Fields["TenantOfferId"];
			}
			set
			{
				base.Fields["TenantOfferId"] = value;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00063DEA File Offset: 0x00061FEA
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x00063DF7 File Offset: 0x00061FF7
		[Parameter(Mandatory = false)]
		public SwitchParameter ConfigOnly
		{
			get
			{
				return this.configOnly;
			}
			set
			{
				this.configOnly = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00063E05 File Offset: 0x00062005
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x00063E12 File Offset: 0x00062012
		[Parameter(Mandatory = false)]
		public SwitchParameter Conservative
		{
			get
			{
				return this.conservative;
			}
			set
			{
				this.conservative = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00063E20 File Offset: 0x00062020
		// (set) Token: 0x060017A0 RID: 6048 RVA: 0x00063E2D File Offset: 0x0006202D
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeUserUpdatePhase
		{
			get
			{
				return this.includeUserUpdatePhase;
			}
			set
			{
				this.includeUserUpdatePhase = value;
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00063E3B File Offset: 0x0006203B
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			base.LoadTenantCU();
			TaskLogger.LogExit();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00063E54 File Offset: 0x00062054
		protected override void ResolveTargetOffer()
		{
			if (!string.IsNullOrEmpty(this.OfferId) && !string.IsNullOrEmpty(this.ProgramId))
			{
				this.targetProgramId = this.ProgramId;
				this.targetOfferId = this.OfferId;
				return;
			}
			this.targetProgramId = this.tenantCU.ProgramId;
			this.targetOfferId = this.tenantCU.OfferId;
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000260 RID: 608
	[Cmdlet("Start", "OrganizationPilot", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartOrganizationPilotTask : StartOrganizationUpgradeBase
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00060F8F File Offset: 0x0005F18F
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartOrganizationPilotDescription;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00060F96 File Offset: 0x0005F196
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartOrganizationPilot(base.Identity.ToString());
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00060FA8 File Offset: 0x0005F1A8
		protected override string TargetOfferId
		{
			get
			{
				if (this.pilotOfferId == null)
				{
					if (ServicePlanConfiguration.GetInstance().IsPilotOffer(this.tenantCU.ProgramId, this.tenantCU.OfferId))
					{
						this.pilotOfferId = this.tenantCU.OfferId;
					}
					else if (!ServicePlanConfiguration.GetInstance().TryGetPilotOfferId(this.tenantCU.ProgramId, this.tenantCU.OfferId, out this.pilotOfferId))
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorServicePlanDoesNotSupportPilot(this.tenantCU.Name, this.tenantCU.ServicePlan, this.tenantCU.ProgramId)), (ErrorCategory)1000, null);
					}
				}
				return this.pilotOfferId;
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0006105F File Offset: 0x0005F25F
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new StartOrganizationPilotTaskModuleFactory();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00061068 File Offset: 0x0005F268
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.tenantCU.IsUpgradingOrganization)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotStartPilotFOrOrgBeingUpgraded(this.tenantCU.OrganizationId.OrganizationalUnit.Name)), (ErrorCategory)1002, null);
			}
			string b;
			if (!this.tenantCU.IsPilotingOrganization && (!ServicePlanConfiguration.GetInstance().TryGetAllowedSorceServicePlanForPilot(this.tenantCU.ProgramId, this.TargetOfferId, out b) || this.tenantCU.ServicePlan != b))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServicePlanDoesNotSupportPilot(this.tenantCU.OrganizationId.OrganizationalUnit.Name, this.tenantCU.ServicePlan, this.tenantCU.ProgramId)), (ErrorCategory)1000, null);
			}
			base.InternalPilotEnabled = true;
			TaskLogger.LogExit();
		}

		// Token: 0x040009E0 RID: 2528
		private string pilotOfferId;
	}
}

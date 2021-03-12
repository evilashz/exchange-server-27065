using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D31 RID: 3377
	[Cmdlet("New", "UMHuntGroup", SupportsShouldProcess = true)]
	public sealed class NewUMHuntGroup : NewMultitenancySystemConfigurationObjectTask<UMHuntGroup>
	{
		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x06008164 RID: 33124 RVA: 0x002111CC File Offset: 0x0020F3CC
		// (set) Token: 0x06008165 RID: 33125 RVA: 0x002111D9 File Offset: 0x0020F3D9
		[Parameter(Mandatory = false)]
		public string PilotIdentifier
		{
			get
			{
				return this.DataObject.PilotIdentifier;
			}
			set
			{
				this.DataObject.PilotIdentifier = value;
			}
		}

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x06008166 RID: 33126 RVA: 0x002111E7 File Offset: 0x0020F3E7
		// (set) Token: 0x06008167 RID: 33127 RVA: 0x002111FE File Offset: 0x0020F3FE
		[Parameter(Mandatory = true)]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x06008168 RID: 33128 RVA: 0x00211211 File Offset: 0x0020F411
		// (set) Token: 0x06008169 RID: 33129 RVA: 0x00211228 File Offset: 0x0020F428
		[Parameter(Mandatory = true)]
		public UMIPGatewayIdParameter UMIPGateway
		{
			get
			{
				return (UMIPGatewayIdParameter)base.Fields["IPGateway"];
			}
			set
			{
				base.Fields["IPGateway"] = value;
			}
		}

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x0600816A RID: 33130 RVA: 0x0021123B File Offset: 0x0020F43B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUMHuntGroup(base.Name.ToString(), this.PilotIdentifier.ToString(), this.UMDialPlan.ToString(), this.UMIPGateway.ToString());
			}
		}

		// Token: 0x0600816B RID: 33131 RVA: 0x00211270 File Offset: 0x0020F470
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			UMHuntGroup umhuntGroup = (UMHuntGroup)base.PrepareDataObject();
			IConfigurationSession session = (IConfigurationSession)base.DataSession;
			UMDialPlanIdParameter umdialPlan = this.UMDialPlan;
			UMDialPlan umdialPlan2 = (UMDialPlan)base.GetDataObject<UMDialPlan>(umdialPlan, session, this.RootId, new LocalizedString?(Strings.NonExistantDialPlan(umdialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(umdialPlan.ToString())));
			umhuntGroup.UMDialPlan = umdialPlan2.Id;
			if (umdialPlan2.URIType == UMUriType.SipName && !VariantConfiguration.InvariantNoFlightingSnapshot.UM.HuntGroupCreationForSipDialplans.Enabled)
			{
				base.WriteError(new CannotCreateHuntGroupForHostedSipDialPlanException(), ErrorCategory.InvalidOperation, umhuntGroup);
			}
			UMIPGatewayIdParameter umipgateway = this.UMIPGateway;
			UMIPGateway umipgateway2 = (UMIPGateway)base.GetDataObject<UMIPGateway>(umipgateway, session, this.RootId, new LocalizedString?(Strings.NonExistantIPGateway(umipgateway.ToString())), new LocalizedString?(Strings.MultipleIPGatewaysWithSameId(umipgateway.ToString())));
			bool flag = false;
			foreach (UMHuntGroup umhuntGroup2 in umipgateway2.HuntGroups)
			{
				if (umhuntGroup2.PilotIdentifier == umhuntGroup.PilotIdentifier)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				HuntGroupAlreadyExistsException exception = new HuntGroupAlreadyExistsException(umipgateway2.Name, umhuntGroup.PilotIdentifier);
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			else
			{
				umhuntGroup.SetId(umipgateway2.Id.GetChildId(base.Name));
			}
			TaskLogger.LogExit();
			return umhuntGroup;
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x002113F4 File Offset: 0x0020F5F4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_NewHuntGroupCreated, null, new object[]
				{
					this.DataObject.DistinguishedName,
					this.DataObject.PilotIdentifier,
					this.DataObject.UMDialPlan.Name
				});
			}
			TaskLogger.LogExit();
		}
	}
}

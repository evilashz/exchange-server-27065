using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200045A RID: 1114
	[Cmdlet("New", "UMMailboxPolicy", SupportsShouldProcess = true)]
	public sealed class NewUMPolicy : NewMailboxPolicyBase<UMMailboxPolicy>
	{
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x0009BAB8 File Offset: 0x00099CB8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUMMailboxPolicy(base.Name.ToString(), this.UMDialPlan.ToString());
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0009BAD5 File Offset: 0x00099CD5
		// (set) Token: 0x0600276B RID: 10091 RVA: 0x0009BAEC File Offset: 0x00099CEC
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

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0009BAFF File Offset: 0x00099CFF
		// (set) Token: 0x0600276D RID: 10093 RVA: 0x0009BB25 File Offset: 0x00099D25
		[Parameter(Mandatory = false)]
		public SwitchParameter SharedUMDialPlan
		{
			get
			{
				return (SwitchParameter)(base.Fields["SharedUMDialPlan"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SharedUMDialPlan"] = value;
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0009BB40 File Offset: 0x00099D40
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			UMMailboxPolicy ummailboxPolicy = (UMMailboxPolicy)base.PrepareDataObject();
			if (!base.HasErrors)
			{
				UMDialPlanIdParameter umdialPlan = this.UMDialPlan;
				IConfigurationSession dialPlanSession = this.GetDialPlanSession();
				UMDialPlan umdialPlan2 = (UMDialPlan)base.GetDataObject<UMDialPlan>(umdialPlan, dialPlanSession, null, new LocalizedString?(Strings.NonExistantDialPlan(umdialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(umdialPlan.ToString())));
				ummailboxPolicy.UMDialPlan = umdialPlan2.Id;
				ummailboxPolicy.SourceForestPolicyNames.Add(ummailboxPolicy.Name);
			}
			return ummailboxPolicy;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0009BBC4 File Offset: 0x00099DC4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.CreateParentContainerIfNeeded(this.DataObject);
			UMDialPlanIdParameter umdialPlan = this.UMDialPlan;
			IConfigurationSession dialPlanSession = this.GetDialPlanSession();
			UMDialPlan umdialPlan2 = (UMDialPlan)base.GetDataObject<UMDialPlan>(umdialPlan, dialPlanSession, null, new LocalizedString?(Strings.NonExistantDialPlan(umdialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(umdialPlan.ToString())));
			if (umdialPlan2.SubscriberType == UMSubscriberType.Consumer)
			{
				this.DataObject.AllowDialPlanSubscribers = false;
				this.DataObject.AllowExtensions = false;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0009BC4C File Offset: 0x00099E4C
		private IConfigurationSession GetDialPlanSession()
		{
			IConfigurationSession result = (IConfigurationSession)base.DataSession;
			if (this.SharedUMDialPlan)
			{
				ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				result = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 167, "GetDialPlanSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\UMMailboxPolicyTask.cs");
			}
			return result;
		}

		// Token: 0x04001D9F RID: 7583
		private const string ParameterSharedUMDialPlan = "SharedUMDialPlan";
	}
}

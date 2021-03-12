using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AF7 RID: 2807
	[Cmdlet("Get", "OutlookProtectionRule", DefaultParameterSetName = "Identity")]
	public sealed class GetOutlookProtectionRule : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x060063D2 RID: 25554 RVA: 0x001A15D2 File Offset: 0x0019F7D2
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return null;
				}
				return RuleIdParameter.GetRuleCollectionId(base.DataSession, "OutlookProtectionRules");
			}
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x001A15F0 File Offset: 0x0019F7F0
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn("OutlookProtectionRules");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfOutlookProtectionRuleContainer(this.Identity))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x001A1680 File Offset: 0x0019F880
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)dataObject;
			base.WriteResult(new OutlookProtectionRulePresentationObject(transportRule)
			{
				Priority = this.priorityHelper.GetPriorityFromSequenceNumber(transportRule.Priority)
			});
			TaskLogger.LogExit();
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x001A16C4 File Offset: 0x0019F8C4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = base.CreateSession();
			this.priorityHelper = new PriorityHelper(configDataProvider);
			return configDataProvider;
		}

		// Token: 0x040035FB RID: 13819
		private PriorityHelper priorityHelper;
	}
}

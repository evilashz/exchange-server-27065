using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000101 RID: 257
	[Cmdlet("Set", "ComplianceRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public abstract class SetComplianceRuleBase : SetSystemConfigurationObjectTask<ComplianceRuleIdParameter, PsComplianceRuleBase, RuleStorage>
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002AA88 File Offset: 0x00028C88
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0002AA90 File Offset: 0x00028C90
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002AA99 File Offset: 0x00028C99
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0002AAA1 File Offset: 0x00028CA1
		protected PsComplianceRuleBase PsRulePresentationObject { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002AAAA File Offset: 0x00028CAA
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002AAC1 File Offset: 0x00028CC1
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)base.Fields["Comment"];
			}
			set
			{
				base.Fields["Comment"] = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002AAD4 File Offset: 0x00028CD4
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x0002AAEB File Offset: 0x00028CEB
		[Parameter(Mandatory = false)]
		public bool Disabled
		{
			get
			{
				return (bool)base.Fields["Disabled"];
			}
			set
			{
				base.Fields["Disabled"] = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002AB03 File Offset: 0x00028D03
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0002AB1A File Offset: 0x00028D1A
		[Parameter(Mandatory = false)]
		public string ContentMatchQuery
		{
			get
			{
				return (string)base.Fields["ContentMatchQuery"];
			}
			set
			{
				base.Fields["ContentMatchQuery"] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002AB2D File Offset: 0x00028D2D
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002AB3A File Offset: 0x00028D3A
		public SetComplianceRuleBase()
		{
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002AB4D File Offset: 0x00028D4D
		protected SetComplianceRuleBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002AB68 File Offset: 0x00028D68
		protected virtual void CopyExplicitParameters()
		{
			if (base.Fields.IsModified("Comment"))
			{
				this.PsRulePresentationObject.Comment = this.Comment;
			}
			if (base.Fields.IsModified("Disabled"))
			{
				this.PsRulePresentationObject.Disabled = this.Disabled;
			}
			if (base.Fields.IsModified("ContentMatchQuery"))
			{
				this.PsRulePresentationObject.ContentMatchQuery = this.ContentMatchQuery;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002AC00 File Offset: 0x00028E00
		protected override IConfigurable ResolveDataObject()
		{
			RuleStorage ruleStorage = base.GetDataObjects<RuleStorage>(this.Identity, base.DataSession, null).FirstOrDefault((RuleStorage r) => r.Scenario == this.Scenario);
			if (ruleStorage != null)
			{
				return ruleStorage;
			}
			IEnumerable<RuleStorage> dataObjects = base.GetDataObjects<RuleStorage>(this.Identity, base.DataSession, null);
			foreach (RuleStorage ruleStorage2 in dataObjects)
			{
				IList<PolicyStorage> source = base.GetDataObjects<PolicyStorage>(new PolicyIdParameter(ruleStorage2.ParentPolicyId), base.DataSession, null).ToList<PolicyStorage>();
				if (source.Any((PolicyStorage p) => p.Scenario == this.Scenario))
				{
					return ruleStorage2;
				}
			}
			base.WriteError(new ErrorRuleNotFoundException(this.Identity.ToString()), ErrorCategory.InvalidOperation, null);
			return null;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002ACE4 File Offset: 0x00028EE4
		protected override void InternalValidate()
		{
			Utils.ThrowIfNotRunInEOP();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			base.InternalValidate();
			if (this.DataObject.IsModified(ADObjectSchema.Name) && this.DoesComplianceRuleExist())
			{
				throw new ComplianceRuleAlreadyExistsException((string)this.DataObject[ADObjectSchema.Name]);
			}
			if (base.Fields.IsModified("Disabled") && this.Disabled)
			{
				PolicyStorage policyStorage = (PolicyStorage)base.GetDataObject<PolicyStorage>(new PolicyIdParameter(this.DataObject.ParentPolicyId), base.DataSession, null, new LocalizedString?(Strings.ErrorPolicyNotFound(this.DataObject.ParentPolicyId.ToString())), new LocalizedString?(Strings.ErrorPolicyNotUnique(this.DataObject.ParentPolicyId.ToString())), ExchangeErrorCategory.Client);
				if (policyStorage.IsEnabled)
				{
					this.WriteWarning(Strings.WarningDisabledRuleInEnabledPolicy(this.DataObject.Name));
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002AE08 File Offset: 0x00029008
		private bool DoesComplianceRuleExist()
		{
			bool flag = false;
			flag = (from p in base.GetDataObjects<RuleStorage>(new ComplianceRuleIdParameter((string)this.DataObject[ADObjectSchema.Name]), base.DataSession, null)
			where p.Scenario == this.Scenario
			select p).Any<RuleStorage>();
			if (!flag)
			{
				IEnumerable<RuleStorage> dataObjects = base.GetDataObjects<RuleStorage>(new ComplianceRuleIdParameter((string)this.DataObject[ADObjectSchema.Name]), base.DataSession, null);
				foreach (RuleStorage ruleStorage in dataObjects)
				{
					IList<PolicyStorage> source = base.GetDataObjects<PolicyStorage>(new PolicyIdParameter(ruleStorage.ParentPolicyId), base.DataSession, null).ToList<PolicyStorage>();
					if (source.Any((PolicyStorage p) => p.Scenario == this.Scenario))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002AF00 File Offset: 0x00029100
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges());
			TaskLogger.LogExit();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002AF29 File Offset: 0x00029129
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return AggregatedNotificationClients.NotifyChanges(this, base.DataSession as IConfigurationSession, this.DataObject, this.executionLogger, base.GetType(), null, null);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002AF50 File Offset: 0x00029150
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002AF6C File Offset: 0x0002916C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002AFAC File Offset: 0x000291AC
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002AFC9 File Offset: 0x000291C9
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002AFD7 File Offset: 0x000291D7
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002AFE8 File Offset: 0x000291E8
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x04000433 RID: 1075
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}

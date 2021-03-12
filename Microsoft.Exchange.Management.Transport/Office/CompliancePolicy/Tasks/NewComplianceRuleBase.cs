using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000FE RID: 254
	[Cmdlet("New", "ComplianceRule", SupportsShouldProcess = true)]
	public abstract class NewComplianceRuleBase : NewMultitenancyFixedNameSystemConfigurationObjectTask<RuleStorage>
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000297FC File Offset: 0x000279FC
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x00029804 File Offset: 0x00027A04
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002980D File Offset: 0x00027A0D
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00029824 File Offset: 0x00027A24
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields[ADObjectSchema.Name];
			}
			set
			{
				base.Fields[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00029837 File Offset: 0x00027A37
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0002984E File Offset: 0x00027A4E
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)base.Fields[PsComplianceRuleBaseSchema.Comment];
			}
			set
			{
				base.Fields[PsComplianceRuleBaseSchema.Comment] = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00029861 File Offset: 0x00027A61
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0002988C File Offset: 0x00027A8C
		[Parameter(Mandatory = false)]
		public bool Disabled
		{
			get
			{
				return base.Fields["Disabled"] != null && (bool)base.Fields["Disabled"];
			}
			set
			{
				base.Fields["Disabled"] = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000298A4 File Offset: 0x00027AA4
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x000298BB File Offset: 0x00027ABB
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public PolicyIdParameter Policy
		{
			get
			{
				return (PolicyIdParameter)base.Fields["Policy"];
			}
			set
			{
				base.Fields["Policy"] = value;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000298CE File Offset: 0x00027ACE
		public NewComplianceRuleBase()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000298E1 File Offset: 0x00027AE1
		protected NewComplianceRuleBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002991C File Offset: 0x00027B1C
		protected override void InternalValidate()
		{
			Utils.ThrowIfNotRunInEOP();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			IEnumerable<PolicyStorage> enumerable = base.GetDataObjects<PolicyStorage>(this.Policy, base.DataSession, null).ToList<PolicyStorage>();
			if (enumerable == null || !enumerable.Any((PolicyStorage s) => s.Scenario == this.Scenario))
			{
				base.WriteError(new ErrorPolicyNotFoundException(this.Policy.ToString()), ErrorCategory.InvalidOperation, null);
			}
			enumerable = from s in enumerable
			where s.Scenario == this.Scenario
			select s;
			if (enumerable.Count<PolicyStorage>() > 1)
			{
				base.WriteError(new ErrorPolicyNotUniqueException(this.Policy.ToString()), ErrorCategory.InvalidOperation, null);
			}
			this.policyStorage = enumerable.First<PolicyStorage>();
			base.WriteVerbose(Strings.VerbosePolicyStorageObjectLoadedForCommonRule(this.policyStorage.ToString(), this.Policy.ToString()));
			if (this.policyStorage.Mode == Mode.PendingDeletion)
			{
				base.WriteError(new ErrorCannotCreateRuleUnderPendingDeletionPolicyException(this.policyStorage.Name), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			if (this.DoesComplianceRuleExist())
			{
				throw new ComplianceRuleAlreadyExistsException(this.Name);
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00029A22 File Offset: 0x00027C22
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges());
			TaskLogger.LogExit();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00029A4B File Offset: 0x00027C4B
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return AggregatedNotificationClients.NotifyChanges(this, base.DataSession as IConfigurationSession, this.DataObject, this.executionLogger, base.GetType(), null, null);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00029A72 File Offset: 0x00027C72
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00029AA8 File Offset: 0x00027CA8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00029AE8 File Offset: 0x00027CE8
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.MasterIdentity = Guid.NewGuid();
			ruleStorage.Scenario = this.Scenario;
			return ruleStorage;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00029B3C File Offset: 0x00027D3C
		private bool DoesComplianceRuleExist()
		{
			bool flag = false;
			flag = (from p in base.GetDataObjects<RuleStorage>(new ComplianceRuleIdParameter(this.Name), base.DataSession, null)
			where p.Scenario == this.Scenario
			select p).Any<RuleStorage>();
			if (!flag)
			{
				IEnumerable<RuleStorage> dataObjects = base.GetDataObjects<RuleStorage>(new ComplianceRuleIdParameter(this.Name), base.DataSession, null);
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

		// Token: 0x06000A60 RID: 2656 RVA: 0x00029C14 File Offset: 0x00027E14
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00029C22 File Offset: 0x00027E22
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00029C34 File Offset: 0x00027E34
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x0400042F RID: 1071
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();

		// Token: 0x04000430 RID: 1072
		protected PolicyStorage policyStorage;
	}
}

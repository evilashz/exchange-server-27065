using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F3 RID: 243
	[Cmdlet("Remove", "CompliancePolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public abstract class RemoveCompliancePolicyBase : RemoveSystemConfigurationObjectTask<PolicyIdParameter, PolicyStorage>
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00028EBC File Offset: 0x000270BC
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00028EC4 File Offset: 0x000270C4
		protected IList<RuleStorage> RuleStorages { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00028ECD File Offset: 0x000270CD
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00028ED5 File Offset: 0x000270D5
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00028EDE File Offset: 0x000270DE
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00028F04 File Offset: 0x00027104
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceDeletion
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceDeletion"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceDeletion"] = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00028F1C File Offset: 0x0002711C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveCompliancePolicyConfirmation(this.Identity.ToString());
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00028F2E File Offset: 0x0002712E
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00028F3B File Offset: 0x0002713B
		public RemoveCompliancePolicyBase()
		{
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00028F4E File Offset: 0x0002714E
		protected RemoveCompliancePolicyBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00028F68 File Offset: 0x00027168
		protected override IConfigurable ResolveDataObject()
		{
			IEnumerable<PolicyStorage> dataObjects = base.GetDataObjects<PolicyStorage>(this.Identity, base.DataSession, null);
			foreach (PolicyStorage policyStorage in dataObjects)
			{
				if (policyStorage.Scenario == this.Scenario)
				{
					return policyStorage;
				}
			}
			base.WriteError(new ErrorPolicyNotFoundException(this.Identity.ToString()), ErrorCategory.InvalidOperation, null);
			return null;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00028FEC File Offset: 0x000271EC
		protected override void InternalValidate()
		{
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			base.InternalValidate();
			if (base.DataObject.Mode == Mode.PendingDeletion && this.ShouldSoftDeleteObject())
			{
				base.WriteError(new ErrorCannotRemovePendingDeletionPolicyException(base.DataObject.Name), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00029038 File Offset: 0x00027238
		protected override bool ShouldSoftDeleteObject()
		{
			return !this.ForceDeletion.ToBool();
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00029058 File Offset: 0x00027258
		protected override void SaveSoftDeletedObject()
		{
			base.DataObject.Mode = Mode.PendingDeletion;
			base.DataSession.Save(base.DataObject);
			if (this.RuleStorages != null)
			{
				foreach (RuleStorage ruleStorage in this.RuleStorages)
				{
					if (ruleStorage.Mode != Mode.PendingDeletion)
					{
						ruleStorage.Mode = Mode.PendingDeletion;
						base.DataSession.Save(ruleStorage);
					}
				}
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000290E0 File Offset: 0x000272E0
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000290E3 File Offset: 0x000272E3
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00029100 File Offset: 0x00027300
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.RuleStorages = Utils.LoadRuleStoragesByPolicy(base.DataSession, base.DataObject, this.RootId);
			IList<BindingStorage> list = Utils.LoadBindingStoragesByPolicy(base.DataSession, base.DataObject);
			foreach (RuleStorage ruleStorage in this.RuleStorages)
			{
				base.WriteVerbose(Strings.VerboseLoadRuleStorageObjectsForPolicy(ruleStorage.ToString(), base.DataObject.ToString()));
			}
			if (!this.ShouldSoftDeleteObject())
			{
				Utils.RemovePolicyStorageBase(base.DataSession, new WriteVerboseDelegate(base.WriteVerbose), this.RuleStorages);
				Utils.RemovePolicyStorageBase(base.DataSession, new WriteVerboseDelegate(base.WriteVerbose), list);
			}
			else
			{
				list = null;
			}
			base.InternalProcessRecord();
			if (this.ShouldSoftDeleteObject())
			{
				PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges(list, this.RuleStorages));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00029208 File Offset: 0x00027408
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges(IEnumerable<UnifiedPolicyStorageBase> bindingStorageObjects, IEnumerable<UnifiedPolicyStorageBase> ruleStorageObjects)
		{
			return AggregatedNotificationClients.NotifyChanges(this, (IConfigurationSession)base.DataSession, base.DataObject, this.executionLogger, base.GetType(), bindingStorageObjects, ruleStorageObjects);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00029248 File Offset: 0x00027448
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00029288 File Offset: 0x00027488
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00029296 File Offset: 0x00027496
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000292A8 File Offset: 0x000274A8
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x04000427 RID: 1063
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}

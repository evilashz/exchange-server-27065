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
	// Token: 0x02000104 RID: 260
	[Cmdlet("Remove", "ComplianceRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public abstract class RemoveComplianceRuleBase : RemoveSystemConfigurationObjectTask<PolicyIdParameter, RuleStorage>
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002BDF4 File Offset: 0x00029FF4
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0002BDFC File Offset: 0x00029FFC
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002BE05 File Offset: 0x0002A005
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0002BE2B File Offset: 0x0002A02B
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002BE43 File Offset: 0x0002A043
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002BE50 File Offset: 0x0002A050
		public RemoveComplianceRuleBase()
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002BE63 File Offset: 0x0002A063
		protected RemoveComplianceRuleBase(PolicyScenario scenario)
		{
			this.Scenario = scenario;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002BE7D File Offset: 0x0002A07D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveComplianceRuleConfirmation(this.Identity.ToString());
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		protected override IConfigurable ResolveDataObject()
		{
			RuleStorage ruleStorage = base.GetDataObjects<RuleStorage>(this.Identity, base.DataSession, null).FirstOrDefault((RuleStorage p) => p.Scenario == this.Scenario);
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

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002BF94 File Offset: 0x0002A194
		protected override void InternalValidate()
		{
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			base.InternalValidate();
			if (base.DataObject.Mode == Mode.PendingDeletion && this.ShouldSoftDeleteObject())
			{
				base.WriteError(new ErrorCannotRemovePendingDeletionRuleException(base.DataObject.Name), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		protected override bool ShouldSoftDeleteObject()
		{
			return !this.ForceDeletion.ToBool();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002C002 File Offset: 0x0002A202
		protected override void SaveSoftDeletedObject()
		{
			base.DataObject.Mode = Mode.PendingDeletion;
			base.DataSession.Save(base.DataObject);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002C021 File Offset: 0x0002A221
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002C054 File Offset: 0x0002A254
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002C094 File Offset: 0x0002A294
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.ShouldSoftDeleteObject())
			{
				PolicySettingStatusHelpers.CheckNotificationResultsAndUpdateStatus(this, (IConfigurationSession)base.DataSession, this.OnNotifyChanges());
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002C0C5 File Offset: 0x0002A2C5
		protected virtual IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return AggregatedNotificationClients.NotifyChanges(this, base.DataSession as IConfigurationSession, base.DataObject, this.executionLogger, base.GetType(), null, null);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002C0EC File Offset: 0x0002A2EC
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002C0FA File Offset: 0x0002A2FA
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002C10C File Offset: 0x0002A30C
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x04000436 RID: 1078
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}

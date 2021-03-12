using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000EA RID: 234
	[Cmdlet("Get", "CompliancePolicyBase", DefaultParameterSetName = "Identity")]
	public abstract class GetCompliancePolicyBase : GetMultitenancySystemConfigurationObjectTask<PolicyIdParameter, PolicyStorage>
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00026A50 File Offset: 0x00024C50
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00026A58 File Offset: 0x00024C58
		private protected PolicyScenario? Scenario { protected get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00026A61 File Offset: 0x00024C61
		protected override bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00026A64 File Offset: 0x00024C64
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00026A71 File Offset: 0x00024C71
		protected GetCompliancePolicyBase()
		{
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00026A84 File Offset: 0x00024C84
		protected GetCompliancePolicyBase(PolicyScenario policyScenario)
		{
			this.Scenario = new PolicyScenario?(policyScenario);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00026AA3 File Offset: 0x00024CA3
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			base.ResolveCurrentOrgIdBasedOnIdentity(identity);
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00026AB7 File Offset: 0x00024CB7
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00026AE0 File Offset: 0x00024CE0
		protected override IEnumerable<PolicyStorage> GetPagedData()
		{
			if (this.Scenario != null)
			{
				return from dataObj in base.GetPagedData()
				where dataObj.Scenario == this.Scenario.Value
				select dataObj;
			}
			throw new NotImplementedException("Derived Class need override GetPagedData() if Scenario is not set.");
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00026B26 File Offset: 0x00024D26
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00026B5C File Offset: 0x00024D5C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00026B9C File Offset: 0x00024D9C
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (base.NeedSuppressingPiiData && dataObject is PsCompliancePolicyBase)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
				PsCompliancePolicyBase psCompliancePolicyBase = dataObject as PsCompliancePolicyBase;
				psCompliancePolicyBase.SuppressPiiData(Utils.GetSessionPiiMap(base.ExchangeRunspaceConfig));
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00026BE4 File Offset: 0x00024DE4
		protected void PopulateDistributionStatus(PsCompliancePolicyBase psPolicy, PolicyStorage policyStorage)
		{
			if (ExPolicyConfigProvider.IsFFOOnline)
			{
				PolicySettingStatusHelpers.PopulatePolicyDistributionStatus(psPolicy, policyStorage, base.DataSession, this, this.executionLogger);
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00026C01 File Offset: 0x00024E01
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00026C0F File Offset: 0x00024E0F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00026C20 File Offset: 0x00024E20
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x04000409 RID: 1033
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}

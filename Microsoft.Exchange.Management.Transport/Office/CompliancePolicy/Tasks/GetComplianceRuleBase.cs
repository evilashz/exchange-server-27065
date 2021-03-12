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
	// Token: 0x020000FB RID: 251
	[Cmdlet("Get", "ComplianceRule", DefaultParameterSetName = "Identity")]
	public abstract class GetComplianceRuleBase : GetMultitenancySystemConfigurationObjectTask<ComplianceRuleIdParameter, RuleStorage>
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00029495 File Offset: 0x00027695
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002949D File Offset: 0x0002769D
		private protected PolicyScenario Scenario { protected get; private set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000294A6 File Offset: 0x000276A6
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000294A9 File Offset: 0x000276A9
		protected override ObjectId RootId
		{
			get
			{
				return Utils.GetRootId(base.DataSession);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000294B6 File Offset: 0x000276B6
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.Policy != null)
				{
					return new ComparisonFilter(ComparisonOperator.Equal, RuleStorageSchema.ParentPolicyId, Utils.GetUniversalIdentity(this.PolicyStorage));
				}
				return base.InternalFilter;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000294E2 File Offset: 0x000276E2
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000294EA File Offset: 0x000276EA
		private protected PolicyStorage PolicyStorage { protected get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x000294F3 File Offset: 0x000276F3
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x000294FB File Offset: 0x000276FB
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public PolicyIdParameter Policy { get; set; }

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002952A File Offset: 0x0002772A
		protected override IEnumerable<RuleStorage> GetPagedData()
		{
			if (this.Policy != null)
			{
				return from x in base.GetPagedData()
				where x.ParentPolicyId.Equals(Utils.GetUniversalIdentity(this.PolicyStorage))
				select x;
			}
			return base.GetPagedData();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00029552 File Offset: 0x00027752
		protected GetComplianceRuleBase(PolicyScenario policyScenario)
		{
			this.Scenario = policyScenario;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002956C File Offset: 0x0002776C
		protected override void InternalValidate()
		{
			if (this.Policy != null)
			{
				if (this.Identity != null)
				{
					throw new PolicyAndIdentityParameterUsedTogetherException(this.PolicyStorage.ToString(), this.Identity.ToString());
				}
				this.PolicyStorage = (PolicyStorage)base.GetDataObject<PolicyStorage>(this.Policy, base.DataSession, null, new LocalizedString?(Strings.ErrorPolicyNotFound(this.Policy.ToString())), new LocalizedString?(Strings.ErrorPolicyNotUnique(this.Policy.ToString())), ExchangeErrorCategory.Client);
				base.WriteVerbose(Strings.VerbosePolicyStorageObjectLoaded(this.PolicyStorage.ToString()));
			}
			base.InternalValidate();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00029611 File Offset: 0x00027811
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			base.ResolveCurrentOrgIdBasedOnIdentity(identity);
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00029625 File Offset: 0x00027825
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00029628 File Offset: 0x00027828
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (base.NeedSuppressingPiiData && dataObject is PsComplianceRuleBase)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
				PsComplianceRuleBase psComplianceRuleBase = dataObject as PsComplianceRuleBase;
				psComplianceRuleBase.SuppressPiiData(Utils.GetSessionPiiMap(base.ExchangeRunspaceConfig));
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00029670 File Offset: 0x00027870
		protected override IConfigDataProvider CreateSession()
		{
			return PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForCmdlet(base.CreateSession() as IConfigurationSession, this.executionLogger);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000296A4 File Offset: 0x000278A4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000296E4 File Offset: 0x000278E4
		protected override void InternalStateReset()
		{
			this.DisposePolicyConfigProvider();
			base.InternalStateReset();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000296F2 File Offset: 0x000278F2
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposePolicyConfigProvider();
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00029704 File Offset: 0x00027904
		private void DisposePolicyConfigProvider()
		{
			PolicyConfigProvider policyConfigProvider = base.DataSession as PolicyConfigProvider;
			if (policyConfigProvider != null)
			{
				policyConfigProvider.Dispose();
			}
		}

		// Token: 0x0400042B RID: 1067
		protected ExecutionLog executionLogger = ExExecutionLog.CreateForCmdlet();
	}
}

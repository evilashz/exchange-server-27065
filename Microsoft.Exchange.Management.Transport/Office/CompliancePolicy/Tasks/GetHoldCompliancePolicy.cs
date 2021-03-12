using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200011F RID: 287
	[Cmdlet("Get", "HoldCompliancePolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetHoldCompliancePolicy : GetCompliancePolicyBase
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x0002EFDE File Offset: 0x0002D1DE
		public GetHoldCompliancePolicy() : base(PolicyScenario.Hold)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002EFE8 File Offset: 0x0002D1E8
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsHoldCompliancePolicy psHoldCompliancePolicy = new PsHoldCompliancePolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			foreach (BindingStorage bindingStorage in psHoldCompliancePolicy.StorageBindings)
			{
				base.WriteVerbose(Strings.VerboseLoadBindingStorageObjects(bindingStorage.ToString(), psHoldCompliancePolicy.ToString()));
			}
			psHoldCompliancePolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (psHoldCompliancePolicy.ReadOnly)
			{
				this.WriteWarning(Strings.WarningTaskPolicyIsTooAdvancedToRead(psHoldCompliancePolicy.Name));
			}
			base.PopulateDistributionStatus(psHoldCompliancePolicy, dataObject as PolicyStorage);
			base.WriteResult(psHoldCompliancePolicy);
		}
	}
}

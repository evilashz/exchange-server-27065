using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200011A RID: 282
	[Cmdlet("Get", "AuditConfig")]
	public sealed class GetAuditConfig : GetMultitenancySystemConfigurationObjectTask<PolicyIdParameter, PolicyStorage>
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x0002E6B1 File Offset: 0x0002C8B1
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002E6CC File Offset: 0x0002C8CC
		protected override void InternalProcessRecord()
		{
			Func<ErrorRecord, bool> predicate = (ErrorRecord e) => !(e.Exception is ManagementObjectNotFoundException);
			foreach (Workload workload in AuditConfigUtility.AuditableWorkloads)
			{
				IEnumerable<ErrorRecord> errRecords;
				AuditConfigurationPolicy auditConfigurationPolicy = AuditConfigUtility.GetAuditConfigurationPolicy(workload, this.Organization, out errRecords);
				if (AuditConfigUtility.ValidateErrorRecords(this, errRecords, predicate) && auditConfigurationPolicy != null)
				{
					AuditConfigurationRule auditConfigurationRule = AuditConfigUtility.GetAuditConfigurationRule(workload, this.Organization, out errRecords);
					if (AuditConfigUtility.ValidateErrorRecords(this, errRecords, predicate) && auditConfigurationRule != null)
					{
						this.WriteResult(new AuditConfig(workload)
						{
							Setting = AuditConfigUtility.ValidateAuditConfigurationRule(workload, auditConfigurationRule),
							PolicyDistributionStatus = PolicyApplyStatus.Success,
							DistributionResults = auditConfigurationPolicy.DistributionResults
						});
					}
				}
			}
		}
	}
}

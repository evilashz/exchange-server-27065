using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200011B RID: 283
	[Cmdlet("Set", "AuditConfig", DefaultParameterSetName = "Identity")]
	public sealed class SetAuditConfig : GetMultitenancySystemConfigurationObjectTask<PolicyIdParameter, PolicyStorage>
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002E7AC File Offset: 0x0002C9AC
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<Workload> Workload { get; set; }

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
		protected override void InternalProcessRecord()
		{
			Func<ErrorRecord, bool> predicate = (ErrorRecord e) => !(e.Exception is ManagementObjectNotFoundException);
			foreach (Workload workload in AuditConfigUtility.AuditableWorkloads)
			{
				AuditSwitchStatus auditSwitch = this.Workload.Contains(workload) ? AuditSwitchStatus.On : AuditSwitchStatus.Off;
				MultiValuedProperty<AuditableOperations> auditOperations = AuditConfigUtility.GetAuditOperations(workload, auditSwitch);
				IEnumerable<ErrorRecord> errRecords;
				AuditConfigurationRule auditConfigurationRule = AuditConfigUtility.GetAuditConfigurationRule(workload, this.Organization, out errRecords);
				if (AuditConfigUtility.ValidateErrorRecords(this, errRecords, predicate))
				{
					if (auditConfigurationRule != null)
					{
						AuditConfigUtility.SetAuditConfigurationRule(workload, this.Organization, auditOperations, out errRecords);
						AuditConfigUtility.ValidateErrorRecords(this, errRecords);
					}
					else
					{
						AuditConfigurationPolicy auditConfigurationPolicy = AuditConfigUtility.GetAuditConfigurationPolicy(workload, this.Organization, out errRecords);
						if (AuditConfigUtility.ValidateErrorRecords(this, errRecords, predicate))
						{
							if (auditConfigurationPolicy == null)
							{
								auditConfigurationPolicy = AuditConfigUtility.NewAuditConfigurationPolicy(workload, this.Organization, out errRecords);
								if (!AuditConfigUtility.ValidateErrorRecords(this, errRecords))
								{
									continue;
								}
							}
							auditConfigurationRule = AuditConfigUtility.NewAuditConfigurationRule(workload, this.Organization, auditOperations, out errRecords);
							AuditConfigUtility.ValidateErrorRecords(this, errRecords);
						}
					}
				}
			}
		}
	}
}

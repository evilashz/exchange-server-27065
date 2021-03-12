using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200012B RID: 299
	[Cmdlet("Get", "DlpComplianceRule", DefaultParameterSetName = "Identity")]
	public sealed class GetDlpComplianceRule : GetComplianceRuleBase
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x0002F975 File Offset: 0x0002DB75
		public GetDlpComplianceRule() : base(PolicyScenario.Dlp)
		{
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002F98E File Offset: 0x0002DB8E
		protected override IEnumerable<RuleStorage> GetPagedData()
		{
			return from p in base.GetPagedData()
			where p.Scenario == base.Scenario
			select p;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsDlpComplianceRule psDlpComplianceRule = new PsDlpComplianceRule(dataObject as RuleStorage);
			psDlpComplianceRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (psDlpComplianceRule.ReadOnly)
			{
				this.WriteWarning(Strings.WarningTaskRuleIsTooAdvancedToRead(psDlpComplianceRule.Name));
			}
			base.WriteResult(psDlpComplianceRule);
		}
	}
}

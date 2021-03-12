using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E0 RID: 224
	internal class AuditConfigurationPolicySchema : PsCompliancePolicyBaseSchema
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00025A1B File Offset: 0x00023C1B
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<PolicyStorageSchema>();
		}
	}
}

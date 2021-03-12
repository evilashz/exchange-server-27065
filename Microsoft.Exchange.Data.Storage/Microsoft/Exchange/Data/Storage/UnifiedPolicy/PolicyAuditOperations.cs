using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E7A RID: 3706
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyAuditOperations
	{
		// Token: 0x040056B5 RID: 22197
		public MultiValuedProperty<AuditableOperations> AuditOperationsDelegate;
	}
}

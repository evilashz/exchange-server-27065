using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B1 RID: 177
	internal class QueryableGovernorObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000319 RID: 793
		public static readonly SimpleProviderPropertyDefinition Status = QueryableObjectSchema.Status;

		// Token: 0x0400031A RID: 794
		public static readonly SimpleProviderPropertyDefinition LastRunTime = QueryableObjectSchema.LastRunTime;

		// Token: 0x0400031B RID: 795
		public static readonly SimpleProviderPropertyDefinition NumberConsecutiveFailures = QueryableObjectSchema.NumberConsecutiveFailures;
	}
}

using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AD RID: 173
	internal class QueryableDatabaseManagerObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000310 RID: 784
		public static readonly SimpleProviderPropertyDefinition StartState = QueryableObjectSchema.StartState;

		// Token: 0x04000311 RID: 785
		public static readonly SimpleProviderPropertyDefinition IsStopping = QueryableObjectSchema.IsStopping;

		// Token: 0x04000312 RID: 786
		public static readonly SimpleProviderPropertyDefinition Throttle = QueryableObjectSchema.Throttle;

		// Token: 0x04000313 RID: 787
		public static readonly SimpleProviderPropertyDefinition Governor = QueryableObjectSchema.Governor;
	}
}

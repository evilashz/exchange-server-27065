using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B4 RID: 180
	internal class QueryableOnlineDatabaseObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400034A RID: 842
		public static readonly SimpleProviderPropertyDefinition DatabaseName = QueryableObjectSchema.DatabaseName;

		// Token: 0x0400034B RID: 843
		public static readonly SimpleProviderPropertyDefinition DatabaseGuid = QueryableObjectSchema.DatabaseGuid;

		// Token: 0x0400034C RID: 844
		public static readonly SimpleProviderPropertyDefinition RestartRequired = QueryableObjectSchema.RestartRequired;

		// Token: 0x0400034D RID: 845
		public static readonly SimpleProviderPropertyDefinition EventController = QueryableObjectSchema.EventController;
	}
}

using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D3 RID: 211
	internal class FfoADUserSchemaExtensions : ADObjectSchema
	{
		// Token: 0x04000440 RID: 1088
		public static readonly HygienePropertyDefinition LocalUserIdProp = new HygienePropertyDefinition("LocalUserId", typeof(ADObjectId));
	}
}

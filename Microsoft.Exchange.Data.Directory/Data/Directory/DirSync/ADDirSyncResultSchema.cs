using System;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B6 RID: 438
	internal class ADDirSyncResultSchema : ObjectSchema
	{
		// Token: 0x04000A88 RID: 2696
		public static readonly ADPropertyDefinition Id = ADObjectSchema.Id;

		// Token: 0x04000A89 RID: 2697
		public static readonly ADPropertyDefinition Name = ADObjectSchema.RawName;

		// Token: 0x04000A8A RID: 2698
		public static readonly ADPropertyDefinition WhenCreated = ADObjectSchema.WhenCreatedRaw;

		// Token: 0x04000A8B RID: 2699
		public static readonly ADPropertyDefinition IsDeleted = new ADPropertyDefinition("IsDeleted", ExchangeObjectVersion.Exchange2003, typeof(bool), "isDeleted", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

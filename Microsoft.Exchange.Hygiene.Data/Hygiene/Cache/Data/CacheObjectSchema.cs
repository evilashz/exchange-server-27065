using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000056 RID: 86
	internal static class CacheObjectSchema
	{
		// Token: 0x04000215 RID: 533
		public static readonly HygienePropertyDefinition EntityNameProp = new HygienePropertyDefinition("EntityName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000216 RID: 534
		public static readonly HygienePropertyDefinition LastSyncTimeProp = new HygienePropertyDefinition("LastSyncTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000217 RID: 535
		public static readonly HygienePropertyDefinition LastFullSyncTimeProp = new HygienePropertyDefinition("LastFullSyncTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000218 RID: 536
		public static readonly HygienePropertyDefinition LastTracerSyncTimeProp = new HygienePropertyDefinition("LastTracerSyncTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}

using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000225 RID: 549
	internal class SyncPropertyRefreshSchema : ADObjectSchema
	{
		// Token: 0x04000B4E RID: 2894
		public static readonly HygienePropertyDefinition Status = new HygienePropertyDefinition("Status", typeof(SyncPropertyRefreshStatus), SyncPropertyRefreshStatus.Requested, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}

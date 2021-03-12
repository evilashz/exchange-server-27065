using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Reporting;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001CC RID: 460
	internal class TenantThrottleInfoBatchSchema
	{
		// Token: 0x0400094A RID: 2378
		public static readonly HygienePropertyDefinition PhysicalInstanceKeyProp = DalHelper.PhysicalInstanceKeyProp;

		// Token: 0x0400094B RID: 2379
		public static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;

		// Token: 0x0400094C RID: 2380
		internal static readonly HygienePropertyDefinition TenantThrottleInfoListProperty = new HygienePropertyDefinition("batchProperties", typeof(TenantThrottleInfo), null, ADPropertyDefinitionFlags.MultiValued);
	}
}

using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000014 RID: 20
	internal class AsyncQueueLogBatchSchema
	{
		// Token: 0x04000041 RID: 65
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000042 RID: 66
		internal static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;

		// Token: 0x04000043 RID: 67
		internal static readonly HygienePropertyDefinition LogProperty = new HygienePropertyDefinition("logProperties", typeof(object), null, ADPropertyDefinitionFlags.MultiValued);
	}
}

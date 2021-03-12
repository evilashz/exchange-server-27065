using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OutlookClassIds
{
	// Token: 0x02000ADE RID: 2782
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct OutlookClassId
	{
		// Token: 0x060064F6 RID: 25846 RVA: 0x001AC994 File Offset: 0x001AAB94
		internal OutlookClassId(Guid value)
		{
			this.AsGuid = value;
			this.AsBytes = value.ToByteArray();
		}

		// Token: 0x04003983 RID: 14723
		internal readonly Guid AsGuid;

		// Token: 0x04003984 RID: 14724
		internal readonly byte[] AsBytes;
	}
}

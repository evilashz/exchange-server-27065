using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA0 RID: 3488
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISharingSubscriptionData
	{
		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x060077EB RID: 30699
		VersionedId Id { get; }

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x060077EC RID: 30700
		SharingDataType DataType { get; }

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x060077ED RID: 30701
		string RemoteFolderName { get; }

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x060077EE RID: 30702
		StoreObjectId LocalFolderId { get; }
	}
}

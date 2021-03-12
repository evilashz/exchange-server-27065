using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D4 RID: 724
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolderTreeData : IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001EF7 RID: 7927
		byte[] NodeOrdinal { get; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001EF8 RID: 7928
		int OutlookTagId { get; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001EF9 RID: 7929
		FolderTreeDataType FolderTreeDataType { get; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001EFA RID: 7930
		FolderTreeDataFlags FolderTreeDataFlags { get; }

		// Token: 0x06001EFB RID: 7931
		void SetNodeOrdinal(byte[] nodeBefore, byte[] nodeAfter);
	}
}

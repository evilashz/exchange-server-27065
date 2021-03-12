using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D5 RID: 725
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractFolderTreeData : AbstractMessageItem, IFolderTreeData, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00086318 File Offset: 0x00084518
		public FolderTreeDataFlags FolderTreeDataFlags
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0008631F File Offset: 0x0008451F
		public FolderTreeDataType FolderTreeDataType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00086326 File Offset: 0x00084526
		public byte[] NodeOrdinal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0008632D File Offset: 0x0008452D
		public int OutlookTagId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00086334 File Offset: 0x00084534
		public void SetNodeOrdinal(byte[] nodeBefore, byte[] nodeAfter)
		{
			throw new NotImplementedException();
		}
	}
}

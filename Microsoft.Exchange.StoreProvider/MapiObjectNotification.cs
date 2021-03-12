using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001E0 RID: 480
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiObjectNotification : MapiNotification
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001BAC3 File Offset: 0x00019CC3
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001BACB File Offset: 0x00019CCB
		public ObjectType ObjectType
		{
			get
			{
				return (ObjectType)this.objectType;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001BAD3 File Offset: 0x00019CD3
		public byte[] ParentId
		{
			get
			{
				return this.parentId;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001BADB File Offset: 0x00019CDB
		public byte[] OldId
		{
			get
			{
				return this.oldId;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001BAE3 File Offset: 0x00019CE3
		public byte[] OldParentId
		{
			get
			{
				return this.oldParentId;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001BAEB File Offset: 0x00019CEB
		public PropTag[] Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		internal unsafe MapiObjectNotification(NOTIFICATION* notification) : base(notification)
		{
			if (notification->info.obj.cbEntryID > 0)
			{
				this.entryId = new byte[notification->info.obj.cbEntryID];
				Marshal.Copy(notification->info.obj.lpEntryID, this.entryId, 0, this.entryId.Length);
			}
			this.objectType = notification->info.obj.ulObjType;
			if (notification->info.obj.cbParentID > 0)
			{
				this.parentId = new byte[notification->info.obj.cbParentID];
				Marshal.Copy(notification->info.obj.lpParentID, this.parentId, 0, this.parentId.Length);
			}
			if (notification->info.obj.cbOldID > 0)
			{
				this.oldId = new byte[notification->info.obj.cbOldID];
				Marshal.Copy(notification->info.obj.lpOldID, this.oldId, 0, this.oldId.Length);
			}
			if (notification->info.obj.cbOldParentID > 0)
			{
				this.oldParentId = new byte[notification->info.obj.cbOldParentID];
				Marshal.Copy(notification->info.obj.lpOldParentID, this.oldParentId, 0, this.oldParentId.Length);
			}
			if (notification->info.obj.lpPropTagArray != null)
			{
				this.tags = Array<PropTag>.New(*notification->info.obj.lpPropTagArray);
				for (int i = 0; i < this.tags.Length; i++)
				{
					this.tags[i] = (PropTag)notification->info.obj.lpPropTagArray[i + 1];
				}
			}
		}

		// Token: 0x04000672 RID: 1650
		private readonly byte[] entryId;

		// Token: 0x04000673 RID: 1651
		private readonly int objectType;

		// Token: 0x04000674 RID: 1652
		private readonly byte[] parentId;

		// Token: 0x04000675 RID: 1653
		private readonly byte[] oldId;

		// Token: 0x04000676 RID: 1654
		private readonly byte[] oldParentId;

		// Token: 0x04000677 RID: 1655
		private readonly PropTag[] tags;
	}
}

using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001DD RID: 477
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiNewMailNotification : MapiNotification
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001B900 File Offset: 0x00019B00
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001B908 File Offset: 0x00019B08
		public byte[] ParentId
		{
			get
			{
				return this.parentId;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001B910 File Offset: 0x00019B10
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001B918 File Offset: 0x00019B18
		public int MessageFlags
		{
			get
			{
				return this.messageFlags;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001B920 File Offset: 0x00019B20
		internal unsafe MapiNewMailNotification(NOTIFICATION* notification) : base(notification)
		{
			if (notification->info.newmail.cbEntryID > 0)
			{
				this.entryId = new byte[notification->info.newmail.cbEntryID];
				Marshal.Copy(notification->info.newmail.lpEntryID, this.entryId, 0, this.entryId.Length);
			}
			if (notification->info.newmail.cbParentID > 0)
			{
				this.parentId = new byte[notification->info.newmail.cbParentID];
				Marshal.Copy(notification->info.newmail.lpParentID, this.parentId, 0, this.parentId.Length);
			}
			if (notification->info.newmail.lpszMessageClass != IntPtr.Zero)
			{
				if ((notification->info.newmail.ulFlags & -2147483648) != 0)
				{
					this.messageClass = Marshal.PtrToStringUni(notification->info.newmail.lpszMessageClass);
				}
				else
				{
					this.messageClass = Marshal.PtrToStringAnsi(notification->info.newmail.lpszMessageClass);
				}
			}
			this.messageFlags = notification->info.newmail.ulMessageFlags;
		}

		// Token: 0x0400066B RID: 1643
		private readonly byte[] entryId;

		// Token: 0x0400066C RID: 1644
		private readonly byte[] parentId;

		// Token: 0x0400066D RID: 1645
		private readonly string messageClass;

		// Token: 0x0400066E RID: 1646
		private readonly int messageFlags;
	}
}

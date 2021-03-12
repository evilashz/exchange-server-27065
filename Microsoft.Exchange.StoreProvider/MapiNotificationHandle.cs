using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001DF RID: 479
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiNotificationHandle
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x0001BA5A File Offset: 0x00019C5A
		internal MapiNotificationHandle(MapiNotificationHandler handler)
		{
			this.handler = handler;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001BA69 File Offset: 0x00019C69
		public IntPtr Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001BA71 File Offset: 0x00019C71
		public ulong NotificationCallbackId
		{
			get
			{
				return this.notificationCallbackId;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001BA79 File Offset: 0x00019C79
		public bool IsValid
		{
			get
			{
				return this.connection != IntPtr.Zero;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001BA8B File Offset: 0x00019C8B
		internal MapiNotificationHandler Handler
		{
			get
			{
				return this.handler;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001BA93 File Offset: 0x00019C93
		internal void SetConnection(IntPtr connection, ulong notificationCallbackId)
		{
			if (this.IsValid)
			{
				throw new InvalidOperationException("SetConnection should only be called once");
			}
			this.connection = connection;
			this.notificationCallbackId = notificationCallbackId;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001BAB6 File Offset: 0x00019CB6
		internal void MarkAsInvalid()
		{
			this.connection = IntPtr.Zero;
		}

		// Token: 0x0400066F RID: 1647
		private IntPtr connection;

		// Token: 0x04000670 RID: 1648
		private ulong notificationCallbackId;

		// Token: 0x04000671 RID: 1649
		private readonly MapiNotificationHandler handler;
	}
}

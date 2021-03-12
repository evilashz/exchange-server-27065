using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000080 RID: 128
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiConnectionDroppedNotification : MapiNotification
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000EF03 File Offset: 0x0000D103
		public string ServerDN
		{
			get
			{
				return this.serverDN;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000EF0B File Offset: 0x0000D10B
		public string UserDN
		{
			get
			{
				return this.userDN;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000EF13 File Offset: 0x0000D113
		public int TickDeath
		{
			get
			{
				return this.tickDeath;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000EF1C File Offset: 0x0000D11C
		internal unsafe MapiConnectionDroppedNotification(NOTIFICATION* notification) : base(notification)
		{
			if (notification->info.drop.lpszServerDN != IntPtr.Zero)
			{
				this.serverDN = Marshal.PtrToStringAnsi(notification->info.drop.lpszServerDN);
			}
			if (notification->info.drop.lpszUserDN != IntPtr.Zero)
			{
				this.userDN = Marshal.PtrToStringAnsi(notification->info.drop.lpszUserDN);
			}
			this.tickDeath = notification->info.drop.dwTickDeath;
		}

		// Token: 0x040004DD RID: 1245
		private readonly string serverDN;

		// Token: 0x040004DE RID: 1246
		private readonly string userDN;

		// Token: 0x040004DF RID: 1247
		private readonly int tickDeath;
	}
}

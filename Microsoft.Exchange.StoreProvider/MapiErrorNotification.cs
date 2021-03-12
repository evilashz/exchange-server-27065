using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000085 RID: 133
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiErrorNotification : MapiNotification
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000F4EA File Offset: 0x0000D6EA
		public int SCode
		{
			get
			{
				return this.scode;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000F4F2 File Offset: 0x0000D6F2
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000F4FA File Offset: 0x0000D6FA
		public string Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000F502 File Offset: 0x0000D702
		public int LowLevelError
		{
			get
			{
				return this.lowLevelError;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000F50A File Offset: 0x0000D70A
		public int Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F514 File Offset: 0x0000D714
		internal unsafe MapiErrorNotification(NOTIFICATION* notification) : base(notification)
		{
			if (notification->info.err.cbEntryID > 0)
			{
				this.entryId = new byte[notification->info.err.cbEntryID];
				Marshal.Copy(notification->info.err.lpEntryID, this.entryId, 0, this.entryId.Length);
			}
			this.scode = notification->info.err.scode;
			bool unicodeEncoded = (notification->info.err.ulFlags & int.MinValue) != 0;
			this.error = notification->info.err.lpMAPIError->ErrorText(unicodeEncoded);
			this.component = notification->info.err.lpMAPIError->Component(unicodeEncoded);
			this.lowLevelError = notification->info.err.lpMAPIError->ulLowLevelError;
			this.context = notification->info.err.lpMAPIError->ulContext;
		}

		// Token: 0x040004F8 RID: 1272
		private readonly byte[] entryId;

		// Token: 0x040004F9 RID: 1273
		private readonly int scode;

		// Token: 0x040004FA RID: 1274
		private readonly string error;

		// Token: 0x040004FB RID: 1275
		private readonly string component;

		// Token: 0x040004FC RID: 1276
		private readonly int lowLevelError;

		// Token: 0x040004FD RID: 1277
		private readonly int context;
	}
}

using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x0200000E RID: 14
	internal struct CreateSessionInfo
	{
		// Token: 0x0400002F RID: 47
		public uint Flags;

		// Token: 0x04000030 RID: 48
		public string UserDn;

		// Token: 0x04000031 RID: 49
		public uint ConnectionMode;

		// Token: 0x04000032 RID: 50
		public uint CodePageId;

		// Token: 0x04000033 RID: 51
		public uint LocaleIdString;

		// Token: 0x04000034 RID: 52
		public uint LocaleIdSort;

		// Token: 0x04000035 RID: 53
		public short[] ClientVersion;

		// Token: 0x04000036 RID: 54
		public byte[] AuxiliaryIn;

		// Token: 0x04000037 RID: 55
		public Action<ErrorCode, uint> NotificationPendingCallback;
	}
}

using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal struct NATIVE_EMITDATACTX
	{
		// Token: 0x04000106 RID: 262
		public uint cbStruct;

		// Token: 0x04000107 RID: 263
		public uint dwVersion;

		// Token: 0x04000108 RID: 264
		public ulong qwSequenceNum;

		// Token: 0x04000109 RID: 265
		public uint grbitOperationalFlags;

		// Token: 0x0400010A RID: 266
		public JET_LOGTIME logtimeEmit;

		// Token: 0x0400010B RID: 267
		public JET_LGPOS lgposLogData;

		// Token: 0x0400010C RID: 268
		public uint cbLogData;
	}
}

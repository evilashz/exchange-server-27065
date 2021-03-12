using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200002A RID: 42
	internal struct AsyncBuffer
	{
		// Token: 0x04000092 RID: 146
		internal IAsyncResult AsyncResult;

		// Token: 0x04000093 RID: 147
		internal byte[] Buffer;

		// Token: 0x04000094 RID: 148
		internal long FileOffset;

		// Token: 0x04000095 RID: 149
		internal int ReadLength;

		// Token: 0x04000096 RID: 150
		internal bool Reading;

		// Token: 0x04000097 RID: 151
		internal bool WriteDeferred;
	}
}

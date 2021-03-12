using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C8D RID: 3213
	internal struct SecurityBuffer
	{
		// Token: 0x04003BF3 RID: 15347
		public int count;

		// Token: 0x04003BF4 RID: 15348
		public BufferType type;

		// Token: 0x04003BF5 RID: 15349
		public IntPtr token;

		// Token: 0x04003BF6 RID: 15350
		public static readonly int Size = sizeof(SecurityBuffer);
	}
}

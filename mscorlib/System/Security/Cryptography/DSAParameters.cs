using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200025B RID: 603
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		// Token: 0x04000C2B RID: 3115
		public byte[] P;

		// Token: 0x04000C2C RID: 3116
		public byte[] Q;

		// Token: 0x04000C2D RID: 3117
		public byte[] G;

		// Token: 0x04000C2E RID: 3118
		public byte[] Y;

		// Token: 0x04000C2F RID: 3119
		public byte[] J;

		// Token: 0x04000C30 RID: 3120
		[NonSerialized]
		public byte[] X;

		// Token: 0x04000C31 RID: 3121
		public byte[] Seed;

		// Token: 0x04000C32 RID: 3122
		public int Counter;
	}
}

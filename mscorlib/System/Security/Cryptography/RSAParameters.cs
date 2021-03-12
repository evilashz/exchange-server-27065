using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200027A RID: 634
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		// Token: 0x04000C85 RID: 3205
		public byte[] Exponent;

		// Token: 0x04000C86 RID: 3206
		public byte[] Modulus;

		// Token: 0x04000C87 RID: 3207
		[NonSerialized]
		public byte[] P;

		// Token: 0x04000C88 RID: 3208
		[NonSerialized]
		public byte[] Q;

		// Token: 0x04000C89 RID: 3209
		[NonSerialized]
		public byte[] DP;

		// Token: 0x04000C8A RID: 3210
		[NonSerialized]
		public byte[] DQ;

		// Token: 0x04000C8B RID: 3211
		[NonSerialized]
		public byte[] InverseQ;

		// Token: 0x04000C8C RID: 3212
		[NonSerialized]
		public byte[] D;
	}
}

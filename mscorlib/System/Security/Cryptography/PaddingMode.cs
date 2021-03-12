using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000241 RID: 577
	[ComVisible(true)]
	[Serializable]
	public enum PaddingMode
	{
		// Token: 0x04000BD9 RID: 3033
		None = 1,
		// Token: 0x04000BDA RID: 3034
		PKCS7,
		// Token: 0x04000BDB RID: 3035
		Zeros,
		// Token: 0x04000BDC RID: 3036
		ANSIX923,
		// Token: 0x04000BDD RID: 3037
		ISO10126
	}
}

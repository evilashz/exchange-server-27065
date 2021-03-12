using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000272 RID: 626
	[ComVisible(true)]
	public abstract class MaskGenerationMethod
	{
		// Token: 0x06002228 RID: 8744
		[ComVisible(true)]
		public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);
	}
}

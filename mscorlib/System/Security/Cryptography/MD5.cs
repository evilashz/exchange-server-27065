using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000270 RID: 624
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000788E1 File Offset: 0x00076AE1
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000788F4 File Offset: 0x00076AF4
		public new static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00078900 File Offset: 0x00076B00
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000278 RID: 632
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x00079BCC File Offset: 0x00077DCC
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00079BDF File Offset: 0x00077DDF
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00079BEB File Offset: 0x00077DEB
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}

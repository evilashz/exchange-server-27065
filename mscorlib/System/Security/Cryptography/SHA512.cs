using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x000815EE File Offset: 0x0007F7EE
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00081601 File Offset: 0x0007F801
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0008160D File Offset: 0x0007F80D
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}

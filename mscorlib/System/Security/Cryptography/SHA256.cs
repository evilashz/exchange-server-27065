using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000292 RID: 658
	[ComVisible(true)]
	public abstract class SHA256 : HashAlgorithm
	{
		// Token: 0x06002355 RID: 9045 RVA: 0x000805B4 File Offset: 0x0007E7B4
		protected SHA256()
		{
			this.HashSizeValue = 256;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000805C7 File Offset: 0x0007E7C7
		public new static SHA256 Create()
		{
			return SHA256.Create("System.Security.Cryptography.SHA256");
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000805D3 File Offset: 0x0007E7D3
		public new static SHA256 Create(string hashName)
		{
			return (SHA256)CryptoConfig.CreateFromName(hashName);
		}
	}
}

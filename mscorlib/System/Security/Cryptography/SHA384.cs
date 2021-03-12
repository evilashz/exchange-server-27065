using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	public abstract class SHA384 : HashAlgorithm
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x00080DAE File Offset: 0x0007EFAE
		protected SHA384()
		{
			this.HashSizeValue = 384;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00080DC1 File Offset: 0x0007EFC1
		public new static SHA384 Create()
		{
			return SHA384.Create("System.Security.Cryptography.SHA384");
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00080DCD File Offset: 0x0007EFCD
		public new static SHA384 Create(string hashName)
		{
			return (SHA384)CryptoConfig.CreateFromName(hashName);
		}
	}
}

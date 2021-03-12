using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200028F RID: 655
	[ComVisible(true)]
	public abstract class SHA1 : HashAlgorithm
	{
		// Token: 0x06002343 RID: 9027 RVA: 0x0007FCAD File Offset: 0x0007DEAD
		protected SHA1()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0007FCC0 File Offset: 0x0007DEC0
		public new static SHA1 Create()
		{
			return SHA1.Create("System.Security.Cryptography.SHA1");
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0007FCCC File Offset: 0x0007DECC
		public new static SHA1 Create(string hashName)
		{
			return (SHA1)CryptoConfig.CreateFromName(hashName);
		}
	}
}

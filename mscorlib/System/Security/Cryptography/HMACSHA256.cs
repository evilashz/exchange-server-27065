using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000265 RID: 613
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		// Token: 0x060021C2 RID: 8642 RVA: 0x00077809 File Offset: 0x00075A09
		public HMACSHA256() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00077818 File Offset: 0x00075A18
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.HashSizeValue = 256;
			base.InitializeKey(key);
		}
	}
}

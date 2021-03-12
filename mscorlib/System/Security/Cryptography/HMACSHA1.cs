using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000264 RID: 612
	[ComVisible(true)]
	public class HMACSHA1 : HMAC
	{
		// Token: 0x060021BF RID: 8639 RVA: 0x0007778C File Offset: 0x0007598C
		public HMACSHA1() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0007779B File Offset: 0x0007599B
		public HMACSHA1(byte[] key) : this(key, false)
		{
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000777A8 File Offset: 0x000759A8
		public HMACSHA1(byte[] key, bool useManagedSha1)
		{
			this.m_hashName = "SHA1";
			if (useManagedSha1)
			{
				this.m_hash1 = new SHA1Managed();
				this.m_hash2 = new SHA1Managed();
			}
			else
			{
				this.m_hash1 = new SHA1CryptoServiceProvider();
				this.m_hash2 = new SHA1CryptoServiceProvider();
			}
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}

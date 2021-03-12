using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000262 RID: 610
	[ComVisible(true)]
	public class HMACMD5 : HMAC
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x000776F8 File Offset: 0x000758F8
		public HMACMD5() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00077707 File Offset: 0x00075907
		public HMACMD5(byte[] key)
		{
			this.m_hashName = "MD5";
			this.m_hash1 = new MD5CryptoServiceProvider();
			this.m_hash2 = new MD5CryptoServiceProvider();
			this.HashSizeValue = 128;
			base.InitializeKey(key);
		}
	}
}

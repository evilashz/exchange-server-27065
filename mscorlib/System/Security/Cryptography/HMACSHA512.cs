using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000267 RID: 615
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x00077A00 File Offset: 0x00075C00
		public HMACSHA512() : this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00077A14 File Offset: 0x00075C14
		[SecuritySafeCritical]
		public HMACSHA512(byte[] key)
		{
			this.m_hashName = "SHA512";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x00077AED File Offset: 0x00075CED
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x00077AFF File Offset: 0x00075CFF
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x00077B07 File Offset: 0x00075D07
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04000C4E RID: 3150
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}

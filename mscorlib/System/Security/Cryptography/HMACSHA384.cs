using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x000778DA File Offset: 0x00075ADA
		public HMACSHA384() : this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000778EC File Offset: 0x00075AEC
		[SecuritySafeCritical]
		public HMACSHA384(byte[] key)
		{
			this.m_hashName = "SHA384";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000779C5 File Offset: 0x00075BC5
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x000779D7 File Offset: 0x00075BD7
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x000779DF File Offset: 0x00075BDF
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

		// Token: 0x04000C4D RID: 3149
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026E RID: 622
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x000783E4 File Offset: 0x000765E4
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0007846C File Offset: 0x0007666C
		public MACTripleDES(byte[] rgbKey) : this("System.Security.Cryptography.TripleDES", rgbKey)
		{
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0007847C File Offset: 0x0007667C
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.des = TripleDES.Create();
			}
			else
			{
				this.des = TripleDES.Create(strTripleDES);
			}
			this.HashSizeValue = this.des.BlockSize;
			this.KeyValue = (byte[])rgbKey.Clone();
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00078517 File Offset: 0x00076717
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00078520 File Offset: 0x00076720
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x0007852D File Offset: 0x0007672D
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.des.Padding;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
				}
				this.des.Padding = value;
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00078554 File Offset: 0x00076754
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000785CC File Offset: 0x000767CC
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007864C File Offset: 0x0007684C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000C5A RID: 3162
		private ICryptoTransform m_encryptor;

		// Token: 0x04000C5B RID: 3163
		private CryptoStream _cs;

		// Token: 0x04000C5C RID: 3164
		private TailStream _ts;

		// Token: 0x04000C5D RID: 3165
		private const int m_bitsPerByte = 8;

		// Token: 0x04000C5E RID: 3166
		private int m_bytesPerBlock;

		// Token: 0x04000C5F RID: 3167
		private TripleDES des;
	}
}

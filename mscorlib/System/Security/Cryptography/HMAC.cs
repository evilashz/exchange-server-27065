using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000261 RID: 609
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00077353 File Offset: 0x00075553
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0007735B File Offset: 0x0007555B
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00077364 File Offset: 0x00075564
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00077410 File Offset: 0x00075610
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			this.UpdateIOPadBuffers();
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00077461 File Offset: 0x00075661
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x00077473 File Offset: 0x00075673
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.InitializeKey(value);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x00077494 File Offset: 0x00075694
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0007749C File Offset: 0x0007569C
		public string HashName
		{
			get
			{
				return this.m_hashName;
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
				}
				this.m_hashName = value;
				this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
				this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000774EA File Offset: 0x000756EA
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000774F6 File Offset: 0x000756F6
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00077503 File Offset: 0x00075703
		public override void Initialize()
		{
			this.m_hash1.Initialize();
			this.m_hash2.Initialize();
			this.m_hashing = false;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00077524 File Offset: 0x00075724
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00077574 File Offset: 0x00075774
		protected override byte[] HashFinal()
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0007762C File Offset: 0x0007582C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000776A4 File Offset: 0x000758A4
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		// Token: 0x04000C46 RID: 3142
		private int blockSizeValue = 64;

		// Token: 0x04000C47 RID: 3143
		internal string m_hashName;

		// Token: 0x04000C48 RID: 3144
		internal HashAlgorithm m_hash1;

		// Token: 0x04000C49 RID: 3145
		internal HashAlgorithm m_hash2;

		// Token: 0x04000C4A RID: 3146
		private byte[] m_inner;

		// Token: 0x04000C4B RID: 3147
		private byte[] m_outer;

		// Token: 0x04000C4C RID: 3148
		private bool m_hashing;
	}
}

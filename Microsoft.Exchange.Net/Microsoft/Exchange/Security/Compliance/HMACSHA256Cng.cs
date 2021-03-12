using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Compliance
{
	// Token: 0x02000C18 RID: 3096
	internal class HMACSHA256Cng : KeyedHashAlgorithm
	{
		// Token: 0x060043C2 RID: 17346 RVA: 0x000B60BA File Offset: 0x000B42BA
		public HMACSHA256Cng() : this(HMACSHA256Cng.GenerateRandomKey())
		{
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000B60C7 File Offset: 0x000B42C7
		public HMACSHA256Cng(byte[] key)
		{
			this.hashAlgorithmInner = new SHA256Cng();
			this.hashAlgorithmOuter = new SHA256Cng();
			this.HashSizeValue = 256;
			this.InitializeKey(key);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000B60F7 File Offset: 0x000B42F7
		public override void Initialize()
		{
			this.hashAlgorithmInner.Initialize();
			this.hashAlgorithmOuter.Initialize();
			this.hashing = false;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000B6116 File Offset: 0x000B4316
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.hashAlgorithmInner != null)
				{
					this.hashAlgorithmInner.Clear();
					this.hashAlgorithmInner = null;
				}
				if (this.hashAlgorithmOuter != null)
				{
					this.hashAlgorithmOuter.Clear();
					this.hashAlgorithmOuter = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000B6158 File Offset: 0x000B4358
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.hashing)
			{
				this.hashAlgorithmInner.TransformBlock(this.inner, 0, this.inner.Length, this.inner, 0);
				this.hashing = true;
			}
			this.hashAlgorithmInner.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000B61A8 File Offset: 0x000B43A8
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashAlgorithmInner.TransformBlock(this.inner, 0, this.inner.Length, this.inner, 0);
				this.hashing = true;
			}
			this.hashAlgorithmInner.TransformFinalBlock(new byte[0], 0, 0);
			byte[] hash = this.hashAlgorithmInner.Hash;
			this.hashAlgorithmOuter.TransformBlock(this.outer, 0, this.outer.Length, this.outer, 0);
			this.hashAlgorithmOuter.TransformBlock(hash, 0, hash.Length, hash, 0);
			this.hashing = false;
			this.hashAlgorithmOuter.TransformFinalBlock(new byte[0], 0, 0);
			return this.hashAlgorithmOuter.Hash;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000B6264 File Offset: 0x000B4464
		private static byte[] GenerateRandomKey()
		{
			byte[] array = new byte[64];
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000B6288 File Offset: 0x000B4488
		private void InitializeKey(byte[] key)
		{
			this.inner = null;
			this.outer = null;
			if (key.Length > 64)
			{
				this.KeyValue = this.hashAlgorithmInner.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			if (this.inner == null)
			{
				this.inner = new byte[64];
			}
			if (this.outer == null)
			{
				this.outer = new byte[64];
			}
			for (int i = 0; i < 64; i++)
			{
				this.inner[i] = 54;
				this.outer[i] = 92;
			}
			for (int j = 0; j < this.KeyValue.Length; j++)
			{
				this.inner[j] = (this.inner[j] ^ this.KeyValue[j]);
				this.outer[j] = (this.outer[j] ^ this.KeyValue[j]);
			}
		}

		// Token: 0x040039B9 RID: 14777
		private const int BlockSizeValue = 64;

		// Token: 0x040039BA RID: 14778
		private HashAlgorithm hashAlgorithmInner;

		// Token: 0x040039BB RID: 14779
		private HashAlgorithm hashAlgorithmOuter;

		// Token: 0x040039BC RID: 14780
		private bool hashing;

		// Token: 0x040039BD RID: 14781
		private byte[] inner;

		// Token: 0x040039BE RID: 14782
		private byte[] outer;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200029F RID: 671
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		// Token: 0x060023A9 RID: 9129 RVA: 0x0008203E File Offset: 0x0008023E
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00082054 File Offset: 0x00080254
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00082063 File Offset: 0x00080263
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0008206C File Offset: 0x0008026C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (this.IVValue != null)
				{
					Array.Clear(this.IVValue, 0, this.IVValue.Length);
					this.IVValue = null;
				}
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000820C2 File Offset: 0x000802C2
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x000820CC File Offset: 0x000802CC
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalBlockSizesValue.Length; i++)
				{
					if (this.LegalBlockSizesValue[i].SkipSize == 0)
					{
						if (this.LegalBlockSizesValue[i].MinSize == value)
						{
							this.BlockSizeValue = value;
							this.IVValue = null;
							return;
						}
					}
					else
					{
						for (int j = this.LegalBlockSizesValue[i].MinSize; j <= this.LegalBlockSizesValue[i].MaxSize; j += this.LegalBlockSizesValue[i].SkipSize)
						{
							if (j == value)
							{
								if (this.BlockSizeValue != value)
								{
									this.BlockSizeValue = value;
									this.IVValue = null;
								}
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidBlockSize"));
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00082178 File Offset: 0x00080378
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00082180 File Offset: 0x00080380
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
			set
			{
				if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFeedbackSize"));
				}
				this.FeedbackSizeValue = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000821AB File Offset: 0x000803AB
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x000821CB File Offset: 0x000803CB
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != this.BlockSizeValue / 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00082209 File Offset: 0x00080409
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x0008222C File Offset: 0x0008042C
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00082280 File Offset: 0x00080480
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return (KeySizes[])this.LegalBlockSizesValue.Clone();
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x00082292 File Offset: 0x00080492
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000822A4 File Offset: 0x000804A4
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000822AC File Offset: 0x000804AC
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!this.ValidKeySize(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000822D5 File Offset: 0x000804D5
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x000822DD File Offset: 0x000804DD
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (value < CipherMode.CBC || CipherMode.CFB < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidCipherMode"));
				}
				this.ModeValue = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000822FE File Offset: 0x000804FE
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x00082306 File Offset: 0x00080506
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
				}
				this.PaddingValue = value;
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00082328 File Offset: 0x00080528
		public bool ValidKeySize(int bitLength)
		{
			KeySizes[] legalKeySizes = this.LegalKeySizes;
			if (legalKeySizes == null)
			{
				return false;
			}
			for (int i = 0; i < legalKeySizes.Length; i++)
			{
				if (legalKeySizes[i].SkipSize == 0)
				{
					if (legalKeySizes[i].MinSize == bitLength)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalKeySizes[i].MinSize; j <= legalKeySizes[i].MaxSize; j += legalKeySizes[i].SkipSize)
					{
						if (j == bitLength)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0008238E File Offset: 0x0008058E
		public static SymmetricAlgorithm Create()
		{
			return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0008239A File Offset: 0x0008059A
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000823A7 File Offset: 0x000805A7
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x060023C1 RID: 9153
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		// Token: 0x060023C2 RID: 9154 RVA: 0x000823BB File Offset: 0x000805BB
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		// Token: 0x060023C3 RID: 9155
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		// Token: 0x060023C4 RID: 9156
		public abstract void GenerateKey();

		// Token: 0x060023C5 RID: 9157
		public abstract void GenerateIV();

		// Token: 0x04000CF7 RID: 3319
		protected int BlockSizeValue;

		// Token: 0x04000CF8 RID: 3320
		protected int FeedbackSizeValue;

		// Token: 0x04000CF9 RID: 3321
		protected byte[] IVValue;

		// Token: 0x04000CFA RID: 3322
		protected byte[] KeyValue;

		// Token: 0x04000CFB RID: 3323
		protected KeySizes[] LegalBlockSizesValue;

		// Token: 0x04000CFC RID: 3324
		protected KeySizes[] LegalKeySizesValue;

		// Token: 0x04000CFD RID: 3325
		protected int KeySizeValue;

		// Token: 0x04000CFE RID: 3326
		protected CipherMode ModeValue;

		// Token: 0x04000CFF RID: 3327
		protected PaddingMode PaddingValue;
	}
}

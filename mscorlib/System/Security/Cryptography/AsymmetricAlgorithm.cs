using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000249 RID: 585
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x00072B4F File Offset: 0x00070D4F
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00072B57 File Offset: 0x00070D57
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00072B66 File Offset: 0x00070D66
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00072B68 File Offset: 0x00070D68
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x00072B70 File Offset: 0x00070D70
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00072C02 File Offset: 0x00070E02
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00072C14 File Offset: 0x00070E14
		public virtual string SignatureAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00072C1B File Offset: 0x00070E1B
		public virtual string KeyExchangeAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00072C22 File Offset: 0x00070E22
		public static AsymmetricAlgorithm Create()
		{
			return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00072C2E File Offset: 0x00070E2E
		public static AsymmetricAlgorithm Create(string algName)
		{
			return (AsymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00072C3B File Offset: 0x00070E3B
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00072C42 File Offset: 0x00070E42
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000BE8 RID: 3048
		protected int KeySizeValue;

		// Token: 0x04000BE9 RID: 3049
		protected KeySizes[] LegalKeySizesValue;
	}
}

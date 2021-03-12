using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000275 RID: 629
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x000791A0 File Offset: 0x000773A0
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000791DD File Offset: 0x000773DD
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x000791F4 File Offset: 0x000773F4
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
			set
			{
				if (value > this.KeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				if (value == 0)
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				if (value < 40)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKS40"));
				}
				if (base.ValidKeySize(value))
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0007925A File Offset: 0x0007745A
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x00079262 File Offset: 0x00077462
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				base.KeySize = value;
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00079284 File Offset: 0x00077484
		public new static RC2 Create()
		{
			return RC2.Create("System.Security.Cryptography.RC2");
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00079290 File Offset: 0x00077490
		public new static RC2 Create(string AlgName)
		{
			return (RC2)CryptoConfig.CreateFromName(AlgName);
		}

		// Token: 0x04000C71 RID: 3185
		protected int EffectiveKeySizeValue;

		// Token: 0x04000C72 RID: 3186
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000C73 RID: 3187
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}

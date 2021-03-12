using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000276 RID: 630
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x000792D4 File Offset: 0x000774D4
		[SecuritySafeCritical]
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00079334 File Offset: 0x00077534
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x0007933C File Offset: 0x0007753C
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_RC2_EKSKS2"));
				}
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00079357 File Offset: 0x00077557
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0007935F File Offset: 0x0007755F
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00079368 File Offset: 0x00077568
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00079385 File Offset: 0x00077585
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000793A2 File Offset: 0x000775A2
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000793C7 File Offset: 0x000775C7
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000793E8 File Offset: 0x000775E8
		[SecurityCritical]
		private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int effectiveKeySize, int feedbackSize, CryptoAPITransformMode encryptMode)
		{
			int num = 0;
			int[] array = new int[10];
			object[] array2 = new object[10];
			if (mode == CipherMode.OFB)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
			}
			if (mode == CipherMode.CFB && feedbackSize != 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
			}
			if (rgbKey == null)
			{
				rgbKey = new byte[this.KeySizeValue / 8];
				Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
			}
			int num2 = rgbKey.Length * 8;
			if (!base.ValidKeySize(num2))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			array[num] = 19;
			if (this.EffectiveKeySizeValue == 0)
			{
				array2[num] = num2;
			}
			else
			{
				array2[num] = effectiveKeySize;
			}
			num++;
			if (mode != CipherMode.CBC)
			{
				array[num] = 4;
				array2[num] = mode;
				num++;
			}
			if (mode != CipherMode.ECB)
			{
				if (rgbIV == null)
				{
					rgbIV = new byte[8];
					Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
				}
				if (rgbIV.Length < 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
				}
				array[num] = 1;
				array2[num] = rgbIV;
				num++;
			}
			if (mode == CipherMode.OFB || mode == CipherMode.CFB)
			{
				array[num] = 5;
				array2[num] = feedbackSize;
				num++;
			}
			if (!Utils.HasAlgorithm(26114, num2))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgKeySizeNotAvailable", new object[]
				{
					num2
				}));
			}
			return new CryptoAPITransform(26114, num, array, array2, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, this.m_use40bitSalt, encryptMode);
		}

		// Token: 0x04000C74 RID: 3188
		private bool m_use40bitSalt;

		// Token: 0x04000C75 RID: 3189
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}

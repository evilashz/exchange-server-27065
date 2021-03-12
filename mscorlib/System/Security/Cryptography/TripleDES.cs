using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020002A0 RID: 672
	[ComVisible(true)]
	public abstract class TripleDES : SymmetricAlgorithm
	{
		// Token: 0x060023C6 RID: 9158 RVA: 0x000823CF File Offset: 0x000805CF
		protected TripleDES()
		{
			this.KeySizeValue = 192;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
			this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0008240C File Offset: 0x0008060C
		// (set) Token: 0x060023C8 RID: 9160 RVA: 0x0008243C File Offset: 0x0008063C
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					do
					{
						this.GenerateKey();
					}
					while (TripleDES.IsWeakKey(this.KeyValue));
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!base.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				if (TripleDES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000824AD File Offset: 0x000806AD
		public new static TripleDES Create()
		{
			return TripleDES.Create("System.Security.Cryptography.TripleDES");
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000824B9 File Offset: 0x000806B9
		public new static TripleDES Create(string str)
		{
			return (TripleDES)CryptoConfig.CreateFromName(str);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000824C8 File Offset: 0x000806C8
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!TripleDES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			return TripleDES.EqualBytes(array, 0, 8, 8) || (array.Length == 24 && TripleDES.EqualBytes(array, 8, 16, 8));
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00082518 File Offset: 0x00080718
		private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
		{
			if (start1 < 0)
			{
				throw new ArgumentOutOfRangeException("start1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start2 < 0)
			{
				throw new ArgumentOutOfRangeException("start2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start1 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (start2 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			for (int i = 0; i < count; i++)
			{
				if (rgbKey[start1 + i] != rgbKey[start2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000825A2 File Offset: 0x000807A2
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
		}

		// Token: 0x04000D00 RID: 3328
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000D01 RID: 3329
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 192, 64)
		};
	}
}

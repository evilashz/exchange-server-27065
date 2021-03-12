using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000284 RID: 644
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x0007D708 File Offset: 0x0007B908
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0007D710 File Offset: 0x0007B910
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0007D732 File Offset: 0x0007B932
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x0007D73A File Offset: 0x0007B93A
		public RandomNumberGenerator RNG
		{
			get
			{
				return this.RngValue;
			}
			set
			{
				this.RngValue = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0007D743 File Offset: 0x0007B943
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0007D746 File Offset: 0x0007B946
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0007D748 File Offset: 0x0007B948
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			byte[] array;
			if (this.OverridesDecrypt)
			{
				array = this._rsaKey.Decrypt(rgbIn, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				byte[] array2 = this._rsaKey.DecryptValue(rgbIn);
				int num = 2;
				while (num < array2.Length && array2[num] != 0)
				{
					num++;
				}
				if (num >= array2.Length)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_PKCS1Decoding"));
				}
				num++;
				array = new byte[array2.Length - num];
				Buffer.InternalBlockCopy(array2, num, array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0007D7DB File Offset: 0x0007B9DB
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0007D804 File Offset: 0x0007BA04
		private bool OverridesDecrypt
		{
			get
			{
				if (this._rsaOverridesDecrypt == null)
				{
					this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesDecrypt.Value;
			}
		}

		// Token: 0x04000CB1 RID: 3249
		private RSA _rsaKey;

		// Token: 0x04000CB2 RID: 3250
		private bool? _rsaOverridesDecrypt;

		// Token: 0x04000CB3 RID: 3251
		private RandomNumberGenerator RngValue;
	}
}

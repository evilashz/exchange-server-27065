using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000285 RID: 645
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x0007D864 File Offset: 0x0007BA64
		public RSAPKCS1KeyExchangeFormatter()
		{
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0007D86C File Offset: 0x0007BA6C
		public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0007D88E File Offset: 0x0007BA8E
		public override string Parameters
		{
			get
			{
				return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0007D895 File Offset: 0x0007BA95
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0007D89D File Offset: 0x0007BA9D
		public RandomNumberGenerator Rng
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

		// Token: 0x06002303 RID: 8963 RVA: 0x0007D8A6 File Offset: 0x0007BAA6
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0007D8D0 File Offset: 0x0007BAD0
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			byte[] result;
			if (this.OverridesEncrypt)
			{
				result = this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				int num = this._rsaKey.KeySize / 8;
				if (rgbData.Length + 11 > num)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_EncDataTooBig", new object[]
					{
						num - 11
					}));
				}
				byte[] array = new byte[num];
				if (this.RngValue == null)
				{
					this.RngValue = RandomNumberGenerator.Create();
				}
				this.Rng.GetNonZeroBytes(array);
				array[0] = 0;
				array[1] = 2;
				array[num - rgbData.Length - 1] = 0;
				Buffer.InternalBlockCopy(rgbData, 0, array, num - rgbData.Length, rgbData.Length);
				result = this._rsaKey.EncryptValue(array);
			}
			return result;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0007D9A3 File Offset: 0x0007BBA3
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007D9AC File Offset: 0x0007BBAC
		private bool OverridesEncrypt
		{
			get
			{
				if (this._rsaOverridesEncrypt == null)
				{
					this._rsaOverridesEncrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Encrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesEncrypt.Value;
			}
		}

		// Token: 0x04000CB4 RID: 3252
		private RandomNumberGenerator RngValue;

		// Token: 0x04000CB5 RID: 3253
		private RSA _rsaKey;

		// Token: 0x04000CB6 RID: 3254
		private bool? _rsaOverridesEncrypt;
	}
}

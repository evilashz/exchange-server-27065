using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000283 RID: 643
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x0007D5A4 File Offset: 0x0007B7A4
		public RSAOAEPKeyExchangeFormatter()
		{
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007D5AC File Offset: 0x0007B7AC
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0007D5CE File Offset: 0x0007B7CE
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x0007D5EA File Offset: 0x0007B7EA
		public byte[] Parameter
		{
			get
			{
				if (this.ParameterValue != null)
				{
					return (byte[])this.ParameterValue.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.ParameterValue = (byte[])value.Clone();
					return;
				}
				this.ParameterValue = null;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0007D608 File Offset: 0x0007B808
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0007D60B File Offset: 0x0007B80B
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0007D613 File Offset: 0x0007B813
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

		// Token: 0x060022F1 RID: 8945 RVA: 0x0007D61C File Offset: 0x0007B81C
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0007D644 File Offset: 0x0007B844
		[SecuritySafeCritical]
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this.OverridesEncrypt)
			{
				return this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepEncrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0007D69E File Offset: 0x0007B89E
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0007D6A8 File Offset: 0x0007B8A8
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

		// Token: 0x04000CAD RID: 3245
		private byte[] ParameterValue;

		// Token: 0x04000CAE RID: 3246
		private RSA _rsaKey;

		// Token: 0x04000CAF RID: 3247
		private bool? _rsaOverridesEncrypt;

		// Token: 0x04000CB0 RID: 3248
		private RandomNumberGenerator RngValue;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000282 RID: 642
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x0007D495 File Offset: 0x0007B695
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007D49D File Offset: 0x0007B69D
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x0007D4BF File Offset: 0x0007B6BF
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x0007D4C2 File Offset: 0x0007B6C2
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

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007D4C4 File Offset: 0x0007B6C4
		[SecuritySafeCritical]
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this.OverridesDecrypt)
			{
				return this._rsaKey.Decrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepDecrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007D519 File Offset: 0x0007B719
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0007D544 File Offset: 0x0007B744
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

		// Token: 0x04000CAB RID: 3243
		private RSA _rsaKey;

		// Token: 0x04000CAC RID: 3244
		private bool? _rsaOverridesDecrypt;
	}
}

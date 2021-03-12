using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x02000286 RID: 646
	[ComVisible(true)]
	public class RSAPKCS1SignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x06002307 RID: 8967 RVA: 0x0007DA0C File Offset: 0x0007BC0C
		public RSAPKCS1SignatureDeformatter()
		{
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x0007DA14 File Offset: 0x0007BC14
		public RSAPKCS1SignatureDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0007DA36 File Offset: 0x0007BC36
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesVerifyHash = null;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0007DA5E File Offset: 0x0007BC5E
		public override void SetHashAlgorithm(string strName)
		{
			this._strOID = CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0007DA70 File Offset: 0x0007BC70
		[SecuritySafeCritical]
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (this._strOID == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
			}
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				int algIdFromOid = X509Utils.GetAlgIdFromOid(this._strOID, OidGroup.HashAlgorithm);
				return ((RSACryptoServiceProvider)this._rsaKey).VerifyHash(rgbHash, algIdFromOid, rgbSignature);
			}
			if (this.OverridesVerifyHash)
			{
				HashAlgorithmName hashAlgorithm = Utils.OidToHashAlgorithmName(this._strOID);
				return this._rsaKey.VerifyHash(rgbHash, rgbSignature, hashAlgorithm, RSASignaturePadding.Pkcs1);
			}
			byte[] rhs = Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash);
			return Utils.CompareBigIntArrays(this._rsaKey.EncryptValue(rgbSignature), rhs);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x0007DB4C File Offset: 0x0007BD4C
		private bool OverridesVerifyHash
		{
			get
			{
				if (this._rsaOverridesVerifyHash == null)
				{
					this._rsaOverridesVerifyHash = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "VerifyHash", new Type[]
					{
						typeof(byte[]),
						typeof(byte[]),
						typeof(HashAlgorithmName),
						typeof(RSASignaturePadding)
					}));
				}
				return this._rsaOverridesVerifyHash.Value;
			}
		}

		// Token: 0x04000CB7 RID: 3255
		private RSA _rsaKey;

		// Token: 0x04000CB8 RID: 3256
		private string _strOID;

		// Token: 0x04000CB9 RID: 3257
		private bool? _rsaOverridesVerifyHash;
	}
}

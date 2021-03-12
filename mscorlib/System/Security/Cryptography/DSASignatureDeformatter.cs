using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x0200025F RID: 607
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x060021A1 RID: 8609 RVA: 0x000771B3 File Offset: 0x000753B3
		public DSASignatureDeformatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000771CC File Offset: 0x000753CC
		public DSASignatureDeformatter(AsymmetricAlgorithm key) : this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000771EE File Offset: 0x000753EE
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0007720A File Offset: 0x0007540A
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
			}
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00077230 File Offset: 0x00075430
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
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x04000C42 RID: 3138
		private DSA _dsaKey;

		// Token: 0x04000C43 RID: 3139
		private string _oid;
	}
}

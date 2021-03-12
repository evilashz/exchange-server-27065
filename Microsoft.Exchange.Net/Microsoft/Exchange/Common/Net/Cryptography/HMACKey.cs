using System;
using System.Security;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B9 RID: 1721
	public class HMACKey : CryptographicKey
	{
		// Token: 0x0600200C RID: 8204 RVA: 0x0003E1EC File Offset: 0x0003C3EC
		public HMACKey(CryptoKeyPayloadType payload, byte version, CryptoAlgorithm algorithm, DateTime activeDate, DateTime expireDate, SecureString encryptedKey = null) : base(payload, version, algorithm, activeDate, expireDate, encryptedKey)
		{
		}
	}
}

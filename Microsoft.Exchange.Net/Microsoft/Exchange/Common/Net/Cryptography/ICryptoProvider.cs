using System;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B7 RID: 1719
	public interface ICryptoProvider
	{
		// Token: 0x06002003 RID: 8195
		CryptographicKey GetKeyByPayload(CryptoKeyPayloadType payload);

		// Token: 0x06002004 RID: 8196
		CryptographicKey GetKeyByVersion(CryptoKeyPayloadType payload, byte version);
	}
}

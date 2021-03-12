using System;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B5 RID: 1717
	public static class CryptoKeyStore
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0003DB16 File Offset: 0x0003BD16
		public static void Initialize(ICryptoProvider provider)
		{
			CryptoKeyStore.provider = provider;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0003DB1E File Offset: 0x0003BD1E
		public static CryptographicKey GetKeyByPayload(CryptoKeyPayloadType payload)
		{
			return CryptoKeyStore.provider.GetKeyByPayload(payload);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0003DB2B File Offset: 0x0003BD2B
		public static CryptographicKey GetKeyByVersion(CryptoKeyPayloadType payload, byte version)
		{
			return CryptoKeyStore.provider.GetKeyByVersion(payload, version);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0003DB39 File Offset: 0x0003BD39
		public static bool IsValidKey(CryptographicKey key)
		{
			return false;
		}

		// Token: 0x04001EF7 RID: 7927
		private static ICryptoProvider provider = DefaultCryptoProvider.Provider;
	}
}

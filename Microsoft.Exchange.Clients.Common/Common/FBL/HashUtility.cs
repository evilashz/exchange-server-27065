using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Common.Net.Cryptography;

namespace Microsoft.Exchange.Clients.Common.FBL
{
	// Token: 0x02000037 RID: 55
	internal class HashUtility
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000B49C File Offset: 0x0000969C
		public static byte[] ComputeHash(string[] hashComponents, CryptoKeyPayloadType payloadKey, byte version)
		{
			HMAC hmacforCryptoKey = HashUtility.GetHMACForCryptoKey(payloadKey, version);
			byte[] bytes = Encoding.UTF8.GetBytes(string.Join(string.Empty, hashComponents));
			return hmacforCryptoKey.ComputeHash(bytes);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B4CE File Offset: 0x000096CE
		public static byte[] ComputeHash(string[] hashComponents, CryptoKeyPayloadType payloadKey)
		{
			return HashUtility.ComputeHash(hashComponents, payloadKey, 1);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B4D8 File Offset: 0x000096D8
		private static HMAC GetHMACForCryptoKey(CryptoKeyPayloadType payloadKey, byte version)
		{
			HMAC hmac = null;
			CryptographicKey keyByPayload = CryptoKeyStore.GetKeyByPayload(payloadKey);
			if (version == 0)
			{
				hmac = HMAC.Create("HMACSHA1");
				hmac.Key = keyByPayload.Key;
			}
			else if (version == 1)
			{
				hmac = HMAC.Create(keyByPayload.Algorithm.Name);
				hmac.Key = keyByPayload.Key;
			}
			return hmac;
		}

		// Token: 0x04000303 RID: 771
		public const byte CurrentVersion = 1;

		// Token: 0x04000304 RID: 772
		private const byte Sha1Version = 0;

		// Token: 0x04000305 RID: 773
		private const byte Sha256Version = 1;
	}
}

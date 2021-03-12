using System;
using System.Security;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B3 RID: 1715
	public abstract class CryptographicKey
	{
		// Token: 0x06001FDC RID: 8156 RVA: 0x0003D4FC File Offset: 0x0003B6FC
		public CryptographicKey(CryptoKeyPayloadType payload, byte version, CryptoAlgorithm algorithm, DateTime activeDate, DateTime expireDate, SecureString encryptedKey = null)
		{
			this.Payload = payload;
			this.Version = version;
			this.Algorithm = algorithm;
			this.ActiveDate = activeDate;
			this.ExpireDate = expireDate;
			this.EncryptedKey = encryptedKey;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0003D531 File Offset: 0x0003B731
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x0003D539 File Offset: 0x0003B739
		public byte Version { get; private set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0003D542 File Offset: 0x0003B742
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0003D54A File Offset: 0x0003B74A
		public SecureString EncryptedKey { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0003D553 File Offset: 0x0003B753
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x0003D55B File Offset: 0x0003B75B
		public CryptoKeyPayloadType Payload { get; private set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0003D564 File Offset: 0x0003B764
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0003D56C File Offset: 0x0003B76C
		public CryptoAlgorithm Algorithm { get; private set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0003D575 File Offset: 0x0003B775
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0003D57D File Offset: 0x0003B77D
		public DateTime ActiveDate { get; private set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0003D586 File Offset: 0x0003B786
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0003D58E File Offset: 0x0003B78E
		public DateTime ExpireDate { get; private set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0003D597 File Offset: 0x0003B797
		public byte[] Key
		{
			get
			{
				return Convert.FromBase64String(TextUtil.ConvertToUnsecureString(this.EncryptedKey));
			}
		}
	}
}

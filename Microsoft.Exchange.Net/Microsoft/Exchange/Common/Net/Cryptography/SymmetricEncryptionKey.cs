using System;
using System.Security;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006BA RID: 1722
	public class SymmetricEncryptionKey : CryptographicKey
	{
		// Token: 0x0600200D RID: 8205 RVA: 0x0003E1FD File Offset: 0x0003C3FD
		public SymmetricEncryptionKey(CryptoKeyPayloadType payload, byte version, CryptoAlgorithm algorithm, DateTime activeDate, DateTime expireDate, SecureString encryptedKey = null, string initializationVector = null) : base(payload, version, algorithm, activeDate, expireDate, encryptedKey)
		{
			this.InitializationVector = initializationVector;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0003E216 File Offset: 0x0003C416
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x0003E21E File Offset: 0x0003C41E
		public string InitializationVector { get; set; }
	}
}

using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D9 RID: 985
	internal class E4eDecryptionHelperV2 : E4eDecryptionHelper
	{
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B36C1 File Offset: 0x000B18C1
		internal new static E4eDecryptionHelperV2 Instance
		{
			get
			{
				if (E4eDecryptionHelperV2.instance == null)
				{
					E4eDecryptionHelperV2.instance = new E4eDecryptionHelperV2();
				}
				return E4eDecryptionHelperV2.instance;
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000B36DC File Offset: 0x000B18DC
		protected override bool VerifySignature(byte[] signatureByteArray, RSACryptoServiceProvider csp, byte[] data)
		{
			bool result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] rgbHash = sha256Cng.ComputeHash(data);
				result = csp.VerifyHash(rgbHash, CryptoConfig.MapNameToOID("SHA256"), signatureByteArray);
			}
			return result;
		}

		// Token: 0x04001662 RID: 5730
		private static E4eDecryptionHelperV2 instance;
	}
}

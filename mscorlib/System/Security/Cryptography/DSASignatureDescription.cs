using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200029E RID: 670
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x0008200A File Offset: 0x0008020A
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}

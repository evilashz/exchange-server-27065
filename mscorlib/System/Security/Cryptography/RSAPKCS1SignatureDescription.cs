using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000299 RID: 665
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x00081F44 File Offset: 0x00080144
		protected RSAPKCS1SignatureDescription(string hashAlgorithm, string digestAlgorithm)
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSA";
			base.DigestAlgorithm = digestAlgorithm;
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00081F7C File Offset: 0x0008017C
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = base.CreateDeformatter(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00081FA0 File Offset: 0x000801A0
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = base.CreateFormatter(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x04000CF6 RID: 3318
		private string _hashAlgorithm;
	}
}

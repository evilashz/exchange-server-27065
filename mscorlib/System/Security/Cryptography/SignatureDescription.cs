using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000298 RID: 664
	[ComVisible(true)]
	public class SignatureDescription
	{
		// Token: 0x06002394 RID: 9108 RVA: 0x00081E2E File Offset: 0x0008002E
		public SignatureDescription()
		{
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00081E38 File Offset: 0x00080038
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00081E9D File Offset: 0x0008009D
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x00081EA5 File Offset: 0x000800A5
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00081EAE File Offset: 0x000800AE
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x00081EB6 File Offset: 0x000800B6
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x00081EBF File Offset: 0x000800BF
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x00081EC7 File Offset: 0x000800C7
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x00081ED0 File Offset: 0x000800D0
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x00081ED8 File Offset: 0x000800D8
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00081EE4 File Offset: 0x000800E4
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00081F0C File Offset: 0x0008010C
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00081F32 File Offset: 0x00080132
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x04000CF2 RID: 3314
		private string _strKey;

		// Token: 0x04000CF3 RID: 3315
		private string _strDigest;

		// Token: 0x04000CF4 RID: 3316
		private string _strFormatter;

		// Token: 0x04000CF5 RID: 3317
		private string _strDeformatter;
	}
}

using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000AC RID: 172
	public enum TlsAuthLevel
	{
		// Token: 0x0400029F RID: 671
		[LocDescription(DataStrings.IDs.TlsAuthLevelEncryptionOnly)]
		EncryptionOnly = 1,
		// Token: 0x040002A0 RID: 672
		[LocDescription(DataStrings.IDs.TlsAuthLevelCertificateValidation)]
		CertificateValidation,
		// Token: 0x040002A1 RID: 673
		[LocDescription(DataStrings.IDs.TlsAuthLevelDomainValidation)]
		DomainValidation = 4
	}
}

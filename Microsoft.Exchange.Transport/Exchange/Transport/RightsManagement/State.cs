using System;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003EA RID: 1002
	internal enum State
	{
		// Token: 0x040016B4 RID: 5812
		BeginAcquireTenantLicenses,
		// Token: 0x040016B5 RID: 5813
		AcquireTenantLicensesCallback,
		// Token: 0x040016B6 RID: 5814
		AcquireTenantLicensesFailed,
		// Token: 0x040016B7 RID: 5815
		BeginAcquireRMSTemplate,
		// Token: 0x040016B8 RID: 5816
		AcquireRMSTemplateCallback,
		// Token: 0x040016B9 RID: 5817
		AcquireRMSTemplateFailed,
		// Token: 0x040016BA RID: 5818
		Encrypted,
		// Token: 0x040016BB RID: 5819
		EncryptionFailed,
		// Token: 0x040016BC RID: 5820
		UnexpectedExitAcquireTenantLicensesCallback,
		// Token: 0x040016BD RID: 5821
		UnexpectedExitAcquireRmsTemplateCallback,
		// Token: 0x040016BE RID: 5822
		UnexpectedExceptionInEncryption
	}
}

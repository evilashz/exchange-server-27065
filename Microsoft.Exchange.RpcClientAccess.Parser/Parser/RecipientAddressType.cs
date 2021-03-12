using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000079 RID: 121
	internal enum RecipientAddressType : ushort
	{
		// Token: 0x0400018D RID: 397
		None,
		// Token: 0x0400018E RID: 398
		Exchange,
		// Token: 0x0400018F RID: 399
		MicrosoftMail,
		// Token: 0x04000190 RID: 400
		Smtp,
		// Token: 0x04000191 RID: 401
		Fax,
		// Token: 0x04000192 RID: 402
		ProfessionalOfficeSystem,
		// Token: 0x04000193 RID: 403
		MapiPrivateDistributionList,
		// Token: 0x04000194 RID: 404
		DosPrivateDistributionList,
		// Token: 0x04000195 RID: 405
		Other = 32768,
		// Token: 0x04000196 RID: 406
		ValidMask = 32775
	}
}

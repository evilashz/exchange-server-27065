using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000215 RID: 533
	internal enum ConnectorMatchResult
	{
		// Token: 0x04000B8B RID: 2955
		Success,
		// Token: 0x04000B8C RID: 2956
		InvalidSmtpAddress,
		// Token: 0x04000B8D RID: 2957
		InvalidX400Address,
		// Token: 0x04000B8E RID: 2958
		MaxMessageSizeExceeded,
		// Token: 0x04000B8F RID: 2959
		NoAddressMatch
	}
}

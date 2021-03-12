using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000B9 RID: 185
	internal enum SyncSvcEndPointType
	{
		// Token: 0x04000279 RID: 633
		[EnumMember]
		RestOAuth,
		// Token: 0x0400027A RID: 634
		[EnumMember]
		SoapOAuth,
		// Token: 0x0400027B RID: 635
		[EnumMember]
		SoapCert
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000355 RID: 853
	public enum ClientCertAuthTypes
	{
		// Token: 0x0400180B RID: 6155
		[LocDescription(DirectoryStrings.IDs.ClientCertAuthIgnore)]
		Ignore,
		// Token: 0x0400180C RID: 6156
		[LocDescription(DirectoryStrings.IDs.ClientCertAuthAccepted)]
		Accepted,
		// Token: 0x0400180D RID: 6157
		[LocDescription(DirectoryStrings.IDs.ClientCertAuthRequired)]
		Required
	}
}

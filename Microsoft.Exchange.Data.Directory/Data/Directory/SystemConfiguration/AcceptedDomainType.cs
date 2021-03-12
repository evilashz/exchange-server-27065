using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F4 RID: 756
	public enum AcceptedDomainType
	{
		// Token: 0x040015CD RID: 5581
		[LocDescription(DirectoryStrings.IDs.Authoritative)]
		Authoritative,
		// Token: 0x040015CE RID: 5582
		[LocDescription(DirectoryStrings.IDs.ExternalRelay)]
		ExternalRelay,
		// Token: 0x040015CF RID: 5583
		[LocDescription(DirectoryStrings.IDs.InternalRelay)]
		InternalRelay
	}
}

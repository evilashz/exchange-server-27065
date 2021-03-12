using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000016 RID: 22
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public enum ReadFlag
	{
		// Token: 0x0400004C RID: 76
		[EnumMember]
		DefaultBehavior,
		// Token: 0x0400004D RID: 77
		[EnumMember]
		HitPrimaryIfDataNotFound
	}
}

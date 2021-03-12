using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000097 RID: 151
	[DataContract(Name = "Status")]
	internal enum Status
	{
		// Token: 0x040002A2 RID: 674
		[EnumMember]
		Pending,
		// Token: 0x040002A3 RID: 675
		[EnumMember]
		Failed,
		// Token: 0x040002A4 RID: 676
		[EnumMember]
		Succeeded
	}
}

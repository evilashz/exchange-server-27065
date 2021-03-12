using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005E RID: 94
	[DataContract(Name = "onlineMeetingRel")]
	internal enum OnlineMeetingRel
	{
		// Token: 0x040001CA RID: 458
		[EnumMember]
		MyOnlineMeetings,
		// Token: 0x040001CB RID: 459
		[EnumMember]
		MyAssignedOnlineMeeting
	}
}

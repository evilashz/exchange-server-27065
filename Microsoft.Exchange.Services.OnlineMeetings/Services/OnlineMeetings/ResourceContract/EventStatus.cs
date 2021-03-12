using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008B RID: 139
	[DataContract(Name = "EventStatus")]
	internal enum EventStatus
	{
		// Token: 0x04000283 RID: 643
		Failure = -1,
		// Token: 0x04000284 RID: 644
		Undefined,
		// Token: 0x04000285 RID: 645
		Success
	}
}

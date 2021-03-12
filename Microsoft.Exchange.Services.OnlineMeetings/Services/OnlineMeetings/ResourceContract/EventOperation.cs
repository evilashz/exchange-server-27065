using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008C RID: 140
	[DataContract(Name = "EventOperation")]
	internal enum EventOperation
	{
		// Token: 0x04000287 RID: 647
		Undefined,
		// Token: 0x04000288 RID: 648
		Completed,
		// Token: 0x04000289 RID: 649
		Added,
		// Token: 0x0400028A RID: 650
		Updated,
		// Token: 0x0400028B RID: 651
		Deleted
	}
}

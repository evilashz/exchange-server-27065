using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007B RID: 123
	[DataContract(Name = "applicationState")]
	internal enum ApplicationState
	{
		// Token: 0x0400021F RID: 543
		[EnumMember]
		Idle,
		// Token: 0x04000220 RID: 544
		[EnumMember]
		Establishing,
		// Token: 0x04000221 RID: 545
		[EnumMember]
		Established,
		// Token: 0x04000222 RID: 546
		[EnumMember]
		Terminating,
		// Token: 0x04000223 RID: 547
		[EnumMember]
		Terminated
	}
}

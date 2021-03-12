using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000080 RID: 128
	[DataContract(Name = "applicationType")]
	internal enum ApplicationType
	{
		// Token: 0x0400023A RID: 570
		[EnumMember]
		Browser,
		// Token: 0x0400023B RID: 571
		[EnumMember]
		Phone,
		// Token: 0x0400023C RID: 572
		[EnumMember]
		Tablet
	}
}

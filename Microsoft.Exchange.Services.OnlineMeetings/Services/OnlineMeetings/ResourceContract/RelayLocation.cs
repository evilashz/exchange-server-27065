using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009C RID: 156
	[Flags]
	[DataContract(Name = "RelayLocation")]
	internal enum RelayLocation
	{
		// Token: 0x040002A9 RID: 681
		[EnumMember]
		None = 0,
		// Token: 0x040002AA RID: 682
		[EnumMember]
		Internet = 1,
		// Token: 0x040002AB RID: 683
		[EnumMember]
		Intranet = 2,
		// Token: 0x040002AC RID: 684
		[EnumMember]
		All = 3
	}
}

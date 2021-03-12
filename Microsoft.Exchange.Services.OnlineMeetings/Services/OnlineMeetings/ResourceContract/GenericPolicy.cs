using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006B RID: 107
	[DataContract(Name = "GenericPolicy")]
	internal enum GenericPolicy
	{
		// Token: 0x040001ED RID: 493
		[EnumMember]
		None,
		// Token: 0x040001EE RID: 494
		[EnumMember]
		Disabled,
		// Token: 0x040001EF RID: 495
		[EnumMember]
		Enabled
	}
}

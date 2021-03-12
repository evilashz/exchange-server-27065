using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005D RID: 93
	[DataContract(Name = "phoneUserAdmission")]
	internal enum PhoneUserAdmission
	{
		// Token: 0x040001C7 RID: 455
		[EnumMember]
		Disabled,
		// Token: 0x040001C8 RID: 456
		[EnumMember]
		Enabled
	}
}

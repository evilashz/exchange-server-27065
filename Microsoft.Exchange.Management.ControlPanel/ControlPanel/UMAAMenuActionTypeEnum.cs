using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A7 RID: 1191
	[DataContract]
	public enum UMAAMenuActionTypeEnum
	{
		// Token: 0x04002752 RID: 10066
		[EnumMember]
		None,
		// Token: 0x04002753 RID: 10067
		[EnumMember]
		TransferToExtension,
		// Token: 0x04002754 RID: 10068
		[EnumMember]
		TransferToAutoAttendant,
		// Token: 0x04002755 RID: 10069
		[EnumMember]
		LeaveVoicemailFor,
		// Token: 0x04002756 RID: 10070
		[EnumMember]
		AnnounceBusinessLocation,
		// Token: 0x04002757 RID: 10071
		[EnumMember]
		AnnounceBusinessHours
	}
}

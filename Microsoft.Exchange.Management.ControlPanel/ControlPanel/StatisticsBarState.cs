using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F3 RID: 243
	[DataContract]
	public enum StatisticsBarState
	{
		// Token: 0x04001C16 RID: 7190
		[EnumMember]
		Normal,
		// Token: 0x04001C17 RID: 7191
		[EnumMember]
		Warning,
		// Token: 0x04001C18 RID: 7192
		[EnumMember]
		Exceeded
	}
}

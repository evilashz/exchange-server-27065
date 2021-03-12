using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000046 RID: 70
	[DataContract]
	public enum CmdletStatus
	{
		// Token: 0x04001AD3 RID: 6867
		[EnumMember]
		Failed,
		// Token: 0x04001AD4 RID: 6868
		[EnumMember]
		Stopped,
		// Token: 0x04001AD5 RID: 6869
		[EnumMember]
		Completed
	}
}

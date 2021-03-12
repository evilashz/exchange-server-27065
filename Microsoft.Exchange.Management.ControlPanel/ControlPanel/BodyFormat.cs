using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C4 RID: 708
	[DataContract]
	public enum BodyFormat
	{
		// Token: 0x040021E1 RID: 8673
		[EnumMember]
		PlainText = 1,
		// Token: 0x040021E2 RID: 8674
		[EnumMember]
		Html,
		// Token: 0x040021E3 RID: 8675
		[EnumMember]
		Rtf
	}
}

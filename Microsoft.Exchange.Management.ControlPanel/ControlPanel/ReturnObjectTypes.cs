using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006ED RID: 1773
	[DataContract]
	public enum ReturnObjectTypes
	{
		// Token: 0x040031AE RID: 12718
		[EnumMember]
		Full,
		// Token: 0x040031AF RID: 12719
		[EnumMember]
		PartialForList,
		// Token: 0x040031B0 RID: 12720
		[EnumMember]
		None
	}
}

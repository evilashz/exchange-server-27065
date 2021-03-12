using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A5B RID: 2651
	[DataContract]
	public enum LocationSource
	{
		// Token: 0x04002AC4 RID: 10948
		[EnumMember]
		None,
		// Token: 0x04002AC5 RID: 10949
		[EnumMember]
		LocationServices,
		// Token: 0x04002AC6 RID: 10950
		[EnumMember]
		PhonebookServices,
		// Token: 0x04002AC7 RID: 10951
		[EnumMember]
		Device,
		// Token: 0x04002AC8 RID: 10952
		[EnumMember]
		Contact,
		// Token: 0x04002AC9 RID: 10953
		[EnumMember]
		Resource
	}
}

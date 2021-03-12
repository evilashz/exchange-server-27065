using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000063 RID: 99
	[DataContract(Name = "ExchangeVersion", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public enum ExchangeVersion
	{
		// Token: 0x040002C1 RID: 705
		[EnumMember]
		Exchange2010,
		// Token: 0x040002C2 RID: 706
		[EnumMember]
		Exchange2010_SP1,
		// Token: 0x040002C3 RID: 707
		[EnumMember]
		Exchange2010_SP2,
		// Token: 0x040002C4 RID: 708
		[EnumMember]
		Exchange2012,
		// Token: 0x040002C5 RID: 709
		[EnumMember]
		Exchange2013,
		// Token: 0x040002C6 RID: 710
		[EnumMember]
		Exchange2013_SP1
	}
}

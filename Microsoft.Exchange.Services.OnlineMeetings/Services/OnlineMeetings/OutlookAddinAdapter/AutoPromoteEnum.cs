using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000B7 RID: 183
	[XmlType("AutoPromoteEnum")]
	public enum AutoPromoteEnum
	{
		// Token: 0x040002D8 RID: 728
		[EnumMember]
		[XmlEnum]
		None,
		// Token: 0x040002D9 RID: 729
		[EnumMember]
		[XmlEnum]
		Company,
		// Token: 0x040002DA RID: 730
		[EnumMember]
		[XmlEnum]
		Everyone
	}
}

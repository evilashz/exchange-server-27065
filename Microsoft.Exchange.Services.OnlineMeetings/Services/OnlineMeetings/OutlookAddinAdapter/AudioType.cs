using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C5 RID: 197
	[XmlType("AudioType")]
	public enum AudioType
	{
		// Token: 0x04000320 RID: 800
		[XmlEnum("none")]
		[EnumMember]
		None,
		// Token: 0x04000321 RID: 801
		[EnumMember]
		[XmlEnum("caa")]
		CAA,
		// Token: 0x04000322 RID: 802
		[XmlEnum("acp")]
		[EnumMember]
		ACP
	}
}

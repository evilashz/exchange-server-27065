using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public enum FilterKeyType
	{
		// Token: 0x040003F2 RID: 1010
		[XmlEnum(Name = "Subject")]
		Subject,
		// Token: 0x040003F3 RID: 1011
		[XmlEnum(Name = "From Name")]
		From_Name,
		// Token: 0x040003F4 RID: 1012
		[XmlEnum(Name = "From Address")]
		From_Address,
		// Token: 0x040003F5 RID: 1013
		[XmlEnum(Name = "To or CC Line")]
		To_or_CC_Line
	}
}

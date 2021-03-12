using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public enum FilterOperatorType
	{
		// Token: 0x040003FB RID: 1019
		[XmlEnum(Name = "Contains")]
		Contains,
		// Token: 0x040003FC RID: 1020
		[XmlEnum(Name = "Does not contain")]
		Does_not_contain,
		// Token: 0x040003FD RID: 1021
		[XmlEnum(Name = "Contains word")]
		Contains_word,
		// Token: 0x040003FE RID: 1022
		[XmlEnum(Name = "Starts with")]
		Starts_with,
		// Token: 0x040003FF RID: 1023
		[XmlEnum(Name = "Ends with")]
		Ends_with,
		// Token: 0x04000400 RID: 1024
		[XmlEnum(Name = "Equals")]
		Equals
	}
}

using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	public enum FilterOperatorType
	{
		// Token: 0x040004E0 RID: 1248
		[XmlEnum(Name = "Contains")]
		Contains,
		// Token: 0x040004E1 RID: 1249
		[XmlEnum(Name = "Does not contain")]
		Does_not_contain,
		// Token: 0x040004E2 RID: 1250
		[XmlEnum(Name = "Contains word")]
		Contains_word,
		// Token: 0x040004E3 RID: 1251
		[XmlEnum(Name = "Starts with")]
		Starts_with,
		// Token: 0x040004E4 RID: 1252
		[XmlEnum(Name = "Ends with")]
		Ends_with,
		// Token: 0x040004E5 RID: 1253
		[XmlEnum(Name = "Equals")]
		Equals
	}
}

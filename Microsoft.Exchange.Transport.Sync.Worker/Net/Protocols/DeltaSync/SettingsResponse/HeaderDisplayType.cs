using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public enum HeaderDisplayType
	{
		// Token: 0x040004F6 RID: 1270
		[XmlEnum(Name = "No Header")]
		No_Header,
		// Token: 0x040004F7 RID: 1271
		[XmlEnum(Name = "Basic")]
		Basic,
		// Token: 0x040004F8 RID: 1272
		[XmlEnum(Name = "Full")]
		Full,
		// Token: 0x040004F9 RID: 1273
		[XmlEnum(Name = "Advanced")]
		Advanced
	}
}

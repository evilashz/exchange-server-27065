using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	public enum JunkLevelType
	{
		// Token: 0x040004E7 RID: 1255
		[XmlEnum(Name = "Off")]
		Off,
		// Token: 0x040004E8 RID: 1256
		[XmlEnum(Name = "Low")]
		Low,
		// Token: 0x040004E9 RID: 1257
		[XmlEnum(Name = "High")]
		High,
		// Token: 0x040004EA RID: 1258
		[XmlEnum(Name = "Exclusive")]
		Exclusive
	}
}

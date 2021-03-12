using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public enum ReplyIndicatorType
	{
		// Token: 0x040004F2 RID: 1266
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x040004F3 RID: 1267
		[XmlEnum(Name = "Line")]
		Line,
		// Token: 0x040004F4 RID: 1268
		[XmlEnum(Name = "Arrow")]
		Arrow
	}
}

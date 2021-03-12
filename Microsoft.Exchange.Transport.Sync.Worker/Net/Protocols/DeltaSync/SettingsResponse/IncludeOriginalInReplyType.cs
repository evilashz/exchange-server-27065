using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public enum IncludeOriginalInReplyType
	{
		// Token: 0x040004EC RID: 1260
		[XmlEnum(Name = "Auto")]
		Auto,
		// Token: 0x040004ED RID: 1261
		[XmlEnum(Name = "Manual")]
		Manual
	}
}

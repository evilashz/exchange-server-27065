using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public enum ReplyIndicatorType
	{
		// Token: 0x0400040D RID: 1037
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x0400040E RID: 1038
		[XmlEnum(Name = "Line")]
		Line,
		// Token: 0x0400040F RID: 1039
		[XmlEnum(Name = "Arrow")]
		Arrow
	}
}

using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public enum IncludeOriginalInReplyType
	{
		// Token: 0x04000407 RID: 1031
		[XmlEnum(Name = "Auto")]
		Auto,
		// Token: 0x04000408 RID: 1032
		[XmlEnum(Name = "Manual")]
		Manual
	}
}

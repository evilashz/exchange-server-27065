using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public enum AlertStateType
	{
		// Token: 0x040004FB RID: 1275
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x040004FC RID: 1276
		[XmlEnum(Name = "Always")]
		Always,
		// Token: 0x040004FD RID: 1277
		[XmlEnum(Name = "Rules")]
		Rules
	}
}

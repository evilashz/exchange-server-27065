using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public enum JunkLevelType
	{
		// Token: 0x04000402 RID: 1026
		[XmlEnum(Name = "Off")]
		Off,
		// Token: 0x04000403 RID: 1027
		[XmlEnum(Name = "Low")]
		Low,
		// Token: 0x04000404 RID: 1028
		[XmlEnum(Name = "High")]
		High,
		// Token: 0x04000405 RID: 1029
		[XmlEnum(Name = "Exclusive")]
		Exclusive
	}
}

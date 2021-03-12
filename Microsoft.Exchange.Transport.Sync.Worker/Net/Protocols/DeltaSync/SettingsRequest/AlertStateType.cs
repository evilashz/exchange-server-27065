using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public enum AlertStateType
	{
		// Token: 0x04000416 RID: 1046
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x04000417 RID: 1047
		[XmlEnum(Name = "Always")]
		Always,
		// Token: 0x04000418 RID: 1048
		[XmlEnum(Name = "Rules")]
		Rules
	}
}

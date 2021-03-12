using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public enum JunkMailDestinationType
	{
		// Token: 0x040004EF RID: 1263
		[XmlEnum(Name = "Immediate Deletion")]
		Immediate_Deletion,
		// Token: 0x040004F0 RID: 1264
		[XmlEnum(Name = "Junk Mail")]
		Junk_Mail
	}
}

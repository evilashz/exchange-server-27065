using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public enum RunWhenType
	{
		// Token: 0x040004D9 RID: 1241
		[XmlEnum(Name = "MessageReceived")]
		MessageReceived,
		// Token: 0x040004DA RID: 1242
		[XmlEnum(Name = "MessageSent")]
		MessageSent
	}
}

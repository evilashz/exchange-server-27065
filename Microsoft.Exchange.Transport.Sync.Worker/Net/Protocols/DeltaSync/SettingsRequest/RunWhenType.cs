using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public enum RunWhenType
	{
		// Token: 0x040003EF RID: 1007
		[XmlEnum(Name = "MessageReceived")]
		MessageReceived,
		// Token: 0x040003F0 RID: 1008
		[XmlEnum(Name = "MessageSent")]
		MessageSent
	}
}

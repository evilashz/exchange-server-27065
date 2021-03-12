using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public enum ForwardingMode
	{
		// Token: 0x040003F7 RID: 1015
		[XmlEnum(Name = "NoForwarding")]
		NoForwarding,
		// Token: 0x040003F8 RID: 1016
		[XmlEnum(Name = "ForwardOnly")]
		ForwardOnly,
		// Token: 0x040003F9 RID: 1017
		[XmlEnum(Name = "StoreAndForward")]
		StoreAndForward
	}
}

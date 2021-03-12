using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public enum ForwardingMode
	{
		// Token: 0x040004DC RID: 1244
		[XmlEnum(Name = "NoForwarding")]
		NoForwarding,
		// Token: 0x040004DD RID: 1245
		[XmlEnum(Name = "ForwardOnly")]
		ForwardOnly,
		// Token: 0x040004DE RID: 1246
		[XmlEnum(Name = "StoreAndForward")]
		StoreAndForward
	}
}

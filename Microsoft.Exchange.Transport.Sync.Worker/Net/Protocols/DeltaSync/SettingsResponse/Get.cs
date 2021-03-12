using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000149 RID: 329
	[XmlType(TypeName = "Get", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Get
	{
		// Token: 0x04000534 RID: 1332
		[XmlAnyElement]
		public XmlElement Any;
	}
}

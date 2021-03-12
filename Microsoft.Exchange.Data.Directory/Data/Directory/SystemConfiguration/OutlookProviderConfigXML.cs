using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000533 RID: 1331
	[XmlType(TypeName = "OutlookProviderConfig")]
	[Serializable]
	public sealed class OutlookProviderConfigXML : XMLSerializableBase
	{
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x000E21AC File Offset: 0x000E03AC
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x000E21B4 File Offset: 0x000E03B4
		[XmlElement(ElementName = "RequiredClientVersions")]
		public ClientVersionCollection RequiredClientVersions { get; set; }
	}
}

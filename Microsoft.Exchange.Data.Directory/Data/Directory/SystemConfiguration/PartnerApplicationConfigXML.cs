using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000695 RID: 1685
	[XmlType(TypeName = "PartnerApplicationConfig")]
	[Serializable]
	public sealed class PartnerApplicationConfigXML : XMLSerializableBase
	{
		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0012082E File Offset: 0x0011EA2E
		// (set) Token: 0x06004E61 RID: 20065 RVA: 0x00120836 File Offset: 0x0011EA36
		[XmlElement(ElementName = "IssuerIdentifier")]
		public string IssuerIdentifier { get; set; }

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x0012083F File Offset: 0x0011EA3F
		// (set) Token: 0x06004E63 RID: 20067 RVA: 0x00120847 File Offset: 0x0011EA47
		[XmlElement(ElementName = "AppOnlyPermissions")]
		public string[] AppOnlyPermissions { get; set; }

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00120850 File Offset: 0x0011EA50
		// (set) Token: 0x06004E65 RID: 20069 RVA: 0x00120858 File Offset: 0x0011EA58
		[XmlElement(ElementName = "ActAsPermissions")]
		public string[] ActAsPermissions { get; set; }
	}
}

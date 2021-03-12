using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000375 RID: 885
	[XmlType(TypeName = "ADOwaVirtualDirectoryConfig")]
	[Serializable]
	public sealed class ADOwaVirtualDirectoryConfigXML : XMLSerializableBase
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000AD5FD File Offset: 0x000AB7FD
		// (set) Token: 0x06002923 RID: 10531 RVA: 0x000AD605 File Offset: 0x000AB805
		[XmlElement(ElementName = "IsPublic")]
		public bool IsPublic { get; set; }
	}
}

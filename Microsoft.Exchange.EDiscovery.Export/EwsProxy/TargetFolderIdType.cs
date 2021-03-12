using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B8 RID: 440
	[XmlInclude(typeof(UserConfigurationNameType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TargetFolderIdType
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00024F9A File Offset: 0x0002319A
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00024FA2 File Offset: 0x000231A2
		[XmlElement("FolderId", typeof(FolderIdType))]
		[XmlElement("AddressListId", typeof(AddressListIdType))]
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderIdType))]
		public BaseFolderIdType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000D39 RID: 3385
		private BaseFolderIdType itemField;
	}
}

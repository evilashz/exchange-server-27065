using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000299 RID: 665
	[DesignerCategory("code")]
	[XmlInclude(typeof(UserConfigurationNameType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TargetFolderIdType
	{
		// Token: 0x0400118B RID: 4491
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderIdType))]
		[XmlElement("FolderId", typeof(FolderIdType))]
		[XmlElement("AddressListId", typeof(AddressListIdType))]
		public BaseFolderIdType Item;
	}
}

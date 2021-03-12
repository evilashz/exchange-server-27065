using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E3 RID: 995
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FolderChangeType
	{
		// Token: 0x04001591 RID: 5521
		[XmlElement("FolderId", typeof(FolderIdType))]
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderIdType))]
		public BaseFolderIdType Item;

		// Token: 0x04001592 RID: 5522
		[XmlArrayItem("SetFolderField", typeof(SetFolderFieldType), IsNullable = false)]
		[XmlArrayItem("AppendToFolderField", typeof(AppendToFolderFieldType), IsNullable = false)]
		[XmlArrayItem("DeleteFolderField", typeof(DeleteFolderFieldType), IsNullable = false)]
		public FolderChangeDescriptionType[] Updates;
	}
}

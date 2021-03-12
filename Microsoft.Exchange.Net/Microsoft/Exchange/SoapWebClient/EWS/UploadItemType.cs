using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003EB RID: 1003
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UploadItemType
	{
		// Token: 0x040015A2 RID: 5538
		public FolderIdType ParentFolderId;

		// Token: 0x040015A3 RID: 5539
		public ItemIdType ItemId;

		// Token: 0x040015A4 RID: 5540
		[XmlElement(DataType = "base64Binary")]
		public byte[] Data;

		// Token: 0x040015A5 RID: 5541
		[XmlAttribute]
		public CreateActionType CreateAction;

		// Token: 0x040015A6 RID: 5542
		[XmlAttribute]
		public bool IsAssociated;

		// Token: 0x040015A7 RID: 5543
		[XmlIgnore]
		public bool IsAssociatedSpecified;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200047E RID: 1150
	[XmlInclude(typeof(MoveFolderType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(CopyFolderType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class BaseMoveCopyFolderType : BaseRequestType
	{
		// Token: 0x04001798 RID: 6040
		public TargetFolderIdType ToFolderId;

		// Token: 0x04001799 RID: 6041
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] FolderIds;
	}
}

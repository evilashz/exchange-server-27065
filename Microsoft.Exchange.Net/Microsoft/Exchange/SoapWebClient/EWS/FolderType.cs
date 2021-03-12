using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002F2 RID: 754
	[XmlInclude(typeof(SearchFolderType))]
	[XmlInclude(typeof(TasksFolderType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FolderType : BaseFolderType
	{
		// Token: 0x040012D2 RID: 4818
		public PermissionSetType PermissionSet;

		// Token: 0x040012D3 RID: 4819
		public int UnreadCount;

		// Token: 0x040012D4 RID: 4820
		[XmlIgnore]
		public bool UnreadCountSpecified;
	}
}

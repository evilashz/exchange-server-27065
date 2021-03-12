using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200047D RID: 1149
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateFolderType : BaseRequestType
	{
		// Token: 0x04001797 RID: 6039
		[XmlArrayItem("FolderChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FolderChangeType[] FolderChanges;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200044E RID: 1102
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateManagedFolderRequestType : BaseRequestType
	{
		// Token: 0x040016E6 RID: 5862
		[XmlArrayItem("FolderName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] FolderNames;

		// Token: 0x040016E7 RID: 5863
		public EmailAddressType Mailbox;
	}
}

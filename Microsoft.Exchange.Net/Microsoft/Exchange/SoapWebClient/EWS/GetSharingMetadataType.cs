using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200043D RID: 1085
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSharingMetadataType : BaseRequestType
	{
		// Token: 0x040016BD RID: 5821
		public FolderIdType IdOfFolderToShare;

		// Token: 0x040016BE RID: 5822
		public string SenderSmtpAddress;

		// Token: 0x040016BF RID: 5823
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Recipients;
	}
}

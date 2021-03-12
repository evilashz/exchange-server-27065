using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200043A RID: 1082
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSharingFolderType : BaseRequestType
	{
		// Token: 0x040016B5 RID: 5813
		public string SmtpAddress;

		// Token: 0x040016B6 RID: 5814
		public SharingDataType DataType;

		// Token: 0x040016B7 RID: 5815
		[XmlIgnore]
		public bool DataTypeSpecified;

		// Token: 0x040016B8 RID: 5816
		public string SharedFolderId;
	}
}

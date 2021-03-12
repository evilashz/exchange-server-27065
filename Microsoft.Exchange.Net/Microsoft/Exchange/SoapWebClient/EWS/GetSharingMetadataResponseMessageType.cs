using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C5 RID: 709
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetSharingMetadataResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001213 RID: 4627
		[XmlArrayItem("EncryptedSharedFolderData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EncryptedSharedFolderDataType[] EncryptedSharedFolderDataCollection;

		// Token: 0x04001214 RID: 4628
		[XmlArrayItem("InvalidRecipient", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public InvalidRecipientType[] InvalidRecipients;
	}
}

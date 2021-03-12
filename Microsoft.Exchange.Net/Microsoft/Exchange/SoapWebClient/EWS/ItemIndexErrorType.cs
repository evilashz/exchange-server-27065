using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000271 RID: 625
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemIndexErrorType
	{
		// Token: 0x04001030 RID: 4144
		None,
		// Token: 0x04001031 RID: 4145
		GenericError,
		// Token: 0x04001032 RID: 4146
		Timeout,
		// Token: 0x04001033 RID: 4147
		StaleEvent,
		// Token: 0x04001034 RID: 4148
		MailboxOffline,
		// Token: 0x04001035 RID: 4149
		AttachmentLimitReached,
		// Token: 0x04001036 RID: 4150
		MarsWriterTruncation,
		// Token: 0x04001037 RID: 4151
		DocumentParserFailure
	}
}

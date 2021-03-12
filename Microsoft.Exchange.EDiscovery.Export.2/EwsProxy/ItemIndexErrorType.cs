using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000190 RID: 400
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ItemIndexErrorType
	{
		// Token: 0x04000BDE RID: 3038
		None,
		// Token: 0x04000BDF RID: 3039
		GenericError,
		// Token: 0x04000BE0 RID: 3040
		Timeout,
		// Token: 0x04000BE1 RID: 3041
		StaleEvent,
		// Token: 0x04000BE2 RID: 3042
		MailboxOffline,
		// Token: 0x04000BE3 RID: 3043
		AttachmentLimitReached,
		// Token: 0x04000BE4 RID: 3044
		MarsWriterTruncation,
		// Token: 0x04000BE5 RID: 3045
		DocumentParserFailure
	}
}

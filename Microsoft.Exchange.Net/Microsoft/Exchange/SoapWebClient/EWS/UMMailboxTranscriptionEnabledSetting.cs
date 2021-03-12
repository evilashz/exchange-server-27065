using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000190 RID: 400
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UMMailboxTranscriptionEnabledSetting
	{
		// Token: 0x04000998 RID: 2456
		Disabled,
		// Token: 0x04000999 RID: 2457
		Enabled,
		// Token: 0x0400099A RID: 2458
		Unknown
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200034E RID: 846
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class UserOofSettings
	{
		// Token: 0x0400140A RID: 5130
		public OofState OofState;

		// Token: 0x0400140B RID: 5131
		public ExternalAudience ExternalAudience;

		// Token: 0x0400140C RID: 5132
		public Duration Duration;

		// Token: 0x0400140D RID: 5133
		public ReplyBody InternalReply;

		// Token: 0x0400140E RID: 5134
		public ReplyBody ExternalReply;
	}
}

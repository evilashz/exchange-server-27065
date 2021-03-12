using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022A RID: 554
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class OnlineMeetingSettingsType
	{
		// Token: 0x04000E2F RID: 3631
		public LobbyBypassType LobbyBypass;

		// Token: 0x04000E30 RID: 3632
		public OnlineMeetingAccessLevelType AccessLevel;

		// Token: 0x04000E31 RID: 3633
		public PresentersType Presenters;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000252 RID: 594
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetClientAccessTokenResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00026B46 File Offset: 0x00024D46
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00026B4E File Offset: 0x00024D4E
		public ClientAccessTokenType Token
		{
			get
			{
				return this.tokenField;
			}
			set
			{
				this.tokenField = value;
			}
		}

		// Token: 0x04000F3A RID: 3898
		private ClientAccessTokenType tokenField;
	}
}

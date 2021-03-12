using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037B RID: 891
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetClientAccessTokenType : BaseRequestType
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00029DEE File Offset: 0x00027FEE
		// (set) Token: 0x06001C3C RID: 7228 RVA: 0x00029DF6 File Offset: 0x00027FF6
		[XmlArrayItem("TokenRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ClientAccessTokenRequestType[] TokenRequests
		{
			get
			{
				return this.tokenRequestsField;
			}
			set
			{
				this.tokenRequestsField = value;
			}
		}

		// Token: 0x040012B6 RID: 4790
		private ClientAccessTokenRequestType[] tokenRequestsField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200027F RID: 639
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class MailboxQueryType
	{
		// Token: 0x04001068 RID: 4200
		public string Query;

		// Token: 0x04001069 RID: 4201
		[XmlArrayItem("MailboxSearchScope", IsNullable = false)]
		public MailboxSearchScopeType[] MailboxSearchScopes;
	}
}

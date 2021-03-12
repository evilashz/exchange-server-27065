using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F0 RID: 1008
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetClientIntentType : BaseRequestType
	{
		// Token: 0x040015AF RID: 5551
		public string GlobalObjectId;

		// Token: 0x040015B0 RID: 5552
		public NonEmptyStateDefinitionType StateDefinition;
	}
}

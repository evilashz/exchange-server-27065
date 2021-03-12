using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200027B RID: 635
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DiscoverySearchConfigurationType
	{
		// Token: 0x0400104F RID: 4175
		public string SearchId;

		// Token: 0x04001050 RID: 4176
		public string SearchQuery;

		// Token: 0x04001051 RID: 4177
		[XmlArrayItem("SearchableMailbox", IsNullable = false)]
		public SearchableMailboxType[] SearchableMailboxes;

		// Token: 0x04001052 RID: 4178
		public string InPlaceHoldIdentity;

		// Token: 0x04001053 RID: 4179
		public string ManagedByOrganization;

		// Token: 0x04001054 RID: 4180
		public string Language;
	}
}

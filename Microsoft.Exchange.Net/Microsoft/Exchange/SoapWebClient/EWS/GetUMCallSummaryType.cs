using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F6 RID: 1014
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUMCallSummaryType : BaseRequestType
	{
		// Token: 0x040015B6 RID: 5558
		public string DailPlanGuid;

		// Token: 0x040015B7 RID: 5559
		public string GatewayGuid;

		// Token: 0x040015B8 RID: 5560
		public UMCDRGroupByType GroupRecordsBy;
	}
}

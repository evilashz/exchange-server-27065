using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003AE RID: 942
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FreeBusyViewOptionsType
	{
		// Token: 0x040014D1 RID: 5329
		public Duration TimeWindow;

		// Token: 0x040014D2 RID: 5330
		public int MergedFreeBusyIntervalInMinutes;

		// Token: 0x040014D3 RID: 5331
		[XmlIgnore]
		public bool MergedFreeBusyIntervalInMinutesSpecified;

		// Token: 0x040014D4 RID: 5332
		public FreeBusyViewType RequestedView;

		// Token: 0x040014D5 RID: 5333
		[XmlIgnore]
		public bool RequestedViewSpecified;
	}
}

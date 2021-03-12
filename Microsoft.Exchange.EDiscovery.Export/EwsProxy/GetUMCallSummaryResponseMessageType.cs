using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C2 RID: 194
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUMCallSummaryResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001FF70 File Offset: 0x0001E170
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0001FF78 File Offset: 0x0001E178
		[XmlArrayItem("UMReportRawCounters", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UMReportRawCountersType[] UMReportRawCountersCollection
		{
			get
			{
				return this.uMReportRawCountersCollectionField;
			}
			set
			{
				this.uMReportRawCountersCollectionField = value;
			}
		}

		// Token: 0x04000576 RID: 1398
		private UMReportRawCountersType[] uMReportRawCountersCollectionField;
	}
}

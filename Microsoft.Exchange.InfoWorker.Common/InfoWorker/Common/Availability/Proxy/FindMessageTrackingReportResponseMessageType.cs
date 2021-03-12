using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E7 RID: 743
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[Serializable]
	public class FindMessageTrackingReportResponseMessageType : ResponseMessage
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00066DE4 File Offset: 0x00064FE4
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00066DEC File Offset: 0x00064FEC
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Diagnostics
		{
			get
			{
				return this.diagnosticsField;
			}
			set
			{
				this.diagnosticsField = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00066DF5 File Offset: 0x00064FF5
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00066DFD File Offset: 0x00064FFD
		[XmlArrayItem("MessageTrackingSearchResult", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FindMessageTrackingSearchResultType[] MessageTrackingSearchResults
		{
			get
			{
				return this.messageTrackingSearchResultsField;
			}
			set
			{
				this.messageTrackingSearchResultsField = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00066E06 File Offset: 0x00065006
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x00066E0E File Offset: 0x0006500E
		public string ExecutedSearchScope
		{
			get
			{
				return this.executedSearchScopeField;
			}
			set
			{
				this.executedSearchScopeField = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00066E17 File Offset: 0x00065017
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x00066E1F File Offset: 0x0006501F
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ArrayOfTrackingPropertiesType[] Errors
		{
			get
			{
				return this.errorsField;
			}
			set
			{
				this.errorsField = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00066E28 File Offset: 0x00065028
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x00066E30 File Offset: 0x00065030
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x04000E2E RID: 3630
		private string[] diagnosticsField;

		// Token: 0x04000E2F RID: 3631
		private FindMessageTrackingSearchResultType[] messageTrackingSearchResultsField;

		// Token: 0x04000E30 RID: 3632
		private string executedSearchScopeField;

		// Token: 0x04000E31 RID: 3633
		private ArrayOfTrackingPropertiesType[] errorsField;

		// Token: 0x04000E32 RID: 3634
		private TrackingPropertyType[] propertiesField;
	}
}

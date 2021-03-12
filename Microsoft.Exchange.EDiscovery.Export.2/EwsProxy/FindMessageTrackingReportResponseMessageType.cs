using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BF RID: 447
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindMessageTrackingReportResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000251E1 File Offset: 0x000233E1
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x000251E9 File Offset: 0x000233E9
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

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000251F2 File Offset: 0x000233F2
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x000251FA File Offset: 0x000233FA
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

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00025203 File Offset: 0x00023403
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0002520B File Offset: 0x0002340B
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

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00025214 File Offset: 0x00023414
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0002521C File Offset: 0x0002341C
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

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00025225 File Offset: 0x00023425
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x0002522D File Offset: 0x0002342D
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

		// Token: 0x04000D58 RID: 3416
		private string[] diagnosticsField;

		// Token: 0x04000D59 RID: 3417
		private FindMessageTrackingSearchResultType[] messageTrackingSearchResultsField;

		// Token: 0x04000D5A RID: 3418
		private string executedSearchScopeField;

		// Token: 0x04000D5B RID: 3419
		private ArrayOfTrackingPropertiesType[] errorsField;

		// Token: 0x04000D5C RID: 3420
		private TrackingPropertyType[] propertiesField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BA RID: 442
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetMessageTrackingReportResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00024FCC File Offset: 0x000231CC
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00024FD4 File Offset: 0x000231D4
		public MessageTrackingReportType MessageTrackingReport
		{
			get
			{
				return this.messageTrackingReportField;
			}
			set
			{
				this.messageTrackingReportField = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00024FDD File Offset: 0x000231DD
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00024FE5 File Offset: 0x000231E5
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00024FEE File Offset: 0x000231EE
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00024FF6 File Offset: 0x000231F6
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00024FFF File Offset: 0x000231FF
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00025007 File Offset: 0x00023207
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

		// Token: 0x04000D3B RID: 3387
		private MessageTrackingReportType messageTrackingReportField;

		// Token: 0x04000D3C RID: 3388
		private string[] diagnosticsField;

		// Token: 0x04000D3D RID: 3389
		private ArrayOfTrackingPropertiesType[] errorsField;

		// Token: 0x04000D3E RID: 3390
		private TrackingPropertyType[] propertiesField;
	}
}

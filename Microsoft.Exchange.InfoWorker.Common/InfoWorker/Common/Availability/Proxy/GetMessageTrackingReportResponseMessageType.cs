using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E9 RID: 745
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetMessageTrackingReportResponseMessageType : ResponseMessage
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00066EE2 File Offset: 0x000650E2
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x00066EEA File Offset: 0x000650EA
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00066EF3 File Offset: 0x000650F3
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00066EFB File Offset: 0x000650FB
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00066F04 File Offset: 0x00065104
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x00066F0C File Offset: 0x0006510C
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00066F15 File Offset: 0x00065115
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00066F1D File Offset: 0x0006511D
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

		// Token: 0x04000E3C RID: 3644
		private MessageTrackingReportType messageTrackingReportField;

		// Token: 0x04000E3D RID: 3645
		private string[] diagnosticsField;

		// Token: 0x04000E3E RID: 3646
		private ArrayOfTrackingPropertiesType[] errorsField;

		// Token: 0x04000E3F RID: 3647
		private TrackingPropertyType[] propertiesField;
	}
}

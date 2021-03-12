using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000315 RID: 789
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUMCallSummaryType : BaseRequestType
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00028A26 File Offset: 0x00026C26
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x00028A2E File Offset: 0x00026C2E
		public string DailPlanGuid
		{
			get
			{
				return this.dailPlanGuidField;
			}
			set
			{
				this.dailPlanGuidField = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00028A37 File Offset: 0x00026C37
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00028A3F File Offset: 0x00026C3F
		public string GatewayGuid
		{
			get
			{
				return this.gatewayGuidField;
			}
			set
			{
				this.gatewayGuidField = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00028A48 File Offset: 0x00026C48
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x00028A50 File Offset: 0x00026C50
		public UMCDRGroupByType GroupRecordsBy
		{
			get
			{
				return this.groupRecordsByField;
			}
			set
			{
				this.groupRecordsByField = value;
			}
		}

		// Token: 0x04001164 RID: 4452
		private string dailPlanGuidField;

		// Token: 0x04001165 RID: 4453
		private string gatewayGuidField;

		// Token: 0x04001166 RID: 4454
		private UMCDRGroupByType groupRecordsByField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C5 RID: 197
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AudioMetricsAverageType
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000200CB File Offset: 0x0001E2CB
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x000200D3 File Offset: 0x0001E2D3
		public double TotalValue
		{
			get
			{
				return this.totalValueField;
			}
			set
			{
				this.totalValueField = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000200DC File Offset: 0x0001E2DC
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x000200E4 File Offset: 0x0001E2E4
		public double TotalCount
		{
			get
			{
				return this.totalCountField;
			}
			set
			{
				this.totalCountField = value;
			}
		}

		// Token: 0x04000589 RID: 1417
		private double totalValueField;

		// Token: 0x0400058A RID: 1418
		private double totalCountField;
	}
}

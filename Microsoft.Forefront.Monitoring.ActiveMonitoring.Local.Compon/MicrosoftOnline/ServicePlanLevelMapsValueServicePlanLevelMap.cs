using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D4 RID: 212
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServicePlanLevelMapsValueServicePlanLevelMap
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001F530 File Offset: 0x0001D730
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001F538 File Offset: 0x0001D738
		public MapValue Map
		{
			get
			{
				return this.mapField;
			}
			set
			{
				this.mapField = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001F541 File Offset: 0x0001D741
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001F549 File Offset: 0x0001D749
		[XmlAttribute]
		public string PlanId
		{
			get
			{
				return this.planIdField;
			}
			set
			{
				this.planIdField = value;
			}
		}

		// Token: 0x04000364 RID: 868
		private MapValue mapField;

		// Token: 0x04000365 RID: 869
		private string planIdField;
	}
}

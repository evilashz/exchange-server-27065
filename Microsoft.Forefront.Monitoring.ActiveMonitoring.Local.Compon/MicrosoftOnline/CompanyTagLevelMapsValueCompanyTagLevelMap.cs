using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D5 RID: 213
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompanyTagLevelMapsValueCompanyTagLevelMap
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001F55A File Offset: 0x0001D75A
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001F562 File Offset: 0x0001D762
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001F56B File Offset: 0x0001D76B
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001F573 File Offset: 0x0001D773
		[XmlArrayItem("ServicePlanLevelMap", IsNullable = false)]
		public ServicePlanLevelMapsValueServicePlanLevelMap[] ServicePlanLevelMaps
		{
			get
			{
				return this.servicePlanLevelMapsField;
			}
			set
			{
				this.servicePlanLevelMapsField = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001F57C File Offset: 0x0001D77C
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001F584 File Offset: 0x0001D784
		[XmlAttribute]
		public string CompanyTag
		{
			get
			{
				return this.companyTagField;
			}
			set
			{
				this.companyTagField = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001F58D File Offset: 0x0001D78D
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001F595 File Offset: 0x0001D795
		[XmlAttribute]
		public uint Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x04000366 RID: 870
		private MapValue mapField;

		// Token: 0x04000367 RID: 871
		private ServicePlanLevelMapsValueServicePlanLevelMap[] servicePlanLevelMapsField;

		// Token: 0x04000368 RID: 872
		private string companyTagField;

		// Token: 0x04000369 RID: 873
		private uint priorityField;
	}
}

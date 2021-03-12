using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D3 RID: 211
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceInstanceMapValue
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0001F4FD File Offset: 0x0001D6FD
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001F506 File Offset: 0x0001D706
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001F50E File Offset: 0x0001D70E
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

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001F517 File Offset: 0x0001D717
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001F51F File Offset: 0x0001D71F
		[XmlArrayItem("CompanyTagLevelMap", IsNullable = false)]
		public CompanyTagLevelMapsValueCompanyTagLevelMap[] CompanyTagLevelMaps
		{
			get
			{
				return this.companyTagLevelMapsField;
			}
			set
			{
				this.companyTagLevelMapsField = value;
			}
		}

		// Token: 0x04000361 RID: 865
		private MapValue mapField;

		// Token: 0x04000362 RID: 866
		private ServicePlanLevelMapsValueServicePlanLevelMap[] servicePlanLevelMapsField;

		// Token: 0x04000363 RID: 867
		private CompanyTagLevelMapsValueCompanyTagLevelMap[] companyTagLevelMapsField;
	}
}

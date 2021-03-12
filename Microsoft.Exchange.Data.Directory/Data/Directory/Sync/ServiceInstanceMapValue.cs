using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B5 RID: 2229
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ServiceInstanceMapValue
	{
		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x06006E5A RID: 28250 RVA: 0x001762C7 File Offset: 0x001744C7
		// (set) Token: 0x06006E5B RID: 28251 RVA: 0x001762CF File Offset: 0x001744CF
		[XmlElement(Order = 0)]
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

		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x06006E5C RID: 28252 RVA: 0x001762D8 File Offset: 0x001744D8
		// (set) Token: 0x06006E5D RID: 28253 RVA: 0x001762E0 File Offset: 0x001744E0
		[XmlArrayItem("ServicePlanLevelMap", IsNullable = false)]
		[XmlArray(Order = 1)]
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

		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x06006E5E RID: 28254 RVA: 0x001762E9 File Offset: 0x001744E9
		// (set) Token: 0x06006E5F RID: 28255 RVA: 0x001762F1 File Offset: 0x001744F1
		[XmlArray(Order = 2)]
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

		// Token: 0x040047CA RID: 18378
		private MapValue mapField;

		// Token: 0x040047CB RID: 18379
		private ServicePlanLevelMapsValueServicePlanLevelMap[] servicePlanLevelMapsField;

		// Token: 0x040047CC RID: 18380
		private CompanyTagLevelMapsValueCompanyTagLevelMap[] companyTagLevelMapsField;
	}
}

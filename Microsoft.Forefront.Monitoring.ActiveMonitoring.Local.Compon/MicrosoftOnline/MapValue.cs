using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CD RID: 205
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MapValue
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001F3D7 File Offset: 0x0001D5D7
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001F3DF File Offset: 0x0001D5DF
		[XmlArrayItem("WeightedServiceInstance", IsNullable = false)]
		public WeightedServiceInstancesWeightedServiceInstance[] WeightedServiceInstances
		{
			get
			{
				return this.weightedServiceInstancesField;
			}
			set
			{
				this.weightedServiceInstancesField = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001F3E8 File Offset: 0x0001D5E8
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		[XmlArrayItem("Region", IsNullable = false)]
		public MapValueRegion[] Regions
		{
			get
			{
				return this.regionsField;
			}
			set
			{
				this.regionsField = value;
			}
		}

		// Token: 0x04000353 RID: 851
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x04000354 RID: 852
		private MapValueRegion[] regionsField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D2 RID: 210
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class MapValueRegionCountryState
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001F4CB File Offset: 0x0001D6CB
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001F4D3 File Offset: 0x0001D6D3
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x0400035F RID: 863
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x04000360 RID: 864
		private string nameField;
	}
}

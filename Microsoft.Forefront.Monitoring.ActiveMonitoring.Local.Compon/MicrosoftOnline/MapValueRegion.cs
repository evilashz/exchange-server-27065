using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D0 RID: 208
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MapValueRegion
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001F455 File Offset: 0x0001D655
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001F45D File Offset: 0x0001D65D
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001F466 File Offset: 0x0001D666
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001F46E File Offset: 0x0001D66E
		[XmlArrayItem("Country", IsNullable = false)]
		public MapValueRegionCountry[] Countries
		{
			get
			{
				return this.countriesField;
			}
			set
			{
				this.countriesField = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001F477 File Offset: 0x0001D677
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001F47F File Offset: 0x0001D67F
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

		// Token: 0x04000359 RID: 857
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x0400035A RID: 858
		private MapValueRegionCountry[] countriesField;

		// Token: 0x0400035B RID: 859
		private string nameField;
	}
}

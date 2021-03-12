using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D1 RID: 209
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MapValueRegionCountry
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001F490 File Offset: 0x0001D690
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001F498 File Offset: 0x0001D698
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001F4A1 File Offset: 0x0001D6A1
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001F4A9 File Offset: 0x0001D6A9
		[XmlArrayItem("State", IsNullable = false)]
		public MapValueRegionCountryState[] States
		{
			get
			{
				return this.statesField;
			}
			set
			{
				this.statesField = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001F4BA File Offset: 0x0001D6BA
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

		// Token: 0x0400035C RID: 860
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x0400035D RID: 861
		private MapValueRegionCountryState[] statesField;

		// Token: 0x0400035E RID: 862
		private string nameField;
	}
}

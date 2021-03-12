using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AF RID: 2223
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MapValue
	{
		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x06006E38 RID: 28216 RVA: 0x001761A9 File Offset: 0x001743A9
		// (set) Token: 0x06006E39 RID: 28217 RVA: 0x001761B1 File Offset: 0x001743B1
		[XmlArray(Order = 0)]
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

		// Token: 0x17002734 RID: 10036
		// (get) Token: 0x06006E3A RID: 28218 RVA: 0x001761BA File Offset: 0x001743BA
		// (set) Token: 0x06006E3B RID: 28219 RVA: 0x001761C2 File Offset: 0x001743C2
		[XmlArray(Order = 1)]
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

		// Token: 0x040047BC RID: 18364
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x040047BD RID: 18365
		private MapValueRegion[] regionsField;
	}
}

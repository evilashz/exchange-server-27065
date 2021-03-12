using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B2 RID: 2226
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class MapValueRegion
	{
		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x06006E47 RID: 28231 RVA: 0x00176227 File Offset: 0x00174427
		// (set) Token: 0x06006E48 RID: 28232 RVA: 0x0017622F File Offset: 0x0017442F
		[XmlArrayItem("WeightedServiceInstance", IsNullable = false)]
		[XmlArray(Order = 0)]
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

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x06006E49 RID: 28233 RVA: 0x00176238 File Offset: 0x00174438
		// (set) Token: 0x06006E4A RID: 28234 RVA: 0x00176240 File Offset: 0x00174440
		[XmlArray(Order = 1)]
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

		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x06006E4B RID: 28235 RVA: 0x00176249 File Offset: 0x00174449
		// (set) Token: 0x06006E4C RID: 28236 RVA: 0x00176251 File Offset: 0x00174451
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

		// Token: 0x040047C2 RID: 18370
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x040047C3 RID: 18371
		private MapValueRegionCountry[] countriesField;

		// Token: 0x040047C4 RID: 18372
		private string nameField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B3 RID: 2227
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class MapValueRegionCountry
	{
		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x06006E4E RID: 28238 RVA: 0x00176262 File Offset: 0x00174462
		// (set) Token: 0x06006E4F RID: 28239 RVA: 0x0017626A File Offset: 0x0017446A
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

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x06006E50 RID: 28240 RVA: 0x00176273 File Offset: 0x00174473
		// (set) Token: 0x06006E51 RID: 28241 RVA: 0x0017627B File Offset: 0x0017447B
		[XmlArrayItem("State", IsNullable = false)]
		[XmlArray(Order = 1)]
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

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x06006E52 RID: 28242 RVA: 0x00176284 File Offset: 0x00174484
		// (set) Token: 0x06006E53 RID: 28243 RVA: 0x0017628C File Offset: 0x0017448C
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

		// Token: 0x040047C5 RID: 18373
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x040047C6 RID: 18374
		private MapValueRegionCountryState[] statesField;

		// Token: 0x040047C7 RID: 18375
		private string nameField;
	}
}

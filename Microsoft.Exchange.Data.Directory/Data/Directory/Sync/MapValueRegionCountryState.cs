using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B4 RID: 2228
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MapValueRegionCountryState
	{
		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x0017629D File Offset: 0x0017449D
		// (set) Token: 0x06006E56 RID: 28246 RVA: 0x001762A5 File Offset: 0x001744A5
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

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x06006E57 RID: 28247 RVA: 0x001762AE File Offset: 0x001744AE
		// (set) Token: 0x06006E58 RID: 28248 RVA: 0x001762B6 File Offset: 0x001744B6
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

		// Token: 0x040047C8 RID: 18376
		private WeightedServiceInstancesWeightedServiceInstance[] weightedServiceInstancesField;

		// Token: 0x040047C9 RID: 18377
		private string nameField;
	}
}

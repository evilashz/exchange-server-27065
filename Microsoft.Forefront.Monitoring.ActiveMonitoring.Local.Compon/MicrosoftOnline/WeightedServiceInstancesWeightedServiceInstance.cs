using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CE RID: 206
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class WeightedServiceInstancesWeightedServiceInstance
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001F401 File Offset: 0x0001D601
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001F409 File Offset: 0x0001D609
		[XmlArrayItem("AdditionalServiceInstance", IsNullable = false)]
		public WeightedServiceInstancesWeightedServiceInstanceAdditionalServiceInstance[] AdditionalServiceInstances
		{
			get
			{
				return this.additionalServiceInstancesField;
			}
			set
			{
				this.additionalServiceInstancesField = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001F412 File Offset: 0x0001D612
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001F41A File Offset: 0x0001D61A
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001F423 File Offset: 0x0001D623
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001F42B File Offset: 0x0001D62B
		[XmlAttribute]
		public int Weight
		{
			get
			{
				return this.weightField;
			}
			set
			{
				this.weightField = value;
			}
		}

		// Token: 0x04000355 RID: 853
		private WeightedServiceInstancesWeightedServiceInstanceAdditionalServiceInstance[] additionalServiceInstancesField;

		// Token: 0x04000356 RID: 854
		private string nameField;

		// Token: 0x04000357 RID: 855
		private int weightField;
	}
}

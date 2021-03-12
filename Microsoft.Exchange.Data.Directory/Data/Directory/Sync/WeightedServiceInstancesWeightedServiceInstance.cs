using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B0 RID: 2224
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class WeightedServiceInstancesWeightedServiceInstance
	{
		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x06006E3D RID: 28221 RVA: 0x001761D3 File Offset: 0x001743D3
		// (set) Token: 0x06006E3E RID: 28222 RVA: 0x001761DB File Offset: 0x001743DB
		[XmlArrayItem("AdditionalServiceInstance", IsNullable = false)]
		[XmlArray(Order = 0)]
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

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x06006E3F RID: 28223 RVA: 0x001761E4 File Offset: 0x001743E4
		// (set) Token: 0x06006E40 RID: 28224 RVA: 0x001761EC File Offset: 0x001743EC
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

		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x06006E41 RID: 28225 RVA: 0x001761F5 File Offset: 0x001743F5
		// (set) Token: 0x06006E42 RID: 28226 RVA: 0x001761FD File Offset: 0x001743FD
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

		// Token: 0x040047BE RID: 18366
		private WeightedServiceInstancesWeightedServiceInstanceAdditionalServiceInstance[] additionalServiceInstancesField;

		// Token: 0x040047BF RID: 18367
		private string nameField;

		// Token: 0x040047C0 RID: 18368
		private int weightField;
	}
}

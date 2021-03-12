using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B1 RID: 2225
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class WeightedServiceInstancesWeightedServiceInstanceAdditionalServiceInstance
	{
		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x06006E44 RID: 28228 RVA: 0x0017620E File Offset: 0x0017440E
		// (set) Token: 0x06006E45 RID: 28229 RVA: 0x00176216 File Offset: 0x00174416
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

		// Token: 0x040047C1 RID: 18369
		private string nameField;
	}
}

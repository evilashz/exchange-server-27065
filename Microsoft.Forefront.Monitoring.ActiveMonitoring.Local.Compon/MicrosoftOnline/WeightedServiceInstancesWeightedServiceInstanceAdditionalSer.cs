using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CF RID: 207
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class WeightedServiceInstancesWeightedServiceInstanceAdditionalServiceInstance
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001F43C File Offset: 0x0001D63C
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001F444 File Offset: 0x0001D644
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

		// Token: 0x04000358 RID: 856
		private string nameField;
	}
}

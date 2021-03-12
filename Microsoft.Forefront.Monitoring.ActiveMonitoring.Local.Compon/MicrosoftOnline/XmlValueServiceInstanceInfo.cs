using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DD RID: 221
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueServiceInstanceInfo
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001F732 File Offset: 0x0001D932
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001F73A File Offset: 0x0001D93A
		public ServiceInstanceInfoValue Info
		{
			get
			{
				return this.infoField;
			}
			set
			{
				this.infoField = value;
			}
		}

		// Token: 0x0400037E RID: 894
		private ServiceInstanceInfoValue infoField;
	}
}

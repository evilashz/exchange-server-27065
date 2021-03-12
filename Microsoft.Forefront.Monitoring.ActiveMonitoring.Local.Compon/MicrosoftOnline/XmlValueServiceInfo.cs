using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DF RID: 223
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueServiceInfo
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001F78D File Offset: 0x0001D98D
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001F795 File Offset: 0x0001D995
		public ServiceInfoValue Info
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

		// Token: 0x04000382 RID: 898
		private ServiceInfoValue infoField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DA RID: 218
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueServiceOriginatedResource
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001F681 File Offset: 0x0001D881
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001F689 File Offset: 0x0001D889
		public ServiceOriginatedResourceValue Resource
		{
			get
			{
				return this.resourceField;
			}
			set
			{
				this.resourceField = value;
			}
		}

		// Token: 0x04000375 RID: 885
		private ServiceOriginatedResourceValue resourceField;
	}
}

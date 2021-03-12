using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F7 RID: 247
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueDatacenterRedirection
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001FDB1 File Offset: 0x0001DFB1
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001FDB9 File Offset: 0x0001DFB9
		public DatacenterRedirectionValue DatacenterRedirection
		{
			get
			{
				return this.datacenterRedirectionField;
			}
			set
			{
				this.datacenterRedirectionField = value;
			}
		}

		// Token: 0x040003E4 RID: 996
		private DatacenterRedirectionValue datacenterRedirectionField;
	}
}

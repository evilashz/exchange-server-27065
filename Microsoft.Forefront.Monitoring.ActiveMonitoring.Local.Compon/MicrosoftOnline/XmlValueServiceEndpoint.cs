using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E0 RID: 224
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueServiceEndpoint
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001F7A6 File Offset: 0x0001D9A6
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001F7AE File Offset: 0x0001D9AE
		public ServiceEndpointValue ServiceEndpoint
		{
			get
			{
				return this.serviceEndpointField;
			}
			set
			{
				this.serviceEndpointField = value;
			}
		}

		// Token: 0x04000383 RID: 899
		private ServiceEndpointValue serviceEndpointField;
	}
}

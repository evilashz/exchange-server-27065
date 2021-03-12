using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DB RID: 219
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceInstanceInfoValue
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001F69A File Offset: 0x0001D89A
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001F6A2 File Offset: 0x0001D8A2
		[XmlElement("Endpoint")]
		public ServiceEndpointValue[] Endpoint
		{
			get
			{
				return this.endpointField;
			}
			set
			{
				this.endpointField = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001F6AB File Offset: 0x0001D8AB
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001F6B3 File Offset: 0x0001D8B3
		[XmlAnyElement]
		public XmlElement[] Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x04000376 RID: 886
		private ServiceEndpointValue[] endpointField;

		// Token: 0x04000377 RID: 887
		private XmlElement[] anyField;
	}
}

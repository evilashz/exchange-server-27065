using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000909 RID: 2313
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueServiceDiscoveryEndpoint
	{
		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06006EFE RID: 28414 RVA: 0x00176812 File Offset: 0x00174A12
		// (set) Token: 0x06006EFF RID: 28415 RVA: 0x0017681A File Offset: 0x00174A1A
		[XmlElement(Order = 0)]
		public ServiceDiscoveryEndpointValue ServiceDiscoveryEndpoint
		{
			get
			{
				return this.serviceDiscoveryEndpointField;
			}
			set
			{
				this.serviceDiscoveryEndpointField = value;
			}
		}

		// Token: 0x04004818 RID: 18456
		private ServiceDiscoveryEndpointValue serviceDiscoveryEndpointField;
	}
}

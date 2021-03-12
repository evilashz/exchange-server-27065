using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AE RID: 2222
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceInstanceInfoValue
	{
		// Token: 0x17002731 RID: 10033
		// (get) Token: 0x06006E33 RID: 28211 RVA: 0x0017617F File Offset: 0x0017437F
		// (set) Token: 0x06006E34 RID: 28212 RVA: 0x00176187 File Offset: 0x00174387
		[XmlElement("Endpoint", Order = 0)]
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

		// Token: 0x17002732 RID: 10034
		// (get) Token: 0x06006E35 RID: 28213 RVA: 0x00176190 File Offset: 0x00174390
		// (set) Token: 0x06006E36 RID: 28214 RVA: 0x00176198 File Offset: 0x00174398
		[XmlAnyElement(Order = 1)]
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

		// Token: 0x040047BA RID: 18362
		private ServiceEndpointValue[] endpointField;

		// Token: 0x040047BB RID: 18363
		private XmlElement[] anyField;
	}
}

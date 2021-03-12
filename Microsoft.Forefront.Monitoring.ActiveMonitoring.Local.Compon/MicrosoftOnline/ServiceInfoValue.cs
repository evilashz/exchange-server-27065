using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DE RID: 222
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceInfoValue
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x0001F74B File Offset: 0x0001D94B
		public ServiceInfoValue()
		{
			this.versionField = 0;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001F75A File Offset: 0x0001D95A
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001F762 File Offset: 0x0001D962
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

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001F76B File Offset: 0x0001D96B
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001F773 File Offset: 0x0001D973
		[XmlAttribute]
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstanceField;
			}
			set
			{
				this.serviceInstanceField = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001F77C File Offset: 0x0001D97C
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001F784 File Offset: 0x0001D984
		[DefaultValue(0)]
		[XmlAttribute]
		public int Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x0400037F RID: 895
		private XmlElement[] anyField;

		// Token: 0x04000380 RID: 896
		private string serviceInstanceField;

		// Token: 0x04000381 RID: 897
		private int versionField;
	}
}

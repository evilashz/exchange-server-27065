using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093B RID: 2363
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceInfoValue
	{
		// Token: 0x06006FDE RID: 28638 RVA: 0x00176F7A File Offset: 0x0017517A
		public ServiceInfoValue()
		{
			this.versionField = 0;
		}

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x06006FDF RID: 28639 RVA: 0x00176F89 File Offset: 0x00175189
		// (set) Token: 0x06006FE0 RID: 28640 RVA: 0x00176F91 File Offset: 0x00175191
		[XmlAnyElement(Order = 0)]
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

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x06006FE1 RID: 28641 RVA: 0x00176F9A File Offset: 0x0017519A
		// (set) Token: 0x06006FE2 RID: 28642 RVA: 0x00176FA2 File Offset: 0x001751A2
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

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x06006FE3 RID: 28643 RVA: 0x00176FAB File Offset: 0x001751AB
		// (set) Token: 0x06006FE4 RID: 28644 RVA: 0x00176FB3 File Offset: 0x001751B3
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

		// Token: 0x04004898 RID: 18584
		private XmlElement[] anyField;

		// Token: 0x04004899 RID: 18585
		private string serviceInstanceField;

		// Token: 0x0400489A RID: 18586
		private int versionField;
	}
}

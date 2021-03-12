using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FC RID: 252
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ContextMoveWatermarksValueSourceSubscriberFilterVersion
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001FE59 File Offset: 0x0001E059
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001FE61 File Offset: 0x0001E061
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

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001FE6A File Offset: 0x0001E06A
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001FE72 File Offset: 0x0001E072
		[XmlAttribute(DataType = "nonNegativeInteger")]
		public string Version
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

		// Token: 0x040003EF RID: 1007
		private string serviceInstanceField;

		// Token: 0x040003F0 RID: 1008
		private string versionField;
	}
}

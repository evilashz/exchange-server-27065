using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D7 RID: 215
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class ValidationErrorValue
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001F5BF File Offset: 0x0001D7BF
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public XmlElement ErrorDetail
		{
			get
			{
				return this.errorDetailField;
			}
			set
			{
				this.errorDetailField = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		[XmlAttribute]
		public bool Resolved
		{
			get
			{
				return this.resolvedField;
			}
			set
			{
				this.resolvedField = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		[XmlAttribute]
		public DateTime Timestamp
		{
			get
			{
				return this.timestampField;
			}
			set
			{
				this.timestampField = value;
			}
		}

		// Token: 0x0400036B RID: 875
		private XmlElement errorDetailField;

		// Token: 0x0400036C RID: 876
		private bool resolvedField;

		// Token: 0x0400036D RID: 877
		private string serviceInstanceField;

		// Token: 0x0400036E RID: 878
		private DateTime timestampField;
	}
}

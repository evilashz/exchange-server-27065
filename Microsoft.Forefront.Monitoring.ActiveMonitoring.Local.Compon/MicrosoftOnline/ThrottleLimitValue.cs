using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C6 RID: 198
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ThrottleLimitValue
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001F2C2 File Offset: 0x0001D4C2
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001F2CA File Offset: 0x0001D4CA
		[XmlAttribute]
		public string CounterName
		{
			get
			{
				return this.counterNameField;
			}
			set
			{
				this.counterNameField = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001F2D3 File Offset: 0x0001D4D3
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001F2DB File Offset: 0x0001D4DB
		[XmlAttribute]
		public string CounterValue
		{
			get
			{
				return this.counterValueField;
			}
			set
			{
				this.counterValueField = value;
			}
		}

		// Token: 0x04000346 RID: 838
		private string counterNameField;

		// Token: 0x04000347 RID: 839
		private string counterValueField;
	}
}

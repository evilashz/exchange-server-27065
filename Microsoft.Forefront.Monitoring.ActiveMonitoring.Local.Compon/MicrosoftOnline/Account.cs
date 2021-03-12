using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A3 RID: 419
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class Account
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0002306D File Offset: 0x0002126D
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00023075 File Offset: 0x00021275
		public Guid AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0002307E File Offset: 0x0002127E
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00023086 File Offset: 0x00021286
		public string AccountName
		{
			get
			{
				return this.accountNameField;
			}
			set
			{
				this.accountNameField = value;
			}
		}

		// Token: 0x040006BA RID: 1722
		private Guid accountIdField;

		// Token: 0x040006BB RID: 1723
		private string accountNameField;
	}
}

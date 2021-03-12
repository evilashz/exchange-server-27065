using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A2 RID: 418
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProvisionInfo
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00023032 File Offset: 0x00021232
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0002303A File Offset: 0x0002123A
		public Guid ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00023043 File Offset: 0x00021243
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0002304B File Offset: 0x0002124B
		public string ShortLivedToken
		{
			get
			{
				return this.shortLivedTokenField;
			}
			set
			{
				this.shortLivedTokenField = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00023054 File Offset: 0x00021254
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0002305C File Offset: 0x0002125C
		public string FirstAdminUserNetId
		{
			get
			{
				return this.firstAdminUserNetIdField;
			}
			set
			{
				this.firstAdminUserNetIdField = value;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private Guid contextIdField;

		// Token: 0x040006B8 RID: 1720
		private string shortLivedTokenField;

		// Token: 0x040006B9 RID: 1721
		private string firstAdminUserNetIdField;
	}
}

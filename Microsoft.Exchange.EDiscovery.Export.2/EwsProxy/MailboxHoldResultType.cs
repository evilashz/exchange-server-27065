using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000195 RID: 405
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxHoldResultType
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00024225 File Offset: 0x00022425
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0002422D File Offset: 0x0002242D
		public string HoldId
		{
			get
			{
				return this.holdIdField;
			}
			set
			{
				this.holdIdField = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00024236 File Offset: 0x00022436
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0002423E File Offset: 0x0002243E
		public string Query
		{
			get
			{
				return this.queryField;
			}
			set
			{
				this.queryField = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00024247 File Offset: 0x00022447
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0002424F File Offset: 0x0002244F
		[XmlArrayItem("MailboxHoldStatus", IsNullable = false)]
		public MailboxHoldStatusType[] MailboxHoldStatuses
		{
			get
			{
				return this.mailboxHoldStatusesField;
			}
			set
			{
				this.mailboxHoldStatusesField = value;
			}
		}

		// Token: 0x04000BEF RID: 3055
		private string holdIdField;

		// Token: 0x04000BF0 RID: 3056
		private string queryField;

		// Token: 0x04000BF1 RID: 3057
		private MailboxHoldStatusType[] mailboxHoldStatusesField;
	}
}

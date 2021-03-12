using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000138 RID: 312
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetMailTipsType : BaseRequestType
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00025172 File Offset: 0x00023372
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0002517A File Offset: 0x0002337A
		public EmailAddressType SendingAs
		{
			get
			{
				return this.sendingAsField;
			}
			set
			{
				this.sendingAsField = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00025183 File Offset: 0x00023383
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0002518B File Offset: 0x0002338B
		[XmlArrayItem("Mailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EmailAddressType[] Recipients
		{
			get
			{
				return this.recipientsField;
			}
			set
			{
				this.recipientsField = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00025194 File Offset: 0x00023394
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0002519C File Offset: 0x0002339C
		public MailTipTypes MailTipsRequested
		{
			get
			{
				return this.mailTipsRequestedField;
			}
			set
			{
				this.mailTipsRequestedField = value;
			}
		}

		// Token: 0x040006AA RID: 1706
		private EmailAddressType sendingAsField;

		// Token: 0x040006AB RID: 1707
		private EmailAddressType[] recipientsField;

		// Token: 0x040006AC RID: 1708
		private MailTipTypes mailTipsRequestedField;
	}
}

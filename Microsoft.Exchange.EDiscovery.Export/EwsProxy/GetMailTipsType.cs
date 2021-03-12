using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000377 RID: 887
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetMailTipsType : BaseRequestType
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00029D25 File Offset: 0x00027F25
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x00029D2D File Offset: 0x00027F2D
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

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00029D36 File Offset: 0x00027F36
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x00029D3E File Offset: 0x00027F3E
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

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00029D47 File Offset: 0x00027F47
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x00029D4F File Offset: 0x00027F4F
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

		// Token: 0x040012A7 RID: 4775
		private EmailAddressType sendingAsField;

		// Token: 0x040012A8 RID: 4776
		private EmailAddressType[] recipientsField;

		// Token: 0x040012A9 RID: 4777
		private MailTipTypes mailTipsRequestedField;
	}
}

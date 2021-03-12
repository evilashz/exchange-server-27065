using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043E RID: 1086
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetMailTipsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetMailTipsRequest : BaseRequest
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000A146A File Offset: 0x0009F66A
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x000A1472 File Offset: 0x0009F672
		[DataMember(Name = "SendingAs", IsRequired = true, Order = 1)]
		[XmlElement("SendingAs")]
		public EmailAddressWrapper SendingAs
		{
			get
			{
				return this.sendingAs;
			}
			set
			{
				this.sendingAs = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x000A147B File Offset: 0x0009F67B
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x000A1483 File Offset: 0x0009F683
		[XmlArrayItem("Mailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray("Recipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "Recipients", IsRequired = true, Order = 2)]
		public EmailAddressWrapper[] Recipients
		{
			get
			{
				return this.recipients;
			}
			set
			{
				this.recipients = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000A148C File Offset: 0x0009F68C
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x000A1494 File Offset: 0x0009F694
		[XmlElement("MailTipsRequested")]
		[DataMember(Name = "MailTipsRequested", IsRequired = true, Order = 3)]
		public MailTipTypes MailTipsRequested
		{
			get
			{
				return this.mailTipsRequested;
			}
			set
			{
				this.mailTipsRequested = value;
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000A149D File Offset: 0x0009F69D
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetMailTips(callContext, this);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000A14A6 File Offset: 0x0009F6A6
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000A14A9 File Offset: 0x0009F6A9
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x04001410 RID: 5136
		internal const string SendingAsElementName = "SendingAs";

		// Token: 0x04001411 RID: 5137
		internal const string RecipientsElementName = "Recipients";

		// Token: 0x04001412 RID: 5138
		internal const string MailTipsRequestedElementName = "MailTipsRequested";

		// Token: 0x04001413 RID: 5139
		private EmailAddressWrapper sendingAs;

		// Token: 0x04001414 RID: 5140
		private EmailAddressWrapper[] recipients;

		// Token: 0x04001415 RID: 5141
		private MailTipTypes mailTipsRequested;
	}
}

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000501 RID: 1281
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetMailTipsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetMailTipsResponseMessage : ResponseMessage
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x000A5633 File Offset: 0x000A3833
		public GetMailTipsResponseMessage()
		{
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000A563B File Offset: 0x000A383B
		internal GetMailTipsResponseMessage(ServiceResultCode code, ServiceError error, MailTipsResponseMessage[] mailTips) : base(code, error)
		{
			this.mailTips = mailTips;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000A564C File Offset: 0x000A384C
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x000A5654 File Offset: 0x000A3854
		[XmlArrayItem("MailTipsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		[DataMember(Name = "ResponseMessages", IsRequired = true, Order = 1)]
		public MailTipsResponseMessage[] ResponseMessages
		{
			get
			{
				return this.mailTips;
			}
			set
			{
				this.mailTips = value;
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000A565D File Offset: 0x000A385D
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetMailTipsResponseMessage;
		}

		// Token: 0x040015A7 RID: 5543
		private MailTipsResponseMessage[] mailTips;
	}
}

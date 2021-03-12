using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052F RID: 1327
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MailTipsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MailTipsResponseMessage : ResponseMessage
	{
		// Token: 0x060025E5 RID: 9701 RVA: 0x000A60BF File Offset: 0x000A42BF
		public MailTipsResponseMessage()
		{
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000A60C7 File Offset: 0x000A42C7
		internal MailTipsResponseMessage(ServiceResultCode code, ServiceError error, XmlNode mailTips) : base(code, error)
		{
			this.mailTips = mailTips;
			base.MessageText = mailTips.OuterXml;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000A60E4 File Offset: 0x000A42E4
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x000A60EC File Offset: 0x000A42EC
		[XmlAnyElement]
		public XmlNode MailTips
		{
			get
			{
				return this.mailTips;
			}
			set
			{
				this.mailTips = value;
				base.MessageText = this.mailTips.OuterXml;
			}
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000A6106 File Offset: 0x000A4306
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetMailTipsResponseMessage;
		}

		// Token: 0x040015DB RID: 5595
		private XmlNode mailTips;
	}
}

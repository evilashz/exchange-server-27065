using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E0 RID: 1248
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "FindMailboxStatisticsByKeywordsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsResponseMessage : ResponseMessage
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000A4E6F File Offset: 0x000A306F
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x000A4E77 File Offset: 0x000A3077
		[XmlElement("MailboxStatisticsSearchResult", IsNullable = false)]
		public MailboxStatisticsSearchResult MailboxStatisticsSearchResult
		{
			get
			{
				return this.mailboxStatisticsSearchResult;
			}
			set
			{
				this.mailboxStatisticsSearchResult = value;
			}
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000A4E80 File Offset: 0x000A3080
		public FindMailboxStatisticsByKeywordsResponseMessage()
		{
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000A4E93 File Offset: 0x000A3093
		internal FindMailboxStatisticsByKeywordsResponseMessage(ServiceResultCode code, ServiceError error, UserMailbox userMailbox, KeywordStatisticsSearchResult keywordStatisticsSearchResult) : base(code, error)
		{
			this.mailboxStatisticsSearchResult.UserMailbox = userMailbox;
			this.mailboxStatisticsSearchResult.KeywordStatisticsSearchResult = ((keywordStatisticsSearchResult == null) ? new KeywordStatisticsSearchResult() : keywordStatisticsSearchResult);
		}

		// Token: 0x04001581 RID: 5505
		private MailboxStatisticsSearchResult mailboxStatisticsSearchResult = new MailboxStatisticsSearchResult();
	}
}

using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007FC RID: 2044
	[XmlType(TypeName = "MailboxStatisticsSearchResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxStatisticsSearchResult
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x000D4E30 File Offset: 0x000D3030
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x000D4E38 File Offset: 0x000D3038
		[XmlElement("UserMailbox")]
		public UserMailbox UserMailbox
		{
			get
			{
				return this.userMailboxField;
			}
			set
			{
				this.userMailboxField = value;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000D4E41 File Offset: 0x000D3041
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x000D4E49 File Offset: 0x000D3049
		[XmlElement("KeywordStatisticsSearchResult")]
		public KeywordStatisticsSearchResult KeywordStatisticsSearchResult
		{
			get
			{
				return this.keywordStatisticsSearchResultField;
			}
			set
			{
				this.keywordStatisticsSearchResultField = value;
			}
		}

		// Token: 0x040020F5 RID: 8437
		private UserMailbox userMailboxField;

		// Token: 0x040020F6 RID: 8438
		private KeywordStatisticsSearchResult keywordStatisticsSearchResultField;
	}
}

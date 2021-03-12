using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	internal class LookupMessagesParams
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x000048D0 File Offset: 0x00002AD0
		public LookupMessagesParams(List<uint> fetchUidList)
		{
			this.fetchUidList = fetchUidList;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000048DF File Offset: 0x00002ADF
		public FetchMessagesFlags FetchMessagesFlags
		{
			get
			{
				return FetchMessagesFlags.FetchByUid | FetchMessagesFlags.IncludeExtendedData;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000048E4 File Offset: 0x00002AE4
		public string GetUidFetchString()
		{
			if (this.fetchUidList.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (uint num2 in this.fetchUidList)
			{
				stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
				num++;
				if (num < this.fetchUidList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400003A RID: 58
		private const FetchMessagesFlags EnumerateFlags = FetchMessagesFlags.FetchByUid | FetchMessagesFlags.IncludeExtendedData;

		// Token: 0x0400003B RID: 59
		private const string FetchUidStringDelimiter = ",";

		// Token: 0x0400003C RID: 60
		private readonly List<uint> fetchUidList;
	}
}

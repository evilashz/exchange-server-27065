using System;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000055 RID: 85
	internal class MessagingPoliciesUtils
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0001108C File Offset: 0x0000F28C
		public static MessagingPoliciesUtils.JournalVersion CheckJournalReportVersion(HeaderList headerList)
		{
			if (headerList == null)
			{
				return MessagingPoliciesUtils.JournalVersion.None;
			}
			Header header = headerList.FindFirst("X-MS-Journal-Report");
			if (header != null)
			{
				return MessagingPoliciesUtils.JournalVersion.Exchange2007;
			}
			header = headerList.FindFirst("Content-Identifier");
			if (header != null && !string.IsNullOrEmpty(header.Value) && string.Compare(header.Value, "exjournalreport", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return MessagingPoliciesUtils.JournalVersion.Exchange2003;
			}
			header = headerList.FindFirst("Content-Identifer");
			if (header != null && !string.IsNullOrEmpty(header.Value) && string.Compare(header.Value, "exjournalreport", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return MessagingPoliciesUtils.JournalVersion.Exchange2003;
			}
			return MessagingPoliciesUtils.JournalVersion.None;
		}

		// Token: 0x04000359 RID: 857
		public const string E12EnvelopeJournal = "X-MS-Journal-Report";

		// Token: 0x0400035A RID: 858
		public const string ContentIdentifier = "Content-Identifier";

		// Token: 0x0400035B RID: 859
		public const string ContentIdentifierTypo = "Content-Identifer";

		// Token: 0x0400035C RID: 860
		public const string ExchangeJournalReport = "exjournalreport";

		// Token: 0x02000056 RID: 86
		public enum JournalVersion
		{
			// Token: 0x0400035E RID: 862
			None,
			// Token: 0x0400035F RID: 863
			Exchange2003,
			// Token: 0x04000360 RID: 864
			Exchange2007
		}
	}
}

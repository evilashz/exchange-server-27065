using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200011B RID: 283
	public class QueryGenerationNotRequiredException : LocalizedException
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x0002337A File Offset: 0x0002157A
		public QueryGenerationNotRequiredException() : base(Strings.descQueryGenerationNotRequired)
		{
		}
	}
}

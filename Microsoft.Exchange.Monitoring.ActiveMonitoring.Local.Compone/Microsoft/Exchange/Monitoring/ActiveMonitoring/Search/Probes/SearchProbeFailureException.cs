using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000466 RID: 1126
	public class SearchProbeFailureException : LocalizedException
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x000A7C4B File Offset: 0x000A5E4B
		public SearchProbeFailureException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000A7C54 File Offset: 0x000A5E54
		public SearchProbeFailureException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}
	}
}

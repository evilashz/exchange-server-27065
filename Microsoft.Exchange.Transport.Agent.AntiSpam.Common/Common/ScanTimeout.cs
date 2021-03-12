using System;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Transport.Agent.Common
{
	// Token: 0x02000011 RID: 17
	internal class ScanTimeout
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003D0C File Offset: 0x00001F0C
		internal virtual TimeSpan GetTimeout(EmailMessage message = null)
		{
			return TimeSpan.FromMinutes(2.0);
		}
	}
}

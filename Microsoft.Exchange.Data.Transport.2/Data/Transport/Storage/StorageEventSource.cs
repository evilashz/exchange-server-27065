using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Storage
{
	// Token: 0x02000094 RID: 148
	public abstract class StorageEventSource
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00008773 File Offset: 0x00006973
		internal StorageEventSource()
		{
		}

		// Token: 0x0600036D RID: 877
		public abstract void Delete(string sourceContext);

		// Token: 0x0600036E RID: 878
		public abstract void DeleteWithNdr(SmtpResponse response, string sourceContext);
	}
}

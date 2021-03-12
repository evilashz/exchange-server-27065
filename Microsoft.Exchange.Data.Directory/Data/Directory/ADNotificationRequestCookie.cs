using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000040 RID: 64
	internal sealed class ADNotificationRequestCookie
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00012E88 File Offset: 0x00011088
		internal ADNotificationRequest[] Requests
		{
			get
			{
				return this.requests;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00012E90 File Offset: 0x00011090
		internal ADNotificationRequestCookie(params ADNotificationRequest[] requests)
		{
			this.requests = requests;
		}

		// Token: 0x04000109 RID: 265
		private ADNotificationRequest[] requests;
	}
}

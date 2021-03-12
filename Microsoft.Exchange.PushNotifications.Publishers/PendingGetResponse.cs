using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000BB RID: 187
	internal class PendingGetResponse : IPendingGetResponse
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x00013E6B File Offset: 0x0001206B
		internal PendingGetResponse(HttpResponse response)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			this.response = response;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00013E85 File Offset: 0x00012085
		public void Write(string payload)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("payload", payload);
			this.response.Write(payload);
		}

		// Token: 0x0400031B RID: 795
		private HttpResponse response;
	}
}

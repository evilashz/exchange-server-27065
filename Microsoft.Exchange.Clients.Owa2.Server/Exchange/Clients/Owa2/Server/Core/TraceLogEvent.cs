using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000465 RID: 1125
	internal class TraceLogEvent : ILogEvent
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x00088742 File Offset: 0x00086942
		public TraceLogEvent(string eventId, IMailboxContext userContext, string methodName, string message)
		{
			this.eventId = (eventId ?? string.Empty);
			this.userContext = userContext;
			this.methodName = (methodName ?? string.Empty);
			this.message = (message ?? string.Empty);
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00088782 File Offset: 0x00086982
		public string EventId
		{
			get
			{
				return this.eventId;
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0008878C File Offset: 0x0008698C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			string value = string.Empty;
			string value2 = string.Empty;
			string value3 = string.Empty;
			if (this.userContext != null)
			{
				if (this.userContext.ExchangePrincipal != null)
				{
					value = this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString();
					value2 = this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
				}
				if (this.userContext.Key != null)
				{
					value3 = this.userContext.Key.ToString();
				}
			}
			return new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MG", value),
				new KeyValuePair<string, object>("PSA", value2),
				new KeyValuePair<string, object>("MN", this.methodName),
				new KeyValuePair<string, object>(UserContextCookie.UserContextCookiePrefix, value3),
				new KeyValuePair<string, object>("MSG", this.message)
			};
		}

		// Token: 0x040015EF RID: 5615
		private readonly string eventId;

		// Token: 0x040015F0 RID: 5616
		private readonly IMailboxContext userContext;

		// Token: 0x040015F1 RID: 5617
		private readonly string methodName;

		// Token: 0x040015F2 RID: 5618
		private readonly string message;
	}
}

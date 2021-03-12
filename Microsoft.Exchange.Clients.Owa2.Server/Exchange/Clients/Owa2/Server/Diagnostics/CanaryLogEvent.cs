using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200042E RID: 1070
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CanaryLogEvent : ILogEvent
	{
		// Token: 0x0600248D RID: 9357 RVA: 0x00083A34 File Offset: 0x00081C34
		public CanaryLogEvent(HttpContext httpContext, IMailboxContext userContext, CanaryLogEvent.CanaryStatus canaryStatus, DateTime creationTime, string logData)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(httpContext);
			if (userContextCookie != null)
			{
				this.userContextCookie = userContextCookie.CookieValue;
			}
			else
			{
				this.userContextCookie = string.Empty;
			}
			if (userContext != null && userContext.ExchangePrincipal != null)
			{
				this.primarySmtpAddress = userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
				this.mailboxGuid = userContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString();
			}
			else
			{
				this.primarySmtpAddress = string.Empty;
				this.mailboxGuid = string.Empty;
			}
			if (logData != null)
			{
				this.logData = logData;
			}
			else
			{
				this.logData = string.Empty;
			}
			this.action = (httpContext.Request.Headers[OWADispatchOperationSelector.Action] ?? string.Empty);
			this.canaryStatus = canaryStatus;
			this.creationTime = creationTime;
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00083B2F File Offset: 0x00081D2F
		public string EventId
		{
			get
			{
				return "Canary";
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00083B38 File Offset: 0x00081D38
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("MG", this.mailboxGuid),
				new KeyValuePair<string, object>("PSA", ExtensibleLogger.FormatPIIValue(this.primarySmtpAddress)),
				new KeyValuePair<string, object>(UserContextCookie.UserContextCookiePrefix, this.userContextCookie),
				new KeyValuePair<string, object>("CN.A", this.action),
				new KeyValuePair<string, object>("CN.S", ((int)this.canaryStatus).ToString("X")),
				new KeyValuePair<string, object>("CN.T", this.creationTime),
				new KeyValuePair<string, object>("CN.L", this.logData)
			};
		}

		// Token: 0x040013F8 RID: 5112
		private readonly string primarySmtpAddress;

		// Token: 0x040013F9 RID: 5113
		private readonly string mailboxGuid;

		// Token: 0x040013FA RID: 5114
		private readonly string action;

		// Token: 0x040013FB RID: 5115
		private readonly string userContextCookie;

		// Token: 0x040013FC RID: 5116
		private readonly CanaryLogEvent.CanaryStatus canaryStatus;

		// Token: 0x040013FD RID: 5117
		private readonly DateTime creationTime;

		// Token: 0x040013FE RID: 5118
		private readonly string logData;

		// Token: 0x0200042F RID: 1071
		[Flags]
		public enum CanaryStatus
		{
			// Token: 0x04001400 RID: 5120
			None = 0,
			// Token: 0x04001401 RID: 5121
			IsCanaryRenewed = 16,
			// Token: 0x04001402 RID: 5122
			IsCanaryAfterLogonRequest = 32,
			// Token: 0x04001403 RID: 5123
			IsCanaryValid = 64,
			// Token: 0x04001404 RID: 5124
			IsCanaryAboutToExpire = 128,
			// Token: 0x04001405 RID: 5125
			IsCanaryNeeded = 256
		}
	}
}

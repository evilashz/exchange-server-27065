using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200043D RID: 1085
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ExceptionLogEvent : ILogEvent
	{
		// Token: 0x060024EC RID: 9452 RVA: 0x00085C74 File Offset: 0x00083E74
		public ExceptionLogEvent(string eventId, IMailboxContext userContext, Exception exception)
		{
			if (eventId == null)
			{
				throw new ArgumentNullException("eventId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.eventId = eventId;
			this.userContext = userContext;
			this.exceptionText = exception.ToString();
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00085CCB File Offset: 0x00083ECB
		public string EventId
		{
			get
			{
				return this.eventId;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00085CD4 File Offset: 0x00083ED4
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("PSA", ExtensibleLogger.FormatPIIValue(this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString())),
				new KeyValuePair<string, object>(UserContextCookie.UserContextCookiePrefix, this.userContext.Key.UserContextId.ToString(CultureInfo.InvariantCulture)),
				new KeyValuePair<string, object>("EX", this.exceptionText)
			};
		}

		// Token: 0x04001484 RID: 5252
		private readonly IMailboxContext userContext;

		// Token: 0x04001485 RID: 5253
		private readonly string eventId;

		// Token: 0x04001486 RID: 5254
		private readonly string exceptionText;
	}
}

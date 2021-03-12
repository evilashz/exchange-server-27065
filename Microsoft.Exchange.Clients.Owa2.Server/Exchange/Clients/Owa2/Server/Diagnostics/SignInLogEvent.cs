using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000460 RID: 1120
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SignInLogEvent : ILogEvent
	{
		// Token: 0x06002597 RID: 9623 RVA: 0x00088317 File Offset: 0x00086517
		public SignInLogEvent(IMailboxContext userContext, string userContextKey, UserContextStatistics userContextStatistics, Uri requestUri)
		{
			if (userContextStatistics == null)
			{
				throw new ArgumentNullException("userContextStatistics");
			}
			this.userContext = userContext;
			this.userContextKey = (userContextKey ?? string.Empty);
			this.userContextStatistics = userContextStatistics;
			this.requestUri = requestUri;
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x00088353 File Offset: 0x00086553
		public string EventId
		{
			get
			{
				return "SignIn";
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008835C File Offset: 0x0008655C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					UserContextCookie.UserContextCookiePrefix,
					this.userContextKey
				},
				{
					"ActID",
					(currentActivityScope == null) ? Guid.Empty : currentActivityScope.ActivityId
				},
				{
					"C",
					this.userContextStatistics.Created.ToString(CultureInfo.InvariantCulture)
				},
				{
					"CT",
					this.userContextStatistics.AcquireLatency
				},
				{
					"EPT",
					this.userContextStatistics.ExchangePrincipalCreationTime
				},
				{
					"MR",
					this.userContextStatistics.MiniRecipientCreationTime
				},
				{
					"SKU",
					this.userContextStatistics.SKUCapabilityTestTime
				},
				{
					"IL",
					this.userContextStatistics.CookieCreated ? 1 : 0
				},
				{
					"Err",
					(int)this.userContextStatistics.Error
				},
				{
					"CAN",
					this.requestUri.AbsolutePath
				}
			};
			if (this.userContext != null && this.userContext.ExchangePrincipal != null)
			{
				dictionary.Add("MG", this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid);
				dictionary.Add("PSA", ExtensibleLogger.FormatPIIValue(this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			return dictionary;
		}

		// Token: 0x040015C9 RID: 5577
		private const string SignInEventId = "SignIn";

		// Token: 0x040015CA RID: 5578
		private readonly IMailboxContext userContext;

		// Token: 0x040015CB RID: 5579
		private readonly string userContextKey;

		// Token: 0x040015CC RID: 5580
		private readonly UserContextStatistics userContextStatistics;

		// Token: 0x040015CD RID: 5581
		private readonly Uri requestUri;
	}
}

using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000484 RID: 1156
	public class SearchQueryStxProbe : SearchQueryStxProbeBase
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x000AB648 File Offset: 0x000A9848
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			base.CheckSimpleQueryMode(base.Definition.TargetResource);
			try
			{
				using (MailboxSession mailboxSession = SearchStoreHelper.GetMailboxSession(base.MonitoringMailboxSmtpAddress, false, "Monitoring"))
				{
					int num = 0;
					try
					{
						num = SearchStoreHelper.GetQueryHitCount(mailboxSession, SearchQueryStxProbe.StxSubjectQuery, 3);
					}
					catch (TimeoutException)
					{
						base.Result.StateAttribute4 = "Failed";
						throw new SearchProbeFailureException(Strings.SearchQueryStxTimeout("subject:SearchQueryStxProbe", base.MonitoringMailboxSmtpAddress, 180));
					}
					ExDateTime exDateTime;
					if (num > 0)
					{
						base.Result.StateAttribute1 = Strings.SearchQueryStxSuccess(num, "subject:SearchQueryStxProbe", base.MonitoringMailboxSmtpAddress);
						base.Result.StateAttribute4 = "Healthy";
					}
					else if (base.CheckExistenceAndCreateMessage(mailboxSession, out exDateTime))
					{
						string text = SearchQueryFailureProbe.GetFullTextIndexExceptionEventsCached(base.Definition.RecurrenceIntervalSeconds);
						if (string.IsNullOrWhiteSpace(text))
						{
							text = Strings.SearchInformationNotAvailable;
						}
						base.Result.StateAttribute4 = "Failed";
						throw new SearchProbeFailureException(Strings.SearchQueryStxZeroHitMonitoringMailbox("subject:SearchQueryStxProbe", base.MonitoringMailboxSmtpAddress, exDateTime.ToString(), text));
					}
				}
			}
			catch (ConnectionFailedTransientException ex)
			{
				base.Result.StateAttribute1 = ex.GetType().Name;
			}
		}

		// Token: 0x040013F5 RID: 5109
		private static readonly QueryFilter StxSubjectQuery = new AndFilter(new QueryFilter[]
		{
			new TextFilter(ItemSchema.Subject, "SearchQueryStxProbe", MatchOptions.PrefixOnWords, MatchFlags.Loose),
			new OrFilter(new QueryFilter[]
			{
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.Note", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.Schedule.Meeting", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.OCTEL.VOICE", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.VOICENOTES", MatchOptions.PrefixOnWords, MatchFlags.Loose)
			})
		});
	}
}

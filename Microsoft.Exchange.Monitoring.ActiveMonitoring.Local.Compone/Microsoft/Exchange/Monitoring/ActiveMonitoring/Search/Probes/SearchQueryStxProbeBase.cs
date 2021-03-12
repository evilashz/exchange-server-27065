using System;
using Microsoft.Ceres.SearchCore.Admin.Config;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x0200047D RID: 1149
	public abstract class SearchQueryStxProbeBase : SearchProbeBase
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000AA1A6 File Offset: 0x000A83A6
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x000AA1AE File Offset: 0x000A83AE
		internal string MonitoringMailboxSmtpAddress { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x000AA1B7 File Offset: 0x000A83B7
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x000AA1BF File Offset: 0x000A83BF
		internal int MessageAgeMinutes { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x000AA1C8 File Offset: 0x000A83C8
		protected override bool SkipOnNonHealthyCatalog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x000AA1CB File Offset: 0x000A83CB
		protected override bool SkipOnNonActiveDatabase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000AA1D0 File Offset: 0x000A83D0
		internal bool IsSimpleQueryMode(string databaseName)
		{
			string indexSystemName = FastIndexVersion.GetIndexSystemName(SearchMonitoringHelper.GetDatabaseInfo(databaseName).MailboxDatabaseGuid);
			IndexSystemModel indexSystemModel = IndexManager.Instance.GetIndexSystemModel(indexSystemName);
			IIndexSystemIndex indexSystemIndex = indexSystemModel.Indexes[0];
			string[] options = indexSystemIndex.Options;
			int i = 0;
			while (i < options.Length)
			{
				string text = options[i];
				bool result;
				if (text.Equals("query_mode=full", StringComparison.OrdinalIgnoreCase))
				{
					result = false;
				}
				else
				{
					if (!text.Equals("query_mode=simple", StringComparison.OrdinalIgnoreCase))
					{
						i++;
						continue;
					}
					result = true;
				}
				return result;
			}
			return false;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000AA250 File Offset: 0x000A8450
		internal void CheckSimpleQueryMode(string databaseName)
		{
			try
			{
				if (this.IsSimpleQueryMode(databaseName))
				{
					base.Result.StateAttribute4 = "FailedSimpleMode";
					throw new SearchProbeFailureException(Strings.SearchQueryStxSimpleQueryMode);
				}
			}
			catch (PerformingFastOperationException)
			{
				SearchMonitoringHelper.LogInfo(this, "Failed to get query mode.", new object[0]);
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000AA2A8 File Offset: 0x000A84A8
		internal bool CheckExistenceAndCreateMessage(MailboxSession session, out ExDateTime creationTime)
		{
			bool result;
			using (Folder inboxFolder = SearchStoreHelper.GetInboxFolder(session))
			{
				if (SearchStoreHelper.GetMessageBySubject(inboxFolder, "SearchQueryStxProbe", out creationTime) == null)
				{
					SearchStoreHelper.CreateMessage(session, inboxFolder, "SearchQueryStxProbe");
					base.Result.StateAttribute1 = "Message is created in inbox.";
					result = false;
				}
				else
				{
					if (ExDateTime.UtcNow - creationTime > TimeSpan.FromMinutes((double)this.MessageAgeMinutes))
					{
						SearchStoreHelper.CreateMessage(session, inboxFolder, "SearchQueryStxProbe");
						base.Result.StateAttribute1 = string.Format("Message exists in inbox with timestamp {0}. A new message is created.", creationTime);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000AA35C File Offset: 0x000A855C
		protected virtual void InitializeAttributes()
		{
			this.MonitoringMailboxSmtpAddress = base.AttributeHelper.GetString("MonitoringMailboxSmtpAddress", false, string.Empty);
			this.MessageAgeMinutes = base.AttributeHelper.GetInt("MessageAgeMinutes", false, 10, null, null);
			base.Result.StateAttribute2 = this.MonitoringMailboxSmtpAddress;
			base.Result.StateAttribute3 = "subject:SearchQueryStxProbe";
		}

		// Token: 0x040013E2 RID: 5090
		internal const string MonitoringEmailSubject = "SearchQueryStxProbe";

		// Token: 0x040013E3 RID: 5091
		internal const string QueryString = "subject:SearchQueryStxProbe";

		// Token: 0x040013E4 RID: 5092
		internal const int MaxResultsCount = 3;

		// Token: 0x0200047E RID: 1150
		internal static class AttributeNames
		{
			// Token: 0x040013E7 RID: 5095
			internal const string MonitoringMailboxSmtpAddress = "MonitoringMailboxSmtpAddress";

			// Token: 0x040013E8 RID: 5096
			internal const string MessageAgeMinutes = "MessageAgeMinutes";
		}
	}
}

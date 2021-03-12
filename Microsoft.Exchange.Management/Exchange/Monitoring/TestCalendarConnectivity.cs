using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C9 RID: 1481
	[Cmdlet("Test", "CalendarConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestCalendarConnectivity : TestVirtualDirectoryConnectivity
	{
		// Token: 0x06003402 RID: 13314 RVA: 0x000D26DC File Offset: 0x000D08DC
		public TestCalendarConnectivity() : base(Strings.CasHealthCalendarLongName, Strings.CasHealthCalendarShortName, TransientErrorCache.CalendarTransientErrorCache, "MSExchange Monitoring CalendarConnectivity Internal", "MSExchange Monitoring CalendarConnectivity External")
		{
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000D2700 File Offset: 0x000D0900
		protected override IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter)
		{
			return base.GetVirtualDirectories<ADOwaVirtualDirectory>(serverId, new AndFilter(new QueryFilter[]
			{
				filter,
				TestCalendarConnectivity.VersionFilter
			}));
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000D2730 File Offset: 0x000D0930
		protected override CasTransactionOutcome BuildOutcome(string scenarioName, string scenarioDescription, TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return new CasTransactionOutcome(instance.CasFqdn, scenarioName, scenarioDescription, "Calendar Latency", base.LocalSiteName, false, instance.credentials.UserName, instance.VirtualDirectoryName, instance.baseUri, instance.UrlType);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000D2774 File Offset: 0x000D0974
		protected override List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			TaskLogger.LogEnter();
			try
			{
				instance.baseUri = new UriBuilder(new Uri(instance.baseUri, "calendar/"))
				{
					Scheme = Uri.UriSchemeHttp,
					Port = 80
				}.Uri;
				CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(base.ApplicationShortName, base.ApplicationName, instance);
				casTransactionOutcome.Update(CasTransactionResultEnum.Success);
				this.ExecuteCalendarVDirTests(instance, casTransactionOutcome);
				instance.Result.Outcomes.Add(casTransactionOutcome);
			}
			finally
			{
				instance.Result.Complete();
				TaskLogger.LogExit();
			}
			return null;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000D2820 File Offset: 0x000D0A20
		private void ExecuteCalendarVDirTests(TestCasConnectivity.TestCasConnectivityRunInstance instance, CasTransactionOutcome outcome)
		{
			string text = string.Format("{0}/Calendar/calendar.html", instance.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			string text2 = string.Format("{0}/Calendar/calendar.ics", instance.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			ADSessionSettings adsessionSettings = instance.exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 293, "ExecuteCalendarVDirTests", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestCalendarConnectivity.cs");
			using (MailboxCalendarFolderDataProvider mailboxCalendarFolderDataProvider = new MailboxCalendarFolderDataProvider(adsessionSettings, DirectoryHelper.ReadADRecipient(instance.exchangePrincipal.MailboxInfo.MailboxGuid, instance.exchangePrincipal.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession) as ADUser, "Test-CalendarConnectivity"))
			{
				StoreObjectId defaultFolderId = mailboxCalendarFolderDataProvider.MailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
				Microsoft.Exchange.Data.Storage.Management.MailboxFolderId identity = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(instance.exchangePrincipal.ObjectId, defaultFolderId, null);
				MailboxCalendarFolder mailboxCalendarFolder = (MailboxCalendarFolder)mailboxCalendarFolderDataProvider.Read<MailboxCalendarFolder>(identity);
				if (!mailboxCalendarFolder.PublishEnabled)
				{
					mailboxCalendarFolder.SearchableUrlEnabled = true;
					mailboxCalendarFolder.PublishEnabled = true;
					mailboxCalendarFolder.PublishedCalendarUrl = new Uri(instance.baseUri, text).ToString();
					mailboxCalendarFolder.PublishedICalUrl = new Uri(instance.baseUri, text2).ToString();
					try
					{
						mailboxCalendarFolderDataProvider.Save(mailboxCalendarFolder);
					}
					catch (NotAllowedPublishingByPolicyException ex)
					{
						instance.Outcomes.Enqueue(new Warning(ex.LocalizedString));
						return;
					}
				}
			}
			ADOwaVirtualDirectory adowaVirtualDirectory = instance.VirtualDirectory as ADOwaVirtualDirectory;
			if (adowaVirtualDirectory != null && !(adowaVirtualDirectory.AnonymousFeaturesEnabled != true))
			{
				base.WriteMonitoringEvent(1104, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasHealthCalendarVDirSuccess);
				TimeSpan latency;
				if (!this.TestCalendarUrlResponse(text2, instance, TestCalendarConnectivity.CalendarContext.ICalContext, out latency))
				{
					outcome.Update(CasTransactionResultEnum.Failure);
				}
				else
				{
					outcome.UpdateLatency(latency);
				}
				if (!this.TestCalendarUrlResponse(text, instance, TestCalendarConnectivity.CalendarContext.ViewCalendarContext, out latency))
				{
					outcome.Update(CasTransactionResultEnum.Failure);
				}
				return;
			}
			instance.Outcomes.Enqueue(new Warning(Strings.CasHealthCalendarVDirWarning(instance.VirtualDirectoryName, instance.CasFqdn)));
			outcome.Update(CasTransactionResultEnum.Skipped);
			base.WriteMonitoringEvent(1105, this.MonitoringEventSource, EventTypeEnumeration.Warning, Strings.CasHealthCalendarVDirWarning(instance.VirtualDirectoryName, instance.CasFqdn));
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000D2AA4 File Offset: 0x000D0CA4
		private bool TestCalendarUrlResponse(string relativePath, TestCasConnectivity.TestCasConnectivityRunInstance instance, TestCalendarConnectivity.CalendarContext context, out TimeSpan latency)
		{
			bool flag = false;
			string text = string.Empty;
			Uri uri = new Uri(instance.baseUri, relativePath);
			latency = TimeSpan.Zero;
			for (int i = 0; i < 3; i++)
			{
				if (i > 0)
				{
					Thread.Sleep(20000);
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.Method = "GET";
				httpWebRequest.Accept = "*/*";
				httpWebRequest.Credentials = null;
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.UserAgent = context.UserAgent;
				httpWebRequest.Timeout = 30000;
				base.WriteVerbose(Strings.CasHealthWebAppSendingRequest(uri));
				ExDateTime now = ExDateTime.Now;
				try
				{
					string text2 = string.Empty;
					HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
					{
						text2 = streamReader.ReadToEnd();
					}
					base.WriteVerbose(Strings.CasHealthWebAppResponseReceived(uri, httpWebResponse.StatusCode, string.Empty, text2));
					if (httpWebResponse.StatusCode != HttpStatusCode.OK)
					{
						text = Strings.CasHealthCalendarResponseError(httpWebResponse.StatusCode.ToString());
					}
					else if (string.IsNullOrEmpty(text2) || !text2.Contains(context.CheckText))
					{
						text = Strings.CasHealthCalendarResponseError(text2);
					}
					else
					{
						text = string.Empty;
						flag = true;
					}
				}
				catch (WebException ex)
				{
					base.WriteVerbose(Strings.CasHealthWebAppRequestException(uri, ex.Status, string.Empty, ex.Message));
					text = Strings.CasHealthCalendarWebRequestException(ex.Message);
				}
				latency = base.ComputeLatency(now);
				if (flag)
				{
					break;
				}
			}
			CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(context.ScenarioName, context.ScenarioDescription, instance);
			casTransactionOutcome.Update(flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure, latency, text);
			instance.Outcomes.Enqueue(casTransactionOutcome);
			if (!flag)
			{
				base.WriteMonitoringEvent(context.EventIdError, this.MonitoringEventSource, EventTypeEnumeration.Error, Strings.CasHealthCalendarCheckError(context.ScenarioDescription, text));
			}
			else
			{
				base.WriteMonitoringEvent(context.EventIdSuccess, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.CasHealthCalendarCheckSuccess(context.ScenarioDescription));
			}
			return flag;
		}

		// Token: 0x04002400 RID: 9216
		private const string MonitoringEventSourceInternal = "MSExchange Monitoring CalendarConnectivity Internal";

		// Token: 0x04002401 RID: 9217
		private const string MonitoringEventSourceExternal = "MSExchange Monitoring CalendarConnectivity External";

		// Token: 0x04002402 RID: 9218
		public const string PublishedStartPageString = "AnonymousCalendar";

		// Token: 0x04002403 RID: 9219
		private const string VCalendarString = "BEGIN:VCALENDAR";

		// Token: 0x04002404 RID: 9220
		private const int RequestTimeOut = 30000;

		// Token: 0x04002405 RID: 9221
		private const int TimeToWaitBeforRetry = 20000;

		// Token: 0x04002406 RID: 9222
		private const int MaxReTryTimes = 3;

		// Token: 0x04002407 RID: 9223
		private const string MonitoringLatencyPerfCounter = "Calendar Latency";

		// Token: 0x04002408 RID: 9224
		private static readonly QueryFilter VersionFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADOwaVirtualDirectorySchema.OwaVersion, OwaVersions.Exchange2010);

		// Token: 0x020005CA RID: 1482
		private static class CalendarEventId
		{
			// Token: 0x04002409 RID: 9225
			internal const int ICalUrlSuccess = 1100;

			// Token: 0x0400240A RID: 9226
			internal const int ICalUrlError = 1101;

			// Token: 0x0400240B RID: 9227
			internal const int ViewUrlSuccess = 1102;

			// Token: 0x0400240C RID: 9228
			internal const int ViewUrlError = 1103;

			// Token: 0x0400240D RID: 9229
			internal const int PolicyVDirSuccess = 1104;

			// Token: 0x0400240E RID: 9230
			internal const int PolicyVDirWarning = 1105;
		}

		// Token: 0x020005CB RID: 1483
		private struct CalendarContext
		{
			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x06003409 RID: 13321 RVA: 0x000D2D0C File Offset: 0x000D0F0C
			// (set) Token: 0x0600340A RID: 13322 RVA: 0x000D2D14 File Offset: 0x000D0F14
			internal string ScenarioName { get; private set; }

			// Token: 0x17000F8E RID: 3982
			// (get) Token: 0x0600340B RID: 13323 RVA: 0x000D2D1D File Offset: 0x000D0F1D
			// (set) Token: 0x0600340C RID: 13324 RVA: 0x000D2D25 File Offset: 0x000D0F25
			internal string ScenarioDescription { get; private set; }

			// Token: 0x17000F8F RID: 3983
			// (get) Token: 0x0600340D RID: 13325 RVA: 0x000D2D2E File Offset: 0x000D0F2E
			// (set) Token: 0x0600340E RID: 13326 RVA: 0x000D2D36 File Offset: 0x000D0F36
			internal string CheckText { get; private set; }

			// Token: 0x17000F90 RID: 3984
			// (get) Token: 0x0600340F RID: 13327 RVA: 0x000D2D3F File Offset: 0x000D0F3F
			// (set) Token: 0x06003410 RID: 13328 RVA: 0x000D2D47 File Offset: 0x000D0F47
			internal int EventIdSuccess { get; private set; }

			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x06003411 RID: 13329 RVA: 0x000D2D50 File Offset: 0x000D0F50
			// (set) Token: 0x06003412 RID: 13330 RVA: 0x000D2D58 File Offset: 0x000D0F58
			internal int EventIdError { get; private set; }

			// Token: 0x17000F92 RID: 3986
			// (get) Token: 0x06003413 RID: 13331 RVA: 0x000D2D61 File Offset: 0x000D0F61
			// (set) Token: 0x06003414 RID: 13332 RVA: 0x000D2D69 File Offset: 0x000D0F69
			internal string UserAgent { get; private set; }

			// Token: 0x0400240F RID: 9231
			internal static readonly TestCalendarConnectivity.CalendarContext ViewCalendarContext = new TestCalendarConnectivity.CalendarContext
			{
				ScenarioName = Strings.CasHealthCalendarScenarioTestView,
				ScenarioDescription = Strings.CasHealthCalendarScenarioTestViewDesc,
				CheckText = "AnonymousCalendar",
				EventIdSuccess = 1102,
				EventIdError = 1103,
				UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)"
			};

			// Token: 0x04002410 RID: 9232
			internal static readonly TestCalendarConnectivity.CalendarContext ICalContext = new TestCalendarConnectivity.CalendarContext
			{
				ScenarioName = Strings.CasHealthCalendarScenarioTestICal,
				ScenarioDescription = Strings.CasHealthCalendarScenarioTestICalDesc,
				CheckText = "BEGIN:VCALENDAR",
				EventIdSuccess = 1100,
				EventIdError = 1101,
				UserAgent = null
			};
		}
	}
}

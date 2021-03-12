using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x0200000B RID: 11
	internal class MailboxSearchTask : IMailboxSearchTask, IDisposable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00005334 File Offset: 0x00003534
		public MailboxSearchTask(IEwsClient ewsClient, string keywordStatisticsQuery, MultiValuedProperty<string> userKeywords, IRecipientSession recipientSession, IExportContext exportContext, string executingUserPrimarySmtpAddress, int previewMaxMailboxes, OrganizationId orgId) : this(ewsClient, keywordStatisticsQuery, userKeywords, recipientSession, exportContext, executingUserPrimarySmtpAddress, previewMaxMailboxes, false, null, orgId)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005358 File Offset: 0x00003558
		public MailboxSearchTask(IEwsClient ewsClient, string keywordStatisticsQuery, MultiValuedProperty<string> userKeywords, IRecipientSession recipientSession, IExportContext exportContext, string executingUserPrimarySmtpAddress, int previewMaxMailboxes, bool isPFSearchFlightingEnabled, MailboxDiscoverySearch searchObject, OrganizationId orgId)
		{
			Util.ThrowIfNull(ewsClient, "ewsClient");
			Util.ThrowIfNull(exportContext, "exportContext");
			this.isStatisticsOnlySearch = true;
			this.keywordStatisticsQuery = keywordStatisticsQuery;
			this.CurrentState = SearchState.NotStarted;
			this.Errors = new List<string>(1);
			this.executingUserPrimarySmtpAddress = executingUserPrimarySmtpAddress;
			this.ewsClient = ewsClient;
			this.ExportContext = exportContext;
			this.previewMaxMailboxes = previewMaxMailboxes;
			this.InitializeUserKeywordsMapping(keywordStatisticsQuery, userKeywords, recipientSession);
			this.isPFSearchFlightingEnabled = isPFSearchFlightingEnabled;
			this.searchObject = searchObject;
			this.orgId = orgId;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000053E5 File Offset: 0x000035E5
		public MailboxSearchTask(ITargetMailbox targetMailbox, ServerToServerCallingContextFactory callingContextFactory, string executingUserPrimarySmtpAddress, OrganizationId orgId) : this(targetMailbox, callingContextFactory, executingUserPrimarySmtpAddress, orgId, false)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000053F3 File Offset: 0x000035F3
		public MailboxSearchTask(ITargetMailbox targetMailbox, ServerToServerCallingContextFactory callingContextFactory, string executingUserPrimarySmtpAddress, OrganizationId orgId, bool isDocIdHintFlightingEnabled) : this(targetMailbox, ExportHandlerFactory.CreateExportHandler(new MailboxSearchTask.MailboxSearchTracer(), targetMailbox, new ExchangeServiceClientFactory(callingContextFactory ?? new ServerToServerCallingContextFactory(null))), executingUserPrimarySmtpAddress, orgId, isDocIdHintFlightingEnabled)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000541C File Offset: 0x0000361C
		public MailboxSearchTask(ITargetMailbox targetMailbox, IExportHandler exportHandler, string executingUserPrimarySmtpAddress, OrganizationId orgId) : this(targetMailbox, exportHandler, executingUserPrimarySmtpAddress, orgId, false)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000542C File Offset: 0x0000362C
		public MailboxSearchTask(ITargetMailbox targetMailbox, IExportHandler exportHandler, string executingUserPrimarySmtpAddress, OrganizationId orgId, bool isDocIdHintFlightingEnabled)
		{
			Util.ThrowIfNull(targetMailbox, "targetMailbox");
			Util.ThrowIfNull(exportHandler, "exportHandler");
			this.isStatisticsOnlySearch = false;
			this.CurrentState = SearchState.NotStarted;
			this.Errors = new List<string>(1);
			this.TargetMailbox = targetMailbox;
			this.executingUserPrimarySmtpAddress = executingUserPrimarySmtpAddress;
			exportHandler.IsDocIdHintFlightingEnabled = isDocIdHintFlightingEnabled;
			this.exportHandler = exportHandler;
			this.ExportContext = this.exportHandler.ExportContext;
			this.exportHandler.OnReportStatistics += this.ReportStatistics;
			this.previewMaxMailboxes = 0;
			this.orgId = orgId;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000054C4 File Offset: 0x000036C4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000054CC File Offset: 0x000036CC
		public EventHandler<ExportStatusEventArgs> OnReportStatistics { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000054D5 File Offset: 0x000036D5
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000054DD File Offset: 0x000036DD
		public Action<int, long, long, long, List<KeywordHit>> OnEstimateCompleted { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000054E6 File Offset: 0x000036E6
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000054EE File Offset: 0x000036EE
		public Action<ISearchResults> OnPrepareCompleted { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000054F7 File Offset: 0x000036F7
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000054FF File Offset: 0x000036FF
		public Action OnExportCompleted { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005508 File Offset: 0x00003708
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00005510 File Offset: 0x00003710
		public IExportContext ExportContext { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005519 File Offset: 0x00003719
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00005521 File Offset: 0x00003721
		public ISearchResults SearchResults { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000552A File Offset: 0x0000372A
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00005532 File Offset: 0x00003732
		public SearchState CurrentState { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000553B File Offset: 0x0000373B
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00005543 File Offset: 0x00003743
		public IList<string> Errors { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000554C File Offset: 0x0000374C
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00005554 File Offset: 0x00003754
		public ITargetMailbox TargetMailbox { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005560 File Offset: 0x00003760
		private string DiscoverySearchTaskErrorHint
		{
			get
			{
				string result = string.Empty;
				if (this.orgId != null)
				{
					result = this.orgId.ToString();
				}
				else if (this.ExportContext.Sources != null && this.ExportContext.Sources.Count > 0)
				{
					result = this.ExportContext.Sources[0].Id;
				}
				return result;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000055C7 File Offset: 0x000037C7
		public void Abort()
		{
			this.isTaskAborted = true;
			if (this.exportHandler != null)
			{
				this.exportHandler.Stop();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005F21 File Offset: 0x00004121
		public void Start()
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				int mailboxesSearchedCount = 0;
				long itemCount = 0L;
				long totalSize = 0L;
				long unsearchableItemCount = 0L;
				long unsearchableTotalSize = 0L;
				List<ErrorRecord> unsearchableFailedMailboxes = null;
				List<ErrorRecord> failedMailboxes = null;
				List<KeywordHit> keywordHits = null;
				SearchState searchState = SearchState.NotStarted;
				Exception exception = null;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						try
						{
							if (this.searchObject != null)
							{
								this.searchObject.ScenarioId = ScenarioData.Current["SID"];
							}
							if (this.isStatisticsOnlySearch)
							{
								if (string.IsNullOrEmpty(this.keywordStatisticsQuery))
								{
									ScenarioData.Current["S"] = "ES";
									if (this.searchObject != null && !string.IsNullOrEmpty(this.searchObject.CalculatedQuery))
									{
										ScenarioData.Current["QL"] = this.searchObject.CalculatedQuery.Length.ToString();
									}
								}
								else
								{
									ScenarioData.Current["S"] = "KS";
									ScenarioData.Current["QL"] = this.keywordStatisticsQuery.Length.ToString();
								}
								string text = (this.searchObject == null) ? string.Empty : this.searchObject.CalculatedQuery;
								List<string> list = null;
								if (this.ExportContext.Sources != null && this.ExportContext.Sources.Count > 0)
								{
									ScenarioData.Current["M"] = this.ExportContext.Sources.Count.ToString();
									ISource source = this.ExportContext.Sources[0];
									if (string.IsNullOrEmpty(text))
									{
										text = source.SourceFilter;
									}
									list = new List<string>(this.ExportContext.Sources.Count);
									foreach (ISource source2 in this.ExportContext.Sources)
									{
										list.Add(source2.LegacyExchangeDN);
									}
								}
								if (list != null || this.isPFSearchFlightingEnabled)
								{
									bool flag = false;
									this.GetSearchResultEstimation(text, list, out mailboxesSearchedCount, false, ref flag, out itemCount, out totalSize, out failedMailboxes);
								}
								if (!string.IsNullOrEmpty(this.keywordStatisticsQuery) && !this.isTaskAborted)
								{
									List<ErrorRecord> list2 = null;
									List<KeywordStatisticsSearchResultType> keywordStatistics = this.ewsClient.GetKeywordStatistics(this.executingUserPrimarySmtpAddress, this.keywordStatisticsQuery, this.ExportContext.ExportMetadata.Language, (list != null) ? list : new List<string>(1), out list2, (this.searchObject == null || !this.isPFSearchFlightingEnabled) ? null : this.searchObject.Name);
									keywordHits = Util.ConvertToKeywordHitList(keywordStatistics, this.userKeywordsMap);
									if (list2 != null)
									{
										if (failedMailboxes == null)
										{
											failedMailboxes = list2;
										}
										else
										{
											failedMailboxes.AddRange(list2);
										}
									}
								}
								if (Util.IncludeUnsearchableItems(this.ExportContext) && !this.isTaskAborted && (list != null || (this.searchObject != null && this.searchObject.IsFeatureFlighted("PublicFolderSearchFlighted"))))
								{
									KeywordHit keywordHit = null;
									if (keywordHits != null)
									{
										keywordHit = new KeywordHit
										{
											Phrase = "652beee2-75f7-4ca0-8a02-0698a3919cb9"
										};
										keywordHits.Add(keywordHit);
									}
									bool flag2 = false;
									if (this.searchObject != null && this.searchObject.IsFeatureFlighted("PublicFolderSearchFlighted"))
									{
										this.GetSearchResultEstimation(text, list, out mailboxesSearchedCount, true, ref flag2, out unsearchableItemCount, out unsearchableTotalSize, out unsearchableFailedMailboxes);
										if (flag2 && keywordHit != null)
										{
											keywordHit.Count += (int)unsearchableItemCount;
										}
									}
									else
									{
										foreach (string mailboxId in list)
										{
											if (this.isTaskAborted)
											{
												break;
											}
											long unsearchableItemStatistics = this.ewsClient.GetUnsearchableItemStatistics(this.executingUserPrimarySmtpAddress, mailboxId);
											unsearchableItemCount += unsearchableItemStatistics;
											if (keywordHit != null)
											{
												keywordHit.Count += (int)unsearchableItemStatistics;
												keywordHit.MailboxCount += ((unsearchableItemStatistics == 0L) ? 0 : 1);
											}
										}
									}
								}
								searchState = (this.isTaskAborted ? SearchState.EstimateStopped : SearchState.EstimateSucceeded);
							}
							else
							{
								ScenarioData.Current["S"] = "CS";
								this.exportHandler.Prepare();
								this.exportHandler.Export();
								this.PrepareCompleted(this.exportHandler.SearchResults);
								searchState = (this.isTaskAborted ? SearchState.Stopped : SearchState.Succeeded);
							}
						}
						catch (ExportException exception)
						{
							ExportException exception = exception;
						}
						catch (StorageTransientException exception2)
						{
							ExportException exception = exception2;
						}
						catch (StoragePermanentException exception3)
						{
							ExportException exception = exception3;
						}
						catch (DataSourceOperationException exception4)
						{
							ExportException exception = exception4;
						}
					});
				}
				catch (GrayException ex)
				{
					exception = ex;
					ExTraceGlobals.SearchTracer.TraceError<GrayException>((long)this.GetHashCode(), "GrayException {0} is thrown", ex);
				}
				finally
				{
					if (exception != null)
					{
						string message = exception.Message;
						if (string.IsNullOrEmpty(message) && exception.InnerException != null)
						{
							message = exception.InnerException.Message;
						}
						SearchEventLogger.Instance.LogDiscoverySearchTaskErrorEvent(this.ExportContext.ExportMetadata.ExportName, this.DiscoverySearchTaskErrorHint, exception);
						this.Errors.Add(message);
						mailboxesSearchedCount = 0;
						itemCount = 0L;
						totalSize = 0L;
						unsearchableItemCount = 0L;
						keywordHits = null;
						searchState = (this.isStatisticsOnlySearch ? SearchState.EstimateFailed : SearchState.Failed);
					}
					if (failedMailboxes != null && failedMailboxes.Count > 0)
					{
						foreach (ErrorRecord errorRecord in failedMailboxes)
						{
							this.Errors.Add(Util.GenerateErrorMessageFromErrorRecord(errorRecord));
						}
						this.Errors.Insert(0, "Number of failed mailboxes: " + failedMailboxes.Count);
					}
					try
					{
						if (this.isStatisticsOnlySearch)
						{
							this.EstimateCompleted(mailboxesSearchedCount, itemCount, totalSize, unsearchableItemCount, keywordHits, searchState);
						}
						else
						{
							this.ExportCompleted(searchState);
						}
					}
					catch (ExportException ex2)
					{
						SearchEventLogger.Instance.LogDiscoverySearchTaskErrorEvent(this.ExportContext.ExportMetadata.ExportName, this.DiscoverySearchTaskErrorHint, ex2);
						this.Errors.Add(ex2.Message);
						searchState = (this.isStatisticsOnlySearch ? SearchState.EstimateFailed : SearchState.Failed);
					}
				}
			}, delegate(object exception)
			{
				ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "InternalStart: Unhandled exception {0}", new object[]
				{
					exception
				});
				SearchEventLogger.Instance.LogDiscoverySearchTaskErrorEvent(this.ExportContext.ExportMetadata.ExportName, this.DiscoverySearchTaskErrorHint, exception.ToString());
				return !(exception is GrayException);
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005F40 File Offset: 0x00004140
		public void Dispose()
		{
			if (this.TargetMailbox != null)
			{
				this.TargetMailbox.Dispose();
				this.TargetMailbox = null;
			}
			if (this.exportHandler != null)
			{
				this.exportHandler.OnReportStatistics -= this.ReportStatistics;
				this.exportHandler.Dispose();
				this.exportHandler = null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005F98 File Offset: 0x00004198
		private void ReportStatistics(object sender, ExportStatusEventArgs e)
		{
			if (this.OnReportStatistics != null)
			{
				this.OnReportStatistics(sender, e);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005FAF File Offset: 0x000041AF
		private void EstimateCompleted(int mailboxesSearchedCount, long itemCount, long totalSize, long unsearchableItemCount, List<KeywordHit> keywordHits, SearchState status)
		{
			this.CurrentState = status;
			if (this.OnEstimateCompleted != null)
			{
				this.OnEstimateCompleted(mailboxesSearchedCount, itemCount, totalSize, unsearchableItemCount, keywordHits);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005FD3 File Offset: 0x000041D3
		private void PrepareCompleted(ISearchResults searchResults)
		{
			if (!this.isTaskAborted && this.OnPrepareCompleted != null)
			{
				this.OnPrepareCompleted(searchResults);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005FF1 File Offset: 0x000041F1
		private void ExportCompleted(SearchState status)
		{
			this.CurrentState = status;
			if (this.OnExportCompleted != null)
			{
				this.OnExportCompleted();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006010 File Offset: 0x00004210
		private void GetSearchResultEstimation(string query, List<string> mailboxIds, out int mailboxesSearchedCount, bool isUnsearchable, ref bool newSchemaSearchSucceeded, out long totalItemCount, out long totalSize, out List<ErrorRecord> failedMailboxes)
		{
			mailboxesSearchedCount = 0;
			totalItemCount = 0L;
			totalSize = 0L;
			failedMailboxes = null;
			int num = 0;
			while ((mailboxIds != null && num < mailboxIds.Count) || this.isPFSearchFlightingEnabled)
			{
				if (this.isTaskAborted)
				{
					return;
				}
				int num2 = 0;
				long num3 = 0L;
				long num4 = 0L;
				List<ErrorRecord> list = null;
				List<string> list2 = new List<string>(this.previewMaxMailboxes);
				int num5 = 0;
				if (mailboxIds != null)
				{
					num5 = ((num + this.previewMaxMailboxes >= mailboxIds.Count) ? (mailboxIds.Count - num) : this.previewMaxMailboxes);
					list2.AddRange(mailboxIds.GetRange(num, num5));
				}
				newSchemaSearchSucceeded = false;
				this.ewsClient.GetSearchResultEstimation(this.executingUserPrimarySmtpAddress, query, this.ExportContext.ExportMetadata.Language, list2, out num2, isUnsearchable, out num3, out num4, out list, out newSchemaSearchSucceeded, (this.searchObject == null || !this.isPFSearchFlightingEnabled) ? null : this.searchObject.Name);
				mailboxesSearchedCount += num2;
				totalItemCount += num3;
				totalSize += num4;
				if (list != null)
				{
					if (failedMailboxes == null)
					{
						failedMailboxes = list;
					}
					else
					{
						failedMailboxes.AddRange(list);
					}
				}
				num += num5;
				if (this.isPFSearchFlightingEnabled)
				{
					return;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006138 File Offset: 0x00004338
		private void InitializeUserKeywordsMapping(string keywordStatisticsQuery, MultiValuedProperty<string> userKeywords, IRecipientSession recipientSession)
		{
			if (string.IsNullOrEmpty(keywordStatisticsQuery))
			{
				return;
			}
			Util.ThrowIfNull(recipientSession, "recipientSession");
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, recipientSession.SessionSettings, 788, "InitializeUserKeywordsMapping", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\MailboxSearch\\Common\\MailboxSearchTask.cs");
			if (userKeywords != null && userKeywords.Count > 0)
			{
				CultureInfo culture = CultureInfo.InvariantCulture;
				if (this.ExportContext != null && this.ExportContext.ExportMetadata != null && !string.IsNullOrEmpty(this.ExportContext.ExportMetadata.Language))
				{
					try
					{
						culture = new CultureInfo(this.ExportContext.ExportMetadata.Language);
					}
					catch (CultureNotFoundException)
					{
						ExTraceGlobals.SearchTracer.TraceError<string>((long)this.GetHashCode(), "Culture info: \"{0}\" returns CultureNotFoundException", this.ExportContext.ExportMetadata.Language);
					}
				}
				SearchCriteria searchCriteria = new SearchCriteria(keywordStatisticsQuery, null, culture, SearchType.Statistics, recipientSession, tenantOrTopologyConfigurationSession, Guid.NewGuid(), new List<DefaultFolderType>());
				this.userKeywordsMap = new Dictionary<string, string>(userKeywords.Count);
				if (searchCriteria.SubFilters != null && searchCriteria.SubFilters.Count > 0)
				{
					if (userKeywords.Count != searchCriteria.SubFilters.Count)
					{
						return;
					}
					int num = 0;
					using (MultiValuedProperty<string>.Enumerator enumerator = userKeywords.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string key = enumerator.Current;
							string value = searchCriteria.SubFilters.Keys.ElementAt(num++);
							this.userKeywordsMap.Add(key, value);
						}
						return;
					}
				}
				if (userKeywords.Count == 1)
				{
					this.userKeywordsMap.Add(userKeywords[0], keywordStatisticsQuery);
				}
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly bool isStatisticsOnlySearch;

		// Token: 0x0400004D RID: 77
		private readonly string keywordStatisticsQuery;

		// Token: 0x0400004E RID: 78
		private readonly int previewMaxMailboxes;

		// Token: 0x0400004F RID: 79
		private readonly IEwsClient ewsClient;

		// Token: 0x04000050 RID: 80
		private readonly string executingUserPrimarySmtpAddress;

		// Token: 0x04000051 RID: 81
		private readonly bool isPFSearchFlightingEnabled;

		// Token: 0x04000052 RID: 82
		private Dictionary<string, string> userKeywordsMap;

		// Token: 0x04000053 RID: 83
		private IExportHandler exportHandler;

		// Token: 0x04000054 RID: 84
		private bool isTaskAborted;

		// Token: 0x04000055 RID: 85
		private MailboxDiscoverySearch searchObject;

		// Token: 0x04000056 RID: 86
		private OrganizationId orgId;

		// Token: 0x0200000C RID: 12
		internal class MailboxSearchTracer : Microsoft.Exchange.EDiscovery.Export.ITracer
		{
			// Token: 0x060000BA RID: 186 RVA: 0x000062DC File Offset: 0x000044DC
			public void TraceError(string format, params object[] args)
			{
				ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), format, args);
			}

			// Token: 0x060000BB RID: 187 RVA: 0x000062F1 File Offset: 0x000044F1
			public void TraceWarning(string format, params object[] args)
			{
				ExTraceGlobals.SearchTracer.TraceWarning((long)this.GetHashCode(), format, args);
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00006306 File Offset: 0x00004506
			public void TraceInformation(string format, params object[] args)
			{
				ExTraceGlobals.SearchTracer.TraceDebug((long)this.GetHashCode(), format, args);
			}
		}
	}
}

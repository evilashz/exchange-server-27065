using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.Performance;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000008 RID: 8
	internal class CrawlerFeeder : Executable, IFeeder, IExecutable, IDiagnosable, IDisposable
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00003014 File Offset: 0x00001214
		public CrawlerFeeder(MdbPerfCountersInstance mdbFeedingPerfCounters, MdbInfo mdbInfo, ISearchServiceConfig config, CrawlerMailboxIterator mailboxIterator, ICrawlerItemIterator<int> itemIterator, IWatermarkStorage stateStorage, IFailedItemStorage failedItemStorage, ISubmitDocument indexFeeder, IIndexStatusStore indexStatusStore) : base(config)
		{
			Util.ThrowOnNullArgument(mdbFeedingPerfCounters, "mdbFeedingPerfCounters");
			Util.ThrowOnNullArgument(mdbInfo, "mdbInfo");
			Util.ThrowOnNullArgument(config, "config");
			Util.ThrowOnNullArgument(mailboxIterator, "mailboxIterator");
			Util.ThrowOnNullArgument(itemIterator, "itemIterator");
			Util.ThrowOnNullArgument(stateStorage, "stateStorage");
			Util.ThrowOnNullArgument(indexFeeder, "indexFeeder");
			Util.ThrowOnNullArgument(indexStatusStore, "indexStatusStore");
			this.crawlingActivityId = Guid.NewGuid();
			base.DiagnosticsSession.ComponentName = "CrawlerFeeder";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.MdbCrawlerFeederTracer;
			this.mailboxIterator = mailboxIterator;
			this.itemIterator = itemIterator;
			this.stateStorage = stateStorage;
			this.failedItemStorage = failedItemStorage;
			this.indexFeeder = indexFeeder;
			this.indexStatusStore = indexStatusStore;
			this.mdbFeedingPerfCounters = mdbFeedingPerfCounters;
			this.MdbInfo = mdbInfo;
			this.documentQueueManager = new QueueManager<MdbItemIdentity>(base.Config.QueueSize, base.Config.QueueSize, null);
			this.watermarkManager = new CrawlerWatermarkManager(base.Config.QueueSize);
			this.loopDelay = base.Config.CrawlerRateLoopDelay;
			base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.MailboxesLeftToCrawl, 0L);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000014 RID: 20 RVA: 0x0000317C File Offset: 0x0000137C
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000031B4 File Offset: 0x000013B4
		internal event EventHandler Failed;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000031E9 File Offset: 0x000013E9
		public FeederType FeederType
		{
			get
			{
				return FeederType.Crawler;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000031EC File Offset: 0x000013EC
		internal Dictionary<int, List<MailboxCrawlerState>> PendingWatermarkUpdates
		{
			get
			{
				return this.pendingWatermarkUpdates;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000031F4 File Offset: 0x000013F4
		internal HashSet<int> PendingWatermarkDeletes
		{
			get
			{
				return this.pendingWatermarkDeletes;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000031FC File Offset: 0x000013FC
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CrawlerFeeder>(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003204 File Offset: 0x00001404
		public override XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(parameters);
			diagnosticInfo.Add(new XElement("MailboxesLeftToCrawl", (int)this.mdbFeedingPerfCounters.MailboxesLeftToCrawl.RawValue));
			diagnosticInfo.Add(new XElement("MailboxesLeftToRecrawl", (int)this.mdbFeedingPerfCounters.MailboxesLeftToRecrawl.RawValue));
			return diagnosticInfo;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003290 File Offset: 0x00001490
		internal void FinishSetMailboxCrawlerState(IAsyncResult ar)
		{
			MailboxCrawlerState mailboxCrawlerState = (MailboxCrawlerState)ar.AsyncState;
			bool flag = base.TryRunUnderExceptionHandler(delegate()
			{
				this.stateStorage.EndSetMailboxCrawlerState(ar);
			}, CrawlerFeeder.ErrorAccessingStateStorage);
			bool flag2 = false;
			lock (this.pendingWatermarkUpdatesLock)
			{
				if (this.pendingWatermarkUpdates.ContainsKey(mailboxCrawlerState.MailboxNumber))
				{
					List<MailboxCrawlerState> list = this.pendingWatermarkUpdates[mailboxCrawlerState.MailboxNumber];
					list.Remove(mailboxCrawlerState);
					if (list.Count > 0)
					{
						return;
					}
				}
				if (this.pendingWatermarkDeletes.Remove(mailboxCrawlerState.MailboxNumber) && mailboxCrawlerState.RawState != 2147483647)
				{
					flag2 = true;
				}
				if (mailboxCrawlerState.RawState == 2147483647)
				{
					this.pendingWatermarkUpdates.Remove(mailboxCrawlerState.MailboxNumber);
				}
			}
			if (flag && flag2)
			{
				this.UpdateMailboxCrawlerWatermark(mailboxCrawlerState.MailboxNumber, int.MaxValue, 0);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000033A4 File Offset: 0x000015A4
		protected override void InternalExecutionStart()
		{
			using (IDisposable disposable = this.activeThreadCount.AcquireReference())
			{
				if (disposable != null)
				{
					ICollection<MailboxCrawlerState> mailboxesForCrawling = this.stateStorage.GetMailboxesForCrawling();
					int num;
					int num2;
					int num3;
					bool flag;
					bool flag2;
					this.GetMailboxCrawlCount(mailboxesForCrawling, out num, out num2, out num3, out flag, out flag2);
					this.mailboxCrawlerStates = new Dictionary<int, MailboxCrawlerState>(flag2 ? num : (num2 + num3));
					foreach (MailboxCrawlerState mailboxCrawlerState in mailboxesForCrawling)
					{
						if (flag2 && !mailboxCrawlerState.RecrawlMailbox && !this.MdbInfo.CatalogVersion.IsUpgrading)
						{
							this.mailboxCrawlerStates.Add(mailboxCrawlerState.MailboxNumber, mailboxCrawlerState);
						}
						else if (!flag2 && (mailboxCrawlerState.RecrawlMailbox || (this.MdbInfo.CatalogVersion.IsUpgrading && base.Config.SchemaUpgradingEnabled)))
						{
							this.mailboxCrawlerStates.Add(mailboxCrawlerState.MailboxNumber, mailboxCrawlerState);
						}
					}
					this.throttlingManager = Factory.Current.CreateFeederRateThrottlingManager(base.Config, this.MdbInfo, flag2 ? FeederRateThrottlingManager.ThrottlingRateExecutionType.Fast : FeederRateThrottlingManager.ThrottlingRateExecutionType.LowResource);
					if (flag)
					{
						base.DiagnosticsSession.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_SuspendSchemaUpdate, this.MdbInfo.Name, new object[]
						{
							this.MdbInfo,
							this.MdbInfo.CatalogVersion,
							VersionInfo.Latest
						});
					}
					base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.MailboxesLeftToCrawl, (long)(num + num3));
					base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.MailboxesLeftToRecrawl, (long)num2);
					if (num == 0 && num2 == 0 && num3 == 0)
					{
						base.DiagnosticsSession.TraceDebug<MdbInfo>("(MDB {0}): No mailboxes to crawl.", this.MdbInfo);
						if (this.MdbInfo.CatalogVersion.IsUpgrading && !flag)
						{
							this.FinalizeSchemaUpdate();
						}
						lock (CrawlerFeeder.lastIdleLoggingTime)
						{
							DateTime d;
							if (!CrawlerFeeder.lastIdleLoggingTime.TryGetValue(this.MdbInfo.Name, out d) || DateTime.UtcNow - d > base.Config.CrawlerLoggingInterval)
							{
								CrawlerFeeder.lastIdleLoggingTime[this.MdbInfo.Name] = DateTime.UtcNow;
								base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.NoMailboxesToCrawl.ToString(), this.MdbInfo.Name, null, "{0}:{1}", new object[]
								{
									this.MdbInfo.CatalogVersion.FeedingVersion,
									this.MdbInfo.CatalogVersion.QueryVersion
								});
							}
						}
						base.CompleteExecute(null);
					}
					else
					{
						bool flag4 = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Search.RequireMountedForCrawl.Enabled ? (!this.MdbInfo.MountedOnLocalServer) : (!this.MdbInfo.PreferredActiveCopy);
						if (num > 0 && flag4 && (!ExEnvironment.IsTest || !base.Config.AllowCrawlOnPassiveForTest))
						{
							base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Crawling on passive copy and the catalog needs reseed for mdb {0}", new object[]
							{
								this.MdbInfo
							});
							base.CompleteExecute(new CatalogReseedException(IndexStatusErrorCode.CrawlOnPassive));
						}
						else
						{
							this.UpdateCrawlingStatus();
							this.documentEnumerator = this.GetDocuments().GetEnumerator();
							if (!base.Stopping)
							{
								this.MoveToNextDocument();
								this.CollectDocumentsAtRateStart();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000379C File Offset: 0x0000199C
		protected override void InternalExecutionFinish()
		{
			this.activeThreadCount.DisableAddRef();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000037AC File Offset: 0x000019AC
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (!this.activeThreadCount.TryWaitForZero(base.Config.MaxOperationTimeout))
				{
					base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Waiting for threads to stop: Did not shut down in a timely manner.", new object[0]);
				}
				if (this.documentEnumerator != null)
				{
					this.documentEnumerator.Dispose();
					this.documentEnumerator = null;
				}
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003A9C File Offset: 0x00001C9C
		protected virtual StoreSession GetMailboxSession(MailboxInfo mailbox)
		{
			return XsoUtil.TranslateXsoExceptionsWithReturnValue<StoreSession>(base.DiagnosticsSession, Strings.ConnectionToMailboxFailed(mailbox.MailboxGuid), XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound | XsoUtil.XsoExceptionHandlingFlags.DoNotExpectCorruptData, delegate()
			{
				ExchangePrincipal exchangePrincipal;
				try
				{
					exchangePrincipal = XsoUtil.GetExchangePrincipal(this.Config, this.MdbInfo, mailbox.MailboxGuid);
				}
				catch (ObjectNotFoundException ex)
				{
					this.DiagnosticsSession.TraceDebug<MdbInfo, MailboxInfo, ObjectNotFoundException>("(MDB {0}): Skipping mailbox: {1}, reason: {2}", this.MdbInfo, mailbox, ex);
					this.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxSkipped.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), ex.ToString(), new object[0]);
					return null;
				}
				catch (MailboxInfoStaleException ex2)
				{
					this.DiagnosticsSession.TraceDebug<MdbInfo, MailboxInfo, MailboxInfoStaleException>("(MDB {0}): Skipping mailbox: {1}, reason: {2}", this.MdbInfo, mailbox, ex2);
					this.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxSkippedInfoStale.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), ex2.ToString(), new object[0]);
					return null;
				}
				Exception ex3 = null;
				try
				{
					return XsoUtil.GetStoreSession(this.Config, exchangePrincipal, mailbox.IsPublicFolderMailbox, "Client=CI;Action=CrawlerFeeder");
				}
				catch (MailboxUnavailableException ex4)
				{
					ex3 = ex4;
				}
				catch (AccountDisabledException ex5)
				{
					ex3 = ex5;
				}
				catch (WrongServerException ex6)
				{
					ex3 = ex6;
				}
				catch (ObjectNotFoundException ex7)
				{
					ex3 = ex7;
				}
				catch (MailboxInfoStaleException ex8)
				{
					if (!this.Config.ReadFromPassiveEnabled || this.MdbInfo.IsLagCopy)
					{
						throw;
					}
					this.DiagnosticsSession.TraceDebug<MdbInfo, MailboxInfo, MailboxInfoStaleException>("(MDB {0}): Skipping mailbox: {1}, reason: {2}", this.MdbInfo, mailbox, ex8);
					ex3 = ex8;
				}
				this.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxSkippedUnavailable.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), ex3.ToString(), new object[0]);
				return null;
			});
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00004390 File Offset: 0x00002590
		private IEnumerable<MdbItemIdentity> GetDocuments()
		{
			List<MailboxInfo> mailboxList = new List<MailboxInfo>();
			Dictionary<int, MailboxInfo> mailboxesInStore = new Dictionary<int, MailboxInfo>();
			foreach (MailboxInfo mailboxInfo in this.mailboxIterator.GetMailboxes())
			{
				mailboxList.Add(mailboxInfo);
				mailboxesInStore.Add(mailboxInfo.MailboxNumber, mailboxInfo);
			}
			foreach (int num in this.mailboxCrawlerStates.Keys)
			{
				if (base.Stopping)
				{
					yield break;
				}
				if (!mailboxesInStore.ContainsKey(num))
				{
					this.UpdateMailboxCrawlerWatermark(num, int.MaxValue, 0);
				}
			}
			foreach (MailboxInfo mailbox in mailboxList)
			{
				if (base.Stopping)
				{
					yield break;
				}
				MailboxCrawlerState mailboxCrawlerState;
				if (!this.mailboxCrawlerStates.TryGetValue(mailbox.MailboxNumber, out mailboxCrawlerState))
				{
					base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxSkippedRemoved.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), string.Empty, new object[0]);
				}
				else
				{
					base.DiagnosticsSession.TraceDebug<MdbInfo, MailboxInfo>("(MDB {0}): Starting to crawl mailbox: {1}", this.MdbInfo, mailbox);
					FailedItemParameters failedItemParameters = new FailedItemParameters(FailureMode.Permanent, FieldSet.None)
					{
						MailboxGuid = new Guid?(mailbox.MailboxGuid)
					};
					long failedItemsCount = this.TryGetFailedDocumentsCount(failedItemParameters);
					long itemsCount = this.TryGetItemsCount(mailbox.MailboxGuid);
					base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxStarted.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), "{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}", new object[]
					{
						mailbox.IsArchive,
						mailbox.IsPublicFolderMailbox,
						mailbox.IsSharedMailbox,
						mailbox.IsTeamSiteMailbox,
						mailbox.IsModernGroupMailbox,
						this.MdbInfo.CatalogVersion.QueryVersion,
						this.MdbInfo.CatalogVersion.FeedingVersion,
						this.MdbInfo.CatalogVersion.IsUpgrading,
						this.mdbFeedingPerfCounters.MailboxesLeftToCrawl.RawValue,
						failedItemsCount,
						itemsCount
					});
					using (StoreSession session = this.GetMailboxSession(mailbox))
					{
						if (session != null)
						{
							goto IL_474;
						}
						int num2 = mailboxCrawlerState.AttemptCount + 1;
						int num3 = (num2 < MailboxCrawlerState.MaxCrawlAttemptCount) ? -3 : -4;
						base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Failed to get a session for Mailbox: {0}, Attempt Count: {1}, MailboxCrawlState: {2}, MailboxGuid: {3}", new object[]
						{
							mailbox.DisplayName,
							num2,
							(num3 == -4) ? "PermanentFailure" : "RetriableFailure",
							mailbox.MailboxGuid
						});
						this.UpdateMailboxCrawlerWatermark(mailbox.MailboxNumber, num3, num2);
					}
					continue;
					IL_474:
					MdbItemIdentity lastItem = null;
					StoreSession session;
					foreach (MdbItemIdentity item in this.itemIterator.GetItems(session, mailboxCrawlerState.LastDocumentIdIndexed, int.MaxValue))
					{
						lastItem = item;
						yield return item;
					}
					if (lastItem != null)
					{
						base.DiagnosticsSession.TraceDebug<MdbInfo, int, int>("(MDB {0}): Mailbox ({1}) is done. Mark the last document id = {2}", this.MdbInfo, mailbox.MailboxNumber, lastItem.DocumentId);
						this.watermarkManager.SetLast(mailbox.MailboxNumber, lastItem.DocumentId);
						long num4 = this.TryGetFailedDocumentsCount(failedItemParameters);
						failedItemParameters.FailureMode = FailureMode.Transient;
						long num5 = this.TryGetFailedDocumentsCount(failedItemParameters);
						base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxCompleted.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), "{0}:{1}", new object[]
						{
							num4,
							num5
						});
					}
					else
					{
						base.DiagnosticsSession.TraceDebug<MdbInfo, int>("(MDB {0}): Mailbox ({1}) is empty. Mark state completed", this.MdbInfo, mailbox.MailboxNumber);
						this.UpdateMailboxCrawlerWatermark(mailbox.MailboxNumber, int.MaxValue, 0);
						base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.MailboxCompletedEmpty.ToString(), this.MdbInfo.Name, mailbox.MailboxGuid.ToString(), string.Empty, new object[0]);
					}
				}
			}
			yield break;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000043BC File Offset: 0x000025BC
		private bool MoveToNextDocument()
		{
			bool result;
			using (ExPerfTrace.RelatedActivity(this.crawlingActivityId))
			{
				bool flag;
				if (!base.TryRunUnderExceptionHandler<bool>(() => this.documentEnumerator.MoveNext(), out flag, CrawlerFeeder.FailedToCrawlMailbox))
				{
					this.currentDocument = null;
					result = false;
				}
				else
				{
					this.currentDocument = (flag ? this.documentEnumerator.Current : null);
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000443C File Offset: 0x0000263C
		private bool CollectDocuments(ref int documentsToQueue)
		{
			bool flag = false;
			int num = documentsToQueue;
			bool result;
			using (IDisposable disposable = this.activeThreadCount.AcquireReference())
			{
				if (disposable == null)
				{
					result = flag;
				}
				else
				{
					MdbItemIdentity mdbItemIdentity = null;
					try
					{
						while (!base.Stopping && documentsToQueue != 0)
						{
							mdbItemIdentity = this.currentDocument;
							if (mdbItemIdentity == null)
							{
								break;
							}
							if (!this.documentQueueManager.Enqueue(mdbItemIdentity))
							{
								base.DiagnosticsSession.TraceDebug<MdbInfo>("(MDB {0}):Document queue is full", this.MdbInfo);
								break;
							}
							documentsToQueue--;
							if (!this.MoveToNextDocument())
							{
								base.DiagnosticsSession.TraceDebug<MdbInfo>("(MDB {0}): No more documents", this.MdbInfo);
								break;
							}
						}
					}
					finally
					{
						documentsToQueue = num - documentsToQueue;
						bool crawlerFeederCollectDocumentsVerifyPendingWatermarks = base.Config.CrawlerFeederCollectDocumentsVerifyPendingWatermarks;
						bool flag2 = false;
						if (crawlerFeederCollectDocumentsVerifyPendingWatermarks)
						{
							lock (this.pendingWatermarkUpdates)
							{
								foreach (List<MailboxCrawlerState> list in this.pendingWatermarkUpdates.Values)
								{
									if (list.Count != 0)
									{
										flag2 = true;
										break;
									}
								}
								flag2 |= (this.pendingWatermarkDeletes.Count != 0);
							}
						}
						if (num != 0 && mdbItemIdentity == null && this.documentQueueManager.Length == 0 && this.documentQueueManager.OutstandingLength == 0 && (!crawlerFeederCollectDocumentsVerifyPendingWatermarks || (crawlerFeederCollectDocumentsVerifyPendingWatermarks && !flag2)))
						{
							base.DiagnosticsSession.TraceDebug<MdbInfo>("(MDB {0}): Crawling has completed", this.MdbInfo);
							base.DiagnosticsSession.LogCrawlerInfo(DiagnosticsLoggingTag.Informational, CrawlerFeeder.CrawlerEvent.CrawlCompleted.ToString(), this.MdbInfo.Name, null, "{0}:{1}:{2}", new object[]
							{
								this.mdbFeedingPerfCounters.MailboxesLeftToCrawl.RawValue,
								this.MdbInfo.CatalogVersion.FeedingVersion,
								this.MdbInfo.CatalogVersion.QueryVersion
							});
							base.CompleteExecute("CrawlComplete");
							flag = true;
						}
						else
						{
							this.SendDocuments();
						}
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000046CC File Offset: 0x000028CC
		private void SendDocuments()
		{
			lock (this.documentQueueManager)
			{
				IEnumerable<MdbItemIdentity> enumerable;
				if (this.documentQueueManager.Dequeue(out enumerable))
				{
					foreach (MdbItemIdentity mdbItemIdentity in enumerable)
					{
						if (base.Stopping)
						{
							this.documentQueueManager.Remove(mdbItemIdentity);
						}
						else
						{
							if (this.mdbFeedingPerfCounters != null)
							{
								base.DiagnosticsSession.IncrementCounter(this.mdbFeedingPerfCounters.NumberOfDocumentsSentForProcessingCrawler);
							}
							this.watermarkManager.Add(mdbItemIdentity);
							IFastDocument fastDocument = this.indexFeeder.CreateFastDocument(DocumentOperation.Insert);
							this.indexFeeder.DocumentHelper.PopulateFastDocumentForIndexing(fastDocument, this.MdbInfo.CatalogVersion.FeedingVersion, mdbItemIdentity.MailboxGuid, mdbItemIdentity.MailboxNumber, false, !this.MdbInfo.IsLagCopy, mdbItemIdentity.DocumentId, mdbItemIdentity);
							try
							{
								this.indexFeeder.BeginSubmitDocument(fastDocument, new AsyncCallback(this.DocumentCompleteCallback), mdbItemIdentity);
							}
							catch (ObjectDisposedException result)
							{
								base.DiagnosticsSession.TraceError<MdbInfo>("(MDB {0}): FastFeeder has been disposed", this.MdbInfo);
								base.CompleteExecute(result);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000485C File Offset: 0x00002A5C
		private void DocumentCompleteCallback(IAsyncResult asyncResult)
		{
			using (ExPerfTrace.RelatedActivity(this.crawlingActivityId))
			{
				using (IDisposable disposable = this.activeThreadCount.AcquireReference())
				{
					MdbItemIdentity mdbItemIdentity = (MdbItemIdentity)asyncResult.AsyncState;
					try
					{
						if (!this.indexFeeder.EndSubmitDocument(asyncResult))
						{
							base.CompleteExecute(null);
							return;
						}
						if (this.mdbFeedingPerfCounters != null)
						{
							base.DiagnosticsSession.IncrementCounter(this.mdbFeedingPerfCounters.NumberOfDocumentsIndexedCrawler);
							base.DiagnosticsSession.IncrementCounter(this.mdbFeedingPerfCounters.NumberOfDocumentsProcessed);
						}
					}
					catch (Exception ex)
					{
						EventHandler failed = this.Failed;
						if (failed != null)
						{
							failed(this, new EventArgs());
						}
						base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Document failure: ID: {0}, Failure: {1}", new object[]
						{
							mdbItemIdentity,
							ex
						});
						base.CompleteExecute(ex);
						return;
					}
					if (disposable != null)
					{
						try
						{
							MailboxCrawlerState mailboxCrawlerState;
							if (this.watermarkManager.TryComplete(mdbItemIdentity, out mailboxCrawlerState))
							{
								this.UpdateMailboxCrawlerWatermark(mailboxCrawlerState.MailboxNumber, mailboxCrawlerState.LastDocumentIdIndexed, 0);
							}
						}
						finally
						{
							this.documentQueueManager.Remove(mdbItemIdentity);
						}
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000049B4 File Offset: 0x00002BB4
		private void CollectDocumentsAtRate(object state, bool timerFired)
		{
			if (!timerFired)
			{
				return;
			}
			using (ExPerfTrace.RelatedActivity(this.crawlingActivityId))
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				TimeSpan timeSpan = utcNow.Subtract(this.lastCollectionTime);
				int num = (this.rate > 0.0) ? Math.Max((int)(timeSpan.Duration().TotalSeconds * this.rate), 1) : 0;
				if (this.mdbFeedingPerfCounters != null)
				{
					base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.AverageAttemptedCrawlerRate, (long)((double)num / timeSpan.Duration().TotalSeconds));
				}
				bool flag = this.CollectDocuments(ref num);
				if (this.mdbFeedingPerfCounters != null)
				{
					base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.AverageCrawlerRate, (long)((double)num / timeSpan.Duration().TotalSeconds));
				}
				this.lastCollectionTime = utcNow;
				this.rate = this.throttlingManager.ThrottlingRateContinue(this.rate);
				if (!flag)
				{
					try
					{
						RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(base.StopEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.CollectDocumentsAtRate)), state, this.loopDelay.Duration(), true);
					}
					catch (ObjectDisposedException result)
					{
						base.DiagnosticsSession.TraceError<MdbInfo>("(MDB {0}): CrawlerFeeder has been disposed", this.MdbInfo);
						base.CompleteExecute(result);
					}
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004B40 File Offset: 0x00002D40
		private void CollectDocumentsAtRateStart()
		{
			this.lastCollectionTime = ExDateTime.UtcNow.Subtract(this.loopDelay);
			this.rate = this.throttlingManager.ThrottlingRateStart();
			RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(base.StopEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.CollectDocumentsAtRate)), null, this.loopDelay.Duration(), true);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004C04 File Offset: 0x00002E04
		private void UpdateMailboxCrawlerWatermark(int mailboxNumber, int mailboxState, int attemptCount = 0)
		{
			MailboxCrawlerState mailboxCrawlerState = new MailboxCrawlerState(mailboxNumber, mailboxState, attemptCount);
			lock (this.pendingWatermarkUpdatesLock)
			{
				if (mailboxState == 2147483647)
				{
					if (this.pendingWatermarkUpdates.ContainsKey(mailboxNumber) && this.pendingWatermarkUpdates[mailboxNumber].Count > 0)
					{
						this.pendingWatermarkDeletes.Add(mailboxNumber);
						return;
					}
				}
				else if (this.pendingWatermarkUpdates.ContainsKey(mailboxNumber))
				{
					this.pendingWatermarkUpdates[mailboxNumber].Add(mailboxCrawlerState);
				}
				else
				{
					this.pendingWatermarkUpdates.Add(mailboxNumber, new List<MailboxCrawlerState>(1)
					{
						mailboxCrawlerState
					});
				}
			}
			Exception objectDiposedExceptionInsideHandler = null;
			bool flag2 = base.TryRunUnderExceptionHandler(delegate()
			{
				try
				{
					this.stateStorage.BeginSetMailboxCrawlerState(mailboxCrawlerState, new AsyncCallback(this.FinishSetMailboxCrawlerState), mailboxCrawlerState);
				}
				catch (ObjectDisposedException objectDiposedExceptionInsideHandler)
				{
					objectDiposedExceptionInsideHandler = objectDiposedExceptionInsideHandler;
				}
			}, CrawlerFeeder.ErrorAccessingStateStorage);
			if (objectDiposedExceptionInsideHandler == null)
			{
				if (flag2 && (mailboxState == 2147483647 || mailboxState == -4) && this.mailboxCrawlerStates.ContainsKey(mailboxNumber))
				{
					base.DiagnosticsSession.DecrementCounter(this.mdbFeedingPerfCounters.MailboxesLeftToCrawl);
					if (!this.mailboxCrawlerStates[mailboxNumber].RecrawlMailbox)
					{
						this.UpdateCrawlingStatus();
					}
				}
				return;
			}
			base.DiagnosticsSession.TraceError<MdbInfo>("(MDB {0}): CrawlerFeeder has been disposed", this.MdbInfo);
			base.CompleteExecute(objectDiposedExceptionInsideHandler);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004D78 File Offset: 0x00002F78
		private long TryGetFailedDocumentsCount(FailedItemParameters parameters)
		{
			long result;
			try
			{
				result = this.failedItemStorage.GetFailedItemsCount(parameters);
			}
			catch (Exception)
			{
				result = -1L;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004DAC File Offset: 0x00002FAC
		private long TryGetItemsCount(Guid mailboxGuid)
		{
			long result;
			try
			{
				result = this.failedItemStorage.GetItemsCount(mailboxGuid);
			}
			catch (Exception)
			{
				result = -1L;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004E00 File Offset: 0x00003000
		private void FinalizeSchemaUpdate()
		{
			if (base.TryRunUnderExceptionHandler(delegate()
			{
				this.stateStorage.BeginSetVersionInfo(VersionInfo.Latest, new AsyncCallback(this.FinishSetVersionInfo), null);
			}, CrawlerFeeder.ErrorAccessingStateStorage))
			{
				base.DiagnosticsSession.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_FinishSchemaUpdate, this.MdbInfo.Name, new object[]
				{
					this.MdbInfo,
					VersionInfo.Latest
				});
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004E80 File Offset: 0x00003080
		private void FinishSetVersionInfo(IAsyncResult ar)
		{
			base.TryRunUnderExceptionHandler(delegate()
			{
				this.stateStorage.EndSetVersionInfo(ar);
			}, CrawlerFeeder.ErrorAccessingStateStorage);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004EBC File Offset: 0x000030BC
		private void GetMailboxCrawlCount(ICollection<MailboxCrawlerState> mailboxesToCrawl, out int crawlCount, out int recrawlCount, out int upgradeCount, out bool suspendSchemaUpgrade, out bool crawling)
		{
			crawlCount = 0;
			recrawlCount = 0;
			upgradeCount = 0;
			suspendSchemaUpgrade = false;
			crawling = false;
			foreach (MailboxCrawlerState mailboxCrawlerState in mailboxesToCrawl)
			{
				if (mailboxCrawlerState.RecrawlMailbox)
				{
					recrawlCount++;
				}
				else if (!this.MdbInfo.CatalogVersion.IsUpgrading)
				{
					crawlCount++;
					crawling = true;
				}
				else if (base.Config.SchemaUpgradingEnabled)
				{
					upgradeCount++;
				}
				else
				{
					suspendSchemaUpgrade = true;
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004F5C File Offset: 0x0000315C
		private void UpdateCrawlingStatus()
		{
			int num = (int)this.mdbFeedingPerfCounters.MailboxesLeftToCrawl.RawValue;
			bool crawlerFeederUpdateCrawlingStatusResetCache = base.Config.CrawlerFeederUpdateCrawlingStatusResetCache;
			if (crawlerFeederUpdateCrawlingStatusResetCache && num == 0)
			{
				this.stateStorage.RefreshCachedCrawlerWatermarks();
				if (this.stateStorage.HasCrawlerWatermarks())
				{
					ICollection<MailboxCrawlerState> mailboxesForCrawling = this.stateStorage.GetMailboxesForCrawling();
					int num2;
					int num3;
					int num4;
					bool flag;
					bool flag2;
					this.GetMailboxCrawlCount(mailboxesForCrawling, out num2, out num3, out num4, out flag, out flag2);
					num = num2 + num4;
				}
			}
			if (num == 0 && base.Config.SchemaUpgradingEnabled)
			{
				this.FinalizeSchemaUpdate();
				this.MdbInfo.CatalogVersion = VersionInfo.Latest;
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "SchemaUpgradeComplete", new object[]
				{
					this.MdbInfo.Guid,
					this.MdbInfo.CatalogVersion
				});
			}
			IndexStatus indexStatus;
			if (num > 0)
			{
				indexStatus = this.indexStatusStore.SetIndexStatus(this.MdbInfo.Guid, num, this.MdbInfo.CatalogVersion);
			}
			else
			{
				indexStatus = this.indexStatusStore.SetIndexStatus(this.MdbInfo.Guid, ContentIndexStatusType.Healthy, IndexStatusErrorCode.Success, this.MdbInfo.CatalogVersion, null);
			}
			if (this.mdbFeedingPerfCounters != null)
			{
				base.DiagnosticsSession.SetCounterRawValue(this.mdbFeedingPerfCounters.IndexingStatus, (long)indexStatus.IndexingState);
			}
		}

		// Token: 0x0400000E RID: 14
		public const string CrawlComplete = "CrawlComplete";

		// Token: 0x0400000F RID: 15
		protected readonly MdbInfo MdbInfo;

		// Token: 0x04000010 RID: 16
		private static readonly LocalizedString ErrorAccessingStateStorage = Strings.ErrorAccessingStateStorage;

		// Token: 0x04000011 RID: 17
		private static readonly LocalizedString FailedToCrawlMailbox = Strings.FailedToCrawlMailbox;

		// Token: 0x04000012 RID: 18
		private static Dictionary<string, DateTime> lastIdleLoggingTime = new Dictionary<string, DateTime>();

		// Token: 0x04000013 RID: 19
		private readonly IWatermarkStorage stateStorage;

		// Token: 0x04000014 RID: 20
		private readonly IFailedItemStorage failedItemStorage;

		// Token: 0x04000015 RID: 21
		private readonly ISubmitDocument indexFeeder;

		// Token: 0x04000016 RID: 22
		private readonly CrawlerMailboxIterator mailboxIterator;

		// Token: 0x04000017 RID: 23
		private readonly ICrawlerItemIterator<int> itemIterator;

		// Token: 0x04000018 RID: 24
		private readonly MdbPerfCountersInstance mdbFeedingPerfCounters;

		// Token: 0x04000019 RID: 25
		private readonly QueueManager<MdbItemIdentity> documentQueueManager;

		// Token: 0x0400001A RID: 26
		private readonly CrawlerWatermarkManager watermarkManager;

		// Token: 0x0400001B RID: 27
		private readonly RefCount activeThreadCount = new RefCount();

		// Token: 0x0400001C RID: 28
		private readonly Guid crawlingActivityId;

		// Token: 0x0400001D RID: 29
		private readonly IIndexStatusStore indexStatusStore;

		// Token: 0x0400001E RID: 30
		private readonly TimeSpan loopDelay;

		// Token: 0x0400001F RID: 31
		private readonly Dictionary<int, List<MailboxCrawlerState>> pendingWatermarkUpdates = new Dictionary<int, List<MailboxCrawlerState>>();

		// Token: 0x04000020 RID: 32
		private readonly HashSet<int> pendingWatermarkDeletes = new HashSet<int>();

		// Token: 0x04000021 RID: 33
		private readonly object pendingWatermarkUpdatesLock = new object();

		// Token: 0x04000022 RID: 34
		private IEnumerator<MdbItemIdentity> documentEnumerator;

		// Token: 0x04000023 RID: 35
		private MdbItemIdentity currentDocument;

		// Token: 0x04000024 RID: 36
		private Dictionary<int, MailboxCrawlerState> mailboxCrawlerStates;

		// Token: 0x04000025 RID: 37
		private ExDateTime lastCollectionTime;

		// Token: 0x04000026 RID: 38
		private IFeederRateThrottlingManager throttlingManager;

		// Token: 0x04000027 RID: 39
		private double rate;

		// Token: 0x02000009 RID: 9
		private enum CrawlerEvent
		{
			// Token: 0x0400002A RID: 42
			NoMailboxesToCrawl = 1,
			// Token: 0x0400002B RID: 43
			MailboxStarted,
			// Token: 0x0400002C RID: 44
			MailboxSkipped,
			// Token: 0x0400002D RID: 45
			MailboxSkippedInfoStale,
			// Token: 0x0400002E RID: 46
			MailboxSkippedUnavailable,
			// Token: 0x0400002F RID: 47
			MailboxSkippedRemoved,
			// Token: 0x04000030 RID: 48
			MailboxCompleted,
			// Token: 0x04000031 RID: 49
			MailboxCompletedEmpty,
			// Token: 0x04000032 RID: 50
			CrawlCompleted
		}
	}
}

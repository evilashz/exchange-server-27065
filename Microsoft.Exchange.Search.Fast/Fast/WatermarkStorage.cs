﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000029 RID: 41
	internal sealed class WatermarkStorage : IDisposeTrackable, IWatermarkStorage, IDisposable
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public WatermarkStorage(ISubmitDocument fastWatermarkFeeder, ISearchServiceConfig config, string indexSystemName)
		{
			Util.ThrowOnNullArgument(fastWatermarkFeeder, "fastWatermarkFeeder");
			Util.ThrowOnNullArgument(config, "config");
			Util.ThrowOnNullArgument(indexSystemName, "indexSystemName");
			this.fastWatermarkFeeder = fastWatermarkFeeder;
			this.indexSystemName = indexSystemName;
			this.diagnosticSession = DiagnosticsSession.CreateComponentDiagnosticsSession("WatermarkStorage", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.WatermarkStorageTracer, (long)this.GetHashCode());
			this.fastQueryExecutor = new ExchangeQueryExecutor(config.HostName, config.QueryServicePort, FlowDescriptor.GetImsInternalFlowDescriptor(config, indexSystemName).DisplayName, false, 0, config.QueryOperationTimeout, config.QueryProxyCacheTimeout);
			this.lastSuccessfulUpdateAction = DateTime.MinValue;
			this.acceptableRestartInterval = config.WatermarkAcceptableFailureInterval;
			this.assertOnCorruptNotificationsWatermarkValue = config.AssertOnCorruptNotificationsWatermarkValue;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		public void Dispose()
		{
			if (!this.isDisposedFlag)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.fastQueryExecutor != null)
				{
					this.fastQueryExecutor.Dispose();
					this.fastQueryExecutor = null;
				}
				this.isDisposedFlag = true;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DDC1 File Offset: 0x0000BFC1
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<WatermarkStorage>(this);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public VersionInfo GetVersionInfo()
		{
			this.CheckDisposed();
			VersionInfo result;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				result = this.cachedVersionInfo;
			}
			return result;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public IAsyncResult BeginSetVersionInfo(VersionInfo version, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				this.cachedVersionInfo = version;
			}
			this.SendWatermarkUpdateToFast(lazyAsyncResult, WatermarkStorage.FastVersioningWatermarkDocumentId, version.RawValue, false, 0L, 0);
			this.diagnosticSession.TraceDebug<string, VersionInfo>("Updating versioning watermark for {0}, new version: {1}", this.indexSystemName, version);
			return lazyAsyncResult;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public void EndSetVersionInfo(IAsyncResult asyncResult)
		{
			this.EndSubmitDocument(asyncResult);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public long GetNotificationsWatermark()
		{
			this.CheckDisposed();
			long value;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				value = this.cachedNotificationsWatermark.Value;
			}
			return value;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DF04 File Offset: 0x0000C104
		public IAsyncResult BeginSetNotificationsWatermark(long watermark, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			long num = watermark;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				if (this.cachedNotificationsWatermark != null)
				{
					num -= this.cachedNotificationsWatermark.Value;
				}
				this.cachedNotificationsWatermark = new long?(watermark);
			}
			this.SendWatermarkUpdateToFast(lazyAsyncResult, WatermarkStorage.FastNotificationWatermarkDocumentId, watermark, false, num, 0);
			this.diagnosticSession.TraceDebug<string, long>("Updating notifications watermark for {0}, new watermark: {1}", this.indexSystemName, watermark);
			return lazyAsyncResult;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void EndSetNotificationsWatermark(IAsyncResult asyncResult)
		{
			this.EndSubmitDocument(asyncResult);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		public IAsyncResult BeginSetMailboxCrawlerState(MailboxCrawlerState mailboxState, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			long num = (long)mailboxState.LastDocumentIdIndexed;
			MailboxCrawlerState mailboxCrawlerState;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				if (this.cachedCrawlerWatermarks.TryGetValue(mailboxState.MailboxNumber, out mailboxCrawlerState))
				{
					num -= (long)mailboxCrawlerState.LastDocumentIdIndexed;
					mailboxCrawlerState.LastDocumentIdIndexed = mailboxState.LastDocumentIdIndexed;
					mailboxCrawlerState.AttemptCount = mailboxState.AttemptCount;
				}
				else
				{
					mailboxCrawlerState = mailboxState;
					this.cachedCrawlerWatermarks.Add(mailboxState.MailboxNumber, mailboxState);
				}
				if (mailboxState.IsCompleted)
				{
					this.cachedCrawlerWatermarks.Remove(mailboxState.MailboxNumber);
				}
			}
			this.SendWatermarkUpdateToFast(lazyAsyncResult, IndexId.CreateCrawlerWatermarkIndexId(mailboxCrawlerState.MailboxNumber), (long)mailboxCrawlerState.LastDocumentIdIndexed, mailboxCrawlerState.RecrawlMailbox, num, mailboxCrawlerState.AttemptCount);
			this.diagnosticSession.TraceDebug("Updating crawler watermark for {0}, mailbox {1}, attempt count: {2} new watermark: {3}", new object[]
			{
				this.indexSystemName,
				mailboxCrawlerState.MailboxNumber,
				mailboxCrawlerState.AttemptCount,
				mailboxCrawlerState.LastDocumentIdIndexed
			});
			return lazyAsyncResult;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		public void EndSetMailboxCrawlerState(IAsyncResult asyncResult)
		{
			this.EndSubmitDocument(asyncResult);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		public ICollection<MailboxCrawlerState> GetMailboxesForCrawling()
		{
			this.CheckDisposed();
			ICollection<MailboxCrawlerState> result;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				List<MailboxCrawlerState> list = new List<MailboxCrawlerState>(this.cachedCrawlerWatermarks.Values);
				result = list;
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000E150 File Offset: 0x0000C350
		public IAsyncResult BeginSetMailboxDeletionPending(MailboxState mailboxState, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				if (!mailboxState.IsCompleted)
				{
					this.cachedDeletedMailboxes.Add(mailboxState.MailboxNumber);
				}
				else
				{
					this.cachedDeletedMailboxes.Remove(mailboxState.MailboxNumber);
				}
			}
			this.SendWatermarkUpdateToFast(lazyAsyncResult, IndexId.CreateDeletionPendingIndexId(mailboxState.MailboxNumber), (long)mailboxState.RawState, false, 0L, 0);
			this.diagnosticSession.TraceDebug<string, int, int>("Updationg deletion pending for {0}, mailbox {1}, new watermark: {2}", this.indexSystemName, mailboxState.MailboxNumber, mailboxState.RawState);
			return lazyAsyncResult;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000E210 File Offset: 0x0000C410
		public void EndSetMailboxDeletionPending(IAsyncResult asyncResult)
		{
			this.EndSubmitDocument(asyncResult);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E21C File Offset: 0x0000C41C
		public ICollection<MailboxState> GetMailboxesForDeleting()
		{
			this.CheckDisposed();
			ICollection<MailboxState> result;
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				List<MailboxState> list = new List<MailboxState>(this.cachedDeletedMailboxes.Count);
				foreach (int mailboxNumber in this.cachedDeletedMailboxes)
				{
					list.Add(new MailboxState(mailboxNumber, -1));
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public void ResetWatermarkCache()
		{
			this.CheckDisposed();
			lock (this.sessionLock)
			{
				this.cachedNotificationsWatermark = null;
				this.cachedCrawlerWatermarks = null;
				this.cachedDeletedMailboxes = null;
				this.cachedVersionInfo = VersionInfo.Legacy;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000E32C File Offset: 0x0000C52C
		public void RefreshCachedCrawlerWatermarks()
		{
			this.CheckDisposed();
			lock (this.sessionLock)
			{
				this.cachedCrawlerWatermarks = null;
				this.FetchWatermarksFromFast();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E37C File Offset: 0x0000C57C
		public bool HasCrawlerWatermarks()
		{
			bool result = false;
			this.CheckDisposed();
			lock (this.sessionLock)
			{
				this.FetchWatermarksFromFast();
				if (this.cachedCrawlerWatermarks.Count != 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		internal static string GetWatermarkQuery()
		{
			return string.Format("mailboxguid:{0}", WatermarkStorageId.FastWatermarkTenantId);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		private void EndSubmitDocument(IAsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			lazyAsyncResult.InternalWaitForCompletion();
			Exception ex = (Exception)lazyAsyncResult.Result;
			if (ex == null)
			{
				return;
			}
			ComponentException ex2 = ex as ComponentException;
			if (ex2 != null)
			{
				throw new OperationFailedException(ex2);
			}
			throw new InvalidOperationException("WatermarkStorage.EndSubmitDocument: got unexpected exception from ISubmitDocument", ex);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E43E File Offset: 0x0000C63E
		private void CheckDisposed()
		{
			if (this.isDisposedFlag)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E45C File Offset: 0x0000C65C
		private void FetchWatermarksFromFast()
		{
			if (this.cachedNotificationsWatermark != null && this.cachedCrawlerWatermarks != null)
			{
				return;
			}
			this.diagnosticSession.TraceDebug<string>("Reading watermarks from FAST for {0}", this.indexSystemName);
			VersionInfo versionInfo = VersionInfo.Legacy;
			long? num = null;
			Dictionary<int, MailboxCrawlerState> dictionary = new Dictionary<int, MailboxCrawlerState>();
			HashSet<int> hashSet = new HashSet<int>();
			int num2 = 0;
			List<string> list = new List<string>(WatermarkStorage.watermarkSchema.Length);
			foreach (IndexSystemField indexSystemField in WatermarkStorage.watermarkSchema)
			{
				list.Add(indexSystemField.Name);
			}
			foreach (SearchResultItem searchResultItem in this.fastQueryExecutor.ExecuteQueryWithFields(WatermarkStorageId.FastWatermarkTenantId, WatermarkStorage.GetWatermarkQuery(), list))
			{
				num2++;
				long? num3 = null;
				long? num4 = null;
				int attemptCount = 0;
				EvaluationErrors evaluationErrors = EvaluationErrors.None;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (IFieldHolder fieldHolder in searchResultItem.Fields)
				{
					string name;
					if (fieldHolder.Value != null && (name = fieldHolder.Name) != null)
					{
						if (!(name == "DocId"))
						{
							if (name == "Other")
							{
								ISearchResultItem searchResultItem2 = (ISearchResultItem)fieldHolder.Value;
								foreach (IFieldHolder fieldHolder2 in searchResultItem2.Fields)
								{
									if (fieldHolder2.Value != null)
									{
										if (fieldHolder2.Name == FastIndexSystemSchema.Watermark.Name)
										{
											num3 = new long?((long)fieldHolder2.Value);
										}
										if (fieldHolder2.Name == FastIndexSystemSchema.ErrorCode.Name)
										{
											evaluationErrors = (EvaluationErrors)((long)fieldHolder2.Value);
										}
										if (fieldHolder2.Name == FastIndexSystemSchema.AttemptCount.Name)
										{
											attemptCount = (int)((long)fieldHolder2.Value);
										}
										stringBuilder.AppendFormat(", {0}: {1}", fieldHolder2.Name, fieldHolder2.Value);
									}
								}
							}
						}
						else
						{
							num4 = new long?((long)fieldHolder.Value);
							stringBuilder.AppendFormat("DocId: {0}", num4);
						}
					}
				}
				this.diagnosticSession.Assert(num4 != null, "[IndexSystem: {0}] DocId not found on watermark query result item: {1}", new object[]
				{
					this.indexSystemName,
					stringBuilder
				});
				if (num4 == WatermarkStorage.FastNotificationWatermarkDocumentId)
				{
					if (num3 != null)
					{
						num = num3;
						this.diagnosticSession.TraceDebug<string, long>("Got notifications watermark from FAST for {0}, watermark: {1}", this.indexSystemName, num3.Value);
					}
					else
					{
						string text = string.Format("[IndexSystem: {0}] Watermark query returned a corrupt result item: DocId: {1}, Watermark: {2}", this.indexSystemName, (num4 != null) ? num4.Value.ToString() : "missing", (num3 != null) ? num3.Value.ToString() : "missing");
						if (this.assertOnCorruptNotificationsWatermarkValue)
						{
							throw new InvalidOperationException(text);
						}
						this.diagnosticSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, text, new object[0]);
						num = new long?(-1L);
					}
				}
				else
				{
					this.diagnosticSession.Assert(num3 != null, "[IndexSystem: {0}] Watermark value not found on watermark query result item: {1}", new object[]
					{
						this.indexSystemName,
						stringBuilder
					});
					if (num4 == WatermarkStorage.FastVersioningWatermarkDocumentId)
					{
						this.diagnosticSession.TraceDebug<string, long>("Got versioning watermark from FAST for {0}, watermark: {1}", this.indexSystemName, num3.Value);
						versionInfo = VersionInfo.FromRaw(num3.Value);
					}
					else if (IndexId.GetMailboxNumber(num4.Value) == 0)
					{
						int documentId = IndexId.GetDocumentId(num4.Value);
						new MailboxState(IndexId.GetDocumentId(num4.Value), (int)num3.Value);
						this.diagnosticSession.TraceDebug<string, int, long>("Got deletion pending from FAST for {0}, mailbox {1} watermark: {2}", this.indexSystemName, documentId, num3.Value);
						hashSet.Add(documentId);
					}
					else
					{
						MailboxCrawlerState mailboxCrawlerState = new MailboxCrawlerState(IndexId.GetMailboxNumber(num4.Value), (int)num3.Value, attemptCount)
						{
							RecrawlMailbox = (evaluationErrors == EvaluationErrors.RecrawlWatermark)
						};
						if (!mailboxCrawlerState.IsCompleted)
						{
							this.diagnosticSession.TraceDebug<string, int, long>("Got crawler watermark from FAST for {0}, mailbox {1} watermark: {2}", this.indexSystemName, mailboxCrawlerState.MailboxNumber, num3.Value);
							dictionary.Add(mailboxCrawlerState.MailboxNumber, mailboxCrawlerState);
						}
					}
				}
			}
			if (num2 == 0)
			{
				this.diagnosticSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Watermark query returned 0 results for {0}.", new object[]
				{
					this.indexSystemName
				});
				this.diagnosticSession.TraceError<string>("Watermark query returned 0 results for {0}.", this.indexSystemName);
				this.diagnosticSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_CatalogResetDetected, new object[]
				{
					this.indexSystemName
				});
				if (this.cachedNotificationsWatermark == null)
				{
					this.cachedNotificationsWatermark = new long?(-1L);
				}
			}
			else
			{
				if (num == null)
				{
					this.diagnosticSession.TraceDebug<string>("Watermark query results do not contain notifications watermark, index system: {0}.", this.indexSystemName);
					this.diagnosticSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_CatalogResetDetected, new object[]
					{
						this.indexSystemName
					});
					num = new long?(-1L);
					dictionary.Clear();
				}
				if (this.cachedNotificationsWatermark == null)
				{
					this.cachedNotificationsWatermark = num;
				}
			}
			if (this.cachedVersionInfo.Equals(VersionInfo.Unknown) || this.cachedVersionInfo.Equals(VersionInfo.Legacy))
			{
				this.cachedVersionInfo = versionInfo;
			}
			if (this.cachedCrawlerWatermarks == null)
			{
				this.cachedCrawlerWatermarks = dictionary;
			}
			if (this.cachedDeletedMailboxes == null)
			{
				this.cachedDeletedMailboxes = hashSet;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000EC98 File Offset: 0x0000CE98
		private void SendWatermarkUpdateToFast(LazyAsyncResult asyncResult, long fastId, long watermark, bool recrawlMailbox, long watermarkChangeSinceLastUpdate, int attemptCount = 0)
		{
			IFastDocument fastDocument = this.fastWatermarkFeeder.CreateFastDocument((watermark == 2147483647L) ? DocumentOperation.Delete : DocumentOperation.Update);
			if (fastId == WatermarkStorage.FastNotificationWatermarkDocumentId)
			{
				this.diagnosticSession.Assert(watermark != 0L, "Watermark must be a value other than 0.", new object[0]);
			}
			if (attemptCount > 0)
			{
				fastDocument.AttemptCount = attemptCount;
			}
			this.fastWatermarkFeeder.DocumentHelper.PopulateFastDocumentForWatermarkUpdate(fastDocument, fastId, watermark, recrawlMailbox);
			this.fastWatermarkFeeder.BeginSubmitDocument(fastDocument, delegate(IAsyncResult ar)
			{
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
				try
				{
					bool flag = this.fastWatermarkFeeder.EndSubmitDocument(ar);
					this.diagnosticSession.TraceDebug<string, long, long>(flag ? "WatermarkStorage.SendWatermarkUpdateToFast - Successfully saved watermark. Index system {0}, documentId {1}, watermark {2}." : "WatermarkStorage.SendWatermarkUpdateToFast - Watermark Document submission was canceled. Index system {0}, documentId {1}, watermark {2}.", this.indexSystemName, fastId, watermark);
					if (!flag)
					{
						this.diagnosticSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "WatermarkStorage.SendWatermarkUpdateToFast - FAST returned an error. Index system {0}, documentId {1}, watermark {2}, watermarkChangeSinceLastUpdate {3} error: OperationCanceledException.", new object[]
						{
							this.indexSystemName,
							fastId,
							watermark,
							watermarkChangeSinceLastUpdate
						});
					}
					this.lastSuccessfulUpdateAction = DateTime.UtcNow;
					lazyAsyncResult.InvokeCallback();
				}
				catch (Exception ex)
				{
					this.diagnosticSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "WatermarkStorage.SendWatermarkUpdateToFast - FAST returned an error. Index system {0}, documentId {1}, watermark {2}, watermarkChangeSinceLastUpdate {3} exception: {4}.", new object[]
					{
						this.indexSystemName,
						fastId,
						watermark,
						watermarkChangeSinceLastUpdate,
						ex
					});
					this.diagnosticSession.TraceError("WatermarkStorage.SendWatermarkUpdateToFast - FAST returned an error. Index system {0}, documentId {1}, watermark {2}, watermarkChangeSinceLastUpdate {3} exception: {4}.", new object[]
					{
						this.indexSystemName,
						fastId,
						watermark,
						watermarkChangeSinceLastUpdate,
						ex
					});
					if (DateTime.UtcNow > this.lastSuccessfulUpdateAction + this.acceptableRestartInterval)
					{
						lazyAsyncResult.InvokeCallback(ex);
					}
					else
					{
						lazyAsyncResult.InvokeCallback();
					}
				}
			}, asyncResult);
		}

		// Token: 0x0400011B RID: 283
		private const string FastWatermarkQueryTemplate = "mailboxguid:{0}";

		// Token: 0x0400011C RID: 284
		private const string DocIdField = "DocId";

		// Token: 0x0400011D RID: 285
		private const string OtherField = "Other";

		// Token: 0x0400011E RID: 286
		internal static readonly long FastNotificationWatermarkDocumentId = IndexId.CreateNotificationsWatermarkIndexId();

		// Token: 0x0400011F RID: 287
		internal static readonly long FastVersioningWatermarkDocumentId = IndexId.CreateVersioningWatermarkIndexId();

		// Token: 0x04000120 RID: 288
		private static readonly IndexSystemField[] watermarkSchema = new IndexSystemField[]
		{
			FastIndexSystemSchema.Watermark.Definition,
			FastIndexSystemSchema.ErrorCode.Definition,
			FastIndexSystemSchema.AttemptCount.Definition
		};

		// Token: 0x04000121 RID: 289
		private readonly IDiagnosticsSession diagnosticSession;

		// Token: 0x04000122 RID: 290
		private readonly object sessionLock = new object();

		// Token: 0x04000123 RID: 291
		private readonly string indexSystemName;

		// Token: 0x04000124 RID: 292
		private readonly ISubmitDocument fastWatermarkFeeder;

		// Token: 0x04000125 RID: 293
		private readonly TimeSpan acceptableRestartInterval;

		// Token: 0x04000126 RID: 294
		private readonly bool assertOnCorruptNotificationsWatermarkValue;

		// Token: 0x04000127 RID: 295
		private Dictionary<int, MailboxCrawlerState> cachedCrawlerWatermarks;

		// Token: 0x04000128 RID: 296
		private HashSet<int> cachedDeletedMailboxes;

		// Token: 0x04000129 RID: 297
		private ExchangeQueryExecutor fastQueryExecutor;

		// Token: 0x0400012A RID: 298
		private VersionInfo cachedVersionInfo;

		// Token: 0x0400012B RID: 299
		private long? cachedNotificationsWatermark;

		// Token: 0x0400012C RID: 300
		private DateTime lastSuccessfulUpdateAction;

		// Token: 0x0400012D RID: 301
		private DisposeTracker disposeTracker;

		// Token: 0x0400012E RID: 302
		private bool isDisposedFlag;
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Microsoft.Ceres.External.ContentApi;
using Microsoft.Ceres.External.ContentApi.DocumentFeeder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000013 RID: 19
	internal class FastFeeder : IDisposeTrackable, ISubmitDocument, IDisposable
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000060A8 File Offset: 0x000042A8
		public FastFeeder(string hostName, int contentSubmissionPort, TimeSpan submissionTimeout, TimeSpan processingTimeout, TimeSpan lostCallbackTimeout, bool setPerDocumentTimeout, int numSessions, string flowName)
		{
			Util.ThrowOnNullOrEmptyArgument(hostName, "hostName");
			Util.ThrowOnNullOrEmptyArgument(flowName, "flowName");
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("FastFeeder", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.FastFeederTracer, (long)this.GetHashCode());
			this.contentSubmissionAddresses = new string[]
			{
				string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					hostName,
					contentSubmissionPort
				})
			};
			this.flowName = flowName;
			this.numberOfSessions = Math.Max(1, numSessions);
			this.DocumentProcessingTimeout = processingTimeout;
			this.LostCallbackInterval = processingTimeout;
			this.LostCallbackTimeout = lostCallbackTimeout;
			this.SubmissionTimeout = submissionTimeout;
			this.disposeTracker = this.GetDisposeTracker();
			this.SetPerDocumentTimeout = setPerDocumentTimeout;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000061AB File Offset: 0x000043AB
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000061B3 File Offset: 0x000043B3
		public TimeSpan SubmissionTimeout { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000061BC File Offset: 0x000043BC
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000061C4 File Offset: 0x000043C4
		public TimeSpan ConnectionTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.connectionTimeout;
			}
			[DebuggerStepThrough]
			set
			{
				this.connectionTimeout = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000061CD File Offset: 0x000043CD
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000061D5 File Offset: 0x000043D5
		public bool SetPerDocumentTimeout { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000061DE File Offset: 0x000043DE
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000061E6 File Offset: 0x000043E6
		public TimeSpan DocumentProcessingTimeout { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000061EF File Offset: 0x000043EF
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000061F7 File Offset: 0x000043F7
		public TimeSpan LostCallbackTimeout { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006200 File Offset: 0x00004400
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006208 File Offset: 0x00004408
		public int DocumentFeederBatchSize
		{
			[DebuggerStepThrough]
			get
			{
				return this.documentFeederBatchSize;
			}
			[DebuggerStepThrough]
			set
			{
				this.documentFeederBatchSize = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006211 File Offset: 0x00004411
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006219 File Offset: 0x00004419
		public int DocumentFeederMaxConnectRetries { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006222 File Offset: 0x00004422
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000622A File Offset: 0x0000442A
		public int DocumentRetries { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006233 File Offset: 0x00004433
		public int CompletedCallbackCount
		{
			[DebuggerStepThrough]
			get
			{
				return this.completedCallbackCount;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000623B File Offset: 0x0000443B
		public int FailedCallbackCount
		{
			[DebuggerStepThrough]
			get
			{
				return this.failedCallbackCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006243 File Offset: 0x00004443
		public IFastDocumentHelper DocumentHelper
		{
			[DebuggerStepThrough]
			get
			{
				return FastFeeder.documentHelper;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000624A File Offset: 0x0000444A
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00006252 File Offset: 0x00004452
		public IDocumentTracker Tracker { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000625B File Offset: 0x0000445B
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00006263 File Offset: 0x00004463
		public List<string> PoisonErrorMessages { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000626C File Offset: 0x0000446C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00006274 File Offset: 0x00004474
		public string InstanceName { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000627D File Offset: 0x0000447D
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006285 File Offset: 0x00004485
		public string IndexSystemName { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000628E File Offset: 0x0000448E
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006296 File Offset: 0x00004496
		internal TimeSpan LostCallbackInterval { get; set; }

		// Token: 0x06000120 RID: 288 RVA: 0x000062A0 File Offset: 0x000044A0
		public virtual void Initialize()
		{
			try
			{
				IndexManager instance = IndexManager.Instance;
				if (!instance.CheckForNoPendingConfigurationUpdate())
				{
					this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Check for Pending Configuration Update failed while trying to Create the CSS connection.", new object[0]);
					throw new FastConnectionException();
				}
				if (!NodeManagementClient.Instance.AreAllNodesHealthy(true))
				{
					this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Check for Node health failed while trying to Create the CSS connection.", new object[0]);
					throw new FastConnectionException();
				}
			}
			catch (PerformingFastOperationException innerException)
			{
				throw new FastConnectionException(innerException);
			}
			try
			{
				DocumentFeeder documentFeeder = new DocumentFeeder(new DocumentFeederOptions
				{
					CssNodeList = this.contentSubmissionAddresses,
					Flow = this.flowName,
					Causality = 1,
					BatchSubmissionTimeout = this.SubmissionTimeout,
					MaxDocsInBatch = this.DocumentFeederBatchSize,
					Name = this.flowName,
					NumberOfSessions = this.numberOfSessions,
					ConnectionTimeout = this.ConnectionTimeout,
					CancelOnDispose = true
				});
				documentFeeder.MaxRetries = this.DocumentRetries;
				if (!this.SetPerDocumentTimeout || this.DocumentProcessingTimeout > documentFeeder.DocumentTimeout)
				{
					documentFeeder.DocumentTimeout = this.DocumentProcessingTimeout;
				}
				documentFeeder.MaxConnectRetries = this.DocumentFeederMaxConnectRetries;
				documentFeeder.CompletedCallbackHandlers += this.SubmitDocumentComplete;
				documentFeeder.ReceivedCallbackHandlers += this.SubmitDocumentComplete;
				documentFeeder.FailedCallbackHandlers += this.SubmitDocumentComplete;
				if (!this.SetPerDocumentTimeout)
				{
					this.eventTimer = new GuardedTimer(new TimerCallback(this.LostCallbackTimerCallback), null, TimeSpan.Zero, this.LostCallbackInterval);
				}
				this.documentFeeder = documentFeeder;
			}
			catch (Exception ex)
			{
				if (Util.ShouldRethrowException(ex))
				{
					throw;
				}
				throw new FastConnectionException(ex);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006470 File Offset: 0x00004670
		public ICancelableAsyncResult BeginSubmitDocument(IFastDocument document, AsyncCallback callback, object state)
		{
			Util.ThrowOnNullArgument(document, "document");
			FastFeeder.FastFeederAsyncResult fastFeederAsyncResult = new FastFeeder.FastFeederAsyncResult(document, state, callback);
			lock (this.pendingOperationsLock)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("FastFeeder", "BeginSubmitDocument");
				}
				this.pendingOperations.Add(((FastDocument)document).ContextId, fastFeederAsyncResult);
			}
			if (this.SetPerDocumentTimeout)
			{
				fastFeederAsyncResult.StartTimer(this.DocumentProcessingTimeout);
			}
			this.SubmitDocumentInternal(fastFeederAsyncResult);
			return fastFeederAsyncResult;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000650C File Offset: 0x0000470C
		public bool EndSubmitDocument(IAsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			FastFeeder.FastFeederAsyncResult fastFeederAsyncResult = LazyAsyncResult.EndAsyncOperation<FastFeeder.FastFeederAsyncResult>(asyncResult);
			FastDocument fastDocument = (FastDocument)fastFeederAsyncResult.Document;
			lock (this.pendingOperationsLock)
			{
				this.pendingOperations.Remove(fastDocument.ContextId);
			}
			if (fastFeederAsyncResult.Result == null)
			{
				this.ClearDocumentFromTracker(fastDocument);
				return true;
			}
			Exception ex = (Exception)fastFeederAsyncResult.Result;
			this.diagnosticsSession.TraceError<Exception>("FastFeeder.EndSubmitDocument - async operation returned exception: {0}.", ex);
			if (ex is FastConnectionException)
			{
				this.diagnosticsSession.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_FastConnectionException, this.flowName, new object[]
				{
					ex
				});
				throw new FastConnectionException(ex);
			}
			if (ex is FastPermanentDocumentException)
			{
				if (this.Tracker != null)
				{
					this.Tracker.MarkDocumentAsPoison(fastDocument.DocumentId);
				}
				throw new FastPermanentDocumentException(ex.Message, ex);
			}
			if (ex is FastTransientDocumentException)
			{
				if (this.Tracker != null)
				{
					this.Tracker.MarkDocumentAsRetriablePoison(fastDocument.DocumentId);
				}
				throw new FastTransientDocumentException(ex.Message, ex);
			}
			if (ex is TimeoutException)
			{
				throw new FastDocumentTimeoutException(ex.Message, ex);
			}
			if (ex is DocumentFeederLostCallbackException)
			{
				throw new DocumentFeederLostCallbackException(ex.Message, ex);
			}
			if (ex is OperationCanceledException)
			{
				return false;
			}
			throw new ExAssertException(string.Format("Got an Unexpected exception: {0}", ex));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000667C File Offset: 0x0000487C
		public bool TryCompleteSubmitDocument(IAsyncResult asyncResult)
		{
			FastFeeder.FastFeederAsyncResult fastFeederAsyncResult = (FastFeeder.FastFeederAsyncResult)asyncResult;
			return fastFeederAsyncResult.InvokeCallback();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006698 File Offset: 0x00004898
		public IFastDocument CreateFastDocument(DocumentOperation operation)
		{
			FastDocument fastDocument = new FastDocument(this.diagnosticsSession, Interlocked.Increment(ref FastFeeder.contextId).ToString("X"), operation);
			if (!string.IsNullOrEmpty(this.InstanceName))
			{
				fastDocument.InstanceName = this.InstanceName;
			}
			if (!string.IsNullOrEmpty(this.IndexSystemName))
			{
				fastDocument.IndexSystemName = this.IndexSystemName;
			}
			return fastDocument;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000066FC File Offset: 0x000048FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000670B File Offset: 0x0000490B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastFeeder>(this);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006713 File Offset: 0x00004913
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006728 File Offset: 0x00004928
		internal virtual void WaitForCompletion(TimeSpan timeout)
		{
			if (this.documentFeeder != null)
			{
				try
				{
					this.documentFeeder.WaitForCompletion(timeout);
				}
				catch (ShutdownException)
				{
				}
				catch (TimeoutException)
				{
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006770 File Offset: 0x00004970
		internal void ResetCounters()
		{
			this.completedCallbackCount = 0;
			this.failedCallbackCount = 0;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006780 File Offset: 0x00004980
		protected virtual void SubmitDocumentWithDocumentFeeder(FastDocument fastDocument)
		{
			this.documentFeeder.SubmitDocument(fastDocument.Document);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006794 File Offset: 0x00004994
		protected void SubmitDocumentComplete(object sender, DocumentCallback documentCallback)
		{
			string documentID = documentCallback.DocumentID;
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			this.diagnosticsSession.TraceDebug<string, CallbackType>("Document {0} complete, reason: {1}", documentID, documentCallback.CallbackType);
			Exception value;
			switch (documentCallback.CallbackType)
			{
			case 0:
				Interlocked.Increment(ref this.completedCallbackCount);
				value = null;
				break;
			case 1:
			{
				Interlocked.Increment(ref this.failedCallbackCount);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Message message in documentCallback.Messages)
				{
					stringBuilder.AppendLine(message.MessageText);
				}
				text = stringBuilder.ToString();
				this.diagnosticsSession.TraceError<string, string>("SubmitDocumentComplete: FAST CTS flow returned a {0} error: {1}.", documentCallback.IsTransientError ? "transient" : "permanent", text);
				if (text.Contains("timeout"))
				{
					value = new TimeoutException(text);
					flag = true;
				}
				else if (text.Contains("InvalidRecordDetected"))
				{
					value = new FastPermanentDocumentException(text);
					flag2 = true;
				}
				else if (documentCallback.IsTransientError)
				{
					value = new FastTransientDocumentException(text);
				}
				else
				{
					value = new FastPermanentDocumentException(text);
				}
				break;
			}
			case 2:
				value = null;
				break;
			default:
				text = string.Format("Unknown CallbackType: {0}", documentCallback.CallbackType);
				this.diagnosticsSession.TraceError(text, new object[0]);
				throw new ExAssertException(text);
			}
			FastFeeder.FastFeederAsyncResult fastFeederAsyncResult;
			lock (this.pendingOperationsLock)
			{
				if (!this.pendingOperations.TryGetValue(documentID, out fastFeederAsyncResult))
				{
					this.diagnosticsSession.TraceDebug<string>("Document {0} has already been completed.", documentID);
					return;
				}
			}
			if (!flag && !flag2 && !fastFeederAsyncResult.DocumentResubmission && !string.IsNullOrEmpty(text))
			{
				FastDocument fastDocument = (FastDocument)fastFeederAsyncResult.Document;
				if (fastDocument.FlowOperation == "Indexing" || fastDocument.FlowOperation == "FolderUpdate")
				{
					int errorCode = 1;
					if (text.Contains("Item truncated"))
					{
						if (this.Tracker != null)
						{
							this.Tracker.MarkDocumentAsPoison(fastDocument.DocumentId);
						}
						errorCode = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.MarsWriterTruncation);
					}
					else if (!documentCallback.IsTransientError)
					{
						if (this.Tracker != null)
						{
							this.Tracker.MarkDocumentAsPoison(fastDocument.DocumentId);
						}
						errorCode = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.PoisonDocument);
					}
					else
					{
						foreach (string value2 in this.PoisonErrorMessages)
						{
							if (text.Contains(value2))
							{
								if (this.Tracker != null)
								{
									this.Tracker.MarkDocumentAsPoison(fastDocument.DocumentId);
								}
								errorCode = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.PoisonDocument);
								break;
							}
						}
					}
					this.ResubmitFailureDocument(fastFeederAsyncResult, errorCode, text);
					return;
				}
			}
			fastFeederAsyncResult.InvokeCallback(value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006AA0 File Offset: 0x00004CA0
		private void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				List<FastFeeder.FastFeederAsyncResult> list;
				lock (this.pendingOperationsLock)
				{
					list = new List<FastFeeder.FastFeederAsyncResult>(this.pendingOperations.Values);
					this.pendingOperations.Clear();
					this.disposed = true;
				}
				foreach (FastFeeder.FastFeederAsyncResult fastFeederAsyncResult in list)
				{
					fastFeederAsyncResult.Cancel();
				}
				if (this.documentFeeder != null)
				{
					try
					{
						this.documentFeeder.Dispose();
					}
					catch (Exception)
					{
					}
				}
				if (this.eventTimer != null)
				{
					this.eventTimer.Pause();
					this.eventTimer.Dispose(false);
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006BA0 File Offset: 0x00004DA0
		private void SubmitDocumentInternal(object state)
		{
			FastFeeder.FastFeederAsyncResult fastFeederAsyncResult = (FastFeeder.FastFeederAsyncResult)state;
			FastDocument fastDocument = (FastDocument)fastFeederAsyncResult.Document;
			if (this.disposed)
			{
				fastFeederAsyncResult.InvokeCallback(new ObjectDisposedException("FastFeeder"));
				return;
			}
			Exception ex = null;
			try
			{
				if (this.Tracker != null && fastDocument.Document.Operation != Operation.Delete.Name && fastDocument.ErrorCode == 0 && !IndexId.IsWatermarkIndexId(fastDocument.DocumentId))
				{
					int num = this.Tracker.ShouldDocumentBeStampedWithError(fastDocument.DocumentId);
					if (num != 0)
					{
						fastDocument.Tracked = true;
						fastDocument.ErrorCode = num;
						this.diagnosticsSession.TraceDebug<long>("Marking document with a poison error code. DocumentId: {0}", fastDocument.DocumentId);
					}
					if (this.Tracker.ShouldDocumentBeSkipped(fastDocument.DocumentId))
					{
						this.diagnosticsSession.TraceDebug<long>("Skipping known poison document: {0}", fastDocument.DocumentId);
						fastFeederAsyncResult.InvokeCallback();
						return;
					}
				}
				fastDocument.PrepareForSubmit();
				this.DocumentHelper.ValidateDocumentConsistency(fastDocument, "Immediately before SubmitDocument.");
				this.diagnosticsSession.TraceDebug<string>("Submitting Document {0}", fastDocument.ContextId);
				this.SubmitDocumentWithDocumentFeeder(fastDocument);
			}
			catch (DocumentException ex2)
			{
				this.diagnosticsSession.TraceError<DocumentException>("Received a DocumentException from FAST. Document submission failed - {0}", ex2);
				FastTransientDocumentException value = new FastTransientDocumentException(ex2.Message, ex2);
				fastFeederAsyncResult.InvokeCallback(value);
			}
			catch (CommunicationObjectAbortedException ex3)
			{
				ex = ex3;
			}
			catch (ConnectionException ex4)
			{
				ex = ex4;
			}
			catch (KeyNotFoundException ex5)
			{
				ex = ex5;
			}
			catch (Exception ex6)
			{
				if (Util.ShouldRethrowException(ex6))
				{
					throw;
				}
				ex = ex6;
			}
			if (ex != null)
			{
				this.diagnosticsSession.TraceError<Exception>("Received a connection exception from FAST. Shutting down the document feeder: {0}", ex);
				FastConnectionException value2 = new FastConnectionException(ex);
				fastFeederAsyncResult.InvokeCallback(value2);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006D7C File Offset: 0x00004F7C
		private void ResubmitFailureDocument(FastFeeder.FastFeederAsyncResult asyncResult, int errorCode, string errorMessage)
		{
			FastDocument fastDocument = (FastDocument)asyncResult.Document;
			FastDocument fastDocument2 = (FastDocument)this.CreateFastDocument(DocumentOperation.Update);
			this.DocumentHelper.ValidateDocumentConsistency(fastDocument, "Original document used as source to clone for resubmit is invalid.");
			this.DocumentHelper.PopulateFastDocumentForIndexing(fastDocument2, fastDocument.FeedingVersion, fastDocument.MailboxGuid, fastDocument.IsMoveDestination, fastDocument.IsLocalMdb, fastDocument.DocumentId, fastDocument.CompositeItemId, errorCode, fastDocument.AttemptCount);
			fastDocument2.ErrorMessage = errorMessage;
			fastDocument2.Tracked = true;
			asyncResult.Document = fastDocument2;
			asyncResult.DocumentResubmission = true;
			lock (this.pendingOperationsLock)
			{
				this.pendingOperations.Add(fastDocument2.ContextId, asyncResult);
				this.pendingOperations.Remove(fastDocument.ContextId);
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SubmitDocumentInternal), asyncResult);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006E6C File Offset: 0x0000506C
		private void ClearDocumentFromTracker(FastDocument fastDocument)
		{
			if (this.Tracker != null)
			{
				this.Tracker.RecordDocumentProcessingComplete(fastDocument.CorrelationId, fastDocument.DocumentId, fastDocument.Tracked);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006E94 File Offset: 0x00005094
		private void LostCallbackTimerCallback(object state)
		{
			lock (this.pendingOperationsLock)
			{
				TimeSpan t = this.DocumentProcessingTimeout + this.LostCallbackTimeout;
				List<FastFeeder.FastFeederAsyncResult> list = new List<FastFeeder.FastFeederAsyncResult>(this.pendingOperations.Values);
				foreach (FastFeeder.FastFeederAsyncResult fastFeederAsyncResult in list)
				{
					if (fastFeederAsyncResult.SubmitTime + t < DateTime.UtcNow)
					{
						fastFeederAsyncResult.InvokeCallback(new DocumentFeederLostCallbackException(fastFeederAsyncResult.Document.CompositeItemId));
					}
				}
			}
		}

		// Token: 0x04000066 RID: 102
		private static readonly IFastDocumentHelper documentHelper = new FastDocumentHelper();

		// Token: 0x04000067 RID: 103
		private static long contextId;

		// Token: 0x04000068 RID: 104
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000069 RID: 105
		private readonly string[] contentSubmissionAddresses;

		// Token: 0x0400006A RID: 106
		private readonly string flowName;

		// Token: 0x0400006B RID: 107
		private readonly int numberOfSessions;

		// Token: 0x0400006C RID: 108
		private readonly Dictionary<string, FastFeeder.FastFeederAsyncResult> pendingOperations = new Dictionary<string, FastFeeder.FastFeederAsyncResult>();

		// Token: 0x0400006D RID: 109
		private readonly object pendingOperationsLock = new object();

		// Token: 0x0400006E RID: 110
		private TimeSpan connectionTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400006F RID: 111
		private int documentFeederBatchSize = 1000;

		// Token: 0x04000070 RID: 112
		private DocumentFeeder documentFeeder;

		// Token: 0x04000071 RID: 113
		private int completedCallbackCount;

		// Token: 0x04000072 RID: 114
		private int failedCallbackCount;

		// Token: 0x04000073 RID: 115
		private GuardedTimer eventTimer;

		// Token: 0x04000074 RID: 116
		private bool disposed;

		// Token: 0x04000075 RID: 117
		private DisposeTracker disposeTracker;

		// Token: 0x02000014 RID: 20
		private class FastFeederAsyncResult : LazyAsyncResultWithTimeout
		{
			// Token: 0x06000132 RID: 306 RVA: 0x00006F68 File Offset: 0x00005168
			public FastFeederAsyncResult(IFastDocument document, object callerState, AsyncCallback callback) : base(null, callerState, callback)
			{
				this.Document = document;
				this.SubmitTime = DateTime.UtcNow;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000133 RID: 307 RVA: 0x00006F85 File Offset: 0x00005185
			// (set) Token: 0x06000134 RID: 308 RVA: 0x00006F8D File Offset: 0x0000518D
			public IFastDocument Document { get; set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00006F96 File Offset: 0x00005196
			// (set) Token: 0x06000136 RID: 310 RVA: 0x00006F9E File Offset: 0x0000519E
			public bool DocumentResubmission { get; set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000137 RID: 311 RVA: 0x00006FA7 File Offset: 0x000051A7
			// (set) Token: 0x06000138 RID: 312 RVA: 0x00006FAF File Offset: 0x000051AF
			public DateTime SubmitTime { get; set; }
		}
	}
}

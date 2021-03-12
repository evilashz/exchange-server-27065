using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Performance;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000024 RID: 36
	internal class TransportFlowFeeder : ITransportFlowFeeder
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000D164 File Offset: 0x0000B364
		public TransportFlowFeeder(IStreamManager streamManager, ISubmitDocument feeder)
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("IndexDeliveryAgent", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.TransportFlowFeederTracer, (long)this.GetHashCode());
			this.streamManager = streamManager;
			this.feeder = feeder;
			TransportFlowFeeder.InitPerformanceCounters();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		internal TransportFlowFeeder.PopulateFastDocumentProperties PopulateFastDocumentPropertiesHandler { get; set; }

		// Token: 0x06000210 RID: 528 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static void InitPerformanceCounters()
		{
			if (TransportFlowFeeder.instanceCounters == null)
			{
				lock (TransportFlowFeeder.lockObject)
				{
					if (TransportFlowFeeder.instanceCounters == null)
					{
						TransportFlowFeeder.instanceCounters = TransportCtsFlowCounters.GetInstance(Process.GetCurrentProcess().ProcessName);
						TransportFlowFeeder.instanceCounters.Reset();
					}
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D22C File Offset: 0x0000B42C
		public static void ReportTimings(TransportFlowOperatorTimings transportFlowOperatorTimings, long messageProcessingTimeInMsec, bool languageDetectionFailed)
		{
			if (TransportFlowFeeder.instanceCounters == null)
			{
				return;
			}
			TransportFlowFeeder.instanceCounters.TotalDocuments.Increment();
			TransportFlowFeeder.instanceCounters.NumberOfProcessedDocuments.Increment();
			TransportFlowFeeder.instanceCounters.TotalTimeProcessingMessageInMsec.IncrementBy(messageProcessingTimeInMsec);
			if (languageDetectionFailed)
			{
				TransportFlowFeeder.instanceCounters.LanguageDetectionFailures.Increment();
			}
			if (transportFlowOperatorTimings != null)
			{
				TransportFlowFeeder.instanceCounters.TimeInDocParserInMsec.IncrementBy(transportFlowOperatorTimings.TimeInDocParserInMsec);
				TransportFlowFeeder.instanceCounters.TimeInNLGSubflowInMsec.IncrementBy(transportFlowOperatorTimings.TimeInNLGSubflowInMsec);
				TransportFlowFeeder.instanceCounters.TimeInQueueInMsec.IncrementBy(transportFlowOperatorTimings.TimeInQueueInMsec);
				TransportFlowFeeder.instanceCounters.TimeInTransportRetrieverInMsec.IncrementBy(transportFlowOperatorTimings.TimeInTransportRetrieverInMsec);
				TransportFlowFeeder.instanceCounters.TimeInWordbreakerInMsec.IncrementBy(transportFlowOperatorTimings.TimeInWordbreakerInMsec);
			}
			if (messageProcessingTimeInMsec < 250L)
			{
				TransportFlowFeeder.instanceCounters.ProcessedUnder250ms.Increment();
				return;
			}
			if (messageProcessingTimeInMsec < 500L)
			{
				TransportFlowFeeder.instanceCounters.ProcessedUnder500ms.Increment();
				return;
			}
			if (messageProcessingTimeInMsec < 1000L)
			{
				TransportFlowFeeder.instanceCounters.ProcessedUnder1000ms.Increment();
				return;
			}
			if (messageProcessingTimeInMsec < 2000L)
			{
				TransportFlowFeeder.instanceCounters.ProcessedUnder2000ms.Increment();
				return;
			}
			if (messageProcessingTimeInMsec < 5000L)
			{
				TransportFlowFeeder.instanceCounters.ProcessedUnder5000ms.Increment();
				return;
			}
			TransportFlowFeeder.instanceCounters.ProcessedOver5000ms.Increment();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D388 File Offset: 0x0000B588
		public static void ReportClientSideTimings(ClientSideTimings clientSideTimings)
		{
			if (TransportFlowFeeder.instanceCounters == null)
			{
				return;
			}
			if (clientSideTimings != null)
			{
				TransportFlowFeeder.instanceCounters.TimeInGetConnectionInMsec.IncrementBy((long)clientSideTimings.TimeInGetConnection.TotalMilliseconds);
				TransportFlowFeeder.instanceCounters.TimeInPropertyBagLoadInMsec.IncrementBy((long)clientSideTimings.TimeInPropertyBagLoad.TotalMilliseconds);
				TransportFlowFeeder.instanceCounters.TimeInMessageItemConversionInMsec.IncrementBy((long)clientSideTimings.TimeInMessageItemConversion.TotalMilliseconds);
				TransportFlowFeeder.instanceCounters.TimeDeterminingAgeOfItemInMsec.IncrementBy((long)clientSideTimings.TimeDeterminingAgeOfItem.TotalMilliseconds);
				TransportFlowFeeder.instanceCounters.TimeInMimeConversionInMsec.IncrementBy((long)clientSideTimings.TimeInMimeConversion.TotalMilliseconds);
				TransportFlowFeeder.instanceCounters.TimeInShouldAnnotateMessageInMsec.IncrementBy((long)clientSideTimings.TimeInShouldAnnotateMessage.TotalMilliseconds);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D45F File Offset: 0x0000B65F
		public static void ReportSkippedDocument()
		{
			if (TransportFlowFeeder.instanceCounters == null)
			{
				return;
			}
			TransportFlowFeeder.instanceCounters.TotalSkippedDocuments.Increment();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000D47C File Offset: 0x0000B67C
		public void ProcessMessage(Stream mimeStream, Stream propertyStream, TransportFlowMessageFlags transportFlowMessageFlags)
		{
			if ((transportFlowMessageFlags & TransportFlowMessageFlags.ShouldDiscardToken) == TransportFlowMessageFlags.ShouldDiscardToken && (transportFlowMessageFlags & TransportFlowMessageFlags.ShouldBypassNlg) == TransportFlowMessageFlags.ShouldBypassNlg)
			{
				return;
			}
			TransportFlowFeeder.Context context = new TransportFlowFeeder.Context();
			IFastDocument fastDocument = this.feeder.CreateFastDocument(DocumentOperation.Insert);
			if (this.PopulateFastDocumentPropertiesHandler != null)
			{
				this.PopulateFastDocumentPropertiesHandler(mimeStream, fastDocument, context);
			}
			else
			{
				fastDocument.Port = this.streamManager.ListenPort;
				fastDocument.TransportContextId = context.ContextId.ToString();
				fastDocument.MessageFlags = (int)transportFlowMessageFlags;
			}
			if ((transportFlowMessageFlags & TransportFlowMessageFlags.ShouldBypassNlg) == TransportFlowMessageFlags.ShouldBypassNlg)
			{
				TransportFlowFeeder.instanceCounters.NumberOfSkippedNlg.Increment();
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				context.ConnectAsyncResult = this.streamManager.BeginWaitForConnection(context.ContextId, new AsyncCallback(this.WaitForConnectionComplete), context);
				using (WaitHandle asyncWaitHandle = context.ConnectAsyncResult.AsyncWaitHandle)
				{
					context.SubmitAsyncResult = this.feeder.BeginSubmitDocument(fastDocument, new AsyncCallback(this.SubmitDocumentComplete), context);
					this.diagnosticsSession.TraceDebug("Wait for connection", new object[0]);
					asyncWaitHandle.WaitOne();
					if (context.SubmitException != null)
					{
						throw context.SubmitException;
					}
					if (context.ConnectException != null)
					{
						throw context.ConnectException;
					}
				}
				long num = 0L;
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				long num2 = elapsedMilliseconds - num;
				TransportFlowFeeder.instanceCounters.TimeSpentWaitingForConnectInMsec.IncrementBy(num2);
				this.diagnosticsSession.TraceDebug<long>("Connected: {0} ms. Sending MIME", num2);
				num = elapsedMilliseconds;
				mimeStream.CopyTo(context.RemoteStream);
				context.RemoteStream.Flush();
				elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				TransportFlowFeeder.instanceCounters.BytesSent.IncrementBy(mimeStream.Position);
				this.diagnosticsSession.TraceDebug<long, long>("Mime sent: {0} ms, {1} bytes. Read properties", elapsedMilliseconds - num, mimeStream.Position);
				num = elapsedMilliseconds;
				context.RemoteStream.CopyTo(propertyStream);
				elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				this.diagnosticsSession.TraceDebug<long, long>("Read properties: {0} ms, {1} bytes. Tell FAST we're done", elapsedMilliseconds - num, propertyStream.Length);
				TransportFlowFeeder.instanceCounters.BytesReceived.IncrementBy(propertyStream.Length);
				context.RemoteStream.Flush();
				this.feeder.TryCompleteSubmitDocument(context.SubmitAsyncResult);
			}
			catch (Exception arg)
			{
				this.diagnosticsSession.TraceError<Exception, long>("Exception processing message: {0}, elapsed {1} ms", arg, stopwatch.ElapsedMilliseconds);
				TransportFlowFeeder.instanceCounters.NumberOfFailedDocuments.Increment();
				TransportFlowFeeder.instanceCounters.TotalTimeProcessingFailedMessageInMsec.IncrementBy(stopwatch.ElapsedMilliseconds);
				context.CancelConnect();
				context.CancelSubmit();
				throw;
			}
			this.diagnosticsSession.TraceDebug<long>("Document processed, elapsed {0} ms", stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000D734 File Offset: 0x0000B934
		private void WaitForConnectionComplete(IAsyncResult connectAsyncResult)
		{
			TransportFlowFeeder.Context context = (TransportFlowFeeder.Context)connectAsyncResult.AsyncState;
			try
			{
				context.RemoteStream = this.streamManager.EndWaitForConnection(connectAsyncResult);
			}
			catch (Exception ex)
			{
				context.ConnectException = ex;
				this.diagnosticsSession.TraceError<Exception>("Exception waiting for connection: {0}", ex);
				context.CancelSubmit();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D794 File Offset: 0x0000B994
		private void SubmitDocumentComplete(IAsyncResult submitAsyncResult)
		{
			TransportFlowFeeder.Context context = (TransportFlowFeeder.Context)submitAsyncResult.AsyncState;
			try
			{
				this.feeder.EndSubmitDocument(submitAsyncResult);
			}
			catch (Exception ex)
			{
				context.SubmitException = ex;
				this.diagnosticsSession.TraceError<Exception>("Exception submitting document: {0}", ex);
			}
			context.CancelConnect();
			context.CloseRemoteStream();
		}

		// Token: 0x040000F7 RID: 247
		private static TransportCtsFlowCountersInstance instanceCounters;

		// Token: 0x040000F8 RID: 248
		private static object lockObject = new object();

		// Token: 0x040000F9 RID: 249
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000FA RID: 250
		private readonly IStreamManager streamManager;

		// Token: 0x040000FB RID: 251
		private readonly ISubmitDocument feeder;

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000219 RID: 537
		internal delegate void PopulateFastDocumentProperties(Stream mimeStream, IFastDocument fastDocument, TransportFlowFeeder.Context context);

		// Token: 0x02000026 RID: 38
		internal class Context
		{
			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D800 File Offset: 0x0000BA00
			public Guid ContextId
			{
				get
				{
					return this.contextId;
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0000D808 File Offset: 0x0000BA08
			// (set) Token: 0x0600021E RID: 542 RVA: 0x0000D810 File Offset: 0x0000BA10
			public ICancelableAsyncResult ConnectAsyncResult { get; set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600021F RID: 543 RVA: 0x0000D819 File Offset: 0x0000BA19
			// (set) Token: 0x06000220 RID: 544 RVA: 0x0000D821 File Offset: 0x0000BA21
			public ICancelableAsyncResult SubmitAsyncResult { get; set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000221 RID: 545 RVA: 0x0000D82A File Offset: 0x0000BA2A
			// (set) Token: 0x06000222 RID: 546 RVA: 0x0000D832 File Offset: 0x0000BA32
			public Stream RemoteStream { get; set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000223 RID: 547 RVA: 0x0000D83B File Offset: 0x0000BA3B
			// (set) Token: 0x06000224 RID: 548 RVA: 0x0000D843 File Offset: 0x0000BA43
			public Exception ConnectException { get; set; }

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000225 RID: 549 RVA: 0x0000D84C File Offset: 0x0000BA4C
			// (set) Token: 0x06000226 RID: 550 RVA: 0x0000D854 File Offset: 0x0000BA54
			public Exception SubmitException { get; set; }

			// Token: 0x06000227 RID: 551 RVA: 0x0000D85D File Offset: 0x0000BA5D
			public void CancelConnect()
			{
				if (this.ConnectAsyncResult != null)
				{
					this.ConnectAsyncResult.Cancel();
				}
			}

			// Token: 0x06000228 RID: 552 RVA: 0x0000D872 File Offset: 0x0000BA72
			public void CancelSubmit()
			{
				if (this.SubmitAsyncResult != null)
				{
					this.SubmitAsyncResult.Cancel();
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000D888 File Offset: 0x0000BA88
			public void CloseRemoteStream()
			{
				Stream remoteStream = this.RemoteStream;
				if (remoteStream != null)
				{
					remoteStream.Close();
				}
			}

			// Token: 0x040000FD RID: 253
			private readonly Guid contextId = Guid.NewGuid();
		}
	}
}

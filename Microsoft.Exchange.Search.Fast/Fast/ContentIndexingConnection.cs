using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000003 RID: 3
	internal sealed class ContentIndexingConnection : Disposable
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		internal ContentIndexingConnection(SearchConfig configuration)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				FlowManager.Instance.EnsureTransportFlow();
				this.configuration = configuration;
				string displayName = FlowDescriptor.GetTransportFlowDescriptor(configuration).DisplayName;
				this.fastFeeder = Factory.Current.CreateFastFeeder(configuration.HostName, configuration.SubmissionPort, configuration.DocumentFeeders, configuration.TxDocumentFeederConnectionTimeout, configuration.BatchSubmissionTimeout, configuration.StreamTimeout, displayName);
				this.fastFeeder.IndexSystemName = string.Empty;
				this.streamManager = StreamManager.CreateForListen(configuration.ServiceName, new TcpListener.HandleFailure(this.OnTcpListenerFailure));
				this.streamManager.ConnectionTimeout = configuration.StreamTimeout;
				this.streamManager.ListenPort = configuration.ListenPort;
				this.streamManager.StartListening();
				if (!configuration.SkipMdmGeneration)
				{
					IIndexManager indexManager = Factory.Current.CreateIndexManager();
					string transportIndexSystem = indexManager.GetTransportIndexSystem();
					if (string.IsNullOrWhiteSpace(transportIndexSystem))
					{
						throw new IndexSystemNotFoundException(displayName);
					}
					this.fastFeeder.IndexSystemName = transportIndexSystem;
				}
				this.flowFeeder = new TransportFlowFeeder(this.streamManager, this.fastFeeder);
				disposeGuard.Success();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		internal static IDiagnosticsSession Diagnostics
		{
			get
			{
				return ContentIndexingConnectionFactory.Diagnostics;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000228F File Offset: 0x0000048F
		internal int NumberOfConnectionLevelFailures
		{
			get
			{
				return this.numberOfConnectionLevelFailures;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002297 File Offset: 0x00000497
		internal TimeSpan AgeOfItemToBypassNlg
		{
			get
			{
				return this.configuration.AgeOfItemToBypassNlg;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022A4 File Offset: 0x000004A4
		internal bool SkipWordBreakingDuringMRS
		{
			get
			{
				return this.configuration.SkipWordBreakingDuringMRS;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022B1 File Offset: 0x000004B1
		internal bool AlwaysInvalidateAnnotationToken
		{
			get
			{
				return this.configuration.AlwaysInvalidateAnnotationToken;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022BE File Offset: 0x000004BE
		internal static IDisposable SetProcessMessageTestHook(Action action)
		{
			return ContentIndexingConnection.processMessageTestHook.SetTestHook(action);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022CB File Offset: 0x000004CB
		internal void ProcessMessage(Stream mimeStream, Stream propertyStream)
		{
			this.ProcessMessage(mimeStream, propertyStream, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022D8 File Offset: 0x000004D8
		internal void ProcessMessage(Stream mimeStream, Stream propertyStream, bool shouldBypassNlg)
		{
			if (ContentIndexingConnection.processMessageTestHook.Value != null)
			{
				ContentIndexingConnection.processMessageTestHook.Value();
			}
			TransportFlowMessageFlags transportFlowMessageFlags = shouldBypassNlg ? TransportFlowMessageFlags.ShouldBypassNlg : TransportFlowMessageFlags.None;
			if (this.configuration.SkipMdmGeneration)
			{
				transportFlowMessageFlags |= TransportFlowMessageFlags.SkipMdmGeneration;
			}
			if (this.configuration.SkipTokenInfoGeneration)
			{
				transportFlowMessageFlags |= TransportFlowMessageFlags.SkipTokenInfoGeneration;
			}
			this.flowFeeder.ProcessMessage(mimeStream, propertyStream, transportFlowMessageFlags);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002338 File Offset: 0x00000538
		internal void ProcessAnnotationFailure(bool connectionLevel, Exception exception)
		{
			ContentIndexingConnection.Diagnostics.TraceError<bool, string>("ConnectionLevel: {0}, Exception: {1}", connectionLevel, (exception != null) ? exception.ToString() : "<null>");
			if (connectionLevel)
			{
				Interlocked.Increment(ref this.numberOfConnectionLevelFailures);
				ContentIndexingConnectionFactory.OnConnectionLevelFailure(this, false);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002370 File Offset: 0x00000570
		internal void MarkAlive()
		{
			Interlocked.Exchange(ref this.numberOfConnectionLevelFailures, 0);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000237F File Offset: 0x0000057F
		protected sealed override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ContentIndexingConnection>(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002387 File Offset: 0x00000587
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.streamManager != null)
				{
					this.streamManager.Dispose();
				}
				if (this.fastFeeder != null)
				{
					this.fastFeeder.Dispose();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B2 File Offset: 0x000005B2
		private void OnTcpListenerFailure(bool addressAlreadyInUseFailure)
		{
			ContentIndexingConnection.Diagnostics.TraceDebug("ContentIndexingConnection::OnTcpListenerFailure() called", new object[0]);
			ContentIndexingConnectionFactory.OnConnectionLevelFailure(this, true);
		}

		// Token: 0x04000007 RID: 7
		private static Hookable<Action> processMessageTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000008 RID: 8
		private readonly ISubmitDocument fastFeeder;

		// Token: 0x04000009 RID: 9
		private StreamManager streamManager;

		// Token: 0x0400000A RID: 10
		private TransportFlowFeeder flowFeeder;

		// Token: 0x0400000B RID: 11
		private int numberOfConnectionLevelFailures;

		// Token: 0x0400000C RID: 12
		private SearchConfig configuration;
	}
}

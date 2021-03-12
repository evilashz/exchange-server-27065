using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200000B RID: 11
	internal class Factory
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000046E3 File Offset: 0x000028E3
		protected Factory()
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000046EB File Offset: 0x000028EB
		internal static Hookable<Factory> Instance
		{
			get
			{
				return Factory.instance;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000046F2 File Offset: 0x000028F2
		internal static Factory Current
		{
			get
			{
				return Factory.instance.Value;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000046FE File Offset: 0x000028FE
		internal virtual IFlowManager CreateFlowManager()
		{
			return FlowManager.Instance;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004705 File Offset: 0x00002905
		internal virtual IIndexManager CreateIndexManager()
		{
			return IndexManager.Instance;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000470C File Offset: 0x0000290C
		internal virtual INodeManager CreateNodeManagementClient()
		{
			return NodeManagementClient.Instance;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004713 File Offset: 0x00002913
		internal virtual IWatermarkStorage CreateWatermarkStorage(ISubmitDocument fastWatermarkFeeder, ISearchServiceConfig config, string indexSystemName)
		{
			return new WatermarkStorage(fastWatermarkFeeder, config, indexSystemName);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000471D File Offset: 0x0000291D
		internal virtual IFailedItemStorage CreateFailedItemStorage(ISearchServiceConfig config, string indexSystemName)
		{
			return this.CreateFailedItemStorage(config, indexSystemName, config.HostName);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000472D File Offset: 0x0000292D
		internal virtual IFailedItemStorage CreateFailedItemStorage(ISearchServiceConfig config, string indexSystemName, string hostName)
		{
			return new FailedItemStorage(config, indexSystemName, hostName);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004737 File Offset: 0x00002937
		internal virtual ISubmitDocument CreateFastFeeder(ISearchServiceConfig config, string indexSystemFlow, string indexSystemName, string instanceName)
		{
			return this.InternalCreateFastFeeder(config, indexSystemFlow, indexSystemName, instanceName, config.MinFeederSessions);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000474A File Offset: 0x0000294A
		internal virtual ISubmitDocument CreateFastFeeder(ISearchServiceConfig config, string indexSystemFlow, string indexSystemName, string instanceName, int numberOfSessions)
		{
			return this.InternalCreateFastFeeder(config, indexSystemFlow, indexSystemName, instanceName, numberOfSessions);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000475C File Offset: 0x0000295C
		internal virtual ISubmitDocument CreateFastFeeder(string hostName, int contentSubmissionPort, int numSessions, TimeSpan connectionTimeout, TimeSpan submissionTimeout, TimeSpan processingTimeout, string flowName)
		{
			ISubmitDocument result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastFeeder fastFeeder = new FastFeeder(hostName, contentSubmissionPort, submissionTimeout, processingTimeout, TimeSpan.Zero, true, numSessions, flowName);
				disposeGuard.Add<FastFeeder>(fastFeeder);
				fastFeeder.Initialize();
				fastFeeder.ConnectionTimeout = connectionTimeout;
				disposeGuard.Success();
				result = fastFeeder;
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000047C8 File Offset: 0x000029C8
		internal virtual ISubmitDocument InternalCreateFastFeeder(ISearchServiceConfig config, string indexSystemFlow, string indexSystemName, string instanceName, int numberOfSessions)
		{
			Util.ThrowOnNullArgument(indexSystemFlow, "indexSystemFlow");
			ISubmitDocument result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastFeeder fastFeeder = new FastFeeder(config.HostName, config.ContentSubmissionPort, config.DocumentFeederSubmissionTimeout, config.DocumentFeederProcessingTimeout, config.DocumentFeederLostCallbackTimeout, false, numberOfSessions, indexSystemFlow);
				disposeGuard.Add<FastFeeder>(fastFeeder);
				fastFeeder.IndexSystemName = indexSystemName;
				fastFeeder.InstanceName = instanceName;
				fastFeeder.PoisonErrorMessages = config.PoisonErrorMessages;
				fastFeeder.DocumentRetries = config.MaxAttemptCount;
				fastFeeder.DocumentFeederBatchSize = config.DocumentFeederBatchSize;
				fastFeeder.ConnectionTimeout = config.DocumentFeederConnectionTimeout;
				fastFeeder.DocumentFeederMaxConnectRetries = config.DocumentFeederMaxConnectRetries;
				fastFeeder.Initialize();
				disposeGuard.Success();
				result = fastFeeder;
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004894 File Offset: 0x00002A94
		internal virtual PagingImsFlowExecutor CreatePagingImsFlowExecutor(string hostName, int queryServicePort, int channelPoolSize, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout, TimeSpan proxyIdleTimeout, long maxReceivedMessageSize, int maxStringContentLength, int retryCount)
		{
			return new PagingImsFlowExecutor(hostName, queryServicePort, channelPoolSize, openTimeout, sendTimeout, receiveTimeout, proxyIdleTimeout, maxReceivedMessageSize, maxStringContentLength, retryCount);
		}

		// Token: 0x04000038 RID: 56
		private static readonly Hookable<Factory> instance = Hookable<Factory>.Create(true, new Factory());
	}
}

using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200010F RID: 271
	internal class ServiceProxyPool<TClient> : IServiceProxyPool<TClient>, IDisposable
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00016A98 File Offset: 0x00014C98
		public ServiceProxyPool(string endpointName, string hostName, uint maxNumberOfClientProxies, ChannelFactory<TClient> channelFactory, ExecutionLog logProvider)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("endpointName", endpointName);
			ArgumentValidator.ThrowIfNullOrEmpty("hostName", hostName);
			ArgumentValidator.ThrowIfNull("channelFactory", channelFactory);
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			this.ChannelFactory = channelFactory;
			this.MaxNumberOfClientProxies = maxNumberOfClientProxies;
			this.EndpointName = endpointName;
			this.TargetInfo = string.Format("{0} ({1})", endpointName, hostName);
			this.outstandingProxies = 0L;
			this.logProvider = logProvider;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00016B1C File Offset: 0x00014D1C
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00016B24 File Offset: 0x00014D24
		public uint MaxNumberOfClientProxies { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00016B2D File Offset: 0x00014D2D
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00016B35 File Offset: 0x00014D35
		private protected ChannelFactory<TClient> ChannelFactory { protected get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00016B3E File Offset: 0x00014D3E
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00016B46 File Offset: 0x00014D46
		private protected string EndpointName { protected get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00016B4F File Offset: 0x00014D4F
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00016B57 File Offset: 0x00014D57
		private protected string TargetInfo { protected get; private set; }

		// Token: 0x06000761 RID: 1889 RVA: 0x00016B60 File Offset: 0x00014D60
		public void Dispose()
		{
			while (!this.pool.IsEmpty)
			{
				WCFConnectionStateTuple<TClient> wcfconnectionStateTuple = null;
				if (this.pool.TryPop(out wcfconnectionStateTuple))
				{
					PolicySyncUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), this.logProvider, false);
				}
			}
			if (this.ChannelFactory != null)
			{
				PolicySyncUtils.DisposeWcfClientGracefully(this.ChannelFactory, this.logProvider, false);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00016BC9 File Offset: 0x00014DC9
		public bool TryCallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries, out Exception exception)
		{
			return this.TryCallServiceWithRetry(action, debugMessage, null, numberOfRetries, true, out exception);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00016BD8 File Offset: 0x00014DD8
		public bool TryCallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage, out Exception exception)
		{
			ArgumentValidator.ThrowIfNull("cachedProxy", cachedProxy);
			WCFConnectionStateTuple<TClient> wcfconnectionStateTuple = cachedProxy as WCFConnectionStateTuple<TClient>;
			ArgumentValidator.ThrowIfNull("proxyToUse", wcfconnectionStateTuple);
			return this.TryCallServiceWithRetry(action, debugMessage, wcfconnectionStateTuple, 1, false, out exception);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00016C0F File Offset: 0x00014E0F
		internal void CallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries = 3)
		{
			this.CallServiceWithRetry(action, debugMessage, null, numberOfRetries, false);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00016C1C File Offset: 0x00014E1C
		internal void CallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries = 3)
		{
			this.CallServiceWithRetry(action, debugMessage, null, numberOfRetries, true);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00016C2C File Offset: 0x00014E2C
		internal void CallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage)
		{
			ArgumentValidator.ThrowIfNull("cachedProxy", cachedProxy);
			WCFConnectionStateTuple<TClient> wcfconnectionStateTuple = cachedProxy as WCFConnectionStateTuple<TClient>;
			ArgumentValidator.ThrowIfNull("proxyToUse", wcfconnectionStateTuple);
			this.CallServiceWithRetry(action, debugMessage, wcfconnectionStateTuple, 1, false);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00016C61 File Offset: 0x00014E61
		protected SyncAgentTransientException GetTransientWrappedException(FaultException<PolicySyncTransientFault> transientFault)
		{
			ArgumentValidator.ThrowIfNull("transientFault", transientFault);
			return new SyncAgentTransientException(transientFault.Detail.Message, transientFault.InnerException, transientFault.Detail.IsPerObjectException, SyncAgentErrorCode.Generic);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00016C90 File Offset: 0x00014E90
		protected SyncAgentPermanentException GetPermanentWrappedException(FaultException<PolicySyncPermanentFault> permanentFault)
		{
			ArgumentValidator.ThrowIfNull("permanentFault", permanentFault);
			return new SyncAgentPermanentException(permanentFault.Detail.Message, permanentFault.InnerException, permanentFault.Detail.IsPerObjectException, SyncAgentErrorCode.Generic);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00016CC0 File Offset: 0x00014EC0
		protected void LogCallServiceError(Exception error, string periodicKey, string debugMessage, int numberOfRetries)
		{
			PolicySyncUtils.ServiceProxyPoolErrorData serviceProxyPoolErrorData = new PolicySyncUtils.ServiceProxyPoolErrorData(periodicKey, debugMessage, numberOfRetries);
			this.logProvider.LogOneEntry(this.ToString(), null, ExecutionLog.EventType.Error, serviceProxyPoolErrorData.ToString(), error);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00016CF1 File Offset: 0x00014EF1
		private static bool IsUsable(CommunicationState clientState)
		{
			return clientState != CommunicationState.Closing && clientState != CommunicationState.Closed && clientState != CommunicationState.Faulted;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00016D04 File Offset: 0x00014F04
		private SyncAgentExceptionBase HandleException(Exception exception, string debugMessage)
		{
			if (exception is FaultException<PolicySyncTransientFault>)
			{
				return this.GetTransientWrappedException(exception as FaultException<PolicySyncTransientFault>);
			}
			if (exception is FaultException<PolicySyncPermanentFault>)
			{
				return this.GetPermanentWrappedException(exception as FaultException<PolicySyncPermanentFault>);
			}
			return new SyncAgentPermanentException("Unexpected exception occured, considered by default a permanent exception.", exception);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00016D3C File Offset: 0x00014F3C
		private void CallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, WCFConnectionStateTuple<TClient> proxyToUse, int numberOfRetries, bool doNotReturnProxyOnSuccess)
		{
			Exception ex;
			if (!this.TryCallServiceWithRetry(action, debugMessage, proxyToUse, numberOfRetries, doNotReturnProxyOnSuccess, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00016D5C File Offset: 0x00014F5C
		private bool TryCallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, WCFConnectionStateTuple<TClient> proxyToUse, int numberOfRetries, bool doNotReturnProxyOnSuccess, out Exception exception)
		{
			ArgumentValidator.ThrowIfNull("action", action);
			exception = null;
			int num = numberOfRetries;
			debugMessage = (string.IsNullOrEmpty(debugMessage) ? string.Empty : debugMessage);
			Exception ex;
			Exception error;
			for (;;)
			{
				WCFConnectionStateTuple<TClient> wcfconnectionStateTuple = null;
				ex = null;
				error = null;
				try
				{
					wcfconnectionStateTuple = (proxyToUse ?? this.GetClient(num == numberOfRetries || num > 1));
					action(wcfconnectionStateTuple);
					wcfconnectionStateTuple.LastUsed = DateTime.UtcNow;
				}
				catch (Exception ex2)
				{
					error = ex2;
					ex = this.HandleException(ex2, debugMessage);
				}
				finally
				{
					if (wcfconnectionStateTuple != null)
					{
						if (ex == null)
						{
							if (!doNotReturnProxyOnSuccess)
							{
								this.ReturnClientToPool(wcfconnectionStateTuple);
								this.DecrementOutstandingProxies(debugMessage);
							}
						}
						else
						{
							PolicySyncUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), this.logProvider, false);
							wcfconnectionStateTuple = null;
							this.DecrementOutstandingProxies(debugMessage);
						}
					}
					num--;
				}
				if (ex == null)
				{
					break;
				}
				if (0 >= num || !(ex is SyncAgentTransientException))
				{
					goto IL_C2;
				}
			}
			return true;
			IL_C2:
			this.LogCallServiceError(error, this.TargetInfo, debugMessage, num);
			exception = ex;
			return false;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00016E5C File Offset: 0x0001505C
		private WCFConnectionStateTuple<TClient> GetClient(bool useCache = true)
		{
			WCFConnectionStateTuple<TClient> wcfconnectionStateTuple = null;
			while (useCache && !this.pool.IsEmpty && this.pool.TryPop(out wcfconnectionStateTuple) && !this.IsWCFClientUsable(wcfconnectionStateTuple))
			{
				PolicySyncUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), this.logProvider, false);
				wcfconnectionStateTuple = null;
			}
			ICommunicationObject communicationObject;
			if (wcfconnectionStateTuple == null)
			{
				wcfconnectionStateTuple = new WCFConnectionStateTuple<TClient>();
				wcfconnectionStateTuple.Client = this.ChannelFactory.CreateChannel();
				communicationObject = (ICommunicationObject)((object)wcfconnectionStateTuple.Client);
				communicationObject.Open();
			}
			else
			{
				communicationObject = (ICommunicationObject)((object)wcfconnectionStateTuple.Client);
			}
			if (communicationObject.State != CommunicationState.Opened)
			{
				int num = 3;
				while (communicationObject.State != CommunicationState.Opened)
				{
					Thread.Sleep(500);
					num--;
					if (num == 0)
					{
						break;
					}
				}
			}
			Interlocked.Increment(ref this.outstandingProxies);
			return wcfconnectionStateTuple;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00016F30 File Offset: 0x00015130
		private void ReturnClientToPool(WCFConnectionStateTuple<TClient> clientInfo)
		{
			ArgumentValidator.ThrowIfNull("clientInfo", clientInfo);
			if (this.IsWCFClientUsable(clientInfo) && (long)this.pool.Count <= (long)((ulong)this.MaxNumberOfClientProxies))
			{
				this.pool.Push(clientInfo);
				return;
			}
			PolicySyncUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)clientInfo.Client), this.logProvider, false);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00016F90 File Offset: 0x00015190
		private bool IsWCFClientUsable(WCFConnectionStateTuple<TClient> clientInfo)
		{
			ArgumentValidator.ThrowIfNull("clientInfo", clientInfo);
			return !(DateTime.UtcNow - clientInfo.LastUsed >= ServiceProxyPool<TClient>.ClientMaximumLifetime) && ServiceProxyPool<TClient>.IsUsable((clientInfo.Client as ICommunicationObject).State);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00016FE0 File Offset: 0x000151E0
		private void DecrementOutstandingProxies(string debugMessage)
		{
			Interlocked.Decrement(ref this.outstandingProxies);
		}

		// Token: 0x04000416 RID: 1046
		private const int TotalRetries = 3;

		// Token: 0x04000417 RID: 1047
		public static readonly TimeSpan DefaultInactivityTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000418 RID: 1048
		private static readonly TimeSpan ClientMaximumLifetime = ServiceProxyPool<TClient>.DefaultInactivityTimeout.Subtract(TimeSpan.FromSeconds(30.0));

		// Token: 0x04000419 RID: 1049
		private readonly ConcurrentStack<WCFConnectionStateTuple<TClient>> pool = new ConcurrentStack<WCFConnectionStateTuple<TClient>>();

		// Token: 0x0400041A RID: 1050
		private long outstandingProxies;

		// Token: 0x0400041B RID: 1051
		private ExecutionLog logProvider;
	}
}

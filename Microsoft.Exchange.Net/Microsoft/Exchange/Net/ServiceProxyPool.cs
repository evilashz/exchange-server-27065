using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000891 RID: 2193
	internal abstract class ServiceProxyPool<TClient> : IServiceProxyPool<TClient>, IDisposeTrackable, IDisposable
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x00068C98 File Offset: 0x00066E98
		protected ServiceProxyPool(string endpointName, string hostName, int maxNumberOfClientProxies, ChannelFactory<TClient> channelFactory, bool useDisposeTracker) : this(endpointName, hostName, maxNumberOfClientProxies, useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("channelFactory", channelFactory);
			this.channelFactoryList = new List<ChannelFactory<TClient>>
			{
				channelFactory
			};
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00068CD1 File Offset: 0x00066ED1
		protected ServiceProxyPool(string endpointName, string hostName, int maxNumberOfClientProxies, List<ChannelFactory<TClient>> channelFactoryList, bool useDisposeTracker) : this(endpointName, hostName, maxNumberOfClientProxies, useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("channelFactoryList", channelFactoryList);
			ArgumentValidator.ThrowIfZeroOrNegative("channelFactoryList.Count", channelFactoryList.Count);
			this.channelFactoryList = channelFactoryList;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00068D04 File Offset: 0x00066F04
		private ServiceProxyPool(string endpointName, string hostName, int maxNumberOfClientProxies, bool useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("endpointName", endpointName);
			ArgumentValidator.ThrowIfNullOrEmpty("hostName", hostName);
			this.maxNumberOfClientProxies = maxNumberOfClientProxies;
			this.EndpointName = endpointName;
			this.TargetInfo = string.Format("{0} ({1})", endpointName, hostName);
			this.pool = new ConcurrentStack<ServiceProxyPool<TClient>.WCFConnectionStateTuple>();
			this.outstandingProxies = 0L;
			if (useDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
			this.counters = ServiceProxyPoolCounters.GetInstance(string.Format("{0} {1}", endpointName, Process.GetCurrentProcess().ProcessName));
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x00068D90 File Offset: 0x00066F90
		protected ChannelFactory<TClient> ChannelFactory
		{
			get
			{
				return this.channelFactoryList[0];
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x00068D9E File Offset: 0x00066F9E
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x00068DA6 File Offset: 0x00066FA6
		private protected string EndpointName { protected get; private set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x00068DAF File Offset: 0x00066FAF
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x00068DB7 File Offset: 0x00066FB7
		private protected string TargetInfo { protected get; private set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002EDD RID: 11997
		protected abstract Microsoft.Exchange.Diagnostics.Trace Tracer { get; }

		// Token: 0x06002EDE RID: 11998 RVA: 0x00068DC0 File Offset: 0x00066FC0
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ServiceProxyPool<TClient>>(this);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00068DC8 File Offset: 0x00066FC8
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x00068DE0 File Offset: 0x00066FE0
		public void Dispose()
		{
			this.Tracer.TraceDebug<int, string>((long)this.GetHashCode(), "Disposing of ServiceProxyPool instance {0} for {1}", this.GetHashCode(), this.EndpointName);
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			while (!this.pool.IsEmpty)
			{
				ServiceProxyPool<TClient>.WCFConnectionStateTuple wcfconnectionStateTuple = null;
				if (this.pool.TryPop(out wcfconnectionStateTuple))
				{
					WcfUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), false);
					this.counters.ProxyInstanceCount.Decrement();
				}
			}
			foreach (ChannelFactory<TClient> channelFactory in this.channelFactoryList)
			{
				if (channelFactory != null)
				{
					WcfUtils.DisposeWcfClientGracefully(channelFactory, false);
				}
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00068EBC File Offset: 0x000670BC
		internal void CallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries = 3)
		{
			this.CallServiceWithRetry(action, debugMessage, null, numberOfRetries, false);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x00068EC9 File Offset: 0x000670C9
		internal void CallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries = 3)
		{
			this.CallServiceWithRetry(action, debugMessage, null, numberOfRetries, true);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x00068ED8 File Offset: 0x000670D8
		internal void CallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage)
		{
			ArgumentValidator.ThrowIfNull("cachedProxy", cachedProxy);
			ServiceProxyPool<TClient>.WCFConnectionStateTuple wcfconnectionStateTuple = cachedProxy as ServiceProxyPool<TClient>.WCFConnectionStateTuple;
			ArgumentValidator.ThrowIfNull("proxyToUse", wcfconnectionStateTuple);
			this.CallServiceWithRetry(action, debugMessage, wcfconnectionStateTuple, 1, false);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00068F0D File Offset: 0x0006710D
		public bool TryCallServiceWithRetryAsyncBegin(Action<IPooledServiceProxy<TClient>> action, string debugMessage, int numberOfRetries, out Exception exception)
		{
			return this.TryCallServiceWithRetry(action, debugMessage, null, numberOfRetries, true, out exception);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x00068F1C File Offset: 0x0006711C
		public bool TryCallServiceWithRetryAsyncEnd(IPooledServiceProxy<TClient> cachedProxy, Action<IPooledServiceProxy<TClient>> action, string debugMessage, out Exception exception)
		{
			ArgumentValidator.ThrowIfNull("cachedProxy", cachedProxy);
			ServiceProxyPool<TClient>.WCFConnectionStateTuple wcfconnectionStateTuple = cachedProxy as ServiceProxyPool<TClient>.WCFConnectionStateTuple;
			ArgumentValidator.ThrowIfNull("proxyToUse", wcfconnectionStateTuple);
			return this.TryCallServiceWithRetry(action, debugMessage, wcfconnectionStateTuple, 1, false, out exception);
		}

		// Token: 0x06002EE6 RID: 12006
		protected abstract Exception GetTransientWrappedException(Exception wcfException);

		// Token: 0x06002EE7 RID: 12007
		protected abstract Exception GetPermanentWrappedException(Exception wcfException);

		// Token: 0x06002EE8 RID: 12008
		protected abstract void LogCallServiceError(Exception error, string priodicKey, string debugMessage, int numberOfRetries);

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00068F53 File Offset: 0x00067153
		protected virtual bool IsTransientException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			return ServiceProxyPool<TClient>.transientExceptions.Contains(ex.GetType());
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x00068F70 File Offset: 0x00067170
		private Exception HandleException(Exception ex, string debugMessage)
		{
			if (this.IsTransientException(ex))
			{
				this.Tracer.TraceError<string, string, string>((long)this.GetHashCode(), "{0} got WCF {1} exception {2}. Transient Exception.", debugMessage, ex.GetType().Name, ex.ToString());
				return this.GetTransientWrappedException(ex);
			}
			this.Tracer.TraceError<string, string, string>((long)this.GetHashCode(), "{0} got WCF {1} exception {2}. Permanent Exception.", debugMessage, ex.GetType().Name, ex.ToString());
			return this.GetPermanentWrappedException(ex);
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x00068FE8 File Offset: 0x000671E8
		private void CallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, ServiceProxyPool<TClient>.WCFConnectionStateTuple proxyToUse, int numberOfRetries, bool doNotReturnProxyOnSuccess)
		{
			Exception ex;
			if (!this.TryCallServiceWithRetry(action, debugMessage, proxyToUse, numberOfRetries, doNotReturnProxyOnSuccess, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x00069008 File Offset: 0x00067208
		private bool TryCallServiceWithRetry(Action<IPooledServiceProxy<TClient>> action, string debugMessage, ServiceProxyPool<TClient>.WCFConnectionStateTuple proxyToUse, int numberOfRetries, bool doNotReturnProxyOnSuccess, out Exception exception)
		{
			ArgumentValidator.ThrowIfNull("action", action);
			Stopwatch stopwatch = Stopwatch.StartNew();
			exception = null;
			int num = numberOfRetries;
			debugMessage = (string.IsNullOrEmpty(debugMessage) ? string.Empty : debugMessage);
			Exception ex;
			Exception error;
			for (;;)
			{
				ServiceProxyPool<TClient>.WCFConnectionStateTuple wcfconnectionStateTuple = null;
				ex = null;
				error = null;
				bool flag = false;
				try
				{
					wcfconnectionStateTuple = (proxyToUse ?? this.GetClient(numberOfRetries - num, ref flag, num == numberOfRetries || num > 1));
					this.counters.NumberOfCalls.Increment();
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
								if (!flag)
								{
									this.ReturnClientToPool(wcfconnectionStateTuple);
									this.DecrementOutstandingProxies(debugMessage);
								}
								else
								{
									WcfUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), false);
									wcfconnectionStateTuple = null;
									this.counters.ProxyInstanceCount.Decrement();
								}
							}
						}
						else
						{
							WcfUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), false);
							wcfconnectionStateTuple = null;
							this.counters.ProxyInstanceCount.Decrement();
							this.DecrementOutstandingProxies(debugMessage);
						}
					}
					num--;
				}
				stopwatch.Stop();
				this.counters.AverageLatency.IncrementBy(stopwatch.ElapsedMilliseconds);
				this.counters.AverageLatencyBase.Increment();
				if (ex == null)
				{
					break;
				}
				if (0 >= num || !(ex is TransientException))
				{
					goto IL_14C;
				}
			}
			return true;
			IL_14C:
			this.LogCallServiceError(error, this.TargetInfo, debugMessage, numberOfRetries - num);
			exception = ex;
			return false;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x00069198 File Offset: 0x00067398
		private ServiceProxyPool<TClient>.WCFConnectionStateTuple GetClient(int retry, ref bool doNotReturnProxyAfterRetry, bool useCache = true)
		{
			ServiceProxyPool<TClient>.WCFConnectionStateTuple wcfconnectionStateTuple = null;
			if (retry > 0 && this.channelFactoryList.Count > retry)
			{
				useCache = false;
				doNotReturnProxyAfterRetry = true;
			}
			while (useCache && !this.pool.IsEmpty && this.pool.TryPop(out wcfconnectionStateTuple) && !this.IsWCFClientUsable(wcfconnectionStateTuple))
			{
				WcfUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)wcfconnectionStateTuple.Client), false);
				this.counters.ProxyInstanceCount.Decrement();
				wcfconnectionStateTuple = null;
			}
			ICommunicationObject communicationObject;
			if (wcfconnectionStateTuple == null)
			{
				wcfconnectionStateTuple = new ServiceProxyPool<TClient>.WCFConnectionStateTuple();
				wcfconnectionStateTuple.Client = this.GetChannelFactory(retry).CreateChannel();
				communicationObject = (ICommunicationObject)((object)wcfconnectionStateTuple.Client);
				communicationObject.Open();
				this.counters.ProxyInstanceCount.Increment();
			}
			else
			{
				communicationObject = (ICommunicationObject)((object)wcfconnectionStateTuple.Client);
			}
			if (communicationObject.State != CommunicationState.Opened)
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "Channel is created but not in opened state. Will wait and retry.");
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
				if (communicationObject.State != CommunicationState.Opened)
				{
					this.Tracer.TraceError<int>((long)this.GetHashCode(), "Channel cannot be opened with {0} retries.", 3);
				}
				else
				{
					this.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Channel was opened with {0} retries.", 3 - num);
				}
			}
			long arg = Interlocked.Increment(ref this.outstandingProxies);
			this.counters.OutstandingCalls.Increment();
			this.Tracer.TraceDebug<long>((long)this.GetHashCode(), "ServiceProxyPool.GetClient: Outstanding Proxies are {0}", arg);
			return wcfconnectionStateTuple;
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x0006931B File Offset: 0x0006751B
		private ChannelFactory<TClient> GetChannelFactory(int retry)
		{
			if (this.channelFactoryList.Count - 1 < retry)
			{
				return this.channelFactoryList[this.channelFactoryList.Count - 1];
			}
			return this.channelFactoryList[retry];
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x00069354 File Offset: 0x00067554
		private void ReturnClientToPool(ServiceProxyPool<TClient>.WCFConnectionStateTuple clientInfo)
		{
			ArgumentValidator.ThrowIfNull("clientInfo", clientInfo);
			if (this.IsWCFClientUsable(clientInfo) && this.pool.Count <= this.maxNumberOfClientProxies)
			{
				this.pool.Push(clientInfo);
				return;
			}
			WcfUtils.DisposeWcfClientGracefully((ICommunicationObject)((object)clientInfo.Client), false);
			this.counters.ProxyInstanceCount.Decrement();
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000693BC File Offset: 0x000675BC
		private bool IsWCFClientUsable(ServiceProxyPool<TClient>.WCFConnectionStateTuple clientInfo)
		{
			ArgumentValidator.ThrowIfNull("clientInfo", clientInfo);
			if (DateTime.UtcNow - clientInfo.LastUsed >= ServiceProxyPool<TClient>.ClientMaximumLifetime)
			{
				return false;
			}
			CommunicationState state = ((ICommunicationObject)((object)clientInfo.Client)).State;
			return state != CommunicationState.Closing && state != CommunicationState.Closed && state != CommunicationState.Faulted;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x00069418 File Offset: 0x00067618
		private void DecrementOutstandingProxies(string debugMessage)
		{
			long arg = Interlocked.Decrement(ref this.outstandingProxies);
			this.counters.OutstandingCalls.Decrement();
			this.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "ServiceProxyPool.DecrementOutstandingProxies: {0} Outstanding Proxies are {1}", debugMessage, arg);
		}

		// Token: 0x040028CF RID: 10447
		private const int TotalRetries = 3;

		// Token: 0x040028D0 RID: 10448
		internal static readonly TimeSpan DefaultInactivityTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x040028D1 RID: 10449
		private static readonly TimeSpan ClientMaximumLifetime = ServiceProxyPool<TClient>.DefaultInactivityTimeout.Subtract(TimeSpan.FromSeconds(30.0));

		// Token: 0x040028D2 RID: 10450
		private static readonly Type[] transientExceptions = new Type[]
		{
			typeof(TimeoutException),
			typeof(EndpointNotFoundException),
			typeof(FaultException<ExceptionDetail>),
			typeof(CommunicationException),
			typeof(InvalidOperationException)
		};

		// Token: 0x040028D3 RID: 10451
		private readonly int maxNumberOfClientProxies;

		// Token: 0x040028D4 RID: 10452
		private DisposeTracker disposeTracker;

		// Token: 0x040028D5 RID: 10453
		private ConcurrentStack<ServiceProxyPool<TClient>.WCFConnectionStateTuple> pool;

		// Token: 0x040028D6 RID: 10454
		private List<ChannelFactory<TClient>> channelFactoryList;

		// Token: 0x040028D7 RID: 10455
		private long outstandingProxies;

		// Token: 0x040028D8 RID: 10456
		private ServiceProxyPoolCountersInstance counters;

		// Token: 0x02000893 RID: 2195
		private class WCFConnectionStateTuple : IPooledServiceProxy<TClient>
		{
			// Token: 0x17000C6F RID: 3183
			// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000694E7 File Offset: 0x000676E7
			// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000694EF File Offset: 0x000676EF
			public TClient Client { get; set; }

			// Token: 0x17000C70 RID: 3184
			// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000694F8 File Offset: 0x000676F8
			// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x00069500 File Offset: 0x00067700
			public DateTime LastUsed { get; set; }

			// Token: 0x17000C71 RID: 3185
			// (get) Token: 0x06002EFA RID: 12026 RVA: 0x00069509 File Offset: 0x00067709
			// (set) Token: 0x06002EFB RID: 12027 RVA: 0x00069511 File Offset: 0x00067711
			public string Tag { get; set; }
		}
	}
}

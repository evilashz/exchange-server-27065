using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D43 RID: 3395
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ADNotificationHandler
	{
		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x060075BC RID: 30140 RVA: 0x0020933D File Offset: 0x0020753D
		// (set) Token: 0x060075BD RID: 30141 RVA: 0x00209348 File Offset: 0x00207548
		internal ExchangeTopologyScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				lock (this.registeredExchangeTopologyNotificationLock)
				{
					if (this.registeredExchangeTopologyNotification != null)
					{
						throw new InvalidOperationException("Can't set scope after notification is regiestered");
					}
					this.scope = value;
				}
			}
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x0020939C File Offset: 0x0020759C
		internal void RegisterExchangeTopologyNotificationIfNeeded()
		{
			if (this.registeredExchangeTopologyNotification == null)
			{
				this.RegisterExchangeTopologyNotification();
			}
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x002093AC File Offset: 0x002075AC
		internal void UnRegisterExchangeTopologyNotification()
		{
			this.InternalUnRegisterExchangeTopologyNotification();
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x002093B4 File Offset: 0x002075B4
		private void RegisterExchangeTopologyNotification()
		{
			Exception ex = null;
			try
			{
				lock (this.registeredExchangeTopologyNotificationLock)
				{
					this.registeredExchangeTopologyNotification = ServiceDiscovery.ExchangeTopologyBridge.RegisterExchangeTopologyNotification(new ADNotificationCallback(this.ADNotificationCallback), this.scope);
				}
			}
			catch (ServiceDiscoveryPermanentException ex2)
			{
				ex = ex2;
			}
			catch (ServiceDiscoveryTransientException ex3)
			{
				ex = ex3;
			}
			if (ex == null)
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug(0L, "ADNotificationHandler::RegisterExchangeTopologyNotification. Successfully registered for ExchangeTopologyNotification.");
				ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_RegisteredForTopologyChangedNotification, null, new object[0]);
				return;
			}
			string text = ex.ToString().TruncateToUseInEventLog();
			ExTraceGlobals.ServiceDiscoveryTracer.TraceError<string>(0L, "ADNotificationHandler::RegisterExchangeTopologyNotification. Failed to Failed to register for ExchangeTopologyNotification. Error = {0}", text);
			ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorRegisteringForTopologyChangedNotification, null, new object[]
			{
				text
			});
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x0020949C File Offset: 0x0020769C
		private void InternalUnRegisterExchangeTopologyNotification()
		{
			try
			{
				if (this.registeredExchangeTopologyNotification != null)
				{
					this.registeredExchangeTopologyNotification.Unregister();
				}
			}
			catch (ServiceDiscoveryPermanentException arg)
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryPermanentException>(0L, "ADNotificationHandler::UnRegisterExchangeTopologyNotification. Failed to unregister for ExchangeTopologyNotification. Error = {0}", arg);
			}
			catch (ServiceDiscoveryTransientException arg2)
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryTransientException>(0L, "ADNotificationHandler::UnRegisterExchangeTopologyNotification. Failed to Failed to unregister for ExchangeTopologyNotification. Error = {0}", arg2);
			}
			lock (this.thisLock)
			{
				this.notificationArrived = false;
			}
			this.registeredExchangeTopologyNotification = null;
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x0020953C File Offset: 0x0020773C
		private void ADNotificationCallback(ADNotificationEventArgs args)
		{
			lock (this.thisLock)
			{
				if (!this.notificationArrived)
				{
					this.notificationArrived = true;
					this.EnqueueTriggerCacheRefresh();
				}
			}
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x0020958C File Offset: 0x0020778C
		private void EnqueueTriggerCacheRefresh()
		{
			ThreadPool.RegisterWaitForSingleObject(this.stopTimerEvent, new WaitOrTimerCallback(this.TriggerCacheRefresh), null, ServiceDiscovery.ExchangeTopologyBridge.NotificationDelayTimeout, true);
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x002095B4 File Offset: 0x002077B4
		private void TriggerCacheRefresh(object state, bool timedOut)
		{
			lock (this.thisLock)
			{
				this.notificationArrived = false;
			}
			ServiceCache.TriggerCacheRefreshDueToNotification();
		}

		// Token: 0x040051B8 RID: 20920
		private readonly object thisLock = new object();

		// Token: 0x040051B9 RID: 20921
		private readonly ManualResetEvent stopTimerEvent = new ManualResetEvent(false);

		// Token: 0x040051BA RID: 20922
		private readonly object registeredExchangeTopologyNotificationLock = new object();

		// Token: 0x040051BB RID: 20923
		private IRegisteredExchangeTopologyNotification registeredExchangeTopologyNotification;

		// Token: 0x040051BC RID: 20924
		private bool notificationArrived;

		// Token: 0x040051BD RID: 20925
		private ExchangeTopologyScope scope = ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology;
	}
}

using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D4F RID: 3407
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RegisteredExchangeTopologyNotification : IRegisteredExchangeTopologyNotification
	{
		// Token: 0x0600761C RID: 30236 RVA: 0x00209F68 File Offset: 0x00208168
		internal RegisteredExchangeTopologyNotification(ADNotificationCallback callback, ExchangeTopologyScope scope)
		{
			try
			{
				this.adNotificationRequestCookie = ADNotificationAdapter.RegisterExchangeTopologyChangeNotification(callback, null, scope);
			}
			catch (DataSourceOperationException innerException)
			{
				ServiceDiscoveryPermanentException ex = new ServiceDiscoveryPermanentException(ServerStrings.ExFailedToRegisterExchangeTopologyNotification, innerException);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryPermanentException>(0L, "RegisteredExchangeTopologyNotification::Constructor. Failed to register. Throwing exception: {0}.", ex);
				throw ex;
			}
			catch (ADTransientException innerException2)
			{
				ServiceDiscoveryTransientException ex2 = new ServiceDiscoveryTransientException(ServerStrings.ExFailedToRegisterExchangeTopologyNotification, innerException2);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryTransientException>(0L, "RegisteredExchangeTopologyNotification::Constructor. Failed to register. Throwing exception: {0}.", ex2);
				throw ex2;
			}
		}

		// Token: 0x0600761D RID: 30237 RVA: 0x00209FF4 File Offset: 0x002081F4
		public void Unregister()
		{
			try
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.adNotificationRequestCookie);
			}
			catch (DataSourceOperationException innerException)
			{
				ServiceDiscoveryPermanentException ex = new ServiceDiscoveryPermanentException(ServerStrings.ExFailedToUnregisterExchangeTopologyNotification, innerException);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryPermanentException>(0L, "RegisteredExchangeTopologyNotification::Unregister. Failed to unregister. Throwing exception: {0}.", ex);
				throw ex;
			}
			catch (ADTransientException innerException2)
			{
				ServiceDiscoveryTransientException ex2 = new ServiceDiscoveryTransientException(ServerStrings.ExFailedToUnregisterExchangeTopologyNotification, innerException2);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryTransientException>(0L, "RegisteredExchangeTopologyNotification::Unregister. Failed to unregister. Throwing exception: {0}.", ex2);
				throw ex2;
			}
		}

		// Token: 0x040051DB RID: 20955
		private readonly ADNotificationRequestCookie adNotificationRequestCookie;
	}
}

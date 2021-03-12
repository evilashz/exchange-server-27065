using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000027 RID: 39
	internal class PushNotificationsProxyPool<TClient> : ServiceProxyPool<TClient>
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000465B File Offset: 0x0000285B
		internal PushNotificationsProxyPool(string endpointName, string hostName, ChannelFactory<TClient> channelFactory, bool useDisposeTracker) : base(endpointName, hostName, 10, channelFactory, useDisposeTracker)
		{
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000466A File Offset: 0x0000286A
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.PushNotificationClientTracer;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004671 File Offset: 0x00002871
		protected override bool IsTransientException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			if (ex is FaultException<PushNotificationFault>)
			{
				return ((FaultException<PushNotificationFault>)ex).Detail.CanRetry;
			}
			return base.IsTransientException(ex);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000046A0 File Offset: 0x000028A0
		protected override Exception GetTransientWrappedException(Exception wcfException)
		{
			if (wcfException is TimeoutException)
			{
				return new PushNotificationTransientException(Strings.ExceptionMessageTimeoutCall(base.TargetInfo, wcfException.Message), wcfException);
			}
			if (wcfException is EndpointNotFoundException)
			{
				return new PushNotificationEndpointNotFoundException(Strings.ExceptionEndpointNotFoundError(base.TargetInfo, wcfException.Message), wcfException);
			}
			if (wcfException is FaultException<PushNotificationFault>)
			{
				return new PushNotificationTransientException(Strings.ExceptionPushNotificationError(base.TargetInfo, ((FaultException<PushNotificationFault>)wcfException).Detail.Message), wcfException);
			}
			return new PushNotificationTransientException(Strings.ExceptionPushNotificationError(base.TargetInfo, wcfException.Message), wcfException);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004730 File Offset: 0x00002930
		protected override Exception GetPermanentWrappedException(Exception wcfException)
		{
			if (wcfException is FaultException<InvalidOperationException>)
			{
				return ((FaultException<InvalidOperationException>)wcfException).Detail;
			}
			if (wcfException is FaultException<ArgumentNullException>)
			{
				return ((FaultException<ArgumentNullException>)wcfException).Detail;
			}
			if (wcfException is FaultException<ArgumentException>)
			{
				return ((FaultException<ArgumentException>)wcfException).Detail;
			}
			if (wcfException is FaultException<PushNotificationFault>)
			{
				return new PushNotificationPermanentException(Strings.ExceptionPushNotificationError(base.TargetInfo, ((FaultException<PushNotificationFault>)wcfException).Detail.Message), wcfException);
			}
			return new PushNotificationPermanentException(Strings.ExceptionPushNotificationError(base.TargetInfo, wcfException.Message), wcfException);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000047BA File Offset: 0x000029BA
		protected override void LogCallServiceError(Exception error, string periodicKey, string debugMessage, int numberOfRetries)
		{
			this.Tracer.TraceError<string, int, string>((long)this.GetHashCode(), "Client failed to execute a service command '{0}' after '{1}' number of attempts.\n{2}.", debugMessage, numberOfRetries, error.ToTraceString());
			PushNotificationsCrimsonEvents.CallServiceError.Log<string, string, int>(debugMessage, error.ToTraceString(), numberOfRetries);
		}

		// Token: 0x0400005D RID: 93
		private const int MaxNumberOfClientProxies = 10;
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000894 RID: 2196
	internal class MserveCacheServiceProxyPool<TClient> : ServiceProxyPool<TClient>
	{
		// Token: 0x06002EFD RID: 12029 RVA: 0x00069524 File Offset: 0x00067724
		private MserveCacheServiceProxyPool(string endpointName, string hostName, Trace tracer, int maxNumberOfClientProxies, ChannelFactory<TClient> factory, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker) : base(endpointName, hostName, maxNumberOfClientProxies, factory, useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("getPermanentWrappedExceptionDelegate", getPermanentWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("getTransientWrappedExceptionDelegate", getTransientWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("errorEvent", errorEvent);
			this.getPermanentWrappedExceptionDelegate = getPermanentWrappedExceptionDelegate;
			this.getTransientWrappedExceptionDelegate = getTransientWrappedExceptionDelegate;
			this.errorEvent = errorEvent;
			this.tracer = tracer;
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x00069588 File Offset: 0x00067788
		internal static MserveCacheServiceProxyPool<TClient> CreateMserveCacheServiceProxyPool(string endpointName, EndpointAddress endpointAddress, Trace tracer, int maxNumberOfClientProxies, Binding defaultBinding, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ChannelFactory<TClient> factory = MserveCacheServiceProxyPool<TClient>.CreateChannelFactory(endpointName, endpointAddress, defaultBinding, tracer);
			return new MserveCacheServiceProxyPool<TClient>(endpointName, endpointAddress.Uri.Host ?? "localhost", tracer, maxNumberOfClientProxies, factory, getTransientWrappedExceptionDelegate, getPermanentWrappedExceptionDelegate, errorEvent, useDisposeTracker);
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000695D0 File Offset: 0x000677D0
		protected override Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000695D8 File Offset: 0x000677D8
		protected override Exception GetTransientWrappedException(Exception wcfException)
		{
			return this.getTransientWrappedExceptionDelegate(wcfException, base.TargetInfo);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000695EC File Offset: 0x000677EC
		protected override Exception GetPermanentWrappedException(Exception wcfException)
		{
			return this.getPermanentWrappedExceptionDelegate(wcfException, base.TargetInfo);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00069600 File Offset: 0x00067800
		protected override void LogCallServiceError(Exception error, string priodicKey, string debugMessage, int numberOfRetries)
		{
			MserveCacheServiceProvider.EventLog.LogEvent(this.errorEvent, error.Message, new object[]
			{
				error.Message,
				numberOfRetries,
				error.ToString()
			});
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00069648 File Offset: 0x00067848
		protected override bool IsTransientException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			return ex is SecurityNegotiationException || base.IsTransientException(ex);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00069668 File Offset: 0x00067868
		private static ChannelFactory<TClient> CreateChannelFactory(string endpointName, EndpointAddress endpointAddress, Binding defaultBinding, Trace tracer)
		{
			ArgumentValidator.ThrowIfNull("endpointName", endpointName);
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ArgumentValidator.ThrowIfNull("defaultBinding", endpointAddress);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ChannelFactory<TClient> channelFactory = null;
			try
			{
				channelFactory = WcfUtils.TryCreateChannelFactoryFromConfig<TClient>(endpointName);
			}
			catch (Exception ex)
			{
				tracer.TraceError<string, string>(0L, "ServiceProxyPool - Error Creating channel factory from config file for {0}. Details {1}", endpointName, ex.ToString());
				MserveCacheServiceProvider.EventLog.LogEvent(CommonEventLogConstants.Tuple_WcfClientConfigError, endpointName, new object[]
				{
					endpointName,
					ex.Message
				});
			}
			if (channelFactory != null)
			{
				string host = endpointAddress.Uri.Host;
				Uri uri = channelFactory.Endpoint.Address.Uri;
				string uri2 = string.Format("{0}://{1}:{2}{3}", new object[]
				{
					uri.Scheme,
					host,
					uri.Port,
					uri.PathAndQuery
				});
				channelFactory.Endpoint.Address = new EndpointAddress(uri2);
			}
			else
			{
				tracer.TraceDebug<string>(0L, "ServiceProxyPool - Creating channel factory for {0} using default configuration", endpointName);
				channelFactory = new ChannelFactory<TClient>(defaultBinding, endpointAddress);
			}
			return channelFactory;
		}

		// Token: 0x040028DE RID: 10462
		private readonly GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate;

		// Token: 0x040028DF RID: 10463
		private readonly GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate;

		// Token: 0x040028E0 RID: 10464
		private readonly ExEventLog.EventTuple errorEvent;

		// Token: 0x040028E1 RID: 10465
		private readonly Trace tracer;
	}
}

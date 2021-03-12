using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E3 RID: 227
	internal class DirectoryServiceProxyPool<TClient> : ServiceProxyPool<TClient>
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x00033744 File Offset: 0x00031944
		private DirectoryServiceProxyPool(string endpointName, string hostName, Trace tracer, int maxNumberOfClientProxies, ChannelFactory<TClient> factory, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker) : base(endpointName, hostName, maxNumberOfClientProxies, factory, useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("getPermanentWrappedExceptionDelegate", getPermanentWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("getTransientWrappedExceptionDelegate", getTransientWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("errorEvent", errorEvent);
			this.getPermanentWrappedExceptionDelegate = getPermanentWrappedExceptionDelegate;
			this.getTransientWrappedExceptionDelegate = getTransientWrappedExceptionDelegate;
			this.errorEvent = errorEvent;
			this.tracer = tracer;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000337A8 File Offset: 0x000319A8
		private DirectoryServiceProxyPool(string endpointName, string hostName, Trace tracer, int maxNumberOfClientProxies, List<ChannelFactory<TClient>> factoryList, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker) : base(endpointName, hostName, maxNumberOfClientProxies, factoryList, useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("getPermanentWrappedExceptionDelegate", getPermanentWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("getTransientWrappedExceptionDelegate", getTransientWrappedExceptionDelegate);
			ArgumentValidator.ThrowIfNull("errorEvent", errorEvent);
			this.getPermanentWrappedExceptionDelegate = getPermanentWrappedExceptionDelegate;
			this.getTransientWrappedExceptionDelegate = getTransientWrappedExceptionDelegate;
			this.errorEvent = errorEvent;
			this.tracer = tracer;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003380C File Offset: 0x00031A0C
		internal static DirectoryServiceProxyPool<TClient> CreateDirectoryServiceProxyPool(string endpointName, EndpointAddress endpointAddress, Trace tracer, int maxNumberOfClientProxies, Binding defaultBinding, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ChannelFactory<TClient> factory = DirectoryServiceProxyPool<TClient>.CreateChannelFactory(endpointName, endpointAddress, defaultBinding, tracer);
			return new DirectoryServiceProxyPool<TClient>(endpointName, endpointAddress.Uri.Host ?? "localhost", tracer, maxNumberOfClientProxies, factory, getTransientWrappedExceptionDelegate, getPermanentWrappedExceptionDelegate, errorEvent, useDisposeTracker);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00033854 File Offset: 0x00031A54
		internal static DirectoryServiceProxyPool<TClient> CreateDirectoryServiceProxyPool(string endpointName, ServiceEndpoint serviceEndpoint, Trace tracer, int maxNumberOfClientProxies, Binding defaultBinding, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("serviceEndpoint", serviceEndpoint);
			ChannelFactory<TClient> factory = DirectoryServiceProxyPool<TClient>.CreateChannelFactory(endpointName, serviceEndpoint, defaultBinding, tracer);
			return new DirectoryServiceProxyPool<TClient>(endpointName, serviceEndpoint.Uri.Host, tracer, maxNumberOfClientProxies, factory, getTransientWrappedExceptionDelegate, getPermanentWrappedExceptionDelegate, errorEvent, useDisposeTracker);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00033894 File Offset: 0x00031A94
		internal static DirectoryServiceProxyPool<TClient> CreateDirectoryServiceProxyPool(string endpointName, ServiceEndpoint serviceEndpoint, Trace tracer, int maxNumberOfClientProxies, List<WSHttpBinding> httpBindings, GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate, GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate, ExEventLog.EventTuple errorEvent, bool useDisposeTracker)
		{
			ArgumentValidator.ThrowIfNull("serviceEndpoint", serviceEndpoint);
			List<ChannelFactory<TClient>> list = new List<ChannelFactory<TClient>>();
			foreach (Binding defaultBinding in httpBindings)
			{
				list.Add(DirectoryServiceProxyPool<TClient>.CreateChannelFactory(endpointName, serviceEndpoint, defaultBinding, tracer));
			}
			return new DirectoryServiceProxyPool<TClient>(endpointName, serviceEndpoint.Uri.Host, tracer, maxNumberOfClientProxies, list, getTransientWrappedExceptionDelegate, getPermanentWrappedExceptionDelegate, errorEvent, useDisposeTracker);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00033918 File Offset: 0x00031B18
		protected override Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00033920 File Offset: 0x00031B20
		protected override Exception GetTransientWrappedException(Exception wcfException)
		{
			return this.getTransientWrappedExceptionDelegate(wcfException, base.TargetInfo);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00033934 File Offset: 0x00031B34
		protected override Exception GetPermanentWrappedException(Exception wcfException)
		{
			return this.getPermanentWrappedExceptionDelegate(wcfException, base.TargetInfo);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00033948 File Offset: 0x00031B48
		protected override void LogCallServiceError(Exception error, string priodicKey, string debugMessage, int numberOfRetries)
		{
			if (Globals.ProcessInstanceType != InstanceType.NotInitialized)
			{
				string text = error.ToString();
				if (error is FaultException<TopologyServiceFault>)
				{
					text = ((FaultException<TopologyServiceFault>)error).Detail.ToString();
				}
				Globals.LogEvent(this.errorEvent, priodicKey, new object[]
				{
					debugMessage,
					base.TargetInfo,
					numberOfRetries,
					text
				});
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000339AC File Offset: 0x00031BAC
		protected override bool IsTransientException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			if (ex is FaultException<TopologyServiceFault>)
			{
				return ((FaultException<TopologyServiceFault>)ex).Detail.CanRetry;
			}
			if (ex is FaultException<LocatorServiceFault>)
			{
				return ((FaultException<LocatorServiceFault>)ex).Detail.CanRetry;
			}
			return ex is SecurityNegotiationException || base.IsTransientException(ex);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00033A08 File Offset: 0x00031C08
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
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_WcfClientConfigError, endpointName, new object[]
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
			DirectoryServiceProxyPool<TClient>.ConfigWCFServicePointManager();
			return channelFactory;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00033B28 File Offset: 0x00031D28
		private static ChannelFactory<TClient> CreateChannelFactory(string endpointName, ServiceEndpoint serviceEndpoint, Binding defaultBinding, Trace tracer)
		{
			ArgumentValidator.ThrowIfNull("endpointName", endpointName);
			ArgumentValidator.ThrowIfNull("serviceEndpoint", serviceEndpoint);
			ArgumentValidator.ThrowIfNull("defaultBinding", defaultBinding);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ChannelFactory<TClient> channelFactory = null;
			try
			{
				channelFactory = WcfUtils.TryCreateChannelFactoryFromConfig<TClient>(endpointName);
			}
			catch (Exception ex)
			{
				tracer.TraceError<string, string>(0L, "ServiceProxyPool - Error Creating channel factory from config file for {0}. Details {1}", endpointName, ex.ToString());
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_WcfClientConfigError, endpointName, new object[]
				{
					endpointName,
					ex.Message
				});
			}
			if (channelFactory == null)
			{
				channelFactory = new ChannelFactory<TClient>(defaultBinding, serviceEndpoint.Uri.ToString());
			}
			WSHttpBinding wshttpBinding = defaultBinding as WSHttpBinding;
			if (wshttpBinding != null && wshttpBinding.Security.Transport.ClientCredentialType == HttpClientCredentialType.Certificate)
			{
				try
				{
					channelFactory.Credentials.ClientCertificate.Certificate = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(serviceEndpoint.CertificateSubject);
				}
				catch (ArgumentException ex2)
				{
					throw new GlsPermanentException(DirectoryStrings.PermanentGlsError(ex2.Message));
				}
			}
			DirectoryServiceProxyPool<TClient>.ConfigWCFServicePointManager();
			return channelFactory;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00033C2C File Offset: 0x00031E2C
		private static void ConfigWCFServicePointManager()
		{
			ServicePointManager.DefaultConnectionLimit = Math.Max(ServicePointManager.DefaultConnectionLimit, 8 * Environment.ProcessorCount);
			ServicePointManager.EnableDnsRoundRobin = true;
		}

		// Token: 0x0400046B RID: 1131
		private readonly GetWrappedExceptionDelegate getTransientWrappedExceptionDelegate;

		// Token: 0x0400046C RID: 1132
		private readonly GetWrappedExceptionDelegate getPermanentWrappedExceptionDelegate;

		// Token: 0x0400046D RID: 1133
		private readonly ExEventLog.EventTuple errorEvent;

		// Token: 0x0400046E RID: 1134
		private readonly Trace tracer;
	}
}

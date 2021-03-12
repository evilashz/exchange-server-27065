using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000029 RID: 41
	internal abstract class ProtocolPingStrategyBase
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00007424 File Offset: 0x00005624
		public virtual Uri BuildUrl(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			return new UriBuilder
			{
				Scheme = "https:",
				Host = fqdn,
				Path = HttpRuntime.AppDomainAppVirtualPath
			}.Uri;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007470 File Offset: 0x00005670
		public Exception Ping(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<Uri>((long)this.GetHashCode(), "[ProtocolPingStrategyBase::Ctor]: Testing server with URL {0}.", url);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ServicePoint.ConnectionLimit = HttpProxySettings.ServicePointConnectionLimit.Value;
			httpWebRequest.Method = "HEAD";
			httpWebRequest.Timeout = ProtocolPingStrategyBase.DownLevelServerPingTimeout.Value;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.UserAgent = "HttpProxy.ClientAccessServer2010Ping";
			httpWebRequest.KeepAlive = false;
			if (!HttpProxySettings.UseDefaultWebProxy.Value)
			{
				httpWebRequest.Proxy = NullWebProxy.Instance;
			}
			httpWebRequest.ServerCertificateValidationCallback = ProxyApplication.RemoteCertificateValidationCallback;
			CertificateValidationManager.SetComponentId(httpWebRequest, Constants.CertificateValidationComponentId);
			this.PrepareRequest(httpWebRequest);
			try
			{
				using (httpWebRequest.GetResponse())
				{
				}
			}
			catch (WebException ex)
			{
				ExTraceGlobals.VerboseTracer.TraceWarning<WebException>((long)this.GetHashCode(), "[ProtocolPingStrategyBase::TestServer]: Web exception: {0}.", ex);
				if (!this.IsWebExceptionExpected(ex))
				{
					return ex;
				}
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.VerboseTracer.TraceError<Exception>((long)this.GetHashCode(), "[ProtocolPingStrategyBase::TestServer]: General exception {0}.", ex2);
				return ex2;
			}
			finally
			{
				try
				{
					httpWebRequest.Abort();
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000075E0 File Offset: 0x000057E0
		protected virtual void PrepareRequest(HttpWebRequest request)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000075E2 File Offset: 0x000057E2
		protected virtual bool IsWebExceptionExpected(WebException exception)
		{
			return HttpWebHelper.CheckConnectivityError(exception) == HttpWebHelper.ConnectivityError.None;
		}

		// Token: 0x04000070 RID: 112
		private static readonly IntAppSettingsEntry DownLevelServerPingTimeout = new IntAppSettingsEntry(HttpProxySettings.Prefix("DownLevelServerPingTimeout"), 5000, ExTraceGlobals.VerboseTracer);
	}
}

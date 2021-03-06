using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200002D RID: 45
	public sealed class PreloadSessionDataRequestCreator
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000AADC File Offset: 0x00008CDC
		public static void CreateAsyncRequest(HttpContext httpContext, string orgIdPuid, string upn, string rpsHeaders)
		{
			if (PreloadSessionDataRequestCreator.preloadThrottlingCache.Contains(orgIdPuid))
			{
				return;
			}
			try
			{
				ExAssert.RetailAssert(httpContext != null, "HttpContext is null");
				ExAssert.RetailAssert(orgIdPuid != null, "orgIdPuid is null");
				PreloadSessionDataRequestCreator preloadSessionDataRequestCreator = new PreloadSessionDataRequestCreator();
				preloadSessionDataRequestCreator.InternalCreateAsyncRequest(httpContext, orgIdPuid, upn, rpsHeaders);
			}
			catch (Exception ex)
			{
				if (!(ex is HttpException))
				{
					ExWatson.SendReport(ex, ReportOptions.DoNotFreezeThreads, null);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AB50 File Offset: 0x00008D50
		private static Cookie GetCookieFromHttpCookie(HttpCookie httpCookie, string updatedDomainName = null)
		{
			return new Cookie(httpCookie.Name, httpCookie.Value, httpCookie.Path, string.IsNullOrWhiteSpace(updatedDomainName) ? httpCookie.Domain : updatedDomainName);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AB98 File Offset: 0x00008D98
		private void InternalCreateAsyncRequest(HttpContext httpContext, string orgIdPuid, string upn, string rpsHeaders)
		{
			HttpRequest request = httpContext.Request;
			if (request.HttpMethod != HttpMethod.Post.ToString() || !request.RawUrl.Contains("wsignin") || request.Cookies["RPSAuth"] != null)
			{
				ExTraceGlobals.CoreTracer.TraceWarning(0L, string.Format("[PreloadSessionDataRequestCreator::InternalCreateAsyncRequest]: Not the logon postback request. Hence returning... Method = {0}, RawUrl = {1}, RpsAuth cookie present = {2}", request.HttpMethod, request.RawUrl, request.Cookies["RPSAuth"] != null));
				return;
			}
			string sourceCafeServer = CafeHelper.GetSourceCafeServer(request);
			if (string.IsNullOrWhiteSpace(sourceCafeServer))
			{
				ExTraceGlobals.CoreTracer.TraceError(0L, "[PreloadSessionDataRequestCreator::InternalCreateAsyncRequest]: cannot send preload request becuase the CAFE server that made the request cannot be determined");
				return;
			}
			HttpWebRequest preloadRequest = (HttpWebRequest)WebRequest.Create(new UriBuilder(request.GetRequestUrlEvenIfProxied().ToString().Split(new char[]
			{
				'?'
			})[0] + "PreloadSessionData.ashx")
			{
				Host = sourceCafeServer
			}.Uri);
			preloadRequest.Method = request.HttpMethod;
			preloadRequest.Timeout = 20000;
			preloadRequest.PreAuthenticate = true;
			preloadRequest.UserAgent = request.UserAgent;
			preloadRequest.KeepAlive = false;
			preloadRequest.ContentLength = 0L;
			preloadRequest.AllowAutoRedirect = false;
			this.SetCookies(httpContext, preloadRequest, upn, rpsHeaders);
			preloadRequest.ServerCertificateValidationCallback = this.remoteCertificateValidationCallback;
			PreloadSessionDataRequestCreator.preloadThrottlingCache.TryInsertAbsolute(orgIdPuid, true, new TimeSpan(0, 0, 5));
			ThreadPool.QueueUserWorkItem(delegate(object stateInfo)
			{
				this.ExecuteAsyncRequest(preloadRequest);
			});
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AD6C File Offset: 0x00008F6C
		private void SetCookies(HttpContext httpContext, HttpWebRequest preloadRequest, string upn, string rpsHeaders)
		{
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			preloadRequest.CookieContainer = new CookieContainer(request.Cookies.Count + response.Cookies.Count + 1);
			foreach (object obj in request.Cookies)
			{
				string text = (string)obj;
				try
				{
					preloadRequest.CookieContainer.Add(PreloadSessionDataRequestCreator.GetCookieFromHttpCookie(request.Cookies[text], preloadRequest.Host));
				}
				catch (CookieException arg)
				{
					ExTraceGlobals.CoreTracer.TraceWarning<string, CookieException>((long)this.GetHashCode(), "[PreloadSessionDataRequestCreator::SetCookies]: Could not set cookie {0}: {1}.", text, arg);
				}
			}
			foreach (object obj2 in response.Cookies)
			{
				string name = (string)obj2;
				preloadRequest.CookieContainer.Add(PreloadSessionDataRequestCreator.GetCookieFromHttpCookie(response.Cookies[name], preloadRequest.Host));
			}
			UserContextCookie2 userContextCookie = UserContextCookie2.Create(null, Guid.NewGuid().ToString("N"), null, httpContext.Request.IsSecureConnection);
			if (!AuthCommon.IsFrontEnd && !string.IsNullOrWhiteSpace(rpsHeaders))
			{
				IEnumerable<string> enumerable = from s in rpsHeaders.Split(new char[]
				{
					' '
				})
				where s.StartsWith("RPSAuth=") || s.StartsWith("RPSSecAuth=")
				select s;
				foreach (string text2 in enumerable)
				{
					if (text2.StartsWith("RPSAuth="))
					{
						preloadRequest.CookieContainer.Add(new Cookie("RPSAuth", text2.Substring(8, text2.Length - 9), "/", preloadRequest.Host));
					}
					else if (text2.StartsWith("RPSSecAuth="))
					{
						preloadRequest.CookieContainer.Add(new Cookie("RPSSecAuth", text2.Substring(11, text2.Length - 12), "/", preloadRequest.Host));
					}
				}
			}
			preloadRequest.CookieContainer.Add(PreloadSessionDataRequestCreator.GetCookieFromHttpCookie(userContextCookie.HttpCookie, preloadRequest.Host));
			httpContext.Response.Cookies.Set(userContextCookie.HttpCookie);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B01C File Offset: 0x0000921C
		private void ExecuteAsyncRequest(HttpWebRequest request)
		{
			try
			{
				ExTraceGlobals.CoreTracer.TraceDebug((long)this.GetHashCode(), "[PreloadSessionDataRequestCreator::ExecuteAsyncRequest]: Executing preload request");
				LiveIdAuthenticationCounters.TotalSessionDataPreloadRequestsSent.Increment();
				using (request.GetResponse())
				{
				}
			}
			catch (Exception ex)
			{
				if (!(ex is HttpException) && !(ex is WebException))
				{
					ExWatson.SendReport(ex, ReportOptions.DoNotFreezeThreads, null);
				}
				LiveIdAuthenticationCounters.TotalSessionDataPreloadRequestsFailed.Increment();
				ExTraceGlobals.CoreTracer.TraceError<Exception>(0L, "[PreloadSessionDataRequestCreator::ExecuteAsyncRequest]: General exception {0}.", ex);
			}
			finally
			{
				try
				{
					request.Abort();
				}
				catch
				{
				}
			}
		}

		// Token: 0x04000169 RID: 361
		private const int RequestTimeout = 20000;

		// Token: 0x0400016A RID: 362
		private const string CertificateValidationComponentId = "ClientAccessFrontEnd";

		// Token: 0x0400016B RID: 363
		private const string PreloadSessionDataPageName = "PreloadSessionData.ashx";

		// Token: 0x0400016C RID: 364
		private const string CookieHeaderName = "Cookie";

		// Token: 0x0400016D RID: 365
		private const string SetCookieHeaderName = "Set-Cookie";

		// Token: 0x0400016E RID: 366
		private const int PreloadCacheThrottleDuration = 5;

		// Token: 0x0400016F RID: 367
		private static ExactTimeoutCache<string, bool> preloadThrottlingCache = new ExactTimeoutCache<string, bool>(null, null, null, 1024, false);

		// Token: 0x04000170 RID: 368
		private readonly RemoteCertificateValidationCallback remoteCertificateValidationCallback = (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => OwaRegistry.OwaAllowInternalUntrustedCerts.Member || errors == SslPolicyErrors.None;
	}
}

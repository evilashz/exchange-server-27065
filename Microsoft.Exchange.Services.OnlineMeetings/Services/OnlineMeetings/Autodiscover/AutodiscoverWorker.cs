﻿using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover.DataContract;
using Microsoft.Win32;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x02000032 RID: 50
	internal class AutodiscoverWorker
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000068AC File Offset: 0x00004AAC
		static AutodiscoverWorker()
		{
			CertificateValidationManager.RegisterCallback("LyncAutodiscover.NoSsl", new RemoteCertificateValidationCallback(AutodiscoverWorker.ServerCertificateValidatorIgnoreSslErrors));
			CertificateValidationManager.RegisterCallback("LyncAutodiscover", new RemoteCertificateValidationCallback(AutodiscoverWorker.ServerCertificateValidator));
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000690C File Offset: 0x00004B0C
		internal static string LyncAutodiscoverUrlPrefix
		{
			get
			{
				return AppConfigLoader.GetConfigStringValue("OnlineMeetingExternalAutodiscoverPrefix", string.Empty);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000691D File Offset: 0x00004B1D
		internal static string LyncAutodiscoverInternalUrlPrefix
		{
			get
			{
				return AppConfigLoader.GetConfigStringValue("OnlineMeetingInternalAutodiscoverPrefix", string.Empty);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000692E File Offset: 0x00004B2E
		internal static int LyncAutodiscoverTimeout
		{
			get
			{
				return AppConfigLoader.GetConfigIntValue("OnlineMeetingAutodiscoverTimeout", 3000, int.MaxValue, 10000);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006949 File Offset: 0x00004B49
		internal static string UserAgent
		{
			get
			{
				return string.Format("Exchange/{0}/LyncAutodiscover", OAuthUtilities.ServerVersionString);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000695C File Offset: 0x00004B5C
		public static AutodiscoverResult GetUcwaDiscoveryUrl(string sipAddress, OAuthCredentials credentials)
		{
			AutodiscoverResult autodiscoverResult = new AutodiscoverResult();
			autodiscoverResult.SipUri = sipAddress;
			if (!AppConfigLoader.GetConfigBoolValue("IsOnlineMeetingEnabled", false))
			{
				return autodiscoverResult;
			}
			autodiscoverResult.IsOnlineMeetingEnabled = true;
			int num = sipAddress.LastIndexOf("@");
			if (num <= 0)
			{
				autodiscoverResult.Error = new AutodiscoverError(AutodiscoverStep.SipDomain, new ArgumentException(string.Format("Unable to determine the domain from userAddress: {0}", sipAddress)));
				return autodiscoverResult;
			}
			string domain = sipAddress.Substring(num + 1);
			AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpoint(autodiscoverResult, sipAddress, domain);
			if (!autodiscoverResult.HasError)
			{
				AutodiscoverWorker.GetUcwaUrl(autodiscoverResult, sipAddress, credentials);
			}
			return autodiscoverResult;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000069E0 File Offset: 0x00004BE0
		internal static void ExecuteAnonymousLyncAutodiscoverRequests(AutodiscoverResult result, bool sendInternal, string domain, string user)
		{
			if (string.IsNullOrWhiteSpace(AutodiscoverWorker.LyncAutodiscoverInternalUrlPrefix) || string.IsNullOrWhiteSpace(AutodiscoverWorker.LyncAutodiscoverUrlPrefix))
			{
				result.Error = new AutodiscoverError(AutodiscoverStep.AnonymousAutodiscover, new Exception(string.Format("Configuration error: both internal and external autodiscover prefixes must be defined", new object[0])));
				return;
			}
			string arg = string.Format("?sipuri={0}", user);
			try
			{
				string text = sendInternal ? string.Format("http://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}", AutodiscoverWorker.LyncAutodiscoverInternalUrlPrefix, domain, arg) : string.Format("http://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}", AutodiscoverWorker.LyncAutodiscoverUrlPrefix, domain, arg);
				string text2 = sendInternal ? string.Format("https://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}", AutodiscoverWorker.LyncAutodiscoverInternalUrlPrefix, domain, arg) : string.Format("https://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}", AutodiscoverWorker.LyncAutodiscoverUrlPrefix, domain, arg);
				HttpWebRequest httpWebRequest = AutodiscoverWorker.CreateWebRequest(text, null);
				AutodiscoverRequestState autodiscoverRequestState = new AutodiscoverRequestState(httpWebRequest);
				HttpWebRequest httpWebRequest2 = AutodiscoverWorker.CreateWebRequest(text2, null);
				AutodiscoverRequestState autodiscoverRequestState2 = new AutodiscoverRequestState(httpWebRequest2);
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.ExecuteAnonymousLyncAutodiscoverRequests] Sending request to anonymous autodiscover servers: {0} and {1}", text, text2);
				WaitHandle[] waitHandles = new WaitHandle[]
				{
					autodiscoverRequestState.ManualResetEvent,
					autodiscoverRequestState2.ManualResetEvent
				};
				httpWebRequest.BeginGetResponse(new AsyncCallback(AutodiscoverWorker.ProcessLyncAnonymousAutodiscoverResponse), autodiscoverRequestState);
				httpWebRequest2.BeginGetResponse(new AsyncCallback(AutodiscoverWorker.ProcessLyncAnonymousAutodiscoverResponse), autodiscoverRequestState2);
				if (!WaitHandle.WaitAll(waitHandles, AutodiscoverWorker.LyncAutodiscoverTimeout))
				{
					if (!autodiscoverRequestState.ManualResetEvent.WaitOne(0))
					{
						AutodiscoverWorker.UpdateRequestStateWithTimeout(autodiscoverRequestState);
					}
					if (!autodiscoverRequestState2.ManualResetEvent.WaitOne(0))
					{
						AutodiscoverWorker.UpdateRequestStateWithTimeout(autodiscoverRequestState2);
					}
				}
				if (autodiscoverRequestState.HasException)
				{
					result.AddExceptionToLog(autodiscoverRequestState.Result.Exception);
				}
				if (autodiscoverRequestState2.HasException)
				{
					result.AddExceptionToLog(autodiscoverRequestState2.Result.Exception);
				}
				if (autodiscoverRequestState.HasException && autodiscoverRequestState2.HasException)
				{
					result.Error = new AutodiscoverError(AutodiscoverStep.AnonymousAutodiscover, new Exception(string.Format("Both http and https anonymous autodiscover requests failed with errors ('{0}', '{1}')", text, text2)));
					result.Error.RequestHeaders = string.Format("Request headers for http/https calls respectively: '{0}','{1}'", autodiscoverRequestState.Result.RequestHeaders, autodiscoverRequestState2.Result.RequestHeaders);
					result.Error.ResponseHeaders = string.Format("Response headers for http/https calls respectively: '{0}','{1}'", autodiscoverRequestState.Result.ResponseHeaders, autodiscoverRequestState2.Result.ResponseHeaders);
					result.Error.ResponseBody = string.Format("Response body for http/https calls respectively: '{0}','{1}'", autodiscoverRequestState.Result.ResponseBody, autodiscoverRequestState2.Result.ResponseBody);
				}
				else
				{
					AutodiscoverRequestState autodiscoverRequestState3 = autodiscoverRequestState.HasException ? autodiscoverRequestState2 : autodiscoverRequestState;
					result.UnauthenticatedRedirects = autodiscoverRequestState3.Result.Redirects;
					if (string.IsNullOrEmpty(autodiscoverRequestState3.Result.AuthenticatedServerUri))
					{
						result.Error = new AutodiscoverError(AutodiscoverStep.AnonymousAutodiscover, new Exception(string.Format("Authenticated server uri is empty from call originating to {0}", autodiscoverRequestState3.Url)));
						result.Error.ResponseBody = autodiscoverRequestState3.Result.ResponseBody;
					}
					result.AuthenticatedLyncAutodiscoverServer = autodiscoverRequestState3.Result.AuthenticatedServerUri;
				}
			}
			catch (WebException ex)
			{
				AutodiscoverWorker.TraceHttpResponseHeaders("ExecuteAnonymousLyncAutodiscoverRequests", ex.Response);
				result.Error = new AutodiscoverError(AutodiscoverStep.AnonymousAutodiscover, ex, null, ex.Response);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006D04 File Offset: 0x00004F04
		internal static void ExecuteAnonymousLyncAutodiscoverRedirect(AnonymousAutodiscoverResult result, string requestUrl, int redirectCount)
		{
			result.Redirects.Add(requestUrl);
			if (redirectCount >= 10)
			{
				result.Exception = new Exception("The maximum number of redirects have been exceeded.");
				return;
			}
			HttpWebRequest httpWebRequest = AutodiscoverWorker.CreateWebRequest(requestUrl, null);
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				AutodiscoverWorker.TraceHttpRequestHeaders("ExecuteAnonymousLyncAutodiscoverRedirect", httpWebRequest);
				AutodiscoverWorker.TraceHttpResponseHeaders("ExecuteAnonymouseLyncAutodiscoverRedirect", httpWebResponse);
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse(result, httpWebResponse, redirectCount);
				}
				else
				{
					result.Exception = new Exception(string.Format("[ExecuteAnonymousLyncAutodiscoverRedirect] HttpWebResponse returned status code: {0}", httpWebResponse.StatusCode));
					result.RequestHeaders = httpWebRequest.GetRequestHeadersAsString();
					result.ResponseHeaders = httpWebResponse.GetResponseHeadersAsString();
					result.ResponseBody = httpWebResponse.GetResponseBodyAsString();
				}
			}
			catch (WebException ex)
			{
				AutodiscoverWorker.TraceHttpRequestHeaders("ExecuteAnonymousLyncAutodiscoverRedirect-WebException", httpWebRequest);
				AutodiscoverWorker.TraceHttpResponseHeaders("ExecuteAnonymouseLyncAutodiscoverRedirect-WebException", ex.Response);
				result.Exception = ex;
				result.RequestHeaders = httpWebRequest.GetRequestHeadersAsString();
				result.ResponseHeaders = ex.Response.GetResponseHeadersAsString();
				result.ResponseBody = ex.Response.GetResponseBodyAsString();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006E20 File Offset: 0x00005020
		internal static void ProcessLyncAnonymousAutodiscoverResponse(IAsyncResult asyncResult)
		{
			AutodiscoverRequestState autodiscoverRequestState = (AutodiscoverRequestState)asyncResult.AsyncState;
			HttpWebRequest request = autodiscoverRequestState.Request;
			if (request != null)
			{
				try
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse)request.EndGetResponse(asyncResult);
					AutodiscoverWorker.TraceHttpRequestHeaders("ProcessLyncAnonymousAutodiscoverResponse", request);
					AutodiscoverWorker.TraceHttpResponseHeaders("ProcessLyncAnonymousAutodiscoverResponse", httpWebResponse);
					if (httpWebResponse.StatusCode == HttpStatusCode.OK)
					{
						AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse(autodiscoverRequestState.Result, httpWebResponse, 0);
					}
					else
					{
						autodiscoverRequestState.Result.Exception = new Exception(string.Format("[ProcessLyncAnonymousAutodiscoverResponse] HttpWebResponse returned status code: {0}", httpWebResponse.StatusCode));
						autodiscoverRequestState.Result.RequestHeaders = request.GetRequestHeadersAsString();
						autodiscoverRequestState.Result.ResponseHeaders = httpWebResponse.GetResponseHeadersAsString();
						autodiscoverRequestState.Result.ResponseBody = httpWebResponse.GetResponseBodyAsString();
					}
					goto IL_141;
				}
				catch (WebException ex)
				{
					autodiscoverRequestState.Result.Exception = ex;
					AutodiscoverWorker.TraceHttpRequestHeaders("ProcessLyncAnonymousAutodiscoverResponse-WebException", request);
					AutodiscoverWorker.TraceHttpResponseHeaders("ProcessLyncAnonymousAutodiscoverResponse-WebException", ex.Response);
					autodiscoverRequestState.Result.RequestHeaders = request.GetRequestHeadersAsString();
					autodiscoverRequestState.Result.ResponseHeaders = ex.Response.GetResponseHeadersAsString();
					autodiscoverRequestState.Result.ResponseBody = ex.Response.GetResponseBodyAsString();
					goto IL_141;
				}
			}
			autodiscoverRequestState.Result.Exception = new Exception(string.Format("The request to {0} was aborted", autodiscoverRequestState.Request.RequestUri));
			IL_141:
			autodiscoverRequestState.ManualResetEvent.Set();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006F8C File Offset: 0x0000518C
		internal static void GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse(AnonymousAutodiscoverResult result, HttpWebResponse response, int redirectCount)
		{
			try
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text = streamReader.ReadToEnd();
						ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.{0}] Response Body:{1}", "GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse", text);
						AutodiscoverResponse thisObject = DataContractTransformer.TransformResponse(text);
						string text2 = thisObject.RootRedirectUrl();
						if (!string.IsNullOrEmpty(text2))
						{
							AutodiscoverWorker.ExecuteAnonymousLyncAutodiscoverRedirect(result, text2, redirectCount + 1);
						}
						else
						{
							result.AuthenticatedServerUri = thisObject.RootOAuthToken();
							ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse] AuthenticatedServerUri: {0}", result.AuthenticatedServerUri);
							if (string.IsNullOrEmpty(result.AuthenticatedServerUri))
							{
								result.ResponseBody = text;
							}
						}
					}
				}
			}
			catch (SerializationException ex)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string>(0, 0L, "[AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpointFromHttpWebResponse] A serializationException occurred while parsing response body: {0}", ex.Message);
				result.Exception = ex;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000708C File Offset: 0x0000528C
		internal static void GetUcwaUrl(AutodiscoverResult result, string sipAddress, OAuthCredentials credentials)
		{
			result.UcwaDiscoveryUrl = string.Empty;
			if (string.IsNullOrEmpty(result.AuthenticatedLyncAutodiscoverServer))
			{
				result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, new ArgumentNullException("[GetUcwaUrl] result.AuthenticatedLyncAutodiscoverServer"));
				return;
			}
			if (AutodiscoverCache.ContainsUser(sipAddress))
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.GetUcwaUrl] Returning ucwa url from cache for user {0} is {1}", sipAddress, AutodiscoverCache.GetValueForUser(sipAddress));
				result.UcwaDiscoveryUrl = AutodiscoverCache.GetValueForUser(sipAddress);
				result.IsUcwaUrlFromCache = true;
				return;
			}
			HttpWebRequest httpWebRequest = AutodiscoverWorker.CreateWebRequest(result.AuthenticatedLyncAutodiscoverServer, credentials);
			try
			{
				result.AuthenticatedRedirects.Add(httpWebRequest.RequestUri.ToString());
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				AutodiscoverWorker.TraceHttpRequestHeaders("GetUcwaUrl", httpWebRequest);
				AutodiscoverWorker.TraceHttpResponseHeaders("GetUcwaUrl", httpWebResponse);
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					AutodiscoverWorker.GetUcwaUrlFromHttpWebResponse(result, httpWebResponse, credentials, 0);
				}
				else
				{
					result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, new Exception(string.Format("[GetUcwaUrl] HttpWebResponse returned status code: {0}", httpWebResponse.StatusCode)), httpWebRequest, httpWebResponse);
				}
			}
			catch (Exception ex)
			{
				result.AddExceptionToLog(ex);
				AutodiscoverWorker.TraceHttpRequestHeaders("GetUcwaUrl-Exception", httpWebRequest);
				WebException ex2 = ex as WebException;
				HttpWebResponse httpWebResponse2 = null;
				if (ex2 != null)
				{
					httpWebResponse2 = (ex2.Response as HttpWebResponse);
					if (httpWebResponse2 != null)
					{
						AutodiscoverWorker.TraceHttpResponseHeaders("GetUcwaUrl-WebException", httpWebResponse2);
					}
				}
				result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, ex, httpWebRequest, httpWebResponse2);
			}
			if (!result.HasError)
			{
				AutodiscoverCache.UpdateUser(sipAddress, result.UcwaDiscoveryUrl);
				return;
			}
			AutodiscoverCache.InvalidateUser(sipAddress);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007200 File Offset: 0x00005400
		internal static AutodiscoverResult GetUcwaUrlFromHttpWebResponse(AutodiscoverResult result, HttpWebResponse response, OAuthCredentials credentials, int redirectCount)
		{
			try
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text = streamReader.ReadToEnd();
						ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.{0}] Response Body:{1}", "GetUcwaUrlFromHttpWebResponse", text);
						AutodiscoverResponse autodiscoverResponse = DataContractTransformer.TransformResponse(text);
						string text2 = autodiscoverResponse.UserRedirectUrl();
						if (!string.IsNullOrEmpty(text2))
						{
							AutodiscoverWorker.ExecuteAuthenticatedLyncAutodiscoverRedirect(result, text2, credentials, redirectCount + 1);
						}
						else
						{
							result.UcwaDiscoveryUrl = ((autodiscoverResponse.AccessLocation == AccessLocation.Internal) ? autodiscoverResponse.UserInternalUcwaToken() : autodiscoverResponse.UserExternalUcwaToken());
							if (!result.IsUcwaSupported)
							{
								result.ResponseBody = text;
							}
						}
					}
				}
			}
			catch (SerializationException ex)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string>(0, 0L, "[AutodiscoverWorker.GetUcwaUrlFromHttpWebResponse] A serializationException occurred while parsing response body: {0}", ex.Message);
				result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, ex);
				result.AddExceptionToLog(ex);
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007304 File Offset: 0x00005504
		internal static void ExecuteAuthenticatedLyncAutodiscoverRedirect(AutodiscoverResult result, string requestUrl, OAuthCredentials credentials, int redirectCount)
		{
			result.AuthenticatedRedirects.Add(requestUrl);
			if (redirectCount >= 10)
			{
				result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, new Exception("The maximum number of redirects have been exceeded."));
				return;
			}
			HttpWebRequest httpWebRequest = AutodiscoverWorker.CreateWebRequest(requestUrl, credentials);
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				AutodiscoverWorker.TraceHttpRequestHeaders("ExecuteAuthenticatedLyncAutodiscoverRedirect", httpWebRequest);
				AutodiscoverWorker.TraceHttpResponseHeaders("ExecuteAuthenticatedLyncAutodiscoverRedirect", httpWebResponse);
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					AutodiscoverWorker.GetUcwaUrlFromHttpWebResponse(result, httpWebResponse, credentials, redirectCount);
				}
				else
				{
					result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, new Exception(string.Format("[ExecuteAuthenticatedLyncAutodiscoverRedirect] HttpWebResponse returned status code: {0}", httpWebResponse.StatusCode)), httpWebRequest, httpWebResponse);
				}
			}
			catch (WebException ex)
			{
				AutodiscoverWorker.TraceHttpRequestHeaders("ExecuteAuthenticatedLyncAutodiscoverRedirect-WebException", httpWebRequest);
				AutodiscoverWorker.TraceHttpResponseHeaders("ExecuteAuthenticatedLyncAutodiscoverRedirect-WebException", ex.Response);
				result.Error = new AutodiscoverError(AutodiscoverStep.AuthenticatedAutodiscover, ex, httpWebRequest, ex.Response);
				result.AddExceptionToLog(ex);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000073F0 File Offset: 0x000055F0
		internal static void GetAuthenticatedAutodiscoverEndpoint(AutodiscoverResult result, string sipAddress, string domain)
		{
			if (AutodiscoverCache.ContainsDomain(domain))
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpoint] Returning authenticated autodiscover endpoint from cache for domain {0} is {1}", domain, AutodiscoverCache.GetValueForDomain(domain));
				result.AuthenticatedLyncAutodiscoverServer = AutodiscoverCache.GetValueForDomain(domain);
				result.IsAuthdServerFromCache = true;
				return;
			}
			AutodiscoverWorker.ExecuteAnonymousLyncAutodiscoverRequests(result, true, domain, sipAddress);
			if (result.HasError)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation(0, 0L, "[AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpoint] Lync anonymous autodiscover calls to internal servers failed, using external servers.");
				result.AddExceptionToLog(result.Error.Exception);
				result.Error = null;
				AutodiscoverWorker.ExecuteAnonymousLyncAutodiscoverRequests(result, false, domain, sipAddress);
			}
			if (!result.HasError)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverWorker.GetAuthenticatedAutodiscoverEndpoint] Updating Lync autodiscover cache entry for domain '{0}' to point to '{1}'", domain, result.AuthenticatedLyncAutodiscoverServer);
				AutodiscoverCache.UpdateDomain(domain, result.AuthenticatedLyncAutodiscoverServer);
				return;
			}
			AutodiscoverCache.InvalidateDomain(sipAddress);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000074AC File Offset: 0x000056AC
		internal static bool ServerCertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			ExTraceGlobals.OnlineMeetingTracer.TraceDebug<SslPolicyErrors, string, string>(0L, "AutodiscoverWorker::ServerCertificateValidator. Certificate validation errors: '{0}'. SSL certificate Subject='{1}', Issuer='{2}'", sslPolicyErrors, certificate.Subject, certificate.Issuer);
			if (AutodiscoverWorker.OwaAllowInternalUntrustedCerts.Member)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceDebug<string, SslPolicyErrors>(0L, "AutodiscoverWorker::ServerCertificateValidator. Allowed SSL certificate {0} with error {1}", certificate.Subject, sslPolicyErrors);
				return true;
			}
			return sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007501 File Offset: 0x00005701
		private static void TraceHttpRequestHeaders(string methodName, WebRequest request)
		{
			if (request != null)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string, string>(0, 0L, "[AutodiscoverWorker.{0}] Request to {1} contains headers:{2}", methodName, request.RequestUri.ToString(), request.GetRequestHeadersAsString());
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000752A File Offset: 0x0000572A
		private static void TraceHttpResponseHeaders(string methodName, WebResponse response)
		{
			if (response != null)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string, string>(0, 0L, "[AutodiscoverWorker.{0}] Response from {1} contains headers:{2}", methodName, response.ResponseUri.ToString(), response.GetResponseHeadersAsString());
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007554 File Offset: 0x00005754
		private static HttpWebRequest CreateWebRequest(string url, OAuthCredentials credentials)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Accept = "application/vnd.microsoft.rtc.autodiscover+json;v=1";
			httpWebRequest.Timeout = AutodiscoverWorker.LyncAutodiscoverTimeout;
			httpWebRequest.UserAgent = AutodiscoverWorker.UserAgent;
			if (credentials != null)
			{
				if (credentials.ClientRequestId != null)
				{
					httpWebRequest.Headers.Add("client-request-id", credentials.ClientRequestId.Value.ToString());
					httpWebRequest.Headers.Add("return-client-request-id", bool.TrueString);
				}
				httpWebRequest.PreAuthenticate = true;
				httpWebRequest.Credentials = credentials;
				CertificateValidationManager.SetComponentId(httpWebRequest, "LyncAutodiscover");
			}
			else
			{
				CertificateValidationManager.SetComponentId(httpWebRequest, "LyncAutodiscover.NoSsl");
			}
			return httpWebRequest;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000760C File Offset: 0x0000580C
		private static void UpdateRequestStateWithTimeout(AutodiscoverRequestState requestState)
		{
			if (requestState == null)
			{
				return;
			}
			requestState.Result.Exception = new Exception(string.Format("The request to {0} has timed", requestState.Url));
			requestState.Result.RequestHeaders = requestState.Request.GetRequestHeadersAsString();
			requestState.Result.ResponseHeaders = requestState.Response.GetResponseHeadersAsString();
			requestState.Result.ResponseBody = requestState.Response.GetResponseBodyAsString();
			if (requestState.Request != null)
			{
				requestState.Request.Abort();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007694 File Offset: 0x00005894
		private static bool GetOWARegistryValue(string valueName, bool defaultValue)
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA", false))
				{
					object value = registryKey.GetValue(valueName);
					if (value == null || !(value is int))
					{
						result = defaultValue;
					}
					else
					{
						result = ((int)value != 0);
					}
				}
			}
			catch (SecurityException)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string, bool>(0L, "[AutodiscoverWorker::GetOWARegistryValue] Security exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			catch (UnauthorizedAccessException)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string, bool>(0L, "[AutodiscoverWorker::GetOWARegistryValue] Unauthorized exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000773C File Offset: 0x0000593C
		private static bool ServerCertificateValidatorIgnoreSslErrors(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			ExTraceGlobals.OnlineMeetingTracer.TraceDebug(0L, "AutodiscoverWorker::ServerCertificateValidatorIgnoreSslErrors. Certificate validation is being ignored");
			return true;
		}

		// Token: 0x0400014E RID: 334
		public const string LyncAutodiscoverUrlHttpFormat = "http://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}";

		// Token: 0x0400014F RID: 335
		public const string LyncAutodiscoverUrlHttpsFormat = "https://{0}.{1}/autodiscover/autodiscoverservice.svc/root{2}";

		// Token: 0x04000150 RID: 336
		public const string SipUriQueryStringFormat = "?sipuri={0}";

		// Token: 0x04000151 RID: 337
		public const string LyncAutodiscoverAcceptType = "application/vnd.microsoft.rtc.autodiscover+json;v=1";

		// Token: 0x04000152 RID: 338
		internal const string MSExchangeOWARegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04000153 RID: 339
		internal const string CertificateValidationComponentId = "LyncAutodiscover";

		// Token: 0x04000154 RID: 340
		internal const string CertificateValidationComponentIdNoSsl = "LyncAutodiscover.NoSsl";

		// Token: 0x04000155 RID: 341
		private const int DefaultTimeout = 10000;

		// Token: 0x04000156 RID: 342
		private const int MinTimeout = 3000;

		// Token: 0x04000157 RID: 343
		private const int MaxRedirects = 10;

		// Token: 0x04000158 RID: 344
		private const string UserAgentFormat = "Exchange/{0}/LyncAutodiscover";

		// Token: 0x04000159 RID: 345
		public static readonly LazyMember<bool> OwaAllowInternalUntrustedCerts = new LazyMember<bool>(() => AutodiscoverWorker.GetOWARegistryValue("AllowInternalUntrustedCerts", true));
	}
}

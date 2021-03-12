using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x0200075C RID: 1884
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInWebClient : ILinkedInWebClient
	{
		// Token: 0x060024F6 RID: 9462 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		public LinkedInWebClient(LinkedInAppConfig config, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.appConfig = config;
			this.oauth = new LinkedInOAuth(tracer);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0004D0F9 File Offset: 0x0004B2F9
		public void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler)
		{
			this.downloadCompleted = (EventHandler<DownloadCompleteEventArgs>)Delegate.Remove(this.downloadCompleted, eventHandler);
			this.downloadCompleted = (EventHandler<DownloadCompleteEventArgs>)Delegate.Combine(this.downloadCompleted, eventHandler);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0004D12C File Offset: 0x0004B32C
		public void Abort(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			LinkedInWebClient.GetOperationState getOperationState = (LinkedInWebClient.GetOperationState)lazyAsyncResult.AsyncObject;
			if (getOperationState.HttpWebRequest != null)
			{
				getOperationState.HttpWebRequest.Abort();
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0004D160 File Offset: 0x0004B360
		public LinkedInResponse AuthenticateApplication(string url, string authenticationHeader, TimeSpan requestTimeout, IWebProxy proxy)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContentLength = 0L;
			httpWebRequest.Timeout = (int)requestTimeout.TotalMilliseconds;
			httpWebRequest.Headers["Authorization"] = authenticationHeader;
			if (proxy != null)
			{
				httpWebRequest.Proxy = proxy;
			}
			LinkedInResponse result;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
				{
					string body = streamReader.ReadToEnd();
					result = new LinkedInResponse
					{
						Code = httpWebResponse.StatusCode,
						Body = body
					};
				}
			}
			return result;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0004D238 File Offset: 0x0004B438
		public LinkedInPerson GetProfile(string accessToken, string accessTokenSecret, string fields)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw new ArgumentNullException("accessToken");
			}
			if (string.IsNullOrEmpty(accessTokenSecret))
			{
				throw new ArgumentNullException("accessTokenSecret");
			}
			if (string.IsNullOrEmpty(fields))
			{
				throw new ArgumentNullException("fields");
			}
			string url = string.Format("{0}:({1})", this.appConfig.ProfileEndpoint, fields);
			string authorizationHeader = this.oauth.GetAuthorizationHeader(url, "GET", null, accessToken, accessTokenSecret, this.appConfig.AppId, this.appConfig.AppSecret);
			return (LinkedInPerson)this.GetOperation(url, authorizationHeader, this.appConfig.WebRequestTimeout, this.appConfig.WebProxy, new Func<HttpWebResponse, object>(this.GetObjectsFromResponse<LinkedInPerson>));
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0004D2FC File Offset: 0x0004B4FC
		public HttpStatusCode RemoveApplicationPermissions(string accessToken, string accessSecret)
		{
			string authorizationHeader = this.oauth.GetAuthorizationHeader(this.appConfig.RemoveAppEndpoint, "GET", null, accessToken, accessSecret, this.appConfig.AppId, this.appConfig.AppSecret);
			return (HttpStatusCode)this.GetOperation(this.appConfig.RemoveAppEndpoint, authorizationHeader, this.appConfig.WebRequestTimeout, this.appConfig.WebProxy, (HttpWebResponse httpWebResponse) => httpWebResponse.StatusCode);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0004D388 File Offset: 0x0004B588
		public IAsyncResult BeginGetLinkedInConnections(string url, string authorizationHeader, TimeSpan requestTimeout, IWebProxy proxy, AsyncCallback callback, object callbackState)
		{
			return this.BeginGetOperation(url, authorizationHeader, requestTimeout, proxy, callback, callbackState, new Func<HttpWebResponse, object>(this.GetObjectsFromResponse<LinkedInConnections>));
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0004D3B0 File Offset: 0x0004B5B0
		public LinkedInConnections EndGetLinkedInConnections(IAsyncResult ar)
		{
			return (LinkedInConnections)this.EndGetOperation(ar);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
		private object GetOperation(string url, string authorizationHeader, TimeSpan requestTimeout, IWebProxy proxy, Func<HttpWebResponse, object> responseProcessor)
		{
			IAsyncResult ar = this.BeginGetOperation(url, authorizationHeader, requestTimeout, proxy, null, null, responseProcessor);
			return this.EndGetOperation(ar);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0004D3E4 File Offset: 0x0004B5E4
		private IAsyncResult BeginGetOperation(string url, string authorizationHeader, TimeSpan requestTimeout, IWebProxy proxy, AsyncCallback callback, object callbackState, Func<HttpWebResponse, object> responseProcessor)
		{
			HttpWebRequest httpWebRequest = LinkedInWebClient.GetHttpWebRequest(url, authorizationHeader, requestTimeout, proxy);
			LinkedInWebClient.GetOperationState worker = new LinkedInWebClient.GetOperationState(httpWebRequest, responseProcessor);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(worker, callbackState, callback);
			httpWebRequest.BeginGetResponse(new AsyncCallback(this.OnGetOperationsCompleted), lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0004D428 File Offset: 0x0004B628
		private void OnGetOperationsCompleted(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult.AsyncState;
			LinkedInWebClient.GetOperationState getOperationState = (LinkedInWebClient.GetOperationState)lazyAsyncResult.AsyncObject;
			object value = null;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)getOperationState.HttpWebRequest.EndGetResponse(asyncResult))
				{
					Func<HttpWebResponse, object> responseProcessor = getOperationState.ResponseProcessor;
					if (responseProcessor != null)
					{
						value = responseProcessor(httpWebResponse);
					}
				}
			}
			catch (Exception ex)
			{
				value = ex;
			}
			lazyAsyncResult.InvokeCallback(value);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
		private object EndGetOperation(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar;
			lazyAsyncResult.InternalWaitForCompletion();
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException("EndGetOperation is called more than once for the same IAsyncResult object.");
			}
			lazyAsyncResult.EndCalled = true;
			Exception ex = lazyAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
			return lazyAsyncResult.Result;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0004D4FC File Offset: 0x0004B6FC
		private T GetObjectsFromResponse<T>(HttpWebResponse httpWebResponse) where T : class
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
			T result;
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			{
				if (this.downloadCompleted != null && httpWebResponse.ContentLength > 0L)
				{
					this.downloadCompleted(this, new DownloadCompleteEventArgs(httpWebResponse.ContentLength));
				}
				result = (dataContractJsonSerializer.ReadObject(responseStream) as T);
			}
			return result;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0004D578 File Offset: 0x0004B778
		private static HttpWebRequest GetHttpWebRequest(string url, string authorizationHeader, TimeSpan requestTimeout, IWebProxy proxy)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "GET";
			httpWebRequest.Timeout = (int)requestTimeout.TotalMilliseconds;
			httpWebRequest.Headers["x-li-format"] = "json";
			httpWebRequest.Headers["Authorization"] = authorizationHeader;
			if (proxy != null)
			{
				httpWebRequest.Proxy = proxy;
			}
			return httpWebRequest;
		}

		// Token: 0x0400227F RID: 8831
		private EventHandler<DownloadCompleteEventArgs> downloadCompleted;

		// Token: 0x04002280 RID: 8832
		private readonly LinkedInAppConfig appConfig;

		// Token: 0x04002281 RID: 8833
		private readonly LinkedInOAuth oauth;

		// Token: 0x0200075D RID: 1885
		private class GetOperationState
		{
			// Token: 0x06002505 RID: 9477 RVA: 0x0004D5DB File Offset: 0x0004B7DB
			public GetOperationState(HttpWebRequest request, Func<HttpWebResponse, object> responseProcessor)
			{
				this.HttpWebRequest = request;
				this.ResponseProcessor = responseProcessor;
			}

			// Token: 0x170009BC RID: 2492
			// (get) Token: 0x06002506 RID: 9478 RVA: 0x0004D5F1 File Offset: 0x0004B7F1
			// (set) Token: 0x06002507 RID: 9479 RVA: 0x0004D5F9 File Offset: 0x0004B7F9
			public HttpWebRequest HttpWebRequest { get; private set; }

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06002508 RID: 9480 RVA: 0x0004D602 File Offset: 0x0004B802
			// (set) Token: 0x06002509 RID: 9481 RVA: 0x0004D60A File Offset: 0x0004B80A
			public Func<HttpWebResponse, object> ResponseProcessor { get; private set; }
		}
	}
}

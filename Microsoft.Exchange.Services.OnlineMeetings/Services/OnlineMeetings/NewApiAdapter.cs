using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000018 RID: 24
	internal class NewApiAdapter : IApiAdapter
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002930 File Offset: 0x00000B30
		public NewApiAdapter(UcwaRequestFactory requestFactory, ResourceJsonSerializer transformer, string ucwaDiscoveryUrl, string endpointId, CultureInfo culture)
		{
			this.discoveryUri = new Uri(ucwaDiscoveryUrl, UriKind.Absolute);
			this.baseUri = new Uri(this.discoveryUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped), UriKind.Absolute);
			this.requestFactory = requestFactory;
			this.transformer = transformer;
			this.applicationSettings = new ApplicationSettings(null);
			this.applicationSettings.EndpointId = endpointId;
			this.applicationSettings.UserAgent = "UCWA Online Meeting Scheduler Library";
			this.applicationSettings.ApplicationType = ApplicationType.Browser;
			if (culture == null)
			{
				this.applicationSettings.Culture = CultureInfo.CurrentCulture.ToString();
				return;
			}
			this.applicationSettings.Culture = culture.ToString();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A14 File Offset: 0x00000C14
		public Task<Uri> FindTokenAsync(string token)
		{
			Task<Uri> result;
			lock (this.tokensLock)
			{
				if (this.rootTokens == null)
				{
					if (this.getTokensTask == null || this.getTokensTask.IsFaulted)
					{
						this.getTokensTask = this.CreateApplicationAsync().ContinueWith<Dictionary<string, string>>((Task<ApplicationResource> buildTask) => this.BuildTokenDict(buildTask));
					}
					Task<Uri> task = this.getTokensTask.ContinueWith<Uri>((Task<Dictionary<string, string>> uriTask) => NewApiAdapter.GetUriFromToken(uriTask, token));
					result = task;
				}
				else
				{
					string uriString = null;
					Uri uri = null;
					if (this.rootTokens.TryGetValue(token.ToLowerInvariant(), out uriString))
					{
						uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
					}
					result = Task<Uri>.Factory.StartNew(() => uri);
				}
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B30 File Offset: 0x00000D30
		public Task<TResponse> SendRequestToTokenAsync<TResponse>(string token, string method, object request = null) where TResponse : class
		{
			return this.SendRequestToTokenAsyncCore<TResponse>(token, method, request);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B3B File Offset: 0x00000D3B
		public Task<TResponse> SendRequestAsync<TResponse>(Uri uri, string method, object request) where TResponse : class
		{
			return this.SendRequestAsyncCore<TResponse>(ref uri, method, request);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B47 File Offset: 0x00000D47
		internal Task<ApplicationResource> CreateApplicationAsync()
		{
			return this.SendRequestAsync<ApplicationResource>(this.discoveryUri, "POST", this.applicationSettings);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B60 File Offset: 0x00000D60
		private static Uri GetUriFromToken(Task<Dictionary<string, string>> tokensTask, string token)
		{
			Dictionary<string, string> result = tokensTask.Result;
			string uriString = null;
			Uri result2 = null;
			if (result.TryGetValue(token.ToLowerInvariant(), out uriString))
			{
				result2 = new Uri(uriString, UriKind.RelativeOrAbsolute);
			}
			return result2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BB8 File Offset: 0x00000DB8
		private Task<TResponse> SendRequestToTokenAsyncCore<TResponse>(string token, string method, object request) where TResponse : class
		{
			return this.FindTokenAsync(token).ContinueWith<Task<TResponse>>((Task<Uri> task) => this.SendRequestAsync<TResponse>(task.Result, method, request)).Unwrap<TResponse>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private Task<TResponse> SendRequestAsyncCore<TResponse>(ref Uri uri, string method, object request) where TResponse : class
		{
			if (!uri.IsAbsoluteUri)
			{
				uri = this.BuildUrl(uri.ToString());
			}
			UcwaWebRequest ucwaRequest = this.requestFactory.CreateRequest(method, uri.ToString());
			ucwaRequest.AcceptType = this.transformer.GetResponseContentType(typeof(Resource));
			Task<WebResponse> task2;
			if (request != null)
			{
				ucwaRequest.RequestContentType = this.transformer.GetRequestContentType(request.GetType());
				MemoryStream stream = new MemoryStream();
				MemoryStream requestStream = this.transformer.Serialize(request as Resource, stream);
				IEtagProvider etagProvider = request as IEtagProvider;
				if (etagProvider != null)
				{
					string etag = etagProvider.ETag;
					if (!string.IsNullOrEmpty(etag))
					{
						ucwaRequest.Headers.Add("If-Match", "\"" + etag + "\"");
					}
				}
				Task<Stream> requestStreamAsync = ucwaRequest.GetRequestStreamAsync();
				Task task3 = requestStreamAsync.ContinueWith<Task>((Task<Stream> streamTask) => Task.Factory.FromAsync<byte[], int, int>(new Func<byte[], int, int, AsyncCallback, object, IAsyncResult>(streamTask.Result.BeginWrite), new Action<IAsyncResult>(streamTask.Result.EndWrite), requestStream.GetBuffer(), 0, (int)requestStream.Length, new object(), TaskCreationOptions.None));
				task2 = task3.ContinueWith<Task<WebResponse>>(delegate(Task writeTask)
				{
					if (writeTask.IsFaulted)
					{
						throw writeTask.Exception.AsHttpOperationException() ?? writeTask.Exception.AsOperationFailureException("Failed to send a request to the server");
					}
					return ucwaRequest.GetResponseAsync();
				}).Unwrap<WebResponse>();
			}
			else
			{
				task2 = ucwaRequest.GetResponseAsync();
			}
			return task2.ContinueWith<TResponse>((Task<WebResponse> task) => this.TransformResponse<TResponse>(task, method));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E34 File Offset: 0x00001034
		private Dictionary<string, string> BuildTokenDict(Task<ApplicationResource> appResource)
		{
			lock (this.tokensLock)
			{
				if (this.rootTokens != null)
				{
					return this.rootTokens;
				}
				this.rootTokens = new Dictionary<string, string>();
				ApplicationResource result = appResource.Result;
				if (result.MyMeetings != null)
				{
					foreach (Link link in result.MyMeetings.Links)
					{
						this.rootTokens.Add(link.Relationship.ToLowerInvariant(), link.Href);
					}
				}
			}
			return this.rootTokens;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F00 File Offset: 0x00001100
		private TResponse TransformResponse<TResponse>(Task<WebResponse> task, string method) where TResponse : class
		{
			if (task.IsFaulted)
			{
				Exception exception = task.Exception;
				Exception baseException = exception.GetBaseException();
				Exception ex = baseException.AsHttpOperationException() ?? baseException.AsOperationFailureException("Failed to send a request to the server");
				throw ex;
			}
			TResponse result;
			using (Stream responseStream = task.Result.GetResponseStream())
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[OnlineMeetings][NewApiAdapter.TransformResponse] Response Headers:{0}", task.Result.GetResponseHeadersAsString());
				if (!UcwaHttpMethod.IsDeleteMethod(method))
				{
					TResponse tresponse = (TResponse)((object)this.transformer.Deserialize(typeof(TResponse), responseStream));
					string text = task.Result.Headers["ETag"];
					if (!string.IsNullOrEmpty(text))
					{
						IEtagProvider etagProvider = tresponse as IEtagProvider;
						if (etagProvider != null)
						{
							etagProvider.ETag = text.Trim(new char[]
							{
								'"'
							});
						}
					}
					result = tresponse;
				}
				else
				{
					result = default(TResponse);
				}
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000300C File Offset: 0x0000120C
		private Uri BuildUrl(string relativeUrl)
		{
			relativeUrl = relativeUrl.TrimStart(new char[]
			{
				'/'
			});
			return new Uri(this.baseUri, relativeUrl);
		}

		// Token: 0x040000C5 RID: 197
		private readonly ResourceJsonSerializer transformer;

		// Token: 0x040000C6 RID: 198
		private readonly UcwaRequestFactory requestFactory;

		// Token: 0x040000C7 RID: 199
		private readonly Uri discoveryUri;

		// Token: 0x040000C8 RID: 200
		private readonly object tokensLock = new object();

		// Token: 0x040000C9 RID: 201
		private readonly ApplicationSettings applicationSettings;

		// Token: 0x040000CA RID: 202
		private readonly Uri baseUri;

		// Token: 0x040000CB RID: 203
		private Task<Dictionary<string, string>> getTokensTask;

		// Token: 0x040000CC RID: 204
		private Dictionary<string, string> rootTokens;
	}
}

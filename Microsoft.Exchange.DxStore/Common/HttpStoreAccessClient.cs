using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.DxStore;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200007C RID: 124
	internal class HttpStoreAccessClient : IDxStoreAccessClient
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x000109EC File Offset: 0x0000EBEC
		public HttpStoreAccessClient(string self, HttpClient.TargetInfo targetInfo, int timeoutInMsec)
		{
			this.TargetInfo = targetInfo;
			this.Self = self;
			this.TimeoutInMsec = timeoutInMsec;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00010A09 File Offset: 0x0000EC09
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00010A11 File Offset: 0x0000EC11
		public HttpClient.TargetInfo TargetInfo { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00010A1A File Offset: 0x0000EC1A
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00010A22 File Offset: 0x0000EC22
		public int TimeoutInMsec { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00010A2B File Offset: 0x0000EC2B
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00010A33 File Offset: 0x0000EC33
		private string Self { get; set; }

		// Token: 0x060004EA RID: 1258 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public void SetTimeout(TimeSpan timeout)
		{
			this.TimeoutInMsec = (int)timeout.TotalMilliseconds;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public DxStoreAccessReply.CheckKey CheckKey(DxStoreAccessRequest.CheckKey request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.CheckKey, DxStoreAccessReply.CheckKey>(request, timeout);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00010A56 File Offset: 0x0000EC56
		public DxStoreAccessReply.DeleteKey DeleteKey(DxStoreAccessRequest.DeleteKey request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.DeleteKey, DxStoreAccessReply.DeleteKey>(request, timeout);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00010A60 File Offset: 0x0000EC60
		public DxStoreAccessReply.SetProperty SetProperty(DxStoreAccessRequest.SetProperty request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.SetProperty, DxStoreAccessReply.SetProperty>(request, timeout);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00010A6A File Offset: 0x0000EC6A
		public DxStoreAccessReply.DeleteProperty DeleteProperty(DxStoreAccessRequest.DeleteProperty request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.DeleteProperty, DxStoreAccessReply.DeleteProperty>(request, timeout);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00010A74 File Offset: 0x0000EC74
		public DxStoreAccessReply.ExecuteBatch ExecuteBatch(DxStoreAccessRequest.ExecuteBatch request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.ExecuteBatch, DxStoreAccessReply.ExecuteBatch>(request, timeout);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00010A7E File Offset: 0x0000EC7E
		public DxStoreAccessReply.GetProperty GetProperty(DxStoreAccessRequest.GetProperty request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.GetProperty, DxStoreAccessReply.GetProperty>(request, timeout);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00010A88 File Offset: 0x0000EC88
		public DxStoreAccessReply.GetAllProperties GetAllProperties(DxStoreAccessRequest.GetAllProperties request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.GetAllProperties, DxStoreAccessReply.GetAllProperties>(request, timeout);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00010A92 File Offset: 0x0000EC92
		public DxStoreAccessReply.GetPropertyNames GetPropertyNames(DxStoreAccessRequest.GetPropertyNames request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.GetPropertyNames, DxStoreAccessReply.GetPropertyNames>(request, timeout);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public DxStoreAccessReply.GetSubkeyNames GetSubkeyNames(DxStoreAccessRequest.GetSubkeyNames request, TimeSpan? timeout = null)
		{
			return this.Execute<DxStoreAccessRequest.GetSubkeyNames, DxStoreAccessReply.GetSubkeyNames>(request, timeout);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		private TReply Execute<TRequest, TReply>(TRequest request, TimeSpan? timeout = null) where TRequest : DxStoreRequestBase where TReply : DxStoreReplyBase
		{
			string text = null;
			TReply result;
			try
			{
				text = HttpConfiguration.FormClientUriPrefix(this.TargetInfo.TargetHost, this.TargetInfo.TargetNode, this.TargetInfo.GroupName);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
				if (timeout != null)
				{
					httpWebRequest.Timeout = (int)timeout.Value.TotalMilliseconds;
				}
				else
				{
					httpWebRequest.Timeout = this.TimeoutInMsec;
				}
				httpWebRequest.Method = "PUT";
				httpWebRequest.ContentType = "application/octet-stream";
				HttpRequest.DxStoreRequest msg = new HttpRequest.DxStoreRequest(this.Self, request);
				MemoryStream memoryStream = DxSerializationUtil.SerializeMessage(msg);
				httpWebRequest.ContentLength = memoryStream.Length;
				memoryStream.Position = 0L;
				Stream requestStream = httpWebRequest.GetRequestStream();
				using (requestStream)
				{
					memoryStream.CopyTo(requestStream);
				}
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						HttpReply httpReply = DxSerializationUtil.Deserialize<HttpReply>(responseStream);
						HttpReply.DxStoreReply dxStoreReply = httpReply as HttpReply.DxStoreReply;
						if (dxStoreReply != null)
						{
							TReply treply = dxStoreReply.Reply as TReply;
							if (treply == null)
							{
								throw new DxStoreAccessClientException(string.Format("Unexpected DxStoreReply {0}", dxStoreReply.Reply.GetType().FullName));
							}
							result = treply;
						}
						else
						{
							HttpReply.ExceptionReply exceptionReply = httpReply as HttpReply.ExceptionReply;
							if (exceptionReply != null)
							{
								Exception exception = exceptionReply.Exception;
								throw new DxStoreServerException(exception.Message, exception);
							}
							throw new DxStoreServerException(string.Format("unexpected reply: {0}", httpReply.GetType().FullName));
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.AccessClientTracer.TraceError<string, string>(0L, "HttpSend failed. Uri={0} Req={1} Ex={2}", text, request.GetType().FullName);
				if (ex is DxStoreAccessClientException || ex is DxStoreServerException)
				{
					throw;
				}
				throw new DxStoreAccessClientException(ex.Message, ex);
			}
			return result;
		}
	}
}

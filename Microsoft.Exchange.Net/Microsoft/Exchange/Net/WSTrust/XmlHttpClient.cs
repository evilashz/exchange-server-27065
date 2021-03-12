using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B79 RID: 2937
	internal sealed class XmlHttpClient
	{
		// Token: 0x06003F11 RID: 16145 RVA: 0x000A63E0 File Offset: 0x000A45E0
		public XmlHttpClient(Uri endpoint, WebProxy webProxy)
		{
			this.endpoint = endpoint;
			this.webProxy = webProxy;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000A63F8 File Offset: 0x000A45F8
		public XmlDocument Invoke(XmlDocument requestXmlDocument)
		{
			HttpWebRequest httpWebRequest = this.SendRequest(requestXmlDocument);
			if (httpWebRequest == null)
			{
				throw new HttpClientException(this.endpoint);
			}
			XmlDocument result;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					result = this.ReadXmlDocumentFromWebResponse(httpWebResponse);
				}
			}
			catch (WebException ex)
			{
				this.TraceResponseError(null, ex);
				throw new HttpClientException(this.endpoint, ex);
			}
			return result;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000A6470 File Offset: 0x000A4670
		public IAsyncResult BeginInvoke(XmlDocument xmlDocument, AsyncCallback callback, object state)
		{
			HttpWebRequest httpWebRequest = this.SendRequest(xmlDocument);
			CustomContextAsyncResult customContextAsyncResult = new CustomContextAsyncResult(callback, state, httpWebRequest);
			customContextAsyncResult.InnerAsyncResult = httpWebRequest.BeginGetResponse(new AsyncCallback(customContextAsyncResult.CustomCallback), customContextAsyncResult);
			return customContextAsyncResult;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000A64A8 File Offset: 0x000A46A8
		public XmlDocument EndInvoke(IAsyncResult asyncResult)
		{
			CustomContextAsyncResult customContextAsyncResult = (CustomContextAsyncResult)asyncResult;
			HttpWebRequest httpWebRequest = (HttpWebRequest)customContextAsyncResult.CustomState;
			XmlDocument result;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(customContextAsyncResult.InnerAsyncResult))
				{
					result = this.ReadXmlDocumentFromWebResponse(httpWebResponse);
				}
			}
			catch (WebException ex)
			{
				this.TraceResponseError(null, ex);
				throw new HttpClientException(this.endpoint, ex);
			}
			return result;
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000A6524 File Offset: 0x000A4724
		public void AbortInvoke(IAsyncResult asyncResult)
		{
			CustomContextAsyncResult customContextAsyncResult = (CustomContextAsyncResult)asyncResult;
			HttpWebRequest httpWebRequest = (HttpWebRequest)customContextAsyncResult.CustomState;
			httpWebRequest.Abort();
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000A654C File Offset: 0x000A474C
		private HttpWebRequest SendRequest(XmlDocument xmlDocument)
		{
			if (XmlHttpClient.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				XmlHttpClient.Tracer.TraceDebug<XmlHttpClient, string>((long)this.GetHashCode(), "{0}: Sending request: {1}", this, xmlDocument.OuterXml);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.endpoint);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/soap+xml; charset=utf-8";
			httpWebRequest.ServicePoint.Expect100Continue = false;
			if (this.webProxy != null)
			{
				httpWebRequest.Proxy = this.webProxy;
			}
			try
			{
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					xmlDocument.Save(requestStream);
				}
			}
			catch (IOException ex)
			{
				this.TraceRequestError(ex);
				throw new HttpClientException(this.endpoint, ex);
			}
			catch (WebException ex2)
			{
				this.TraceRequestError(ex2);
				throw new HttpClientException(this.endpoint, ex2);
			}
			catch (XmlException ex3)
			{
				this.TraceRequestError(ex3);
				throw new HttpClientException(this.endpoint, ex3);
			}
			return httpWebRequest;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000A6660 File Offset: 0x000A4860
		private XmlDocument ReadXmlDocumentFromWebResponse(HttpWebResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				this.TraceResponseError(response, null);
				throw new HttpClientException(this.endpoint);
			}
			XmlDocument result;
			try
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					XmlDocument xmlDocument = new SafeXmlDocument();
					xmlDocument.Load(responseStream);
					if (XmlHttpClient.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						XmlHttpClient.Tracer.TraceDebug<XmlHttpClient, string>((long)this.GetHashCode(), "{0}: Received response: {1}", this, xmlDocument.OuterXml);
					}
					result = xmlDocument;
				}
			}
			catch (ProtocolViolationException exception)
			{
				this.TraceResponseError(response, exception);
				throw new HttpClientException(this.endpoint);
			}
			catch (IOException exception2)
			{
				this.TraceResponseError(response, exception2);
				throw new HttpClientException(this.endpoint);
			}
			catch (XmlException exception3)
			{
				this.TraceResponseError(response, exception3);
				throw new HttpClientException(this.endpoint);
			}
			catch (WebException exception4)
			{
				this.TraceResponseError(response, exception4);
				throw new HttpClientException(this.endpoint);
			}
			return result;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000A6778 File Offset: 0x000A4978
		private void TraceRequestError(Exception exception)
		{
			XmlHttpClient.Tracer.TraceError<XmlHttpClient, Exception>((long)this.GetHashCode(), "{0}: Failed request with: {1}", this, exception);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000A6794 File Offset: 0x000A4994
		private void TraceResponseError(HttpWebResponse response, Exception exception)
		{
			if (XmlHttpClient.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (response != null)
				{
					stringBuilder.Append("StatusCode=");
					stringBuilder.Append(response.StatusCode);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append("Headers=");
					stringBuilder.Append(response.Headers);
					stringBuilder.Append(Environment.NewLine);
				}
				if (exception == null && response != null)
				{
					try
					{
						using (Stream responseStream = response.GetResponseStream())
						{
							using (StreamReader streamReader = new StreamReader(responseStream))
							{
								stringBuilder.Append("Content=");
								stringBuilder.Append(streamReader.ReadToEnd());
								stringBuilder.Append(Environment.NewLine);
							}
						}
					}
					catch (ProtocolViolationException ex)
					{
						exception = ex;
					}
					catch (IOException ex2)
					{
						exception = ex2;
					}
					catch (WebException ex3)
					{
						exception = ex3;
					}
				}
				if (exception != null)
				{
					stringBuilder.Append("Exception=");
					while (exception != null)
					{
						stringBuilder.Append(exception.ToString());
						if (exception.InnerException != null)
						{
							stringBuilder.Append("InnerException=");
						}
						exception = exception.InnerException;
						stringBuilder.Append(Environment.NewLine);
					}
				}
				XmlHttpClient.Tracer.TraceError<XmlHttpClient, StringBuilder>((long)this.GetHashCode(), "{0}: Failed response with: {1}", this, stringBuilder);
			}
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000A6914 File Offset: 0x000A4B14
		public override string ToString()
		{
			return "XmlHttpClient for " + this.endpoint.ToString();
		}

		// Token: 0x040036C3 RID: 14019
		private Uri endpoint;

		// Token: 0x040036C4 RID: 14020
		private WebProxy webProxy;

		// Token: 0x040036C5 RID: 14021
		private static readonly Trace Tracer = ExTraceGlobals.WSTrustTracer;
	}
}

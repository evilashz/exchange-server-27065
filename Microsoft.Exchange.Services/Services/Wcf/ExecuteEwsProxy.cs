using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000912 RID: 2322
	internal class ExecuteEwsProxy : ServiceCommand<IAsyncResult>
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x000E1A84 File Offset: 0x000DFC84
		static ExecuteEwsProxy()
		{
			CertificateValidationManager.RegisterCallback("EwsProxy", Global.RemoteCertificateValidationCallback);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000E1A9F File Offset: 0x000DFC9F
		public ExecuteEwsProxy(CallContext callContext, string body, string token, string extensionId, AsyncCallback asyncCallback, object asyncState) : base(callContext)
		{
			this.body = body;
			this.token = token;
			this.asyncCallback = asyncCallback;
			this.asyncState = asyncState;
			this.extensionId = extensionId;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000E1AD0 File Offset: 0x000DFCD0
		protected override IAsyncResult InternalExecute()
		{
			this.asyncResult = new ServiceAsyncResult<EwsProxyResponse>();
			this.asyncResult.AsyncState = this.asyncState;
			this.asyncResult.AsyncCallback = this.asyncCallback;
			try
			{
				string text = (base.CallContext.IsOwa && VariantConfiguration.InvariantNoFlightingSnapshot.Ews.UseInternalEwsUrlForExtensionEwsProxyInOwa.Enabled) ? EwsHelper.DiscoverEwsUrl(base.CallContext.AccessingPrincipal) : EwsHelper.DiscoverExternalEwsUrl(base.CallContext.AccessingPrincipal);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "[ExecuteEwsProxy::Execute] Original request url: {0}", text);
				Uri uri = new Uri(text);
				string text2 = string.Format("https://{0}/ews/exchange.asmx", uri.Host);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "[ExecuteEwsProxy::Execute] Ews url: {0}", text2);
				Uri requestUri = new Uri(text2);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
				httpWebRequest.Headers.Add("Authorization", string.Format("Bearer {0}", this.token));
				httpWebRequest.PreAuthenticate = true;
				httpWebRequest.AllowAutoRedirect = false;
				httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
				httpWebRequest.ServerCertificateValidationCallback = Global.RemoteCertificateValidationCallback;
				CertificateValidationManager.SetComponentId(httpWebRequest, "EwsProxy");
				GccUtils.CopyClientIPEndpointsForServerToServerProxy(HttpContext.Current, httpWebRequest);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "text/xml; charset=utf-8";
				httpWebRequest.UserAgent = "EWSProxy/MailApp/" + this.extensionId;
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					currentActivityScope.SerializeTo(httpWebRequest);
				}
				byte[] bytes = Encoding.UTF8.GetBytes(this.body);
				httpWebRequest.ContentLength = (long)bytes.Length;
				httpWebRequest.BeginGetRequestStream(new AsyncCallback(this.GetRequestStreamCallback), httpWebRequest);
			}
			catch (WebException ex)
			{
				this.asyncResult.Data = ExecuteEwsProxy.GetEwsProxyResponseFromWebException(ex);
				this.asyncResult.Complete(ex);
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, Exception>(0L, "[ExecuteEwsProxy::Execute] Exception occurred during EWS proxy for extension {0}: {1}", this.extensionId, ex2);
				this.asyncResult.Data = new EwsProxyResponse(ex2.Message);
				this.asyncResult.Complete(ex2);
			}
			return this.asyncResult;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000E1D20 File Offset: 0x000DFF20
		private static EwsProxyResponse GetEwsProxyResponseFromWebException(WebException webException)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<WebException>(0L, "ExecuteEwsProxy.GetEwsProxyResponseFromWebException: Exception occurred during EWS proxy: {0}", webException);
			string errorMessage = webException.Message;
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				string faultString = ExecuteEwsProxy.GetFaultString(httpWebResponse);
				if (!string.IsNullOrEmpty(faultString))
				{
					errorMessage = faultString;
				}
			}
			return new EwsProxyResponse(errorMessage);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000E1D6C File Offset: 0x000DFF6C
		private static string GetFaultString(HttpWebResponse httpWebResponse)
		{
			string result = null;
			string text = null;
			try
			{
				text = ExecuteEwsProxy.GetResponseBody(httpWebResponse);
			}
			finally
			{
				if (httpWebResponse != null)
				{
					((IDisposable)httpWebResponse).Dispose();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExecuteEwsProxy.GetFaultString: Response body is empty");
			}
			else
			{
				result = ExecuteEwsProxy.GetFaultString(text);
			}
			return result;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000E1DC8 File Offset: 0x000DFFC8
		private static string GetFaultString(string responseBody)
		{
			string result = null;
			XDocument xdocument = null;
			try
			{
				xdocument = XDocument.Load(new StringReader(responseBody));
			}
			catch (XmlException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ExecuteEwsProxy.GetFaultString: Response body is not xml, {0}", responseBody);
			}
			if (xdocument != null)
			{
				IEnumerable<XElement> source = xdocument.Descendants("faultstring");
				if (source.Count<XElement>() == 1)
				{
					result = source.First<XElement>().Value;
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExecuteEwsProxy.GetFaultString: faultString element is missing");
				}
			}
			return result;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000E1E4C File Offset: 0x000E004C
		private static string GetResponseBody(HttpWebResponse httpWebResponse)
		{
			string result = null;
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000E1EA8 File Offset: 0x000E00A8
		private static EwsProxyResponse GetEwsProxyResponseFromWebResponse(HttpWebResponse httpWebResponse)
		{
			EwsProxyResponse result;
			try
			{
				string responseBody = ExecuteEwsProxy.GetResponseBody(httpWebResponse);
				if (responseBody != null && responseBody.Length > ExecuteEwsProxy.MaxEwsResponseSize)
				{
					result = new EwsProxyResponse(CoreResources.EwsProxyResponseTooBig);
				}
				else
				{
					result = new EwsProxyResponse((int)httpWebResponse.StatusCode, httpWebResponse.StatusDescription, responseBody);
				}
			}
			finally
			{
				if (httpWebResponse != null)
				{
					((IDisposable)httpWebResponse).Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000E1F10 File Offset: 0x000E0110
		private void GetRequestStreamCallback(IAsyncResult result)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
			byte[] bytes = Encoding.UTF8.GetBytes(this.body);
			try
			{
				using (Stream stream = httpWebRequest.EndGetRequestStream(result))
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				httpWebRequest.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), httpWebRequest);
			}
			catch (WebException ex)
			{
				this.asyncResult.Data = new EwsProxyResponse(string.Format("Failed to make a request via HttpWebRequest. Exception: {0}", ex.Message));
				this.asyncResult.Complete(ex);
			}
			catch (IOException ex2)
			{
				this.asyncResult.Data = new EwsProxyResponse(string.Format("Failed to make a request via HttpWebRequest. Exception: {0}", ex2.Message));
				this.asyncResult.Complete(ex2);
			}
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000E1FF8 File Offset: 0x000E01F8
		private void GetResponseCallback(IAsyncResult result)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(result);
				this.asyncResult.Data = ExecuteEwsProxy.GetEwsProxyResponseFromWebResponse(httpWebResponse);
				this.asyncResult.Complete(null);
			}
			catch (WebException ex)
			{
				this.asyncResult.Data = ExecuteEwsProxy.GetEwsProxyResponseFromWebException(ex);
				this.asyncResult.Complete(ex);
			}
		}

		// Token: 0x0400272E RID: 10030
		private const string EwsUrlFormat = "https://{0}/ews/exchange.asmx";

		// Token: 0x0400272F RID: 10031
		private static readonly int MaxEwsResponseSize = 1000000;

		// Token: 0x04002730 RID: 10032
		private readonly string body;

		// Token: 0x04002731 RID: 10033
		private readonly string token;

		// Token: 0x04002732 RID: 10034
		private readonly string extensionId;

		// Token: 0x04002733 RID: 10035
		private readonly AsyncCallback asyncCallback;

		// Token: 0x04002734 RID: 10036
		private readonly object asyncState;

		// Token: 0x04002735 RID: 10037
		private ServiceAsyncResult<EwsProxyResponse> asyncResult;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000800 RID: 2048
	internal sealed class AutoDiscoverClient
	{
		// Token: 0x0600475D RID: 18269 RVA: 0x00125534 File Offset: 0x00123734
		public AutoDiscoverClient(string componentId, Task.TaskVerboseLoggingDelegate verbose, CredentialsImpersonator credentialsImpersonator, string emailAddress, string url, bool reportErrors, params string[] optionalHeaders)
		{
			this.verboseDelegate = verbose;
			this.credentialsImpersonator = credentialsImpersonator;
			this.url = url;
			this.emailAddress = emailAddress;
			this.reportErrors = reportErrors;
			this.componentId = componentId;
			if (optionalHeaders.Length % 2 != 0)
			{
				throw new ArgumentException("optionalHeaders");
			}
			this.additionalHeaders = new Dictionary<string, string>();
			for (int i = 0; i < optionalHeaders.Length; i += 2)
			{
				this.additionalHeaders.Add(optionalHeaders[i], optionalHeaders[i + 1]);
			}
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0012564E File Offset: 0x0012384E
		public AutoDiscoverResponseXML Invoke()
		{
			return this.ExecuteAndReportErrors<AutoDiscoverResponseXML>(delegate
			{
				AutoDiscoverResponseXML result = null;
				this.credentialsImpersonator.Impersonate(delegate(ICredentials credentials)
				{
					HttpWebRequest httpWebRequest = this.SendRequest(credentials);
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						result = this.GetResponse(response);
					}
				});
				return result;
			});
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00125718 File Offset: 0x00123918
		public IAsyncResult BeginInvoke(AsyncCallback asyncCallback)
		{
			return this.ExecuteAndReportErrors<IAsyncResult>(delegate
			{
				IAsyncResult result = null;
				this.credentialsImpersonator.Impersonate(delegate(ICredentials credentials)
				{
					this.request = this.SendRequest(credentials);
					result = this.request.BeginGetResponse(asyncCallback, this);
				});
				return result;
			});
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0012574C File Offset: 0x0012394C
		public static AutoDiscoverResponseXML EndInvoke(IAsyncResult asyncResult, out string url)
		{
			AutoDiscoverClient autoDiscoverClient = asyncResult.AsyncState as AutoDiscoverClient;
			return autoDiscoverClient.EndInvokeInternal(asyncResult, out url);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x001257C8 File Offset: 0x001239C8
		private AutoDiscoverResponseXML EndInvokeInternal(IAsyncResult asyncResult, out string url)
		{
			url = this.url;
			return this.ExecuteAndReportErrors<AutoDiscoverResponseXML>(delegate
			{
				AutoDiscoverResponseXML response;
				using (WebResponse webResponse = this.request.EndGetResponse(asyncResult))
				{
					response = this.GetResponse(webResponse);
				}
				return response;
			});
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x00125804 File Offset: 0x00123A04
		private T ExecuteAndReportErrors<T>(Func<T> func) where T : class
		{
			Exception ex = null;
			try
			{
				return func();
			}
			catch (WebException ex2)
			{
				ex = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				ex = ex3;
			}
			if (this.reportErrors)
			{
				while (ex != null)
				{
					this.TraceVerbose(Strings.TowsException(this.url, ex.Message));
					ex = ex.InnerException;
				}
			}
			return default(T);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0012587C File Offset: 0x00123A7C
		internal static void AddOutlook14Cookie(HttpWebRequest request)
		{
			if (request == null || request.RequestUri == null || string.IsNullOrEmpty(request.RequestUri.Host))
			{
				return;
			}
			request.CookieContainer = new CookieContainer();
			Cookie cookie = new Cookie("OutlookSession", "\"{" + Guid.NewGuid().ToString().ToUpper() + "}\"");
			cookie.Domain = request.RequestUri.Host;
			request.CookieContainer.Add(cookie);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x00125908 File Offset: 0x00123B08
		private HttpWebRequest SendRequest(ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.url);
			AutoDiscoverClient.AddOutlook14Cookie(httpWebRequest);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "text/xml; charset=utf-8";
			httpWebRequest.Credentials = credentials;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
			foreach (string text in this.additionalHeaders.Keys)
			{
				httpWebRequest.Headers.Add(text, this.additionalHeaders[text]);
			}
			httpWebRequest.UserAgent = string.Format("{0}/{1}/{2}", Environment.MachineName, this.componentId, this.emailAddress);
			CertificateValidationManager.SetComponentId(httpWebRequest, this.componentId);
			this.TraceHeaders(httpWebRequest.Headers);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(AutoDiscoverRequestXML));
				AutoDiscoverRequestXML o = AutoDiscoverRequestXML.NewRequest(this.emailAddress);
				safeXmlSerializer.Serialize(requestStream, o);
			}
			return httpWebRequest;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x00125A40 File Offset: 0x00123C40
		private AutoDiscoverResponseXML GetResponse(WebResponse response)
		{
			this.TraceHeaders(response.Headers);
			AutoDiscoverResponseXML result;
			using (Stream responseStream = response.GetResponseStream())
			{
				try
				{
					SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(AutoDiscoverResponseXML));
					AutoDiscoverResponseXML autoDiscoverResponseXML = safeXmlSerializer.Deserialize(responseStream) as AutoDiscoverResponseXML;
					if (this.url.StartsWith("http:", StringComparison.OrdinalIgnoreCase))
					{
						this.TraceVerbose(Strings.TowsDomainNotSsl(this.url));
					}
					result = autoDiscoverResponseXML;
				}
				catch (InvalidOperationException ex)
				{
					if (ex.InnerException == null || !(ex.InnerException is XmlException))
					{
						throw;
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x00125AF0 File Offset: 0x00123CF0
		private void TraceHeaders(WebHeaderCollection headers)
		{
			if (headers != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in headers.AllKeys)
				{
					stringBuilder.AppendFormat("{0}: {1}\n", text, headers[text]);
				}
				this.TraceVerbose(new LocalizedString(stringBuilder.ToString()));
			}
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00125B44 File Offset: 0x00123D44
		private void TraceVerbose(LocalizedString message)
		{
			if (this.verboseDelegate != null)
			{
				this.verboseDelegate(message);
			}
		}

		// Token: 0x04002B1D RID: 11037
		private readonly Task.TaskVerboseLoggingDelegate verboseDelegate;

		// Token: 0x04002B1E RID: 11038
		private CredentialsImpersonator credentialsImpersonator;

		// Token: 0x04002B1F RID: 11039
		private readonly string emailAddress;

		// Token: 0x04002B20 RID: 11040
		private readonly string url;

		// Token: 0x04002B21 RID: 11041
		private readonly bool reportErrors;

		// Token: 0x04002B22 RID: 11042
		private HttpWebRequest request;

		// Token: 0x04002B23 RID: 11043
		private readonly string componentId;

		// Token: 0x04002B24 RID: 11044
		private Dictionary<string, string> additionalHeaders;
	}
}

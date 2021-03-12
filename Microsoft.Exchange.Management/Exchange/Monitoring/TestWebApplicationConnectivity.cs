using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005CC RID: 1484
	public abstract class TestWebApplicationConnectivity : TestVirtualDirectoryConnectivity
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x000D2E3D File Offset: 0x000D103D
		internal TestWebApplicationConnectivity(LocalizedString applicationName, LocalizedString applicationShortName, TransientErrorCache transientErrorCache, string monitoringEventSourceInternal, string monitoringEventSourceExternal) : base(applicationName, applicationShortName, transientErrorCache, monitoringEventSourceInternal, monitoringEventSourceExternal)
		{
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000D2E5C File Offset: 0x000D105C
		// (set) Token: 0x06003418 RID: 13336 RVA: 0x000D2E7D File Offset: 0x000D107D
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string RSTEndpoint
		{
			get
			{
				if (!(this.liveRSTEndpoint != null))
				{
					return string.Empty;
				}
				return this.liveRSTEndpoint.AbsoluteUri;
			}
			set
			{
				this.liveRSTEndpoint = new Uri(value);
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000D2E8C File Offset: 0x000D108C
		protected override List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			TaskLogger.LogEnter();
			instance.liveRSTEndpointUri = this.liveRSTEndpoint;
			try
			{
				base.WriteVerbose(Strings.CasHealthWebAppStartTest(instance.baseUri));
				WebApplication webApplication = this.GetWebApplication(instance);
				if (webApplication != null)
				{
					this.ExecuteWebApplicationTests(instance, webApplication);
				}
				else
				{
					instance.Result.Complete();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return null;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000D2EF4 File Offset: 0x000D10F4
		private WebApplication GetWebApplication(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			CasTransactionOutcome casTransactionOutcome = base.CreateLogonOutcome(instance);
			WebApplication result = null;
			try
			{
				WebApplication webApplication = this.CreateWebApplication(instance);
				if (webApplication.ValidateLogin())
				{
					casTransactionOutcome.Update(CasTransactionResultEnum.Success, LocalizedString.Empty);
					result = webApplication;
				}
				else
				{
					casTransactionOutcome.Update(CasTransactionResultEnum.Failure, Strings.CasHealthOwaNoLogonCookieReturned);
				}
			}
			catch (AuthenticationException ex)
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, Strings.CasHealthWebAppNoSession(instance.CasFqdn, ex.LocalizedString, (ex.InnerException != null) ? ex.GetBaseException().Message : ""));
			}
			catch (WebException ex2)
			{
				casTransactionOutcome.Update(CasTransactionResultEnum.Failure, Strings.CasHealthWebAppNoSession(instance.CasFqdn, ex2.Message, (ex2.InnerException != null) ? ex2.GetBaseException().Message : ""));
			}
			instance.Outcomes.Enqueue(casTransactionOutcome);
			instance.Result.Outcomes.Add(casTransactionOutcome);
			return result;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000D2FFC File Offset: 0x000D11FC
		protected virtual WebApplication CreateWebApplication(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			WebSession webSession = this.GetWebSession(instance);
			return this.CreateWebApplication(instance.baseUri.AbsolutePath, webSession);
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000D3140 File Offset: 0x000D1340
		private WebSession GetWebSession(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			WebSession webSession = this.CreateWebSession(instance);
			if (instance.UrlType == VirtualDirectoryUriScope.Internal)
			{
				webSession.TrustAnySSLCertificate = true;
				base.WriteVerbose(Strings.CasHealthOwaInternalTrustCertificate);
			}
			else if (instance.trustAllCertificates)
			{
				webSession.TrustAnySSLCertificate = true;
				base.WriteVerbose(Strings.CasHealthOwaTrustAnyCertificate);
			}
			webSession.SendingRequest += delegate(object sender, HttpWebRequestEventArgs e)
			{
				LocalizedString localizedString = Strings.CasHealthWebAppSendingRequest(e.Request.RequestUri);
				instance.Outcomes.Enqueue(localizedString);
			};
			webSession.ResponseReceived += delegate(object sender, HttpWebResponseEventArgs e)
			{
				if (e.Response != null)
				{
					string responseHeader = e.Response.GetResponseHeader("X-DiagInfo");
					LocalizedString localizedString = Strings.CasHealthWebAppResponseReceived(e.Response.ResponseUri, e.Response.StatusCode, responseHeader ?? string.Empty, TestWebApplicationConnectivity.GetResponseAdditionalInformation(e.Response));
					instance.Outcomes.Enqueue(localizedString);
				}
			};
			webSession.RequestException += delegate(object sender, WebExceptionEventArgs e)
			{
				if (e.Response != null)
				{
					string responseHeader = e.Response.GetResponseHeader("X-DiagInfo");
					LocalizedString localizedString = Strings.CasHealthWebAppRequestException(e.Request.RequestUri, e.Exception.Status, responseHeader ?? string.Empty, e.Exception.Message);
					instance.Outcomes.Enqueue(localizedString);
				}
			};
			webSession.Initialize();
			return webSession;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000D31E8 File Offset: 0x000D13E8
		private static string GetResponseAdditionalInformation(HttpWebResponse response)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (response.Cookies != null && response.Cookies.Count > 0)
			{
				stringBuilder.Append("Cookie=");
				foreach (object obj in response.Cookies)
				{
					Cookie cookie = (Cookie)obj;
					stringBuilder.Append(cookie.ToString());
					stringBuilder.Append("; ");
				}
			}
			if (response.StatusCode == HttpStatusCode.Found)
			{
				stringBuilder.Append("Location=");
				stringBuilder.Append(response.Headers[HttpResponseHeader.Location]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000D32B0 File Offset: 0x000D14B0
		protected virtual WebSession CreateWebSession(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return ExchangeWebApplication.GetWebSession(instance);
		}

		// Token: 0x0600341F RID: 13343
		protected abstract WebApplication CreateWebApplication(string virtualDirectory, WebSession webSession);

		// Token: 0x06003420 RID: 13344 RVA: 0x000D32B8 File Offset: 0x000D14B8
		protected virtual void ExecuteWebApplicationTests(TestCasConnectivity.TestCasConnectivityRunInstance instance, WebApplication webApplication)
		{
			instance.Result.Complete();
		}

		// Token: 0x04002417 RID: 9239
		protected const string DiagInfo = "X-DiagInfo";

		// Token: 0x04002418 RID: 9240
		protected Uri liveRSTEndpoint = new Uri("https://login.live.com/RST.srf");
	}
}

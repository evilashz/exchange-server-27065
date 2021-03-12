using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Mime;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005CD RID: 1485
	[Cmdlet("Test", "EcpConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestEcpConnectivity : TestWebApplicationConnectivity
	{
		// Token: 0x06003421 RID: 13345 RVA: 0x000D32C5 File Offset: 0x000D14C5
		public TestEcpConnectivity() : base(Strings.CasHealthEcpLongName, Strings.CasHealthEcpShortName, TransientErrorCache.EcpInternalTransientCache, "MSExchange Monitoring ECPConnectivity Internal", "MSExchange Monitoring ECPConnectivity External")
		{
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000D32E6 File Offset: 0x000D14E6
		internal override TransientErrorCache GetTransientErrorCache()
		{
			if (!base.MonitoringContext)
			{
				return null;
			}
			if (base.TestType != OwaConnectivityTestType.Internal)
			{
				return TransientErrorCache.EcpExternalTransientCache;
			}
			return TransientErrorCache.EcpInternalTransientCache;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000D330A File Offset: 0x000D150A
		protected override uint GetDefaultTimeOut()
		{
			if (base.TestType != OwaConnectivityTestType.Internal)
			{
				return 120U;
			}
			return 90U;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000D3319 File Offset: 0x000D1519
		protected override IEnumerable<ExchangeVirtualDirectory> GetVirtualDirectories(ADObjectId serverId, QueryFilter filter)
		{
			return base.GetVirtualDirectories<ADEcpVirtualDirectory>(serverId, filter);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000D3323 File Offset: 0x000D1523
		protected override WebApplication CreateWebApplication(string virtualDirectory, WebSession webSession)
		{
			return new ExchangeControlPanelApplication(virtualDirectory, webSession);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000D332C File Offset: 0x000D152C
		protected override void ExecuteWebApplicationTests(TestCasConnectivity.TestCasConnectivityRunInstance instance, WebApplication webApplication)
		{
			this.ExecuteWebServiceTest(instance, webApplication);
			base.ExecuteWebApplicationTests(instance, webApplication);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000D3340 File Offset: 0x000D1540
		private void ExecuteWebServiceTest(TestCasConnectivity.TestCasConnectivityRunInstance instance, WebApplication webApplication)
		{
			StringBody stringBody = new StringBody("{\"filter\":{\"SearchText\":\"\"},\"sort\":{\"Direction\":0,\"PropertyName\":\"Name\"}}");
			stringBody.ContentType = new System.Net.Mime.ContentType("application/json");
			string text = "RulesEditor/InboxRules.svc/GetList";
			string relativeUrl = "default.aspx";
			CasTransactionOutcome casTransactionOutcome = this.BuildOutcome(Strings.CasHealthEcpScenarioTestWebService, Strings.CasHealthEcpTestWebService(new Uri(webApplication.BaseUri, text).ToString()), "ECP Web Sevice Logon Latency", instance);
			bool flag = false;
			string additionalInformation = "";
			try
			{
				TextResponse textResponse = webApplication.Get<TextResponse>(relativeUrl);
				flag = (textResponse.StatusCode == HttpStatusCode.OK);
				additionalInformation = (flag ? "" : Strings.CasHealthEcpServiceRequestResult(textResponse.StatusCode.ToString()));
				base.WriteVerbose(Strings.CasHealthEcpServiceResponse(textResponse.Text));
				if (flag)
				{
					textResponse = webApplication.Post<TextResponse>(text, stringBody);
					flag = (textResponse.StatusCode == HttpStatusCode.OK);
					additionalInformation = (flag ? "" : Strings.CasHealthEcpServiceRequestResult(textResponse.StatusCode.ToString()));
					base.WriteVerbose(Strings.CasHealthEcpServiceResponse(textResponse.Text));
				}
			}
			catch (WebException ex)
			{
				string casServer = string.Empty;
				string fullResponse = string.Empty;
				if (ex.Response != null)
				{
					casServer = ex.Response.Headers["X-DiagInfo"];
					try
					{
						fullResponse = this.GetResponseHtml(ex.Response);
					}
					catch (Exception ex2)
					{
						if (!(ex2 is ProtocolViolationException) && !(ex2 is IOException) && !(ex2 is NotSupportedException))
						{
							throw;
						}
					}
				}
				additionalInformation = Strings.CasHealthEcpServiceRequestException(ex.Message, casServer, fullResponse);
			}
			casTransactionOutcome.Update(flag ? CasTransactionResultEnum.Success : CasTransactionResultEnum.Failure, additionalInformation);
			instance.Outcomes.Enqueue(casTransactionOutcome);
			instance.Result.Outcomes.Add(casTransactionOutcome);
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000D3524 File Offset: 0x000D1724
		protected override WebSession CreateWebSession(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExchangeWebAppVirtualDirectory exchangeWebAppVirtualDirectory = (ExchangeWebAppVirtualDirectory)instance.VirtualDirectory;
			if (exchangeWebAppVirtualDirectory.LiveIdAuthentication && instance.UrlType == VirtualDirectoryUriScope.Internal)
			{
				return new WindowsLiveIdWebSession(TestCasConnectivity.GetUrlWithTrailingSlash(exchangeWebAppVirtualDirectory.ExternalUrl), instance.baseUri, instance.credentials, instance.LiveIdAuthenticationConfiguration);
			}
			return base.CreateWebSession(instance);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000D3578 File Offset: 0x000D1778
		private string GetResponseHtml(WebResponse response)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x04002419 RID: 9241
		private const string monitoringEventSourceInternal = "MSExchange Monitoring ECPConnectivity Internal";

		// Token: 0x0400241A RID: 9242
		private const string monitoringEventSourceExternal = "MSExchange Monitoring ECPConnectivity External";

		// Token: 0x0400241B RID: 9243
		private const string monitoringServicePerfCounter = "ECP Web Sevice Logon Latency";

		// Token: 0x0400241C RID: 9244
		private const uint ExternalTimeOut = 120U;

		// Token: 0x0400241D RID: 9245
		private const uint InternalTimeOut = 90U;
	}
}

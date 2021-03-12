using System;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EwsConnectionManager : IEwsConnectionManager
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001B449 File Offset: 0x00019649
		public EwsConnectionManager(ExchangePrincipal principal, OpenAsAdminOrSystemServiceBudgetTypeType budgetType, Trace tracer)
		{
			EnumValidator.AssertValid<OpenAsAdminOrSystemServiceBudgetTypeType>(budgetType);
			this.budgetType = budgetType;
			this.currentExchangePrincipal = principal;
			this.Tracer = tracer;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001B46C File Offset: 0x0001966C
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001B474 File Offset: 0x00019674
		private Trace Tracer { get; set; }

		// Token: 0x06000704 RID: 1796 RVA: 0x0001B480 File Offset: 0x00019680
		public string GetSmtpAddress()
		{
			return this.currentExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001B4AB File Offset: 0x000196AB
		public string GetPrincipalInfoForTracing()
		{
			return this.currentExchangePrincipal.ToString();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public void ReloadPrincipal()
		{
			string text = this.currentExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			SmtpAddress smtpAddress = new SmtpAddress(text);
			this.currentExchangePrincipal = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain), text);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001B504 File Offset: 0x00019704
		public Uri GetBackEndWebServicesUrl()
		{
			return BackEndLocator.GetBackEndWebServicesUrl(this.currentExchangePrincipal.MailboxInfo);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001B518 File Offset: 0x00019718
		public virtual IExchangeService CreateBinding(RemoteCertificateValidationCallback certificateErrorHandler)
		{
			bool flag = true;
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				if (this.IsTraceEnabled(TraceType.ErrorTrace))
				{
					this.Tracer.TraceError<LocalizedException>(0L, "Unable to impersonate network service to call EWS due to exception {0}", NetworkServiceImpersonator.Exception);
				}
				flag = false;
			}
			ExchangeServiceBinding exchangeServiceBinding = new ExchangeServiceBinding(certificateErrorHandler);
			exchangeServiceBinding.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent("AuditLog");
			exchangeServiceBinding.RequestServerVersionValue = new RequestServerVersion
			{
				Version = ExchangeVersionType.Exchange2013
			};
			if (flag)
			{
				exchangeServiceBinding.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			}
			else
			{
				exchangeServiceBinding.Authenticator = SoapHttpClientAuthenticator.Create(CredentialCache.DefaultCredentials);
			}
			exchangeServiceBinding.Authenticator.AdditionalSoapHeaders.Add(new OpenAsAdminOrSystemServiceType
			{
				ConnectingSID = new ConnectingSIDType
				{
					Item = new PrimarySmtpAddressType
					{
						Value = this.GetSmtpAddress()
					}
				},
				LogonType = SpecialLogonType.SystemService,
				BudgetType = (int)this.budgetType,
				BudgetTypeSpecified = true
			});
			return exchangeServiceBinding;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001B5FF File Offset: 0x000197FF
		private bool IsTraceEnabled(TraceType traceType)
		{
			return this.Tracer != null && this.Tracer.IsTraceEnabled(traceType);
		}

		// Token: 0x04000321 RID: 801
		private readonly OpenAsAdminOrSystemServiceBudgetTypeType budgetType;

		// Token: 0x04000322 RID: 802
		private ExchangePrincipal currentExchangePrincipal;
	}
}

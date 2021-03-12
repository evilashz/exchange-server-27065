using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D30 RID: 3376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxSearchEwsClient : IDisposable
	{
		// Token: 0x0600751C RID: 29980 RVA: 0x00207785 File Offset: 0x00205985
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (SslConfiguration.AllowExternalUntrustedCerts)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string, SslPolicyErrors>((long)sender.GetHashCode(), "MailboxSearchEwsClient::CertificateErrorHandler. Allowed SSL certificate {0} with error {1}", certificate.Subject, sslPolicyErrors);
				return true;
			}
			return false;
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x002077B4 File Offset: 0x002059B4
		public MailboxSearchEwsClient(ExchangePrincipal principal, ADUser executingUser)
		{
			try
			{
				string url = null;
				DelegationTokenRequest request = null;
				this.Discover(principal, executingUser, out url, out request);
				RequestedToken token = this.securityTokenService.IssueToken(request);
				SoapHttpClientAuthenticator authenticator = SoapHttpClientAuthenticator.Create(token);
				this.binding = new MailboxSearchEwsClient.MailboxSearchEwsBinding("ExchangeEDiscovery", new RemoteCertificateValidationCallback(MailboxSearchEwsClient.CertificateErrorHandler));
				this.binding.Url = url;
				this.binding.RequestServerVersionValue = MailboxSearchEwsClient.RequestServerVersionExchange2010;
				this.binding.Authenticator = authenticator;
				this.binding.Proxy = this.WebProxy;
				this.binding.UserAgent = "ExchangeEDiscovery";
				this.binding.Timeout = 600000;
			}
			catch (WSTrustException ex)
			{
				ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), ex.ToString());
				throw new MailboxSearchEwsFailedException(ex.Message);
			}
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x0020789C File Offset: 0x00205A9C
		private void Discover(ExchangePrincipal principal, ADUser executingUser, out string ewsEndpoint, out DelegationTokenRequest ewsTokenRequest)
		{
			SmtpAddress value = principal.MailboxInfo.RemoteIdentity.Value;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.FullyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 168, "Discover", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxSearchEwsClient.cs");
			ADUser aduser = null;
			TransportConfigContainer transportConfigContainer = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 171, "Discover", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxSearchEwsClient.cs").FindSingletonConfigurationObject<TransportConfigContainer>();
			if (transportConfigContainer != null && transportConfigContainer.OrganizationFederatedMailbox != SmtpAddress.NullReversePath)
			{
				SmtpAddress organizationFederatedMailbox = transportConfigContainer.OrganizationFederatedMailbox;
				ProxyAddress proxyAddress = null;
				try
				{
					proxyAddress = ProxyAddress.Parse(organizationFederatedMailbox.ToString());
				}
				catch (ArgumentException ex)
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "Proxy address of organization federated mailbox is invalid: {0}", ex.ToString());
				}
				if (proxyAddress != null && !(proxyAddress is InvalidProxyAddress))
				{
					aduser = (tenantOrRootOrgRecipientSession.FindByProxyAddress(proxyAddress) as ADUser);
				}
			}
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(OrganizationId.ForestWideOrgId);
			OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(value.Domain);
			if (aduser == null || organizationRelationship == null)
			{
				throw new OrganizationNotFederatedException();
			}
			DelegationTokenRequest request = new DelegationTokenRequest
			{
				FederatedIdentity = aduser.GetFederatedIdentity(),
				EmailAddress = aduser.GetFederatedSmtpAddress().ToString(),
				Target = organizationRelationship.GetTokenTarget(),
				Offer = Offer.Autodiscover
			};
			FedOrgCredentials credentials = new FedOrgCredentials(request, this.GetSecurityTokenService(aduser.OrganizationId));
			Uri uri = null;
			using (AutoDiscoverUserSettingsClient autoDiscoverUserSettingsClient = AutoDiscoverUserSettingsClient.CreateInstance(DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 215, "Discover", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxSearchEwsClient.cs"), credentials, value, organizationRelationship.TargetAutodiscoverEpr, MailboxSearchEwsClient.AutoDiscoverRequestedSettings))
			{
				UserSettings userSettings = autoDiscoverUserSettingsClient.Discover();
				StringSetting stringSetting = userSettings.GetSetting("ExternalEwsUrl") as StringSetting;
				if (stringSetting == null || !Uri.TryCreate(stringSetting.Value, UriKind.Absolute, out uri))
				{
					throw new AutoDAccessException(ServerStrings.AutoDRequestFailed);
				}
			}
			ewsEndpoint = EwsWsSecurityUrl.Fix(uri.ToString());
			string text = null;
			if (executingUser.EmailAddresses != null && executingUser.EmailAddresses.Count > 0)
			{
				List<string> federatedEmailAddresses = executingUser.GetFederatedEmailAddresses();
				if (federatedEmailAddresses != null && federatedEmailAddresses.Count > 0)
				{
					text = federatedEmailAddresses[0];
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				ewsTokenRequest = new DelegationTokenRequest
				{
					FederatedIdentity = aduser.GetFederatedIdentity(),
					EmailAddress = aduser.GetFederatedSmtpAddress().ToString(),
					Target = organizationRelationship.GetTokenTarget(),
					Offer = Offer.MailboxSearch
				};
				return;
			}
			ewsTokenRequest = new DelegationTokenRequest
			{
				FederatedIdentity = executingUser.GetFederatedIdentity(),
				EmailAddress = text.ToString(),
				Target = organizationRelationship.GetTokenTarget(),
				Offer = Offer.MailboxSearch
			};
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x00207B98 File Offset: 0x00205D98
		public IList<KeywordHit> FindMailboxStatisticsByKeywords(ExchangePrincipal principal, SearchObject searchObject)
		{
			FindMailboxStatisticsByKeywordsType findMailboxStatisticsByKeywordsType = new FindMailboxStatisticsByKeywordsType
			{
				Mailboxes = new UserMailboxType[]
				{
					new UserMailboxType
					{
						Id = (principal.MailboxInfo.IsArchive ? principal.MailboxInfo.MailboxGuid.ToString() : principal.MailboxInfo.RemoteIdentity.Value.ToString()),
						IsArchive = principal.MailboxInfo.IsArchive
					}
				},
				Keywords = new string[]
				{
					searchObject.SearchQuery
				}
			};
			if (searchObject.Language != null)
			{
				findMailboxStatisticsByKeywordsType.Language = searchObject.Language.ToString();
			}
			if (searchObject.Senders != null && searchObject.Senders.Count > 0)
			{
				findMailboxStatisticsByKeywordsType.Senders = searchObject.Senders.ToArray();
			}
			if (searchObject.Recipients != null && searchObject.Recipients.Count > 0)
			{
				findMailboxStatisticsByKeywordsType.Recipients = searchObject.Recipients.ToArray();
			}
			if (searchObject.StartDate != null)
			{
				findMailboxStatisticsByKeywordsType.FromDate = (DateTime)searchObject.StartDate.Value.ToUtc();
				findMailboxStatisticsByKeywordsType.FromDateSpecified = true;
			}
			if (searchObject.EndDate != null)
			{
				findMailboxStatisticsByKeywordsType.ToDate = (DateTime)searchObject.EndDate.Value.ToUtc();
				findMailboxStatisticsByKeywordsType.ToDateSpecified = true;
			}
			if (searchObject.SearchDumpster)
			{
				findMailboxStatisticsByKeywordsType.SearchDumpster = true;
				findMailboxStatisticsByKeywordsType.SearchDumpsterSpecified = true;
			}
			if (searchObject.IncludePersonalArchive)
			{
				findMailboxStatisticsByKeywordsType.IncludePersonalArchive = true;
				findMailboxStatisticsByKeywordsType.IncludePersonalArchiveSpecified = true;
			}
			if (searchObject.IncludeUnsearchableItems)
			{
				findMailboxStatisticsByKeywordsType.IncludeUnsearchableItems = true;
				findMailboxStatisticsByKeywordsType.IncludeUnsearchableItemsSpecified = true;
			}
			if (searchObject.MessageTypes != null && searchObject.MessageTypes.Count > 0)
			{
				List<SearchItemKindType> list = new List<SearchItemKindType>(searchObject.MessageTypes.Count);
				foreach (KindKeyword kindKeyword in searchObject.MessageTypes)
				{
					list.Add((SearchItemKindType)Enum.Parse(typeof(SearchItemKindType), kindKeyword.ToString(), true));
				}
				findMailboxStatisticsByKeywordsType.MessageTypes = list.ToArray();
			}
			FindMailboxStatisticsByKeywordsResponseType findMailboxStatisticsByKeywordsResponseType = null;
			int num = 0;
			Exception ex = null;
			do
			{
				try
				{
					ex = null;
					findMailboxStatisticsByKeywordsResponseType = this.binding.FindMailboxStatisticsByKeywords(findMailboxStatisticsByKeywordsType);
					break;
				}
				catch (SoapException ex2)
				{
					ex = ex2;
				}
				catch (WebException ex3)
				{
					ex = ex3;
				}
				catch (IOException ex4)
				{
					ex = ex4;
				}
				catch (InvalidOperationException ex5)
				{
					ex = ex5;
				}
			}
			while (++num < 3);
			if (ex != null)
			{
				ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), ex.ToString());
				throw new MailboxSearchEwsFailedException(ex.Message);
			}
			if (findMailboxStatisticsByKeywordsResponseType != null && findMailboxStatisticsByKeywordsResponseType.ResponseMessages != null && findMailboxStatisticsByKeywordsResponseType.ResponseMessages.Items != null && findMailboxStatisticsByKeywordsResponseType.ResponseMessages.Items.Length > 0)
			{
				List<KeywordHit> list2 = new List<KeywordHit>(findMailboxStatisticsByKeywordsResponseType.ResponseMessages.Items.Length);
				foreach (FindMailboxStatisticsByKeywordsResponseMessageType findMailboxStatisticsByKeywordsResponseMessageType in findMailboxStatisticsByKeywordsResponseType.ResponseMessages.Items)
				{
					if (findMailboxStatisticsByKeywordsResponseMessageType.ResponseClass != ResponseClassType.Success)
					{
						ExTraceGlobals.SessionTracer.TraceError<ResponseClassType, ResponseCodeType, string>((long)this.GetHashCode(), "FindMailboxStatisticsByKeywords EWS call failed with response. ResponseClass={0}, ResponseCode={1}, MessageText={2}", findMailboxStatisticsByKeywordsResponseMessageType.ResponseClass, findMailboxStatisticsByKeywordsResponseMessageType.ResponseCode, findMailboxStatisticsByKeywordsResponseMessageType.MessageText);
						throw new MailboxSearchEwsFailedException(findMailboxStatisticsByKeywordsResponseMessageType.MessageText);
					}
					MailboxStatisticsSearchResultType mailboxStatisticsSearchResult = findMailboxStatisticsByKeywordsResponseMessageType.MailboxStatisticsSearchResult;
					KeywordStatisticsSearchResultType keywordStatisticsSearchResult = mailboxStatisticsSearchResult.KeywordStatisticsSearchResult;
					list2.Add(new KeywordHit
					{
						Phrase = keywordStatisticsSearchResult.Keyword,
						Count = keywordStatisticsSearchResult.ItemHits,
						MailboxCount = ((keywordStatisticsSearchResult.ItemHits > 0) ? 1 : 0),
						Size = new ByteQuantifiedSize((ulong)keywordStatisticsSearchResult.Size)
					});
				}
				return list2;
			}
			ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "Response of FindMailboxStatisticsByKeywords is empty");
			throw new MailboxSearchEwsFailedException(ServerStrings.MailboxSearchEwsEmptyResponse);
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x00207FF8 File Offset: 0x002061F8
		public void Dispose()
		{
			if (this.binding != null)
			{
				this.binding.Dispose();
				this.binding = null;
			}
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x00208014 File Offset: 0x00206214
		private SecurityTokenService GetSecurityTokenService(OrganizationId organizationId)
		{
			if (this.securityTokenService == null)
			{
				ExternalAuthentication current = ExternalAuthentication.GetCurrent();
				if (current.Enabled)
				{
					this.securityTokenService = current.GetSecurityTokenService(organizationId);
					if (this.securityTokenService == null)
					{
						throw new AutoDAccessException(ServerStrings.AutoDFailedToGetToken);
					}
				}
			}
			return this.securityTokenService;
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06007522 RID: 29986 RVA: 0x00208060 File Offset: 0x00206260
		private IWebProxy WebProxy
		{
			get
			{
				if (this.webProxy == null)
				{
					Server localServer = LocalServerCache.LocalServer;
					if (localServer != null && localServer.InternetWebProxy != null)
					{
						this.webProxy = new WebProxy(localServer.InternetWebProxy);
					}
				}
				return this.webProxy;
			}
		}

		// Token: 0x04005176 RID: 20854
		private const string ComponentId = "ExchangeEDiscovery";

		// Token: 0x04005177 RID: 20855
		private const string ExternalEwsUrl = "ExternalEwsUrl";

		// Token: 0x04005178 RID: 20856
		private const int DefaultSoapClientTimeout = 600000;

		// Token: 0x04005179 RID: 20857
		private const int MaximumTransientFailureRetries = 3;

		// Token: 0x0400517A RID: 20858
		private static readonly string[] AutoDiscoverRequestedSettings = new string[]
		{
			"ExternalEwsUrl"
		};

		// Token: 0x0400517B RID: 20859
		private static readonly RequestServerVersion RequestServerVersionExchange2010 = new RequestServerVersion
		{
			Version = ExchangeVersionType.Exchange2010_SP1
		};

		// Token: 0x0400517C RID: 20860
		private MailboxSearchEwsClient.MailboxSearchEwsBinding binding;

		// Token: 0x0400517D RID: 20861
		private SecurityTokenService securityTokenService;

		// Token: 0x0400517E RID: 20862
		private IWebProxy webProxy;

		// Token: 0x02000D31 RID: 3377
		private sealed class MailboxSearchEwsBinding : ExchangeServiceBinding
		{
			// Token: 0x1400000C RID: 12
			// (add) Token: 0x06007524 RID: 29988 RVA: 0x002080DC File Offset: 0x002062DC
			// (remove) Token: 0x06007525 RID: 29989 RVA: 0x00208114 File Offset: 0x00206314
			public event MailboxSearchEwsClient.MailboxSearchEwsBinding.FindMailboxStatisticsByKeywordsCompletedEventHandler FindMailboxStatisticsByKeywordsCompleted;

			// Token: 0x06007526 RID: 29990 RVA: 0x00208149 File Offset: 0x00206349
			public MailboxSearchEwsBinding(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(componentId, remoteCertificateValidationCallback)
			{
			}

			// Token: 0x06007527 RID: 29991 RVA: 0x00208154 File Offset: 0x00206354
			[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindMailboxStatisticsByKeywords", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
			[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
			[SoapHeader("RequestServerVersionValue")]
			[SoapHttpClientTraceExtension]
			[return: XmlElement("FindMailboxStatisticsByKeywordsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
			public FindMailboxStatisticsByKeywordsResponseType FindMailboxStatisticsByKeywords([XmlElement("FindMailboxStatisticsByKeywords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindMailboxStatisticsByKeywordsType findMailboxStatisticsByKeywords1)
			{
				object[] array = this.Invoke("FindMailboxStatisticsByKeywords", new object[]
				{
					findMailboxStatisticsByKeywords1
				});
				return (FindMailboxStatisticsByKeywordsResponseType)array[0];
			}

			// Token: 0x06007528 RID: 29992 RVA: 0x00208184 File Offset: 0x00206384
			public IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsType findMailboxStatisticsByKeywords1, AsyncCallback callback, object asyncState)
			{
				return base.BeginInvoke("FindMailboxStatisticsByKeywords", new object[]
				{
					findMailboxStatisticsByKeywords1
				}, callback, asyncState);
			}

			// Token: 0x06007529 RID: 29993 RVA: 0x002081AC File Offset: 0x002063AC
			public FindMailboxStatisticsByKeywordsResponseType EndFindMailboxStatisticsByKeywords(IAsyncResult asyncResult)
			{
				object[] array = base.EndInvoke(asyncResult);
				return (FindMailboxStatisticsByKeywordsResponseType)array[0];
			}

			// Token: 0x0600752A RID: 29994 RVA: 0x002081C9 File Offset: 0x002063C9
			public void FindMailboxStatisticsByKeywordsAsync(FindMailboxStatisticsByKeywordsType findMailboxStatisticsByKeywords1)
			{
				this.FindMailboxStatisticsByKeywordsAsync(findMailboxStatisticsByKeywords1, null);
			}

			// Token: 0x0600752B RID: 29995 RVA: 0x002081D4 File Offset: 0x002063D4
			public void FindMailboxStatisticsByKeywordsAsync(FindMailboxStatisticsByKeywordsType findMailboxStatisticsByKeywords1, object userState)
			{
				if (this.findMailboxStatisticsByKeywordsOperationCompleted == null)
				{
					this.findMailboxStatisticsByKeywordsOperationCompleted = new SendOrPostCallback(this.OnFindMailboxStatisticsByKeywordsOperationCompleted);
				}
				base.InvokeAsync("FindMailboxStatisticsByKeywords", new object[]
				{
					findMailboxStatisticsByKeywords1
				}, this.findMailboxStatisticsByKeywordsOperationCompleted, userState);
			}

			// Token: 0x0600752C RID: 29996 RVA: 0x0020821C File Offset: 0x0020641C
			private void OnFindMailboxStatisticsByKeywordsOperationCompleted(object arg)
			{
				if (this.FindMailboxStatisticsByKeywordsCompleted != null)
				{
					InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
					this.FindMailboxStatisticsByKeywordsCompleted(this, new MailboxSearchEwsClient.MailboxSearchEwsBinding.FindMailboxStatisticsByKeywordsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
				}
			}

			// Token: 0x0400517F RID: 20863
			private SendOrPostCallback findMailboxStatisticsByKeywordsOperationCompleted;

			// Token: 0x02000D32 RID: 3378
			// (Invoke) Token: 0x0600752E RID: 29998
			public delegate void FindMailboxStatisticsByKeywordsCompletedEventHandler(object sender, MailboxSearchEwsClient.MailboxSearchEwsBinding.FindMailboxStatisticsByKeywordsCompletedEventArgs e);

			// Token: 0x02000D33 RID: 3379
			[DesignerCategory("code")]
			[DebuggerStepThrough]
			public class FindMailboxStatisticsByKeywordsCompletedEventArgs : AsyncCompletedEventArgs
			{
				// Token: 0x06007531 RID: 30001 RVA: 0x00208261 File Offset: 0x00206461
				internal FindMailboxStatisticsByKeywordsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
				{
					this.results = results;
				}

				// Token: 0x17001F6F RID: 8047
				// (get) Token: 0x06007532 RID: 30002 RVA: 0x00208274 File Offset: 0x00206474
				public FindMailboxStatisticsByKeywordsResponseType Result
				{
					get
					{
						base.RaiseExceptionIfNecessary();
						return (FindMailboxStatisticsByKeywordsResponseType)this.results[0];
					}
				}

				// Token: 0x04005181 RID: 20865
				private object[] results;
			}
		}
	}
}

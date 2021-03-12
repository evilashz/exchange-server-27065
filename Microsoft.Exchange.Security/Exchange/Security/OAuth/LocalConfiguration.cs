using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000C0 RID: 192
	internal sealed class LocalConfiguration
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0002F588 File Offset: 0x0002D788
		public static LocalConfiguration Load(ValidationResultCollector resultCollector = null)
		{
			Exception ex = null;
			return LocalConfiguration.InternalLoadHelper(resultCollector, false, out ex);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
		internal static LocalConfiguration InternalLoadHelper(ValidationResultCollector resultCollector, bool loadTrustedIssuers, out Exception exception)
		{
			ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[LocalConfiguration::InternalLoadHelper] entering");
			if (resultCollector == null)
			{
				resultCollector = ValidationResultCollector.NullInstance;
			}
			AuthServer[] authServers = null;
			PartnerApplication[] partnerApps = null;
			AuthConfig authConfig = null;
			exception = null;
			try
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				ITopologyConfigurationSession configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 76, "InternalLoadHelper", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\LocalConfiguration.cs");
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					authServers = configSession.Find<AuthServer>(null, QueryScope.SubTree, new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, AuthServerSchema.Type, AuthServerType.MicrosoftACS),
						new ComparisonFilter(ComparisonOperator.Equal, AuthServerSchema.Type, AuthServerType.AzureAD),
						new ComparisonFilter(ComparisonOperator.Equal, AuthServerSchema.Type, AuthServerType.ADFS)
					}), null, ADGenericPagedReader<AuthServer>.DefaultPageSize);
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<ADOperationErrorCode, Exception>(0L, "[LocalConfiguration::InternalLoadHelper] fail to load AuthServer with error code '{0}', exception: {1}", adoperationResult.ErrorCode, adoperationResult.Exception);
					resultCollector.Add(SecurityStrings.LoadAuthServer, SecurityStrings.ResultADOperationFailure(adoperationResult.ErrorCode.ToString(), adoperationResult.Exception.Message), ResultType.Error);
					return null;
				}
				resultCollector.Add(SecurityStrings.LoadAuthServer, SecurityStrings.ResultAuthServersFound(authServers.Length), ResultType.Success);
				foreach (AuthServer authServer2 in authServers)
				{
					LocalizedString task = SecurityStrings.CheckAuthServer(authServer2.Name);
					if (authServer2.Enabled)
					{
						resultCollector.Add(task, SecurityStrings.ResultAuthServerOK, ResultType.Success);
					}
					else
					{
						resultCollector.Add(task, SecurityStrings.ResultAuthServerDisabled, ResultType.Warning);
					}
				}
				authServers = (from authServer in authServers
				where authServer.Enabled
				select authServer).OrderBy(delegate(AuthServer authServer)
				{
					if (!OAuthCommon.IsRealmEmpty(authServer.Realm))
					{
						return authServer.Realm;
					}
					return string.Empty;
				}).ToArray<AuthServer>();
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					partnerApps = configSession.Find<PartnerApplication>(null, QueryScope.SubTree, null, null, ADGenericPagedReader<PartnerApplication>.DefaultPageSize);
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<ADOperationErrorCode, Exception>(0L, "[LocalConfiguration::InternalLoadHelper] fail to load PartnerApplication with error code '{0}', exception: {1}", adoperationResult.ErrorCode, adoperationResult.Exception);
					resultCollector.Add(SecurityStrings.LoadPartnerApplication, SecurityStrings.ResultADOperationFailure(adoperationResult.ErrorCode.ToString(), adoperationResult.Exception.Message), ResultType.Error);
					return null;
				}
				resultCollector.Add(SecurityStrings.LoadPartnerApplication, SecurityStrings.ResultPartnerApplicationsFound(partnerApps.Length), ResultType.Success);
				foreach (PartnerApplication partnerApplication in partnerApps)
				{
					LocalizedString task2 = SecurityStrings.CheckPartnerApplication(partnerApplication.Name);
					if (partnerApplication.Enabled)
					{
						resultCollector.Add(task2, SecurityStrings.ResultPartnerApplicationOK, ResultType.Success);
					}
					else
					{
						resultCollector.Add(task2, SecurityStrings.ResultPartnerApplicationDisabled, ResultType.Warning);
					}
				}
				partnerApps = (from pa in partnerApps
				where pa.Enabled
				select pa).ToArray<PartnerApplication>();
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					authConfig = configSession.Read<AuthConfig>(configSession.GetOrgContainerId().GetChildId(AuthConfig.ContainerName));
				});
				if (adoperationResult == ADOperationResult.Success)
				{
					resultCollector.Add(SecurityStrings.LoadAuthConfig, SecurityStrings.ResultAuthConfigFound, ResultType.Success);
					return new LocalConfiguration(authConfig, authServers, partnerApps, resultCollector, loadTrustedIssuers);
				}
				ExTraceGlobals.OAuthTracer.TraceDebug<ADOperationErrorCode, Exception>(0L, "[LocalConfiguration::InternalLoadHelper] fail to load AuthConfig with error code '{0}', exception: {1}", adoperationResult.ErrorCode, adoperationResult.Exception);
				resultCollector.Add(SecurityStrings.LoadAuthConfig, SecurityStrings.ResultADOperationFailure(adoperationResult.ErrorCode.ToString(), adoperationResult.Exception.Message), ResultType.Error);
				return null;
			}
			catch (LocalizedException ex)
			{
				exception = ex;
				ExTraceGlobals.OAuthTracer.TraceWarning<Exception>(0L, "[LocalConfiguration::InternalLoadHelper] hit the exception {0}", exception);
				resultCollector.Add(SecurityStrings.LoadConfiguration, SecurityStrings.ResultLoadConfigurationException(exception), ResultType.Error);
			}
			return null;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002FA58 File Offset: 0x0002DC58
		public LocalConfiguration(string applicationId, string realm, X509Certificate2 signingKey, X509Certificate2 previousSigningKey, AuthServer[] authServers, PartnerApplication[] partnerApps, bool loadTrustedIssuers = true) : this(applicationId, realm, signingKey, previousSigningKey, null, authServers, partnerApps, loadTrustedIssuers)
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002FA78 File Offset: 0x0002DC78
		public LocalConfiguration(string applicationId, string realm, X509Certificate2 signingKey, X509Certificate2 previousSigningKey, X509Certificate2 nextSigningKey, AuthServer[] authServers, PartnerApplication[] partnerApps, bool loadTrustedIssuers = true)
		{
			this.Initialize(applicationId, realm, signingKey, previousSigningKey, nextSigningKey, authServers, partnerApps, loadTrustedIssuers);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
		private LocalConfiguration(AuthConfig authConfig, AuthServer[] authServers, PartnerApplication[] partnerApps, ValidationResultCollector resultCollector, bool loadTrustedIssuers)
		{
			OAuthCommon.VerifyNonNullArgument("authConfig", authConfig);
			OAuthCommon.VerifyNonNullArgument("authServers", authServers);
			OAuthCommon.VerifyNonNullArgument("partnerApps", partnerApps);
			string serviceName = authConfig.ServiceName;
			if (resultCollector != null)
			{
				if (string.Equals(serviceName, WellknownPartnerApplicationIdentifiers.Exchange, StringComparison.OrdinalIgnoreCase))
				{
					resultCollector.Add(SecurityStrings.CheckServiceName, SecurityStrings.ResultMatchServiceNameDefaultValue(serviceName), ResultType.Success);
				}
				else
				{
					resultCollector.Add(SecurityStrings.CheckServiceName, SecurityStrings.ResultDidNotMatchServiceNameDefaultValue(serviceName, WellknownPartnerApplicationIdentifiers.Exchange), ResultType.Warning);
				}
			}
			string text = string.Empty;
			if (!AuthCommon.IsMultiTenancyEnabled)
			{
				if (!string.IsNullOrEmpty(authConfig.Realm))
				{
					text = authConfig.Realm;
					if (resultCollector != null)
					{
						resultCollector.Add(SecurityStrings.CheckAuthConfigRealm, SecurityStrings.ResultAuthConfigRealmOverwrote(text), ResultType.Warning);
					}
				}
				else
				{
					text = OAuthCommon.DefaultAcceptedDomain;
				}
			}
			X509Certificate2 x509Certificate = null;
			X509Certificate2 x509Certificate2 = null;
			X509Certificate2 x509Certificate3 = null;
			string currentCertificateThumbprint = authConfig.CurrentCertificateThumbprint;
			string previousCertificateThumbprint = authConfig.PreviousCertificateThumbprint;
			string nextCertificateThumbprint = authConfig.NextCertificateThumbprint;
			X509Store x509Store = null;
			try
			{
				x509Store = new X509Store(StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadOnly);
				x509Certificate = LocalConfiguration.ReadCertificate(SecurityStrings.CheckCurrentCertificate, x509Store, currentCertificateThumbprint, resultCollector);
				x509Certificate2 = LocalConfiguration.ReadCertificate(SecurityStrings.CheckPreviousCertificate, x509Store, previousCertificateThumbprint, resultCollector);
				x509Certificate3 = LocalConfiguration.ReadCertificate(SecurityStrings.CheckNextCertificate, x509Store, nextCertificateThumbprint, resultCollector);
			}
			catch (CryptographicException ex)
			{
				if (resultCollector != null)
				{
					resultCollector.Add(SecurityStrings.ReadCertificates, SecurityStrings.ResultReadCertificatesException(ex), ResultType.Error);
				}
				OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailToReadSigningCertificates, string.Empty, new object[]
				{
					ex
				});
				ExTraceGlobals.OAuthTracer.TraceError<string, string, CryptographicException>(0L, "[LocalConfiguration::ctor] hitting CryptographicException when retrieving certs with thumbprint {0}, {1}. The exception detail: {2}", currentCertificateThumbprint, previousCertificateThumbprint, ex);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			if (x509Certificate == null && x509Certificate2 == null)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<string, string>(0L, "[LocalConfiguration::ctor] no signing cert was found, authconfig has thumbrpints {0}, {1}.", authConfig.CurrentCertificateThumbprint, authConfig.PreviousCertificateThumbprint);
			}
			this.Initialize(serviceName, text, x509Certificate, x509Certificate2, x509Certificate3, authServers, partnerApps, loadTrustedIssuers);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002FC7C File Offset: 0x0002DE7C
		private static X509Certificate2 ReadCertificate(LocalizedString task, X509Store store, string thumbprint, ValidationResultCollector resultCollector)
		{
			if (!string.IsNullOrEmpty(thumbprint))
			{
				X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (x509Certificate2Collection.Count > 0)
				{
					if (x509Certificate2Collection[0].HasPrivateKey)
					{
						X509Certificate2 x509Certificate = x509Certificate2Collection[0];
						if (!(x509Certificate.NotAfter <= DateTime.UtcNow) && !(x509Certificate.NotBefore >= DateTime.UtcNow))
						{
							if (resultCollector != null)
							{
								resultCollector.Add(task, SecurityStrings.ResultCertValid(thumbprint), ResultType.Success);
							}
							return x509Certificate;
						}
						if (resultCollector != null)
						{
							resultCollector.Add(task, SecurityStrings.ResultCertInvalid(thumbprint, x509Certificate.NotBefore, x509Certificate.NotAfter), ResultType.Warning);
						}
					}
					else if (resultCollector != null)
					{
						resultCollector.Add(task, SecurityStrings.ResultCertHasNoPrivateKey(thumbprint), ResultType.Warning);
					}
				}
				else
				{
					if (resultCollector != null)
					{
						resultCollector.Add(task, SecurityStrings.ResultCertNotFound(thumbprint), ResultType.Warning);
					}
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthSigningCertificateNotFoundOrMissingPrivateKey, thumbprint, new object[]
					{
						thumbprint
					});
					ExTraceGlobals.OAuthTracer.TraceWarning<string>(0L, "[LocalConfiguration::ReadCertificate] The certificate with thumbprint {0} was not found or has no private key.", thumbprint);
				}
			}
			else if (resultCollector != null)
			{
				resultCollector.Add(task, SecurityStrings.ResultThumbprintNotSet, ResultType.Warning);
			}
			return null;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0002FE28 File Offset: 0x0002E028
		private void Initialize(string applicationId, string realm, X509Certificate2 signingKey, X509Certificate2 previousSigningKey, X509Certificate2 nextSigningKey, AuthServer[] authServers, PartnerApplication[] partnerApps, bool loadTrustedIssuers)
		{
			this.applicationId = applicationId;
			this.singleTenancyRealm = realm;
			this.authServers = authServers;
			this.partnerApps = partnerApps;
			this.exchangeApp = Array.Find<PartnerApplication>(partnerApps, (PartnerApplication partnerApp) => OAuthCommon.IsIdMatch(partnerApp.ApplicationIdentifier, WellknownPartnerApplicationIdentifiers.Exchange));
			this.signingKey = signingKey;
			this.previousSigningKey = previousSigningKey;
			this.nextSigningKey = nextSigningKey;
			List<X509Certificate2> list = new List<X509Certificate2>(3);
			if (signingKey != null)
			{
				list.Add(signingKey);
			}
			if (previousSigningKey != null)
			{
				list.Add(previousSigningKey);
			}
			if (nextSigningKey != null)
			{
				list.Add(nextSigningKey);
			}
			this.allSigningCertificates = list.ToArray<X509Certificate2>();
			if (loadTrustedIssuers)
			{
				List<AuthServer> list2 = new List<AuthServer>(4);
				List<TrustedIssuer> list3 = new List<TrustedIssuer>(20);
				TrustedIssuer item = null;
				foreach (AuthServer authServer in this.authServers)
				{
					if (TrustedIssuer.TryCreateFromAuthServer(authServer, out item))
					{
						list3.Add(item);
					}
					if (!string.IsNullOrEmpty(authServer.AuthorizationEndpoint) && (authServer.Type == AuthServerType.AzureAD || authServer.Type == AuthServerType.ADFS) && authServer.IsDefaultAuthorizationEndpoint)
					{
						list2.Add(authServer);
					}
				}
				foreach (PartnerApplication partnerApplication in this.partnerApps)
				{
					if (!partnerApplication.UseAuthServer && TrustedIssuer.TryCreateFromPartnerApplication(partnerApplication, out item))
					{
						list3.Add(item);
					}
				}
				this.trustedIssuers = list3.ToArray();
				this.trustedIssuersString = string.Join(",", from trustedIssuer in list3
				select trustedIssuer.IssuerMetadata.ToTrustedIssuerString());
				string arg = string.Join(",", from trustedIssuer in list3
				where trustedIssuer.IssuerMetadata.Kind == IssuerKind.PartnerApp || trustedIssuer.IssuerMetadata.Kind == IssuerKind.ACS
				where !AuthCommon.IsMultiTenancyEnabled || trustedIssuer.IssuerMetadata.Kind != IssuerKind.ACS || trustedIssuer.IssuerMetadata.HasEmptyRealm
				where trustedIssuer.IssuerMetadata.Kind != IssuerKind.PartnerApp || !(trustedIssuer.IssuerMetadata.Realm == Constants.MSExchangeSelfIssuingTokenRealm)
				select trustedIssuer.IssuerMetadata.ToTrustedIssuerString());
				string text = null;
				if (list2.Count == 1)
				{
					text = list2[0].AuthorizationEndpoint;
				}
				else if (list2.Count > 1)
				{
					string text2 = string.Join(",", from s in list2
					select s.Name);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthMoreThanOneAuthServerWithAuthorizationEndpoint, text2, new object[]
					{
						text2
					});
				}
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append(Constants.BearerAuthenticationType);
				stringBuilder.AppendFormat(" {0}=\"{1}\",", Constants.ChallengeTokens.ClientId, this.applicationId);
				stringBuilder.AppendFormat(" {0}=\"{1}\"", Constants.ChallengeTokens.TrustedIssuers, arg);
				this.challengeResponseString = stringBuilder.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(", {0}=\"{1}\"", Constants.ChallengeTokens.AuthorizationUri, text);
				}
				this.challengeResponseStringWithClientProfileEnabled = stringBuilder.ToString();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0003014B File Offset: 0x0002E34B
		public string ApplicationId
		{
			get
			{
				return this.applicationId;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00030153 File Offset: 0x0002E353
		public string SingleTenancyRealm
		{
			get
			{
				return this.singleTenancyRealm;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0003015B File Offset: 0x0002E35B
		public X509Certificate2 SigningKey
		{
			get
			{
				return this.signingKey;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00030163 File Offset: 0x0002E363
		public X509Certificate2 PreviousSigningKey
		{
			get
			{
				return this.previousSigningKey;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0003016B File Offset: 0x0002E36B
		public X509Certificate2 NextSigningKey
		{
			get
			{
				return this.nextSigningKey;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00030173 File Offset: 0x0002E373
		public AuthServer[] AuthServers
		{
			get
			{
				return this.authServers;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0003017B File Offset: 0x0002E37B
		public PartnerApplication[] PartnerApplications
		{
			get
			{
				return this.partnerApps;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00030183 File Offset: 0x0002E383
		public PartnerApplication ExchangeApplication
		{
			get
			{
				return this.exchangeApp;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0003018B File Offset: 0x0002E38B
		public string TrustedIssuersAsString
		{
			get
			{
				return this.trustedIssuersString;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00030193 File Offset: 0x0002E393
		public string ChallengeResponseString
		{
			get
			{
				return this.challengeResponseString;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0003019B File Offset: 0x0002E39B
		public string ChallengeResponseStringWithClientProfileEnabled
		{
			get
			{
				return this.challengeResponseStringWithClientProfileEnabled;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000301A3 File Offset: 0x0002E3A3
		public TrustedIssuer[] TrustedIssuers
		{
			get
			{
				return this.trustedIssuers;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000301AB File Offset: 0x0002E3AB
		public X509Certificate2[] Certificates
		{
			get
			{
				return this.allSigningCertificates;
			}
		}

		// Token: 0x04000633 RID: 1587
		private string applicationId;

		// Token: 0x04000634 RID: 1588
		private string singleTenancyRealm;

		// Token: 0x04000635 RID: 1589
		private X509Certificate2 signingKey;

		// Token: 0x04000636 RID: 1590
		private X509Certificate2 previousSigningKey;

		// Token: 0x04000637 RID: 1591
		private X509Certificate2 nextSigningKey;

		// Token: 0x04000638 RID: 1592
		private X509Certificate2[] allSigningCertificates;

		// Token: 0x04000639 RID: 1593
		private AuthServer[] authServers;

		// Token: 0x0400063A RID: 1594
		private PartnerApplication[] partnerApps;

		// Token: 0x0400063B RID: 1595
		private PartnerApplication exchangeApp;

		// Token: 0x0400063C RID: 1596
		private TrustedIssuer[] trustedIssuers;

		// Token: 0x0400063D RID: 1597
		private string trustedIssuersString;

		// Token: 0x0400063E RID: 1598
		private string challengeResponseString;

		// Token: 0x0400063F RID: 1599
		private string challengeResponseStringWithClientProfileEnabled;
	}
}

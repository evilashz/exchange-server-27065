using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security.ExternalAuthentication;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Storage.Authentication
{
	// Token: 0x02000DEB RID: 3563
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExternalAuthentication
	{
		// Token: 0x06007A82 RID: 31362 RVA: 0x0021DB22 File Offset: 0x0021BD22
		private ExternalAuthentication(ExternalAuthentication.ExternalAuthenticationFailureType failureType, ExternalAuthentication.ExternalAuthenticationSubFailureType subFailureType)
		{
			this.failureType = failureType;
			this.subFailureType = subFailureType;
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x0021DB38 File Offset: 0x0021BD38
		private ExternalAuthentication(Dictionary<ADObjectId, SecurityTokenService> securityTokenServices, TokenValidator tokenValidator, List<X509Certificate2> certificates, Uri applicationUri) : this(ExternalAuthentication.ExternalAuthenticationFailureType.NoFailure, ExternalAuthentication.ExternalAuthenticationSubFailureType.NoFailure)
		{
			this.securityTokenServices = securityTokenServices;
			this.tokenValidator = tokenValidator;
			this.certificates = certificates;
			this.applicationUri = applicationUri;
		}

		// Token: 0x06007A84 RID: 31364 RVA: 0x0021DB5F File Offset: 0x0021BD5F
		private ExternalAuthentication(Dictionary<ADObjectId, SecurityTokenService> securityTokenServices, TokenValidator tokenValidator, List<X509Certificate2> certificates, Uri applicationUri, ExternalAuthentication.ExternalAuthenticationFailureType failureType, ExternalAuthentication.ExternalAuthenticationSubFailureType subFailureType) : this(failureType, subFailureType)
		{
			this.securityTokenServices = securityTokenServices;
			this.tokenValidator = tokenValidator;
			this.certificates = certificates;
			this.applicationUri = applicationUri;
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x0021DB88 File Offset: 0x0021BD88
		public Uri ApplicationUri
		{
			get
			{
				this.ThrowIfNotEnabled();
				return this.applicationUri;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x0021DB96 File Offset: 0x0021BD96
		public bool Enabled
		{
			get
			{
				return this.failureType == ExternalAuthentication.ExternalAuthenticationFailureType.NoFailure;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x06007A87 RID: 31367 RVA: 0x0021DBA1 File Offset: 0x0021BDA1
		public ExternalAuthentication.ExternalAuthenticationFailureType FailureType
		{
			get
			{
				return this.failureType;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x06007A88 RID: 31368 RVA: 0x0021DBA9 File Offset: 0x0021BDA9
		public ExternalAuthentication.ExternalAuthenticationSubFailureType SubFailureType
		{
			get
			{
				return this.subFailureType;
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x0021DBB1 File Offset: 0x0021BDB1
		public TokenValidator TokenValidator
		{
			get
			{
				this.ThrowIfNotEnabled();
				return this.tokenValidator;
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x0021DBBF File Offset: 0x0021BDBF
		public IEnumerable<X509Certificate2> Certificates
		{
			get
			{
				this.ThrowIfNotEnabled();
				return this.certificates;
			}
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x0021DBCD File Offset: 0x0021BDCD
		public IEnumerable<SecurityTokenService> SecurityTokenServices
		{
			get
			{
				this.ThrowIfNotEnabled();
				return this.securityTokenServices.Values;
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x06007A8C RID: 31372 RVA: 0x0021DBE0 File Offset: 0x0021BDE0
		public string SecurityTokenServicesIdentifiers
		{
			get
			{
				if (this.securityTokenServices == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (SecurityTokenService securityTokenService in this.securityTokenServices.Values)
				{
					stringBuilder.Append(securityTokenService.STSClientIdentity + ";");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x06007A8D RID: 31373 RVA: 0x0021DC64 File Offset: 0x0021BE64
		private static ExEventLog EventLogger
		{
			get
			{
				if (ExternalAuthentication.eventLogger == null)
				{
					ExternalAuthentication.eventLogger = new ExEventLog(new Guid("{776208EC-5A13-4b8a-AA53-C84B72E40B86}"), "MSExchange Common");
				}
				return ExternalAuthentication.eventLogger;
			}
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x0021DC8B File Offset: 0x0021BE8B
		internal static void Initialize()
		{
			LocalServerCache.Initialize();
			FederationTrustCache.Initialize();
			ExternalAuthentication.instance = null;
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x0021DC9D File Offset: 0x0021BE9D
		public static void ForceReset()
		{
			ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "A Force Reset was requested.");
			ExternalAuthentication.instance = null;
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x0021DCB6 File Offset: 0x0021BEB6
		public static ExternalAuthentication GetCurrent()
		{
			if (ExternalAuthentication.instance == null)
			{
				ExternalAuthentication.instance = ExternalAuthentication.CreateExternalAuthentication();
			}
			return ExternalAuthentication.instance;
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x0021DCD0 File Offset: 0x0021BED0
		public SecurityTokenService GetSecurityTokenService(OrganizationId organizationId)
		{
			this.ThrowIfNotEnabled();
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			FederatedOrganizationId federatedOrganizationId = organizationIdCacheValue.FederatedOrganizationId;
			if (federatedOrganizationId == null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Unable to find Federated Organization Identifier for organization {0}.", organizationId.ToString());
				return null;
			}
			if (federatedOrganizationId.DelegationTrustLink == null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Unable to find configured delegation trust link for organization {0}.", organizationId.ToString());
				return null;
			}
			SecurityTokenService result;
			if (this.securityTokenServices.TryGetValue(federatedOrganizationId.DelegationTrustLink, out result))
			{
				return result;
			}
			ExternalAuthentication.ConfigurationTracer.TraceError<string, string>(0L, "Unable to find configured Security Token Service client for delegation trust link {0} for organization {1}.", federatedOrganizationId.DelegationTrustLink.DistinguishedName, organizationId.ToString());
			ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Current Security Token Service client list is {0}.", this.SecurityTokenServicesIdentifiers);
			return null;
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x0021DD84 File Offset: 0x0021BF84
		private static ExternalAuthentication CreateExternalAuthentication()
		{
			ExternalAuthentication.InitializeNotificationsIfNeeded();
			ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "Searching for federation trust configuration in AD");
			WebProxy webProxy = null;
			Server localServer = LocalServerCache.LocalServer;
			Uri uri = null;
			if (localServer != null && localServer.InternetWebProxy != null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceDebug<Uri>(0L, "Using custom InternetWebProxy {0}.", localServer.InternetWebProxy);
				uri = localServer.InternetWebProxy;
				webProxy = new WebProxy(localServer.InternetWebProxy);
			}
			ExternalAuthentication.currentWebProxy = uri;
			IEnumerable<FederationTrust> enumerable = null;
			try
			{
				enumerable = FederationTrustCache.GetFederationTrusts();
			}
			catch (LocalizedException arg)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<LocalizedException>(0L, "Unable to get federation trust. Exception: {0}", arg);
				return new ExternalAuthentication(ExternalAuthentication.ExternalAuthenticationFailureType.ErrorReadingFederationTrust, ExternalAuthentication.ExternalAuthenticationSubFailureType.DirectoryReadError);
			}
			Uri uri2 = null;
			List<X509Certificate2> list = new List<X509Certificate2>(4);
			List<X509Certificate2> list2 = new List<X509Certificate2>(4);
			Dictionary<ADObjectId, SecurityTokenService> dictionary = new Dictionary<ADObjectId, SecurityTokenService>(2);
			ExternalAuthentication.ExternalAuthenticationFailureType externalAuthenticationFailureType = ExternalAuthentication.ExternalAuthenticationFailureType.NoFederationTrust;
			ExternalAuthentication.ExternalAuthenticationSubFailureType externalAuthenticationSubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.NoFailure;
			int num = 0;
			bool flag = false;
			if (enumerable != null)
			{
				foreach (FederationTrust federationTrust in enumerable)
				{
					num++;
					ExternalAuthentication.FederationTrustResults federationTrustResults = ExternalAuthentication.TryCreateSecurityTokenService(federationTrust, webProxy);
					if (federationTrustResults.FailureType == ExternalAuthentication.ExternalAuthenticationFailureType.NoFailure)
					{
						dictionary.Add(federationTrust.Id, federationTrustResults.SecurityTokenService);
						list.AddRange(federationTrustResults.TokenSignatureCertificates);
						list2.AddRange(federationTrustResults.TokenDecryptionCertificates);
						if (uri2 == null)
						{
							uri2 = federationTrust.ApplicationUri;
							ExternalAuthentication.ConfigurationTracer.TraceDebug<Uri>(0L, "Using {0} as applicationUri", uri2);
						}
						else if (!federationTrust.ApplicationUri.Equals(uri2))
						{
							ExternalAuthentication.ConfigurationTracer.TraceError<Uri>(0L, "Cannot have multiple FederationTrust with different ApplicationUri values: {0}. Uri will be ignored", federationTrust.ApplicationUri);
							flag = true;
						}
					}
					else
					{
						externalAuthenticationFailureType = federationTrustResults.FailureType;
						externalAuthenticationSubFailureType = federationTrustResults.SubFailureType;
					}
				}
			}
			if (dictionary.Count == 0)
			{
				return new ExternalAuthentication(externalAuthenticationFailureType, externalAuthenticationSubFailureType);
			}
			if (dictionary.Count != num)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string, string>(0L, "Number of Security Token Service clients {0} does not match number of federation trusts {1}", dictionary.Count.ToString(), num.ToString());
			}
			TokenValidator tokenValidator = new TokenValidator(uri2, list, list2);
			List<X509Certificate2> list3 = new List<X509Certificate2>(list.Count + list2.Count);
			list3.AddRange(list);
			list3.AddRange(list2);
			if (flag)
			{
				return new ExternalAuthentication(dictionary, tokenValidator, list3, uri2, ExternalAuthentication.ExternalAuthenticationFailureType.NoFailure, ExternalAuthentication.ExternalAuthenticationSubFailureType.WarningApplicationUriSkipped);
			}
			return new ExternalAuthentication(dictionary, tokenValidator, list3, uri2);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x0021DFEC File Offset: 0x0021C1EC
		private static ExternalAuthentication.FederationTrustResults TryCreateSecurityTokenService(FederationTrust federationTrust, WebProxy webProxy)
		{
			if (!ExternalAuthentication.IsRequiredPropertyAvailable(federationTrust.TokenIssuerUri, "TokenIssuerUri"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.MissingTokenIssuerUri
				};
			}
			if (!ExternalAuthentication.IsRequiredPropertyAvailable(federationTrust.TokenIssuerEpr, "TokenIssuerEpr"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.MissingTokenIssuerEpr
				};
			}
			if (!ExternalAuthentication.IsRequiredPropertyAvailable(federationTrust.ApplicationUri, "ApplicationUri"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.MissingApplicationUri
				};
			}
			if (!ExternalAuthentication.IsRequiredPropertyAvailable(federationTrust.TokenIssuerCertificate, "TokenIssuerCertificate"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.MissingTokenIssuerCertificate
				};
			}
			X509Certificate2[] tokenSignatureCertificates = (federationTrust.TokenIssuerPrevCertificate != null) ? new X509Certificate2[]
			{
				federationTrust.TokenIssuerCertificate,
				federationTrust.TokenIssuerPrevCertificate
			} : new X509Certificate2[]
			{
				federationTrust.TokenIssuerCertificate
			};
			if (!ExternalAuthentication.HasAtLeastOneValidCertificate(tokenSignatureCertificates, federationTrust.Id, "TokenIssuerCertificate and TokenIssuerPrevCertificate"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.InvalidTokenIssuerCertificate,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.NoSubCode
				};
			}
			if (!ExternalAuthentication.IsRequiredPropertyAvailable(federationTrust.OrgPrivCertificate, "OrgPrivCertificate"))
			{
				return new ExternalAuthentication.FederationTrustResults
				{
					FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
					SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.MissingOrgPrivCertificate
				};
			}
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			X509Certificate2 certificate;
			X509Certificate2[] tokenDecryptionCertificates;
			try
			{
				ExternalAuthentication.ExternalAuthenticationSubFailureType externalAuthenticationSubFailureType;
				certificate = ExternalAuthentication.GetCertificate(x509Store, federationTrust.OrgPrivCertificate, federationTrust.Id, "OrgPrivCertificate", true, out externalAuthenticationSubFailureType);
				if (certificate == null)
				{
					ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Federation trust is misconfigured. Unable to find certificate corresponding to OrgPrivCertificate={0}", federationTrust.OrgPrivCertificate);
					return new ExternalAuthentication.FederationTrustResults
					{
						FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.MisconfiguredFederationTrust,
						SubFailureType = externalAuthenticationSubFailureType
					};
				}
				X509Certificate2 x509Certificate = null;
				if (!string.IsNullOrEmpty(federationTrust.OrgPrevPrivCertificate))
				{
					x509Certificate = ExternalAuthentication.GetCertificate(x509Store, federationTrust.OrgPrevPrivCertificate, federationTrust.Id, "OrgPrevPrivCertificate", false, out externalAuthenticationSubFailureType);
				}
				tokenDecryptionCertificates = ((x509Certificate != null) ? new X509Certificate2[]
				{
					certificate,
					x509Certificate
				} : new X509Certificate2[]
				{
					certificate
				});
			}
			finally
			{
				x509Store.Close();
			}
			SecurityTokenService securityTokenService = new SecurityTokenService(federationTrust.TokenIssuerEpr, webProxy, certificate, federationTrust.TokenIssuerUri, federationTrust.PolicyReferenceUri, federationTrust.ApplicationUri.OriginalString);
			ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "New instance of SecurityTokenService successfully built.");
			return new ExternalAuthentication.FederationTrustResults
			{
				FailureType = ExternalAuthentication.ExternalAuthenticationFailureType.NoFailure,
				SubFailureType = ExternalAuthentication.ExternalAuthenticationSubFailureType.NoFailure,
				SecurityTokenService = securityTokenService,
				TokenSignatureCertificates = tokenSignatureCertificates,
				TokenDecryptionCertificates = tokenDecryptionCertificates
			};
		}

		// Token: 0x06007A94 RID: 31380 RVA: 0x0021E280 File Offset: 0x0021C480
		private static bool IsRequiredPropertyAvailable(X509Certificate2 certificate, string name)
		{
			return ExternalAuthentication.IsRequiredPropertyAvailable((certificate == null) ? null : certificate.Thumbprint, name);
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x0021E294 File Offset: 0x0021C494
		private static bool IsRequiredPropertyAvailable(object property, string name)
		{
			if (property == null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Property {0} is missing from federation trust configuration object.", name);
				return false;
			}
			ExternalAuthentication.ConfigurationTracer.TraceDebug<string, object>(0L, "Property {0}={1}", name, property);
			return true;
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x0021E2C4 File Offset: 0x0021C4C4
		private static X509Certificate2 GetCertificate(X509Store store, string thumbprint, ObjectId objectId, string name, bool logExpired, out ExternalAuthentication.ExternalAuthenticationSubFailureType subFailureCode)
		{
			subFailureCode = ExternalAuthentication.ExternalAuthenticationSubFailureType.NoFailure;
			X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
			if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0 || x509Certificate2Collection[0] == null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string, string>(0L, "Certificate with thumprint {0} for {1} is not present in the certificate store.", thumbprint, name);
				ExternalAuthentication.EventLogger.LogEvent(CommonEventLogConstants.Tuple_FederationTrustOrganizationCertificateNotFound, thumbprint, new object[]
				{
					thumbprint,
					objectId.ToString()
				});
				subFailureCode = ExternalAuthentication.ExternalAuthenticationSubFailureType.CertificateNotInStore;
				return null;
			}
			X509Certificate2 x509Certificate = x509Certificate2Collection[0];
			if (!x509Certificate.HasPrivateKey)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string, string>(0L, "Certificate with thumprint {0} for {1} does not have private key.", thumbprint, name);
				ExternalAuthentication.EventLogger.LogEvent(CommonEventLogConstants.Tuple_FederationTrustOrganizationCertificateNoPrivateKey, thumbprint, new object[]
				{
					thumbprint,
					objectId.ToString()
				});
				subFailureCode = ExternalAuthentication.ExternalAuthenticationSubFailureType.CertificateNoPrivateKey;
				return null;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > x509Certificate.NotAfter || utcNow < x509Certificate.NotBefore)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError(0L, "Certificate with thumprint {0} for {1} is expired: NotBefore={2}, NotAfter={3}", new object[]
				{
					thumbprint,
					name,
					x509Certificate.NotBefore,
					x509Certificate.NotAfter
				});
				if (logExpired)
				{
					ExternalAuthentication.EventLogger.LogEvent(CommonEventLogConstants.Tuple_FederationTrustCertificateExpired, x509Certificate.Thumbprint, new object[]
					{
						x509Certificate.Thumbprint,
						objectId.ToString()
					});
				}
				subFailureCode = ExternalAuthentication.ExternalAuthenticationSubFailureType.CertificateExpirationTimeError;
				return null;
			}
			Exception ex = null;
			try
			{
				AsymmetricAlgorithm privateKey = x509Certificate.PrivateKey;
			}
			catch (CryptographicException ex2)
			{
				ex = ex2;
			}
			catch (NotSupportedException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string, string>(0L, "Cannot access private key of certificate with thumprint {0} for {1}.", thumbprint, name);
				ExternalAuthentication.EventLogger.LogEvent(CommonEventLogConstants.Tuple_FederationTrustOrganizationCertificateNoPrivateKey, thumbprint, new object[]
				{
					thumbprint,
					objectId.ToString()
				});
				subFailureCode = ExternalAuthentication.ExternalAuthenticationSubFailureType.CertificatePrivateKeyCryptoError;
				return null;
			}
			ExternalAuthentication.ConfigurationTracer.TraceDebug<string, string, X509Certificate2>(0L, "Loaded certificate with thumprint {0} for {1}. Certificate details: {2}", thumbprint, name, x509Certificate);
			return x509Certificate;
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x0021E4C8 File Offset: 0x0021C6C8
		private static bool HasAtLeastOneValidCertificate(X509Certificate2[] certificates, ObjectId objectId, string description)
		{
			bool flag = false;
			DateTime utcNow = DateTime.UtcNow;
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				if (utcNow < x509Certificate.NotAfter && utcNow > x509Certificate.NotBefore)
				{
					flag = true;
				}
				else
				{
					ExternalAuthentication.ConfigurationTracer.TraceError<X509Certificate2>(0L, "Certificate is not valid: {0}", x509Certificate);
					ExternalAuthentication.EventLogger.LogEvent(CommonEventLogConstants.Tuple_FederationTrustCertificateExpired, x509Certificate.Thumbprint, new object[]
					{
						x509Certificate.Thumbprint,
						objectId.ToString()
					});
				}
			}
			if (!flag)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError<string>(0L, "Federation trust is misconfigured. Unable to find at least one valid certificate in {0}", description);
			}
			return flag;
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x0021E574 File Offset: 0x0021C774
		private static void InitializeNotificationsIfNeeded()
		{
			if (!ExternalAuthentication.subscribedForNotification)
			{
				lock (ExternalAuthentication.subscribedForNotificationLocker)
				{
					if (!ExternalAuthentication.subscribedForNotification)
					{
						LocalServerCache.Change += ExternalAuthentication.LocalServerNotificationHandler;
						FederationTrustCache.Change += ExternalAuthentication.FederationTrustNotificationHandler;
						ExternalAuthentication.subscribedForNotification = true;
					}
				}
			}
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x0021E5E4 File Offset: 0x0021C7E4
		private static void LocalServerNotificationHandler(Server localServer)
		{
			ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "Changes detected in local server object in AD.");
			if (localServer == null)
			{
				ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "Ignoring notification because 'localServer' is null.");
				return;
			}
			if (ExternalAuthentication.currentWebProxy == localServer.InternetWebProxy)
			{
				ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "No need to update ExternalAuthentication because no changes were detected in InternetWebProxy.");
				return;
			}
			if (ExternalAuthentication.currentWebProxy != null && localServer.InternetWebProxy != null && ExternalAuthentication.currentWebProxy.Equals(localServer.InternetWebProxy))
			{
				ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "No need to update ExternalAuthentication because no changes were detected in InternetWebProxy.");
				return;
			}
			ExternalAuthentication.ConfigurationTracer.TraceDebug<Uri>(0L, "Need to update ExternalAuthentication with new web proxy: {0}", localServer.InternetWebProxy);
			ExternalAuthentication.instance = null;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x0021E697 File Offset: 0x0021C897
		private static void FederationTrustNotificationHandler()
		{
			ExternalAuthentication.ConfigurationTracer.TraceDebug(0L, "Changes detected in federation trust object in AD.");
			ExternalAuthentication.instance = null;
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x0021E6B0 File Offset: 0x0021C8B0
		private void ThrowIfNotEnabled()
		{
			if (!this.Enabled)
			{
				ExternalAuthentication.ConfigurationTracer.TraceError(0L, "Throwing exception because callers is trying to use members of ExternalAuthentication that is not enabled.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0400545C RID: 21596
		private static readonly Trace ConfigurationTracer = ExTraceGlobals.ConfigurationTracer;

		// Token: 0x0400545D RID: 21597
		private static ExternalAuthentication instance;

		// Token: 0x0400545E RID: 21598
		private static ExEventLog eventLogger;

		// Token: 0x0400545F RID: 21599
		private static bool subscribedForNotification;

		// Token: 0x04005460 RID: 21600
		private static object subscribedForNotificationLocker = new object();

		// Token: 0x04005461 RID: 21601
		private static Uri currentWebProxy;

		// Token: 0x04005462 RID: 21602
		private Dictionary<ADObjectId, SecurityTokenService> securityTokenServices;

		// Token: 0x04005463 RID: 21603
		private TokenValidator tokenValidator;

		// Token: 0x04005464 RID: 21604
		private List<X509Certificate2> certificates;

		// Token: 0x04005465 RID: 21605
		private Uri applicationUri;

		// Token: 0x04005466 RID: 21606
		private ExternalAuthentication.ExternalAuthenticationFailureType failureType;

		// Token: 0x04005467 RID: 21607
		private ExternalAuthentication.ExternalAuthenticationSubFailureType subFailureType;

		// Token: 0x02000DEC RID: 3564
		public enum ExternalAuthenticationFailureType
		{
			// Token: 0x04005469 RID: 21609
			NoFailure,
			// Token: 0x0400546A RID: 21610
			UnknownFailure,
			// Token: 0x0400546B RID: 21611
			ErrorReadingFederationTrust,
			// Token: 0x0400546C RID: 21612
			InvalidFederationCertificate,
			// Token: 0x0400546D RID: 21613
			InvalidTokenIssuerCertificate,
			// Token: 0x0400546E RID: 21614
			MisconfiguredFederationTrust,
			// Token: 0x0400546F RID: 21615
			NoFederationTrust
		}

		// Token: 0x02000DED RID: 3565
		public enum ExternalAuthenticationSubFailureType
		{
			// Token: 0x04005471 RID: 21617
			NoFailure,
			// Token: 0x04005472 RID: 21618
			NoSubCode,
			// Token: 0x04005473 RID: 21619
			DirectoryReadError,
			// Token: 0x04005474 RID: 21620
			MissingTokenIssuerUri,
			// Token: 0x04005475 RID: 21621
			MissingTokenIssuerEpr,
			// Token: 0x04005476 RID: 21622
			MissingApplicationUri,
			// Token: 0x04005477 RID: 21623
			MissingTokenIssuerCertificate,
			// Token: 0x04005478 RID: 21624
			MissingOrgPrivCertificate,
			// Token: 0x04005479 RID: 21625
			CannotRetrieveOrgPrivCertificate,
			// Token: 0x0400547A RID: 21626
			CertificateNotInStore,
			// Token: 0x0400547B RID: 21627
			CertificateNoPrivateKey,
			// Token: 0x0400547C RID: 21628
			CertificateExpirationTimeError,
			// Token: 0x0400547D RID: 21629
			CertificatePrivateKeyCryptoError,
			// Token: 0x0400547E RID: 21630
			WarningApplicationUriSkipped
		}

		// Token: 0x02000DEE RID: 3566
		private sealed class FederationTrustResults
		{
			// Token: 0x170020CA RID: 8394
			// (get) Token: 0x06007A9D RID: 31389 RVA: 0x0021E6E7 File Offset: 0x0021C8E7
			// (set) Token: 0x06007A9E RID: 31390 RVA: 0x0021E6EF File Offset: 0x0021C8EF
			public ExternalAuthentication.ExternalAuthenticationFailureType FailureType { get; set; }

			// Token: 0x170020CB RID: 8395
			// (get) Token: 0x06007A9F RID: 31391 RVA: 0x0021E6F8 File Offset: 0x0021C8F8
			// (set) Token: 0x06007AA0 RID: 31392 RVA: 0x0021E700 File Offset: 0x0021C900
			public ExternalAuthentication.ExternalAuthenticationSubFailureType SubFailureType { get; set; }

			// Token: 0x170020CC RID: 8396
			// (get) Token: 0x06007AA1 RID: 31393 RVA: 0x0021E709 File Offset: 0x0021C909
			// (set) Token: 0x06007AA2 RID: 31394 RVA: 0x0021E711 File Offset: 0x0021C911
			public SecurityTokenService SecurityTokenService { get; set; }

			// Token: 0x170020CD RID: 8397
			// (get) Token: 0x06007AA3 RID: 31395 RVA: 0x0021E71A File Offset: 0x0021C91A
			// (set) Token: 0x06007AA4 RID: 31396 RVA: 0x0021E722 File Offset: 0x0021C922
			public IEnumerable<X509Certificate2> TokenSignatureCertificates { get; set; }

			// Token: 0x170020CE RID: 8398
			// (get) Token: 0x06007AA5 RID: 31397 RVA: 0x0021E72B File Offset: 0x0021C92B
			// (set) Token: 0x06007AA6 RID: 31398 RVA: 0x0021E733 File Offset: 0x0021C933
			public IEnumerable<X509Certificate2> TokenDecryptionCertificates { get; set; }
		}
	}
}

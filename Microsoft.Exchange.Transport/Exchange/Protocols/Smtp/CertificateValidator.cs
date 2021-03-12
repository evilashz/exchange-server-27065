using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F8 RID: 1016
	internal class CertificateValidator : ICertificateValidator
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x000BB528 File Offset: 0x000B9728
		public CertificateValidator(ChainEnginePool pool, CertificateValidationResultCache anonymousValidationResultCache, TransportAppConfig.SecureMailConfig config)
		{
			ArgumentValidator.ThrowIfNull("pool", pool);
			ArgumentValidator.ThrowIfNull("config", config);
			this.pool = pool;
			this.anonymousValidationResultCache = anonymousValidationResultCache;
			this.subjectAlternativeNameLimit = config.SubjectAlternativeNameLimit;
			this.treatTransientCRLFailuresAsSuccess = config.TreatCRLTransientFailuresAsSuccessEnabled;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000BB577 File Offset: 0x000B9777
		public bool MatchCertificateFqdns(SmtpDomainWithSubdomains domain, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession)
		{
			ArgumentValidator.ThrowIfNull("cert", cert);
			return CertificateValidator.MatchCertificateFqdns(new MatchableDomain(domain), this.GetFqdns(cert, logSession), options);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000BB599 File Offset: 0x000B9799
		public bool MatchCertificateFqdns(SmtpDomainWithSubdomains domain, IX509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession)
		{
			ArgumentValidator.ThrowIfNull("cert", cert);
			return CertificateValidator.MatchCertificateFqdns(new MatchableDomain(domain), this.GetFqdns(cert.Certificate, logSession), options);
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000BB5C0 File Offset: 0x000B97C0
		public string FindBestMatchingCertificateFqdn(MatchableDomain domain, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession)
		{
			ArgumentValidator.ThrowIfNull("cert", cert);
			int num = -1;
			return CertificateValidator.FindBestMatchingCertificateFqdn(domain, this.GetFqdns(cert, logSession), options, ref num);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000BB5EC File Offset: 0x000B97EC
		public bool FindBestMatchingCertificateFqdn<T>(MatchableDomainMap<Tuple<X500DistinguishedName, T>> domains, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession, out T matchedEntry, out string matchedCertName)
		{
			return CertificateValidator.FindBestMatchingCertificateFqdn<T>(domains, this.GetFqdns(cert, logSession), cert.IssuerName, options, out matchedEntry, out matchedCertName);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000BB608 File Offset: 0x000B9808
		public bool FindBestMatchingCertificateFqdn<T>(MatchableDomainMap<Tuple<X500DistinguishedName, T>> domains, IX509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession, out T matchedEntry, out string matchedCertName)
		{
			ArgumentValidator.ThrowIfNull("cert", cert);
			return CertificateValidator.FindBestMatchingCertificateFqdn<T>(domains, this.GetFqdns(cert.Certificate, logSession), cert.IssuerName, options, out matchedEntry, out matchedCertName);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000BB634 File Offset: 0x000B9834
		public ChainValidityStatus ChainValidateAsAnonymous(IX509Certificate2 cert, bool cacheOnlyUrlRetrieval)
		{
			if (cert == null)
			{
				return ChainValidityStatus.EmptyCertificate;
			}
			return this.ChainValidateAsAnonymous(cert.Certificate, cacheOnlyUrlRetrieval);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000BB648 File Offset: 0x000B9848
		public ChainValidityStatus ChainValidateAsAnonymous(X509Certificate2 cert, bool cacheOnlyUrlRetrieval)
		{
			if (cert == null)
			{
				return ChainValidityStatus.EmptyCertificate;
			}
			ChainValidityStatus chainValidityStatus;
			if (this.anonymousValidationResultCache != null && this.anonymousValidationResultCache.TryGetValue(cert, out chainValidityStatus))
			{
				return chainValidityStatus;
			}
			chainValidityStatus = this.ChainValidateInternal(null, cert, true, cacheOnlyUrlRetrieval);
			if (CertificateValidator.IsTransientCRLFailure(chainValidityStatus) && this.treatTransientCRLFailuresAsSuccess)
			{
				chainValidityStatus = this.ChainValidateInternal(null, cert, true, cacheOnlyUrlRetrieval);
			}
			if (this.anonymousValidationResultCache != null && !CertificateValidator.IsTransientCRLFailure(chainValidityStatus))
			{
				this.anonymousValidationResultCache.TryAdd(cert, chainValidityStatus);
			}
			return chainValidityStatus;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000BB6B9 File Offset: 0x000B98B9
		public bool ShouldTreatValidationResultAsSuccess(ChainValidityStatus status)
		{
			return status == ChainValidityStatus.Valid || (this.treatTransientCRLFailuresAsSuccess && CertificateValidator.IsTransientCRLFailure(status));
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000BB6D5 File Offset: 0x000B98D5
		private static bool IsTransientCRLFailure(ChainValidityStatus status)
		{
			return status == (ChainValidityStatus)2148081683U || status == (ChainValidityStatus)2148204814U || status == (ChainValidityStatus)2148081682U;
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000BB714 File Offset: 0x000B9914
		private static bool MatchCertificateFqdns(MatchableDomain domain, IEnumerable<string> certNames, MatchOptions options)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			ArgumentValidator.ThrowIfNull("certNames", certNames);
			return certNames.Any((string certName) => -1 != domain.MatchCertName(certName, options, -1));
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000BB764 File Offset: 0x000B9964
		private static string FindBestMatchingCertificateFqdn(MatchableDomain domain, IEnumerable<string> certNames, MatchOptions options, ref int wildcardMatchDotCount)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			ArgumentValidator.ThrowIfNull("certNames", certNames);
			string result = null;
			foreach (string text in certNames)
			{
				int num = domain.MatchCertName(text, options, wildcardMatchDotCount);
				int num2 = num;
				if (num2 != -1)
				{
					if (num2 == 2147483647)
					{
						wildcardMatchDotCount = int.MaxValue;
						return text;
					}
					if (num > wildcardMatchDotCount)
					{
						result = text;
						wildcardMatchDotCount = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000BB7F8 File Offset: 0x000B99F8
		private static bool FindBestMatchingCertificateFqdn<T>(IEnumerable<KeyValuePair<MatchableDomain, Tuple<X500DistinguishedName, T>>> domains, IList<string> certNames, X500DistinguishedName certIssuerDN, MatchOptions options, out T matchedEntry, out string matchedCertName)
		{
			ArgumentValidator.ThrowIfNull("domains", domains);
			matchedEntry = default(T);
			matchedCertName = null;
			int num = -1;
			foreach (KeyValuePair<MatchableDomain, Tuple<X500DistinguishedName, T>> keyValuePair in domains)
			{
				string text = CertificateValidator.FindBestMatchingCertificateFqdn(keyValuePair.Key, certNames, options, ref num);
				if (text != null && (keyValuePair.Value.Item1 == null || (certIssuerDN != null && string.Compare(certIssuerDN.Name, keyValuePair.Value.Item1.Name, StringComparison.OrdinalIgnoreCase) == 0)))
				{
					matchedCertName = text;
					matchedEntry = keyValuePair.Value.Item2;
					if (num == 2147483647)
					{
						return true;
					}
				}
			}
			return matchedCertName != null;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000BB8CC File Offset: 0x000B9ACC
		private ChainValidityStatus ChainValidateInternal(string domain, X509Certificate2 cert, bool validateAsAnonymous, bool cacheOnlyUrlRetrieval)
		{
			ArgumentValidator.ThrowIfNull("cert", cert);
			ChainBuildParameter parameter = new ChainBuildParameter(AndChainMatchIssuer.PkixKpServerAuth, TimeSpan.FromSeconds(10.0), false, TimeSpan.Zero);
			SSLChainPolicyParameters options = new SSLChainPolicyParameters(domain ?? "anydomain.com", ChainPolicyOptions.None, (domain == null) ? SSLPolicyAuthorizationOptions.IgnoreCertCNInvalid : SSLPolicyAuthorizationOptions.None, SSLPolicyAuthorizationType.Server);
			ChainBuildOptions options2 = ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout | (cacheOnlyUrlRetrieval ? ChainBuildOptions.CacheOnlyUrlRetrieval : ChainBuildOptions.DisableAia);
			ChainValidityStatus result;
			using (ChainEngine engine = this.pool.GetEngine())
			{
				ChainContext chainContext = validateAsAnonymous ? engine.BuildAsAnonymous(cert, options2, parameter) : engine.Build(cert, options2, parameter);
				if (chainContext == null)
				{
					result = (ChainValidityStatus)2148204810U;
				}
				else
				{
					using (chainContext)
					{
						ChainSummary chainSummary = chainContext.Validate(options);
						result = chainSummary.Status;
					}
				}
			}
			return result;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000BB9B4 File Offset: 0x000B9BB4
		private IList<string> GetFqdns(X509Certificate2 cert, IProtocolLogSession logSession)
		{
			int num;
			IList<string> fqdns = TlsCertificateInfo.GetFQDNs(cert, this.subjectAlternativeNameLimit, out num);
			if (num > this.subjectAlternativeNameLimit)
			{
				if (logSession != null)
				{
					logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Certificate '{0}' with {1} SANs exceeded SAN limit {2}; SANs ignored", new object[]
					{
						cert.Thumbprint,
						num,
						this.subjectAlternativeNameLimit
					});
				}
				Utils.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SubjectAlternativeNameLimitExceeded, cert.Thumbprint, new object[]
				{
					cert.Thumbprint,
					num,
					this.subjectAlternativeNameLimit
				});
			}
			return fqdns;
		}

		// Token: 0x040016ED RID: 5869
		public const int ExactMatchResult = 2147483647;

		// Token: 0x040016EE RID: 5870
		public const int NoMatchResult = -1;

		// Token: 0x040016EF RID: 5871
		private readonly bool treatTransientCRLFailuresAsSuccess;

		// Token: 0x040016F0 RID: 5872
		private readonly ChainEnginePool pool;

		// Token: 0x040016F1 RID: 5873
		private readonly CertificateValidationResultCache anonymousValidationResultCache;

		// Token: 0x040016F2 RID: 5874
		private readonly int subjectAlternativeNameLimit;
	}
}

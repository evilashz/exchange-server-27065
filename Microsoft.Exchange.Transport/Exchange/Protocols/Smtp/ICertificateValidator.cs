using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F7 RID: 1015
	internal interface ICertificateValidator
	{
		// Token: 0x06002E71 RID: 11889
		bool MatchCertificateFqdns(SmtpDomainWithSubdomains domain, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession);

		// Token: 0x06002E72 RID: 11890
		bool MatchCertificateFqdns(SmtpDomainWithSubdomains domain, IX509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession);

		// Token: 0x06002E73 RID: 11891
		string FindBestMatchingCertificateFqdn(MatchableDomain domain, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession);

		// Token: 0x06002E74 RID: 11892
		bool FindBestMatchingCertificateFqdn<T>(MatchableDomainMap<Tuple<X500DistinguishedName, T>> domains, X509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession, out T matchedEntry, out string matchedCertName);

		// Token: 0x06002E75 RID: 11893
		bool FindBestMatchingCertificateFqdn<T>(MatchableDomainMap<Tuple<X500DistinguishedName, T>> domains, IX509Certificate2 cert, MatchOptions options, IProtocolLogSession logSession, out T matchedEntry, out string matchedCertName);

		// Token: 0x06002E76 RID: 11894
		ChainValidityStatus ChainValidateAsAnonymous(IX509Certificate2 cert, bool cacheOnlyUrlRetrieval);

		// Token: 0x06002E77 RID: 11895
		ChainValidityStatus ChainValidateAsAnonymous(X509Certificate2 cert, bool cacheOnlyUrlRetrieval);

		// Token: 0x06002E78 RID: 11896
		bool ShouldTreatValidationResultAsSuccess(ChainValidityStatus status);
	}
}

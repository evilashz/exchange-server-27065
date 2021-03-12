using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000148 RID: 328
	internal interface ICertificateCache
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EA0 RID: 3744
		X509Certificate2 EphemeralInternalTransportCertificate { get; }

		// Token: 0x06000EA1 RID: 3745
		void Open(OpenFlags flags);

		// Token: 0x06000EA2 RID: 3746
		void Close();

		// Token: 0x06000EA3 RID: 3747
		void Reset();

		// Token: 0x06000EA4 RID: 3748
		X509Certificate2 Find(IEnumerable<string> names, bool wildcardAllowed);

		// Token: 0x06000EA5 RID: 3749
		X509Certificate2 Find(IEnumerable<string> names, bool wildcardAllowed, WildcardMatchType wildcardMatchType);

		// Token: 0x06000EA6 RID: 3750
		bool TryFind(IEnumerable<string> names, bool wildcardAllowed, WildcardMatchType wildcardMatchType, out IX509Certificate2 certificate);

		// Token: 0x06000EA7 RID: 3751
		IX509Certificate2 FindByThumbprint(string thumbprint);

		// Token: 0x06000EA8 RID: 3752
		X509Certificate2 Find(string thumbprint);

		// Token: 0x06000EA9 RID: 3753
		X509Certificate2 Find(SmtpX509Identifier x509Identifier);

		// Token: 0x06000EAA RID: 3754
		bool TryFind(SmtpX509Identifier x509Identifier, out IX509Certificate2 certificate);

		// Token: 0x06000EAB RID: 3755
		X509Certificate2 GetInternalTransportCertificate(string thumbprint, ExEventLog logger);

		// Token: 0x06000EAC RID: 3756
		IX509Certificate2 GetInternalTransportCertificate(string thumbprint, IExEventLog logger);
	}
}

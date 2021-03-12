using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090C RID: 2316
	internal interface IExchangeCertificate
	{
		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06005220 RID: 21024
		string Subject { get; }

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06005221 RID: 21025
		string Issuer { get; }

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06005222 RID: 21026
		string Thumbprint { get; }

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06005223 RID: 21027
		bool IsSelfSigned { get; }

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06005224 RID: 21028
		DateTime NotAfter { get; }

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06005225 RID: 21029
		DateTime NotBefore { get; }

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06005226 RID: 21030
		IList<SmtpDomainWithSubdomains> CertificateDomains { get; }

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06005227 RID: 21031
		AllowedServices Services { get; }

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06005228 RID: 21032
		SmtpX509Identifier Identifier { get; }

		// Token: 0x06005229 RID: 21033
		bool Verify();
	}
}

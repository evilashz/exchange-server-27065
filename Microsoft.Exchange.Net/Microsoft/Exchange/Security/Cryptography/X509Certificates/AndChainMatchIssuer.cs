using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A89 RID: 2697
	internal class AndChainMatchIssuer : ChainMatchIssuer
	{
		// Token: 0x06003A41 RID: 14913 RVA: 0x00094D90 File Offset: 0x00092F90
		public AndChainMatchIssuer(Oid[] oids) : base(ChainMatchIssuer.Operator.And, oids)
		{
		}

		// Token: 0x04003276 RID: 12918
		public static AndChainMatchIssuer PkixKpServerAuth = new AndChainMatchIssuer(new Oid[]
		{
			WellKnownOid.PkixKpServerAuth
		});

		// Token: 0x04003277 RID: 12919
		public static AndChainMatchIssuer EmailProtection = new AndChainMatchIssuer(new Oid[]
		{
			WellKnownOid.EmailProtection
		});
	}
}

using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A8A RID: 2698
	internal class OrChainMatchIssuer : ChainMatchIssuer
	{
		// Token: 0x06003A43 RID: 14915 RVA: 0x00094DDD File Offset: 0x00092FDD
		public OrChainMatchIssuer(Oid[] oids) : base(ChainMatchIssuer.Operator.Or, oids)
		{
		}
	}
}

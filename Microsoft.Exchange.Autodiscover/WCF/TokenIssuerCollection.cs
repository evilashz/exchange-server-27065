using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B5 RID: 181
	[CollectionDataContract(Name = "TokenIssuers", ItemName = "TokenIssuer", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class TokenIssuerCollection : Collection<TokenIssuer>
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x000181F4 File Offset: 0x000163F4
		public TokenIssuerCollection()
		{
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000181FC File Offset: 0x000163FC
		public TokenIssuerCollection(IList<TokenIssuer> tokenIssuers) : base(tokenIssuers)
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00018208 File Offset: 0x00016408
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Count * 40);
			foreach (TokenIssuer tokenIssuer in this)
			{
				stringBuilder.AppendLine(tokenIssuer.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}

using System;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000074 RID: 116
	internal interface IQueryList
	{
		// Token: 0x06000313 RID: 787
		void Add(UserResultMapping userResultMapping);

		// Token: 0x06000314 RID: 788
		void Execute();
	}
}

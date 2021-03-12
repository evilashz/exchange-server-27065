using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA3 RID: 2979
	internal interface IServerSessionProvider
	{
		// Token: 0x06003FEE RID: 16366
		IServerSession Create(TokenValidationResults tokenValidationResults);
	}
}

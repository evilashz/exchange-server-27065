using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA4 RID: 2980
	internal interface IAuthorizationManager
	{
		// Token: 0x06003FEF RID: 16367
		bool CheckAccess(TokenValidationResults validationResults);
	}
}

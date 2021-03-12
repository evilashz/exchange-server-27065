using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E15 RID: 3605
	internal class InvalidUserException : ODataResponseException
	{
		// Token: 0x06005D49 RID: 23881 RVA: 0x00122F04 File Offset: 0x00121104
		public InvalidUserException(string userAddress) : base(HttpStatusCode.NotFound, ResponseCodeType.ErrorInvalidUser, CoreResources.ErrorInvalidRequestedUser(userAddress), null)
		{
		}
	}
}

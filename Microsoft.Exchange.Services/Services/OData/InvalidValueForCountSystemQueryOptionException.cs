using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E25 RID: 3621
	internal class InvalidValueForCountSystemQueryOptionException : ODataResponseException
	{
		// Token: 0x06005D65 RID: 23909 RVA: 0x001230EA File Offset: 0x001212EA
		public InvalidValueForCountSystemQueryOptionException() : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidValueForCountSystemQueryOption, CoreResources.ErrorInvalidValueForCountSystemQueryOption, null)
		{
		}
	}
}

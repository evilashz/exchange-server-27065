using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E24 RID: 3620
	internal class InvalidUrlQueryException : ODataResponseException
	{
		// Token: 0x06005D63 RID: 23907 RVA: 0x001230B8 File Offset: 0x001212B8
		public InvalidUrlQueryException(string parameter) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidUrlQuery, CoreResources.ErrorInvalidUrlQuery(parameter), null)
		{
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001230D1 File Offset: 0x001212D1
		public InvalidUrlQueryException(string parameter, Exception internalException) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidUrlQuery, CoreResources.ErrorInvalidUrlQuery(parameter), internalException)
		{
		}
	}
}

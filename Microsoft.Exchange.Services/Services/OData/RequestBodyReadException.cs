using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E13 RID: 3603
	internal class RequestBodyReadException : ODataResponseException
	{
		// Token: 0x06005D46 RID: 23878 RVA: 0x00122EBC File Offset: 0x001210BC
		public RequestBodyReadException(ODataException oDataException) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidRequest, CoreResources.ErrorCannotReadRequestBody, oDataException)
		{
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x00122ED4 File Offset: 0x001210D4
		public RequestBodyReadException(HttpRequestTransportException readException) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidRequest, CoreResources.ErrorCannotReadRequestBody, readException)
		{
		}
	}
}

using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E12 RID: 3602
	internal class RequestUnsupportedException : ODataResponseException
	{
		// Token: 0x06005D45 RID: 23877 RVA: 0x00122EA4 File Offset: 0x001210A4
		public RequestUnsupportedException() : base(HttpStatusCode.MethodNotAllowed, ResponseCodeType.ErrorInvalidRequest, CoreResources.ErrorUnsupportedODataRequest, null)
		{
		}
	}
}

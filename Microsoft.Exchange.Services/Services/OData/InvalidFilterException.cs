using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1F RID: 3615
	internal class InvalidFilterException : ODataResponseException
	{
		// Token: 0x06005D5C RID: 23900 RVA: 0x00123055 File Offset: 0x00121255
		public InvalidFilterException(LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidUrlQueryFilter, errorMessage, null)
		{
		}
	}
}

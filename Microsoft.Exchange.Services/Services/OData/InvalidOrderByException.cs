using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E22 RID: 3618
	internal class InvalidOrderByException : ODataResponseException
	{
		// Token: 0x06005D61 RID: 23905 RVA: 0x00123097 File Offset: 0x00121297
		public InvalidOrderByException(LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidUrlQuery, errorMessage, null)
		{
		}
	}
}

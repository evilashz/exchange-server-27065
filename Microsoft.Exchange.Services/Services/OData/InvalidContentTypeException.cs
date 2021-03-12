using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E14 RID: 3604
	internal class InvalidContentTypeException : ODataResponseException
	{
		// Token: 0x06005D48 RID: 23880 RVA: 0x00122EEC File Offset: 0x001210EC
		public InvalidContentTypeException(ODataContentTypeException innerException) : base(HttpStatusCode.NotAcceptable, ResponseCodeType.ErrorNotAcceptable, CoreResources.ErrorNotAcceptable, innerException)
		{
		}
	}
}

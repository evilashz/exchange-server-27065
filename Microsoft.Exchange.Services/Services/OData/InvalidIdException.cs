using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E18 RID: 3608
	internal class InvalidIdException : ODataResponseException
	{
		// Token: 0x06005D4F RID: 23887 RVA: 0x00122F67 File Offset: 0x00121167
		public InvalidIdException() : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidId, CoreResources.ErrorInvalidId, null)
		{
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00122F7F File Offset: 0x0012117F
		public InvalidIdException(LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidId, errorMessage, null)
		{
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x00122F93 File Offset: 0x00121193
		public InvalidIdException(ResponseCodeType errorCode, LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, errorCode.ToString(), errorMessage, null)
		{
		}
	}
}

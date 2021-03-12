using System;
using System.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E11 RID: 3601
	internal class UrlResolutionException : ODataResponseException
	{
		// Token: 0x06005D44 RID: 23876 RVA: 0x00122E8C File Offset: 0x0012108C
		public UrlResolutionException(ODataException odataException) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidRequest, CoreResources.ErrorCannotResolveODataUrl, odataException)
		{
		}
	}
}

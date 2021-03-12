using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000208 RID: 520
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestorReader
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0004E632 File Offset: 0x0004C832
		public PhotoPrincipal Read(HttpContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			return (PhotoPrincipal)context.Items["Photo.Requestor"];
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0004E654 File Offset: 0x0004C854
		public bool EnabledInFasterPhotoFlight(HttpContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			return Convert.ToBoolean(context.Items["Photo.Requestor.EnabledInFasterPhotoFlightHttpContextKey"]);
		}

		// Token: 0x04000A60 RID: 2656
		public const string HttpContextKey = "Photo.Requestor";

		// Token: 0x04000A61 RID: 2657
		public const string EnabledInFasterPhotoFlightHttpContextKey = "Photo.Requestor.EnabledInFasterPhotoFlightHttpContextKey";
	}
}

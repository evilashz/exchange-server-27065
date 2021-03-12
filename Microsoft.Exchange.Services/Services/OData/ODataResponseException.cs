using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E0C RID: 3596
	internal abstract class ODataResponseException : Exception
	{
		// Token: 0x06005D37 RID: 23863 RVA: 0x00122DC1 File Offset: 0x00120FC1
		public ODataResponseException(HttpStatusCode httpStatusCode, string errorCode, LocalizedString errorMessage, Exception innerException = null) : base(errorMessage, innerException)
		{
			this.HttpStatusCode = httpStatusCode;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x00122DDF File Offset: 0x00120FDF
		public ODataResponseException(HttpStatusCode httpStatusCode, ResponseCodeType errorCode, LocalizedString errorMessage, Exception innerException = null) : this(httpStatusCode, errorCode.ToString(), errorMessage, innerException)
		{
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06005D39 RID: 23865 RVA: 0x00122DF6 File Offset: 0x00120FF6
		// (set) Token: 0x06005D3A RID: 23866 RVA: 0x00122DFE File Offset: 0x00120FFE
		public HttpStatusCode HttpStatusCode { get; protected set; }

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06005D3B RID: 23867 RVA: 0x00122E07 File Offset: 0x00121007
		// (set) Token: 0x06005D3C RID: 23868 RVA: 0x00122E0F File Offset: 0x0012100F
		public string ErrorCode { get; protected set; }

		// Token: 0x06005D3D RID: 23869 RVA: 0x00122E18 File Offset: 0x00121018
		public virtual void AppendResponseHeader(HttpContext httpContext)
		{
		}
	}
}

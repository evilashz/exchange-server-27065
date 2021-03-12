using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000005 RID: 5
	[ExcludeFromCodeCoverage]
	internal class AutoDiscoverResponseException : Exception
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002453 File Offset: 0x00000653
		protected AutoDiscoverResponseException(HttpStatusCode httpStatusCode, string errorCode, string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
		{
			this.HttpStatusCodeValue = (int)httpStatusCode;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000246C File Offset: 0x0000066C
		private AutoDiscoverResponseException(HttpStatusCode httpStatusCode, string errorCode, Exception innerException) : base(innerException.Message, innerException)
		{
			this.HttpStatusCodeValue = (int)httpStatusCode;
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002489 File Offset: 0x00000689
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002491 File Offset: 0x00000691
		public int HttpStatusCodeValue { get; protected set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000249A File Offset: 0x0000069A
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024A2 File Offset: 0x000006A2
		public string ErrorCode { get; protected set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000024AB File Offset: 0x000006AB
		public static AutoDiscoverResponseException BadRequest(string errorCode, string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.BadRequest, errorCode, errorMessage, innerException);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024BA File Offset: 0x000006BA
		public static AutoDiscoverResponseException NotFound()
		{
			return new AutoDiscoverResponseException(HttpStatusCode.NotFound, "UserNotFound", "The given user is not found", null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024D1 File Offset: 0x000006D1
		public static AutoDiscoverResponseException NotFound(string errorCode, string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.NotFound, errorCode, errorMessage, innerException);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E0 File Offset: 0x000006E0
		public static AutoDiscoverResponseException ServiceUnavailable(string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.ServiceUnavailable, errorMessage, innerException);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024EE File Offset: 0x000006EE
		public static AutoDiscoverResponseException InternalServerError(string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.InternalServerError, "InternalServerError", errorMessage, innerException);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002501 File Offset: 0x00000701
		public static AutoDiscoverResponseException NotImplemented(string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.NotImplemented, "NotImplemented", errorMessage, innerException);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002514 File Offset: 0x00000714
		public static AutoDiscoverResponseException DomainNotFound(string errorMessage, Exception innerException = null)
		{
			return new AutoDiscoverResponseException(HttpStatusCode.NotFound, "DomainNotFound", errorMessage, innerException);
		}
	}
}

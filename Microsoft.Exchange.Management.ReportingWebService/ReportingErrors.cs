using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200002F RID: 47
	internal class ReportingErrors
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00005658 File Offset: 0x00003858
		static ReportingErrors()
		{
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorMissingTenantDomainInRequest] = HttpStatusCode.BadRequest;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorTenantNotInOrgScope] = HttpStatusCode.Forbidden;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorTenantNotResolved] = HttpStatusCode.NotFound;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.InvalidFormatQuery] = HttpStatusCode.BadRequest;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorSchemaInitializationFail] = HttpStatusCode.InternalServerError;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorOAuthPartnerApplicationNotLinkToServiceAccount] = HttpStatusCode.Forbidden;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorOAuthAuthorizationNoAccount] = HttpStatusCode.Forbidden;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorVersionAmbiguous] = HttpStatusCode.BadRequest;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorInvalidVersion] = HttpStatusCode.NotFound;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ErrorOverBudget] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ADTransientError] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ADOperationError] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.CreateRunspaceConfigTimeoutError] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.ConnectionFailedException] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.InvalidQueryException] = HttpStatusCode.BadRequest;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.DataMartTimeoutException] = HttpStatusCode.ServiceUnavailable;
			ReportingErrors.HttpStatusCodes[ReportingErrorCode.UnknownError] = HttpStatusCode.InternalServerError;
		}

		// Token: 0x0400007B RID: 123
		public static readonly Dictionary<ReportingErrorCode, HttpStatusCode> HttpStatusCodes = new Dictionary<ReportingErrorCode, HttpStatusCode>();
	}
}

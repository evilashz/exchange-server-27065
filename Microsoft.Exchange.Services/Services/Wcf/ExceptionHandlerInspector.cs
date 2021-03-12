using System;
using System.Security.Principal;
using System.ServiceModel;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB6 RID: 3510
	internal class ExceptionHandlerInspector : ExceptionHandlerBase
	{
		// Token: 0x06005952 RID: 22866 RVA: 0x0011723A File Offset: 0x0011543A
		static ExceptionHandlerInspector()
		{
			ExceptionHandlerBase.InternalServerErrorFaultCode = FaultCode.CreateReceiverFaultCode("InternalServerError", "http://schemas.microsoft.com/exchange/services/2006/errors");
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0011725D File Offset: 0x0011545D
		public static bool IsLocalAccount(SecurityIdentifier sid)
		{
			return sid.IsEqualDomainSid(ExceptionHandlerInspector.localSid);
		}

		// Token: 0x0400317F RID: 12671
		private static SecurityIdentifier localSid = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
	}
}

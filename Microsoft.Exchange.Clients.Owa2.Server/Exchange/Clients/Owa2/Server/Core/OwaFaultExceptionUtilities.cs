using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F1 RID: 497
	internal static class OwaFaultExceptionUtilities
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x000432E2 File Offset: 0x000414E2
		public static ServiceError GetServiceError(LocalizedException exception)
		{
			return OwaFaultExceptionUtilities.exceptionMapper.GetServiceError(exception);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000432EF File Offset: 0x000414EF
		public static FaultException CreateFault(LocalizedException exception)
		{
			return new OwaServiceFaultException(OwaFaultExceptionUtilities.GetServiceError(exception), exception);
		}

		// Token: 0x04000A51 RID: 2641
		private static ExceptionMapper exceptionMapper = new ExceptionMapper(ServiceErrors.GetExceptionMapper(), new ExceptionMappingBase[]
		{
			new OwaExceptionMapper(typeof(OwaException)),
			new OwaExceptionMapper(typeof(OwaPermanentException)),
			new OwaExceptionMapper(typeof(OwaTransientException))
		});
	}
}

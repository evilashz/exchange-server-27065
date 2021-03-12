using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000123 RID: 291
	internal class OwaServiceFaultException : FaultException
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x00022CC5 File Offset: 0x00020EC5
		internal OwaServiceFaultException(ServiceError serviceError, LocalizedException serviceException) : base(new FaultReason(serviceException.Message))
		{
			this.ServiceError = serviceError;
			this.ServiceException = serviceException;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00022CE6 File Offset: 0x00020EE6
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00022CEE File Offset: 0x00020EEE
		internal ServiceError ServiceError { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00022CF7 File Offset: 0x00020EF7
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00022CFF File Offset: 0x00020EFF
		internal LocalizedException ServiceException { get; private set; }
	}
}

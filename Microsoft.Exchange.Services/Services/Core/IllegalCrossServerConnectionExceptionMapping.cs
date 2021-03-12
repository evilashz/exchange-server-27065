using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021E RID: 542
	internal class IllegalCrossServerConnectionExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x0004522B File Offset: 0x0004342B
		public IllegalCrossServerConnectionExceptionMapping() : base(typeof(IllegalCrossServerConnectionException), ResponseCodeType.ErrorInternalServerTransientError, CoreResources.IDs.ErrorIllegalCrossServerConnection)
		{
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00045247 File Offset: 0x00043447
		protected override void DoServiceErrorPostProcessing(LocalizedException exception, ServiceError error)
		{
			EWSSettings.ExceptionType = exception.GetType().ToString();
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000229 RID: 553
	internal class SharingSynchronizationExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x00045821 File Offset: 0x00043A21
		public SharingSynchronizationExceptionMapping() : base(typeof(SharingSynchronizationException), ExchangeVersion.Exchange2010, ResponseCodeType.ErrorSharingSynchronizationFailed, (CoreResources.IDs)3469371317U)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00045844 File Offset: 0x00043A44
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			SharingSynchronizationException ex = base.VerifyExceptionType<SharingSynchronizationException>(exception);
			return new Dictionary<string, string>
			{
				{
					"ErrorDetails",
					ex.ErrorDetails
				}
			};
		}

		// Token: 0x04000AFF RID: 2815
		private const string ErrorDetails = "ErrorDetails";
	}
}

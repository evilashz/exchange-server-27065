using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200022A RID: 554
	internal class TimeZoneExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x00045871 File Offset: 0x00043A71
		public TimeZoneExceptionMapping() : base(typeof(TimeZoneException), ExchangeVersion.Exchange2010, ResponseCodeType.ErrorTimeZone, CoreResources.IDs.ErrorTimeZone)
		{
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00045894 File Offset: 0x00043A94
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			TimeZoneException ex = base.VerifyExceptionType<TimeZoneException>(exception);
			if (ex.ConstantValues.Count != 0)
			{
				return new Dictionary<string, string>(ex.ConstantValues);
			}
			return null;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000458C5 File Offset: 0x00043AC5
		public override LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return exception.LocalizedString;
		}
	}
}

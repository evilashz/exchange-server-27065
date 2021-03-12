using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000051 RID: 81
	internal class ExpiringADObjectIdValue : ExpiringValue<ADObjectId, ExpiringADObjectIdValue.ADObjectIdExpirationWindowProvider>
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00017ADF File Offset: 0x00015CDF
		internal ExpiringADObjectIdValue(ADObjectId value) : base(value)
		{
		}

		// Token: 0x02000052 RID: 82
		internal class ADObjectIdExpirationWindowProvider : IExpirationWindowProvider<ADObjectId>
		{
			// Token: 0x0600041F RID: 1055 RVA: 0x00017AE8 File Offset: 0x00015CE8
			TimeSpan IExpirationWindowProvider<ADObjectId>.GetExpirationWindow(ADObjectId unused)
			{
				return ExpiringADObjectIdValue.ADObjectIdExpirationWindowProvider.expirationWindow;
			}

			// Token: 0x04000171 RID: 369
			private static readonly TimeSpan expirationWindow = TimeSpan.FromHours(3.0);
		}
	}
}

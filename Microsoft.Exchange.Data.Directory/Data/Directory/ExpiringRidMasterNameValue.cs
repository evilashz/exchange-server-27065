using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200028A RID: 650
	internal class ExpiringRidMasterNameValue : ExpiringValue<string, ExpiringRidMasterNameValue.RidMasterNameExpirationWindowProvider>
	{
		// Token: 0x06001EB7 RID: 7863 RVA: 0x000898B1 File Offset: 0x00087AB1
		public ExpiringRidMasterNameValue(string value) : base(value)
		{
		}

		// Token: 0x0200028B RID: 651
		internal class RidMasterNameExpirationWindowProvider : IExpirationWindowProvider<string>
		{
			// Token: 0x06001EB8 RID: 7864 RVA: 0x000898BA File Offset: 0x00087ABA
			TimeSpan IExpirationWindowProvider<string>.GetExpirationWindow(string unused)
			{
				return ExpiringRidMasterNameValue.RidMasterNameExpirationWindowProvider.expirationWindow;
			}

			// Token: 0x04001262 RID: 4706
			private static readonly TimeSpan expirationWindow = TimeSpan.FromHours(1.0);
		}
	}
}

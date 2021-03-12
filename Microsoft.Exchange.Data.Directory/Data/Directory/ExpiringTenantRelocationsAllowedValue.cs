using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000286 RID: 646
	internal class ExpiringTenantRelocationsAllowedValue : ExpiringValue<bool, ExpiringTenantRelocationsAllowedValue.TenantRelocationsAllowedExpirationWindowProvider>
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x000897DB File Offset: 0x000879DB
		public ExpiringTenantRelocationsAllowedValue(bool value) : base(value)
		{
		}

		// Token: 0x02000287 RID: 647
		internal class TenantRelocationsAllowedExpirationWindowProvider : IExpirationWindowProvider<bool>
		{
			// Token: 0x06001EAD RID: 7853 RVA: 0x000897E4 File Offset: 0x000879E4
			TimeSpan IExpirationWindowProvider<bool>.GetExpirationWindow(bool unused)
			{
				return ExpiringTenantRelocationsAllowedValue.TenantRelocationsAllowedExpirationWindowProvider.expirationWindow;
			}

			// Token: 0x04001261 RID: 4705
			private static readonly TimeSpan expirationWindow = TimeSpan.FromHours(8.0);
		}
	}
}

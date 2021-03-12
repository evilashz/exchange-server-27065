using System;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000288 RID: 648
	internal class ExpiringTenantRelocationStateValue : ExpiringValue<TenantRelocationState, ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider>
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x00089808 File Offset: 0x00087A08
		public ExpiringTenantRelocationStateValue(TenantRelocationState value) : base(value)
		{
		}

		// Token: 0x02000289 RID: 649
		internal class TenantRelocationStateExpirationWindowProvider : IExpirationWindowProvider<TenantRelocationState>
		{
			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00089811 File Offset: 0x00087A11
			internal static TimeSpan DefaultExpirationWindow
			{
				get
				{
					return TimeSpan.FromMinutes((double)TenantRelocationConfigImpl.GetConfig<int>("DefaultRelocationCacheExpirationTimeInMinutes"));
				}
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00089823 File Offset: 0x00087A23
			internal static TimeSpan AggressiveExpirationWindow
			{
				get
				{
					return TimeSpan.FromMinutes((double)TenantRelocationConfigImpl.GetConfig<int>("AggressiveRelocationCacheExpirationTimeInMinutes"));
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x00089835 File Offset: 0x00087A35
			internal static TimeSpan ModerateExpirationWindow
			{
				get
				{
					return TimeSpan.FromMinutes((double)TenantRelocationConfigImpl.GetConfig<int>("ModerateRelocationCacheExpirationTimeInMinutes"));
				}
			}

			// Token: 0x06001EB4 RID: 7860 RVA: 0x00089847 File Offset: 0x00087A47
			TimeSpan IExpirationWindowProvider<TenantRelocationState>.GetExpirationWindow(TenantRelocationState value)
			{
				return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.GetExpirationWindow(value);
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x00089850 File Offset: 0x00087A50
			internal static TimeSpan GetExpirationWindow(TenantRelocationState value)
			{
				switch (value.SourceForestState)
				{
				case TenantRelocationStatus.NotStarted:
					return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.DefaultExpirationWindow;
				case TenantRelocationStatus.Synchronization:
				case TenantRelocationStatus.Lockdown:
					return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.AggressiveExpirationWindow;
				case TenantRelocationStatus.Retired:
					return ExpiringTenantRelocationStateValue.TenantRelocationStateExpirationWindowProvider.ModerateExpirationWindow;
				default:
					throw new NotSupportedException(value.SourceForestState.ToString());
				}
			}
		}
	}
}

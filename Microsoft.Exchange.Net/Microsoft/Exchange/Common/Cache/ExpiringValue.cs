using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000683 RID: 1667
	internal abstract class ExpiringValue<T, TProvider> : IExpiringValue where TProvider : IExpirationWindowProvider<T>, new()
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00037779 File Offset: 0x00035979
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x00037781 File Offset: 0x00035981
		public T Value { get; private set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0003778A File Offset: 0x0003598A
		// (set) Token: 0x06001E34 RID: 7732 RVA: 0x00037792 File Offset: 0x00035992
		public DateTime ExpirationTime { get; private set; }

		// Token: 0x06001E35 RID: 7733 RVA: 0x0003779C File Offset: 0x0003599C
		protected ExpiringValue(T value)
		{
			this.Value = value;
			TProvider tprovider = (default(TProvider) == null) ? Activator.CreateInstance<TProvider>() : default(TProvider);
			this.ExpirationTime = (DateTime)ExDateTime.Now + tprovider.GetExpirationWindow(value);
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000377FA File Offset: 0x000359FA
		public bool Expired
		{
			get
			{
				return this.ExpirationTime < (DateTime)ExDateTime.Now;
			}
		}
	}
}

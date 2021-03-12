using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000063 RID: 99
	internal sealed class BposAssetCache<T> : LazyLookupTimeoutCache<string, T> where T : class
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000D462 File Offset: 0x0000B662
		internal BposAssetCache() : base(5, 100, false, TimeSpan.FromDays(1.0), TimeSpan.FromDays(7.0))
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000D48A File Offset: 0x0000B68A
		internal static BposAssetCache<T> Instance
		{
			get
			{
				return BposAssetCache<T>.instance;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D491 File Offset: 0x0000B691
		internal void Add(string key, T value)
		{
			base.Remove(key);
			this.TryPerformAdd(key, value);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		protected override T CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			shouldAdd = false;
			return default(T);
		}

		// Token: 0x04000196 RID: 406
		private static BposAssetCache<T> instance = new BposAssetCache<T>();
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000007 RID: 7
	internal interface ICountedEntityWrapper<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000016 RID: 22
		ICountedEntity<TEntityType> Entity { get; }

		// Token: 0x06000017 RID: 23
		ICountedConfig GetConfig(TCountType measure);

		// Token: 0x06000018 RID: 24
		void AddUsage(TCountType measure, ICountedConfig config, int increment);

		// Token: 0x06000019 RID: 25
		Task AddUsageAsync(TCountType measure, ICountedConfig config, int increment);

		// Token: 0x0600001A RID: 26
		bool TrySetUsage(TCountType measure, int value);

		// Token: 0x0600001B RID: 27
		Task<bool> SetUsageAsync(TCountType measure, int value);

		// Token: 0x0600001C RID: 28
		ICount<TEntityType, TCountType> GetUsage(TCountType measure);

		// Token: 0x0600001D RID: 29
		Task<ICount<TEntityType, TCountType>> GetUsageAsync(TCountType measure);

		// Token: 0x0600001E RID: 30
		IDictionary<TCountType, ICount<TEntityType, TCountType>> GetUsage(TCountType[] measures);

		// Token: 0x0600001F RID: 31
		Task<IDictionary<TCountType, ICount<TEntityType, TCountType>>> GetUsageAsync(TCountType[] measures);

		// Token: 0x06000020 RID: 32
		IEnumerable<ICount<TEntityType, TCountType>> GetAllUsages();

		// Token: 0x06000021 RID: 33
		Task<IEnumerable<ICount<TEntityType, TCountType>>> GetAllUsagesAsync();
	}
}

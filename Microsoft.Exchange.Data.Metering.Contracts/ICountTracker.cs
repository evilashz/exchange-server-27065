using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000008 RID: 8
	internal interface ICountTracker<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000022 RID: 34
		void AddUsage(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig countConfig, long increment);

		// Token: 0x06000023 RID: 35
		Task AddUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig countConfig, long increment);

		// Token: 0x06000024 RID: 36
		bool TrySetUsage(ICountedEntity<TEntityType> entity, TCountType measure, long value);

		// Token: 0x06000025 RID: 37
		Task<bool> SetUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure, long value);

		// Token: 0x06000026 RID: 38
		ICount<TEntityType, TCountType> GetUsage(ICountedEntity<TEntityType> entity, TCountType measure);

		// Token: 0x06000027 RID: 39
		Task<ICount<TEntityType, TCountType>> GetUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure);

		// Token: 0x06000028 RID: 40
		IDictionary<TCountType, ICount<TEntityType, TCountType>> GetUsage(ICountedEntity<TEntityType> entity, TCountType[] measures);

		// Token: 0x06000029 RID: 41
		Task<IDictionary<TCountType, ICount<TEntityType, TCountType>>> GetUsageAsync(ICountedEntity<TEntityType> entity, TCountType[] measures);

		// Token: 0x0600002A RID: 42
		IEnumerable<ICount<TEntityType, TCountType>> GetAllUsages(ICountedEntity<TEntityType> entity);

		// Token: 0x0600002B RID: 43
		Task<IEnumerable<ICount<TEntityType, TCountType>>> GetAllUsagesAsync(ICountedEntity<TEntityType> entity);

		// Token: 0x0600002C RID: 44
		bool TryGetEntityObject(ICountedEntity<TEntityType> entity, out ICountedEntityWrapper<TEntityType, TCountType> wrapper);

		// Token: 0x0600002D RID: 45
		ICountedConfig GetConfig(ICountedEntity<TEntityType> entity, TCountType measure);

		// Token: 0x0600002E RID: 46
		IEnumerable<ICount<TEntityType, TCountType>> Filter(Func<ICount<TEntityType, TCountType>, bool> isMatch);

		// Token: 0x0600002F RID: 47
		Task<IEnumerable<ICount<TEntityType, TCountType>>> FilterAsync(Func<ICount<TEntityType, TCountType>, bool> isMatch);

		// Token: 0x06000030 RID: 48
		IEnumerable<ICountedEntityValue<TEntityType, TCountType>> Filter(Func<ICountedEntityValue<TEntityType, TCountType>, bool> isMatch);

		// Token: 0x06000031 RID: 49
		Task<IEnumerable<ICountedEntityValue<TEntityType, TCountType>>> FilterAsync(Func<ICountedEntityValue<TEntityType, TCountType>, bool> isMatch);

		// Token: 0x06000032 RID: 50
		void GetDiagnosticInfo(string argument, XElement element);

		// Token: 0x06000033 RID: 51
		XElement GetDiagnosticInfo(IEntityName<TEntityType> entity);
	}
}

using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000008 RID: 8
	internal static class CountFactory
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000030EA File Offset: 0x000012EA
		public static Count<TEntityType, TCountType> CreateCount<TEntityType, TCountType>(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig config) where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
		{
			return CountFactory.CreateCount<TEntityType, TCountType>(entity, measure, config, () => DateTime.UtcNow);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003100 File Offset: 0x00001300
		public static Count<TEntityType, TCountType> CreateCount<TEntityType, TCountType>(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig config, Func<DateTime> timeProvider) where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
		{
			if (config is IRollingCountConfig)
			{
				return new RollingCount<TEntityType, TCountType>(entity, (IRollingCountConfig)config, measure, timeProvider);
			}
			if (config is IAbsoluteCountConfig)
			{
				return new AbsoluteCount<TEntityType, TCountType>(entity, (IAbsoluteCountConfig)config, measure, timeProvider);
			}
			throw new InvalidOperationException("Need to create a config of a subtype class of ICountedConfig");
		}
	}
}

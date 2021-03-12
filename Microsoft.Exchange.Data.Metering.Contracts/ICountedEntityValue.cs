using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000006 RID: 6
	internal interface ICountedEntityValue<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000013 RID: 19
		ICountedEntity<TEntityType> Entity { get; }

		// Token: 0x06000014 RID: 20
		ICount<TEntityType, TCountType> GetUsage(TCountType measure);

		// Token: 0x06000015 RID: 21
		bool HasUsage(TCountType measure);
	}
}

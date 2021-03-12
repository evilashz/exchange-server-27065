using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000B RID: 11
	internal interface ICountTrackerDiagnostics<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000038 RID: 56
		void EntityAdded(ICountedEntity<TEntityType> entity);

		// Token: 0x06000039 RID: 57
		void EntityRemoved(ICountedEntity<TEntityType> entity);

		// Token: 0x0600003A RID: 58
		void MeasureAdded(TCountType measure);

		// Token: 0x0600003B RID: 59
		void MeasureRemoved(TCountType measure);

		// Token: 0x0600003C RID: 60
		void MeasurePromoted(TCountType measure);

		// Token: 0x0600003D RID: 61
		void MeasureExpired(TCountType measure);
	}
}

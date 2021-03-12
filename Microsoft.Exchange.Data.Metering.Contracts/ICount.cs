using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000004 RID: 4
	internal interface ICount<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000008 RID: 8
		ICountedEntity<TEntityType> Entity { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000009 RID: 9
		ICountedConfig Config { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000A RID: 10
		TCountType Measure { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000B RID: 11
		bool IsPromoted { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000C RID: 12
		long Total { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000D RID: 13
		long Average { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600000E RID: 14
		ITrendline Trend { get; }

		// Token: 0x0600000F RID: 15
		bool TryGetObject(string key, out object value);

		// Token: 0x06000010 RID: 16
		void SetObject(string key, object value);
	}
}

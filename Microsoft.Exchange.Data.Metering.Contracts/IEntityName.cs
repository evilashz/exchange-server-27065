using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000C RID: 12
	internal interface IEntityName<TEntityType> : IEquatable<IEntityName<TEntityType>> where TEntityType : struct, IConvertible
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62
		TEntityType Type { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63
		string Value { get; }
	}
}

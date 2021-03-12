using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000005 RID: 5
	internal interface ICountedEntity<TEntityType> : IEquatable<ICountedEntity<TEntityType>> where TEntityType : struct, IConvertible
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000011 RID: 17
		IEntityName<TEntityType> GroupName { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18
		IEntityName<TEntityType> Name { get; }
	}
}

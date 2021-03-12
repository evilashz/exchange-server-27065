using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000834 RID: 2100
	internal static class SyncPropertyFactory
	{
		// Token: 0x06006825 RID: 26661 RVA: 0x0016EF24 File Offset: 0x0016D124
		internal static object Create(Type type, object value, bool multiValued)
		{
			return SyncPropertyFactory.GetFactoryInstance(type).Create(value, multiValued);
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x0016EF33 File Offset: 0x0016D133
		internal static object CreateDefault(Type type, bool multiValued)
		{
			return SyncPropertyFactory.GetFactoryInstance(type).GetDefault(multiValued);
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x0016EF88 File Offset: 0x0016D188
		private static IGenericSyncPropertyFactory GetFactoryInstance(Type type)
		{
			return SyncPropertyFactory.factoryInstances.AddIfNotExists(type, delegate(Type param0)
			{
				Type type2 = typeof(GenericSyncPropertyFactory<>).MakeGenericType(new Type[]
				{
					type
				});
				return (IGenericSyncPropertyFactory)Activator.CreateInstance(type2, true);
			});
		}

		// Token: 0x0400448D RID: 17549
		private static SynchronizedDictionary<Type, IGenericSyncPropertyFactory> factoryInstances = new SynchronizedDictionary<Type, IGenericSyncPropertyFactory>();
	}
}

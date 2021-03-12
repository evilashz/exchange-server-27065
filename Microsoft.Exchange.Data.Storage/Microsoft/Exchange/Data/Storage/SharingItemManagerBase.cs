using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB4 RID: 3508
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SharingItemManagerBase<TDataObject>
	{
		// Token: 0x06007893 RID: 30867 RVA: 0x002142F5 File Offset: 0x002124F5
		protected static T TryGetPropertyRef<T>(object[] properties, int index) where T : class
		{
			return properties[index] as T;
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x00214304 File Offset: 0x00212504
		protected static T? TryGetPropertyVal<T>(object[] properties, int index) where T : struct
		{
			object obj = properties[index];
			if (obj != null && typeof(T).GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo()))
			{
				return (T?)obj;
			}
			return null;
		}

		// Token: 0x06007895 RID: 30869
		protected abstract void StampItemFromDataObject(Item item, TDataObject data);

		// Token: 0x06007896 RID: 30870
		protected abstract TDataObject CreateDataObjectFromItem(object[] properties);
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000835 RID: 2101
	internal class GenericSyncPropertyFactory<T> : IGenericSyncPropertyFactory
	{
		// Token: 0x06006829 RID: 26665 RVA: 0x0016EFCC File Offset: 0x0016D1CC
		internal GenericSyncPropertyFactory()
		{
			this.defaultValueSingle = SyncProperty<T>.NoChange;
			ISyncProperty syncProperty;
			if (!(typeof(T) == typeof(ProxyAddress)))
			{
				ISyncProperty noChange = SyncProperty<MultiValuedProperty<T>>.NoChange;
				syncProperty = noChange;
			}
			else
			{
				syncProperty = SyncProperty<ProxyAddressCollection>.NoChange;
			}
			this.defaultValueMVP = syncProperty;
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x0016F01C File Offset: 0x0016D21C
		public object Create(object value, bool multiValued)
		{
			if (!multiValued)
			{
				return new SyncProperty<T>((T)((object)value));
			}
			if (typeof(T) == typeof(ProxyAddress))
			{
				return new SyncProperty<ProxyAddressCollection>((ProxyAddressCollection)value);
			}
			return new SyncProperty<MultiValuedProperty<T>>((MultiValuedProperty<T>)value);
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0016F06A File Offset: 0x0016D26A
		public object GetDefault(bool multiValued)
		{
			if (!multiValued)
			{
				return this.defaultValueSingle;
			}
			return this.defaultValueMVP;
		}

		// Token: 0x0400448E RID: 17550
		private readonly ISyncProperty defaultValueSingle;

		// Token: 0x0400448F RID: 17551
		private readonly ISyncProperty defaultValueMVP;
	}
}

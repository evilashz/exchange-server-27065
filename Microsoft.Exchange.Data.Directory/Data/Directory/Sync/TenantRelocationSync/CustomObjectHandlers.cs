using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F1 RID: 2033
	internal class CustomObjectHandlers
	{
		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x0600648F RID: 25743 RVA: 0x0015CAEC File Offset: 0x0015ACEC
		internal static CustomObjectHandlers Instance
		{
			get
			{
				return CustomObjectHandlers.instance.Value;
			}
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x0015CAF8 File Offset: 0x0015ACF8
		internal bool TryGetValue(TenantRelocationSyncObject obj, out ICustomObjectHandler handler)
		{
			bool flag = false;
			handler = null;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)obj[ADObjectSchema.ObjectClass];
			if (multiValuedProperty != null)
			{
				foreach (string key in multiValuedProperty)
				{
					flag = this.handlers.TryGetValue(key, out handler);
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x040042D9 RID: 17113
		private static readonly Lazy<CustomObjectHandlers> instance = new Lazy<CustomObjectHandlers>(() => new CustomObjectHandlers());

		// Token: 0x040042DA RID: 17114
		private Dictionary<string, ICustomObjectHandler> handlers = new Dictionary<string, ICustomObjectHandler>(StringComparer.InvariantCultureIgnoreCase)
		{
			{
				ExchangeConfigurationUnit.MostDerivedClass,
				ExchangeConfigurationUnitHandler.Instance
			}
		};
	}
}

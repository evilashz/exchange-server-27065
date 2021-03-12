using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000042 RID: 66
	internal static class IdConverters
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00017F28 File Offset: 0x00016128
		internal static bool GetClientId(StoreSession propertyMappingReference, ICorePropertyBag propertyBag, PropertyTag property, out PropertyValue clientValue)
		{
			foreach (IdConverter idConverter in IdConverters.converters)
			{
				if (idConverter.GetClientId(propertyMappingReference, propertyBag, property, out clientValue))
				{
					return true;
				}
			}
			clientValue = default(PropertyValue);
			return false;
		}

		// Token: 0x04000103 RID: 259
		private static readonly IdConverter[] converters = new IdConverter[]
		{
			new FolderIdConverter(),
			new MessageIdConverter()
		};
	}
}

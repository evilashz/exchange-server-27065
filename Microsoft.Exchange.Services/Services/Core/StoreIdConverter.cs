using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BF RID: 191
	internal class StoreIdConverter : SimpleIdConverterBase
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
		internal override StoreObjectId ConvertStringToStoreObjectId(string idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				ExTraceGlobals.ConvertIdCallTracer.TraceDebug((long)this.GetHashCode(), "[StoreIdConverter::ConvertStringToStoreObjectId] string idValue passed in was either null or empty");
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			StoreObjectId result;
			try
			{
				result = StoreObjectId.Deserialize(idValue);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException);
			}
			return result;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001C52C File Offset: 0x0001A72C
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			return storeObjectId.ToBase64String();
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001C534 File Offset: 0x0001A734
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.StoreId;
			}
		}
	}
}

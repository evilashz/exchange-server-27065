using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BB RID: 187
	internal class EntryIdConverter : SimpleIdConverterBase
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		internal override StoreObjectId ConvertStringToStoreObjectId(string idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				ExTraceGlobals.ConvertIdCallTracer.TraceDebug((long)this.GetHashCode(), "[EntryIdConverter::ConvertStringToStoreObjectId] string idValue passed in was either null or empty");
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			StoreObjectId result;
			try
			{
				byte[] entryId = Convert.FromBase64String(idValue);
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(entryId);
				result = storeObjectId;
			}
			catch (FormatException innerException)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException2);
			}
			return result;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001BF58 File Offset: 0x0001A158
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			return Convert.ToBase64String(storeObjectId.ProviderLevelItemId);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001BF65 File Offset: 0x0001A165
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.EntryId;
			}
		}
	}
}

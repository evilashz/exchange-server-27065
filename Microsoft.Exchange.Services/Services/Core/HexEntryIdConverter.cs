using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BC RID: 188
	internal class HexEntryIdConverter : SimpleIdConverterBase
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0001BF70 File Offset: 0x0001A170
		internal override StoreObjectId ConvertStringToStoreObjectId(string idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			StoreObjectId result;
			try
			{
				ByteArray byteArray = ByteArray.Parse(idValue);
				result = StoreObjectId.FromProviderSpecificId(byteArray.Bytes);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException);
			}
			return result;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			byte[] providerLevelItemId = storeObjectId.ProviderLevelItemId;
			ByteArray byteArray = new ByteArray(providerLevelItemId);
			return byteArray.ToString();
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001BFF1 File Offset: 0x0001A1F1
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.HexEntryId;
			}
		}
	}
}

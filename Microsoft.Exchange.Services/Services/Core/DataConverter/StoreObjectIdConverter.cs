using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FC RID: 508
	internal sealed class StoreObjectIdConverter : BaseConverter
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00043364 File Offset: 0x00041564
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00043367 File Offset: 0x00041567
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00043370 File Offset: 0x00041570
		public override object ConvertToServiceObjectValue(object propertyValue, IdConverterWithCommandSettings idConverterWithCommandSettings)
		{
			if (propertyValue == null)
			{
				return null;
			}
			StoreObjectId storeobjectId = (StoreObjectId)propertyValue;
			ConcatenatedIdAndChangeKey concatenatedId = idConverterWithCommandSettings.GetConcatenatedId(storeobjectId);
			return new FolderId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
		}
	}
}

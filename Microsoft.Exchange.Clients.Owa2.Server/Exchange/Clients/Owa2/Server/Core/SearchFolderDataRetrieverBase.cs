using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001AD RID: 429
	internal class SearchFolderDataRetrieverBase
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x0003B488 File Offset: 0x00039688
		protected static bool IsPropertyDefined(object[] row, int index)
		{
			return row[index] != null && !(row[index] is PropertyError);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003B49C File Offset: 0x0003969C
		protected static T GetItemProperty<T>(object[] row, int index)
		{
			return SearchFolderDataRetrieverBase.GetItemProperty<T>(row, index, default(T));
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003B4BC File Offset: 0x000396BC
		protected static T GetItemProperty<T>(object[] row, int index, T defaultValue)
		{
			if (!SearchFolderDataRetrieverBase.IsPropertyDefined(row, index))
			{
				return defaultValue;
			}
			object obj = row[index];
			if (!(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003B4E8 File Offset: 0x000396E8
		protected static string GetDateTimeProperty(ExTimeZone timeZone, object[] row, int index)
		{
			ExDateTime itemProperty = SearchFolderDataRetrieverBase.GetItemProperty<ExDateTime>(row, index, ExDateTime.MinValue);
			if (ExDateTime.MinValue.Equals(itemProperty))
			{
				return null;
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			return ExDateTimeConverter.ToOffsetXsdDateTime(itemProperty, timeZone);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003B52C File Offset: 0x0003972C
		protected static ItemId StoreIdToEwsItemId(StoreId storeId, MailboxId mailboxId)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			return new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003B555 File Offset: 0x00039755
		protected static string GetEwsId(StoreId storeId, Guid mailboxGuid)
		{
			if (storeId == null)
			{
				return null;
			}
			return StoreId.StoreIdToEwsId(mailboxGuid, storeId);
		}
	}
}

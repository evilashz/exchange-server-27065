using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SendersInMailFolder
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00016736 File Offset: 0x00014936
		public SendersInMailFolder(Folder folder)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			this.folder = folder;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00016750 File Offset: 0x00014950
		public QueryResult GetQueryResultGroupedBySender(PropertyDefinition[] properties)
		{
			return this.folder.GroupedItemQuery(null, ItemQueryType.None, SendersInMailFolder.GroupAndSortBySmtpAddress, 0, null, properties);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016988 File Offset: 0x00014B88
		public IEnumerable<IStorePropertyBag> GetSendersFromMailFolder(PropertyDefinition[] properties)
		{
			QueryResult queryResult = this.GetQueryResultGroupedBySender(properties);
			for (;;)
			{
				IStorePropertyBag[] results = queryResult.GetPropertyBags(10000);
				if (results == null || results.Length == 0)
				{
					break;
				}
				foreach (IStorePropertyBag result in results)
				{
					yield return result;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x04000647 RID: 1607
		private readonly Folder folder;

		// Token: 0x04000648 RID: 1608
		private static readonly GroupByAndOrder[] GroupAndSortBySmtpAddress = new GroupByAndOrder[]
		{
			new GroupByAndOrder(MessageItemSchema.SenderSmtpAddress, new GroupSort(MessageItemSchema.SenderSmtpAddress, SortOrder.Ascending, Aggregate.Min))
		};
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200034D RID: 845
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleIKnowMarkAllAsRead
	{
		// Token: 0x060017CC RID: 6092 RVA: 0x0007FA4C File Offset: 0x0007DC4C
		public PeopleIKnowMarkAllAsRead(MailboxSession mailboxSession, StoreId folderId, string senderSmtpAddress, bool suppressReadReceipts, ITracer tracer)
		{
			ServiceCommandBase.ThrowIfNull(mailboxSession, "mailboxSession", "PeopleIKnowMarkAllAsRead.PeopleIKnowMarkAllAsRead");
			ServiceCommandBase.ThrowIfNull(folderId, "folderId", "PeopleIKnowMarkAllAsRead.PeopleIKnowMarkAllAsRead");
			ServiceCommandBase.ThrowIfNullOrEmpty(senderSmtpAddress, "senderSmtpAddress", "PeopleIKnowMarkAllAsRead.PeopleIKnowMarkAllAsRead");
			ServiceCommandBase.ThrowIfNull(tracer, "tracer", "PeopleIKnowMarkAllAsRead.PeopleIKnowMarkAllAsRead");
			this.mailboxSession = mailboxSession;
			this.folderId = folderId;
			this.senderSmtpAddress = senderSmtpAddress;
			this.suppressReadReceipts = suppressReadReceipts;
			this.tracer = tracer;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0007FAD4 File Offset: 0x0007DCD4
		public void Execute()
		{
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				PeopleIKnowQuery.GetItemQueryFilter(this.senderSmtpAddress),
				PeopleIKnowMarkAllAsRead.UnreadMessageFilter
			});
			using (Folder folder = Folder.Bind(this.mailboxSession, this.folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, PeopleIKnowQuery.GetItemQuerySortBy(PeopleIKnowMarkAllAsRead.EmptySortBy), PeopleIKnowMarkAllAsRead.ItemQueryProperties))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
					while (propertyBags != null && propertyBags.Length > 0)
					{
						this.tracer.TraceDebug<int>((long)this.GetHashCode(), "PeopleIKnowMarkAllAsRead.Execute. Unread messages count: {0}", propertyBags.Length);
						ICollection<StoreId> source = from bag in propertyBags
						select bag.GetValueOrDefault<StoreId>(ItemSchema.Id, null);
						folder.MarkAsRead(this.suppressReadReceipts, source.ToArray<StoreId>());
						propertyBags = queryResult.GetPropertyBags(100);
					}
				}
			}
		}

		// Token: 0x04000FF9 RID: 4089
		internal const int ChunkSize = 100;

		// Token: 0x04000FFA RID: 4090
		private static readonly ComparisonFilter UnreadMessageFilter = new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsRead, false);

		// Token: 0x04000FFB RID: 4091
		public static readonly PropertyDefinition[] ItemQueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000FFC RID: 4092
		private static readonly SortBy[] EmptySortBy = new SortBy[0];

		// Token: 0x04000FFD RID: 4093
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000FFE RID: 4094
		private readonly StoreId folderId;

		// Token: 0x04000FFF RID: 4095
		private readonly string senderSmtpAddress;

		// Token: 0x04001000 RID: 4096
		private readonly bool suppressReadReceipts;

		// Token: 0x04001001 RID: 4097
		private readonly ITracer tracer;
	}
}

using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000BA RID: 186
	internal sealed class StoreOperations : IStoreOperations
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0001FCAB File Offset: 0x0001DEAB
		public StoreOperations(MailboxSession session, ILogger logger)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.session = session;
			this.logger = logger;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		public bool MessageExistsInSentItems(string internetMessageId)
		{
			using (Folder folder = Folder.Bind(this.session, DefaultFolderType.SentItems))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, StoreOperations.SortByInternetMessageId, new PropertyDefinition[]
				{
					ItemSchema.InternetMessageId
				}))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InternetMessageId, internetMessageId));
					object[][] rows = queryResult.GetRows(1);
					if (rows != null && rows.Length > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001FD74 File Offset: 0x0001DF74
		public void CopyAttachmentToSentItemsFolder(MessageItem attachedMessageItem)
		{
			StoreId destinationSentItemsFolderId = this.CreateOrGetSentItemsFolderId();
			this.CopyAttachmentToDestinationFolder(destinationSentItemsFolderId, attachedMessageItem);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001FD90 File Offset: 0x0001DF90
		private void CopyAttachmentToDestinationFolder(StoreId destinationSentItemsFolderId, MessageItem attachedMessageItem)
		{
			using (MessageItem messageItem = MessageItem.Create(this.session, destinationSentItemsFolderId))
			{
				Item.CopyItemContent(attachedMessageItem, messageItem);
				messageItem.PropertyBag[MessageItemSchema.Flags] = (MessageFlags.IsRead | MessageFlags.HasBeenSubmitted | MessageFlags.IsFromMe);
				messageItem.Save(SaveMode.NoConflictResolution);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		private StoreId CreateOrGetSentItemsFolderId()
		{
			StoreId storeId = this.session.GetDefaultFolderId(DefaultFolderType.SentItems);
			if (storeId == null)
			{
				this.logger.TraceDebug(new string[]
				{
					"SharedMailboxSentItemsAgent.OnPromotedMessageHandler: sent items folder Id does not exists, Create the default folder."
				});
				storeId = this.session.CreateDefaultFolder(DefaultFolderType.SentItems);
			}
			return storeId;
		}

		// Token: 0x04000352 RID: 850
		private static readonly SortBy[] SortByInternetMessageId = new SortBy[]
		{
			new SortBy(ItemSchema.InternetMessageId, SortOrder.Ascending)
		};

		// Token: 0x04000353 RID: 851
		private readonly MailboxSession session;

		// Token: 0x04000354 RID: 852
		private readonly ILogger logger;
	}
}

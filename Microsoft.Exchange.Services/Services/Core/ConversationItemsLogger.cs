using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003BB RID: 955
	internal class ConversationItemsLogger
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x0009AB00 File Offset: 0x00098D00
		public ConversationItemsLogger(Enum conversationItemsMetadata, IdConverter idConverter, MailboxSession mailboxSession)
		{
			this.logger = RequestDetailsLogger.Current;
			this.idConverter = idConverter;
			this.conversationItemsMetadata = conversationItemsMetadata;
			this.mailboxSession = mailboxSession;
			this.bufferCreated = false;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0009AB30 File Offset: 0x00098D30
		public void CreateBufferLog()
		{
			if (this.bufferCreated)
			{
				throw new InvalidOperationException("Before creating a new bufferlog, you should save the one already created");
			}
			this.bufferCreated = true;
			this.firstItemLogged = false;
			string value = this.logger.Get(this.conversationItemsMetadata);
			this.itemLogBuilder = new StringBuilder(value);
			if (!string.IsNullOrEmpty(value))
			{
				this.itemLogBuilder.Append("|");
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0009AB98 File Offset: 0x00098D98
		public void AddToLog(ItemType item)
		{
			if (!this.bufferCreated)
			{
				throw new InvalidOperationException("Adding to log can happen only after creating the buffer");
			}
			if (this.firstItemLogged)
			{
				this.itemLogBuilder.Append("~");
			}
			StoreObjectId folderId = null;
			if (item.ParentFolderId != null)
			{
				IdAndSession idAndSession = this.idConverter.ConvertFolderIdToIdAndSessionReadOnly(item.ParentFolderId);
				folderId = idAndSession.GetAsStoreObjectId();
			}
			this.itemLogBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}/{1}/{2}", new object[]
			{
				(item.ItemId == null) ? "null" : (item.ItemId.Id + ":" + item.ItemId.ChangeKey),
				ServiceCommandBase.GetFolderLogString(folderId, this.mailboxSession),
				item.InstanceKeyString
			});
			this.firstItemLogged = true;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0009AC63 File Offset: 0x00098E63
		public void SaveBufferLog()
		{
			if (!this.bufferCreated)
			{
				throw new InvalidOperationException("You can only save a buffer log that was previoulsy created");
			}
			this.bufferCreated = false;
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.logger, this.conversationItemsMetadata, this.itemLogBuilder.ToString());
		}

		// Token: 0x040011AB RID: 4523
		private readonly RequestDetailsLogger logger;

		// Token: 0x040011AC RID: 4524
		private readonly Enum conversationItemsMetadata;

		// Token: 0x040011AD RID: 4525
		private readonly IdConverter idConverter;

		// Token: 0x040011AE RID: 4526
		private readonly MailboxSession mailboxSession;

		// Token: 0x040011AF RID: 4527
		private bool firstItemLogged;

		// Token: 0x040011B0 RID: 4528
		private StringBuilder itemLogBuilder;

		// Token: 0x040011B1 RID: 4529
		private bool bufferCreated;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E5C RID: 3676
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IcsCallback : IMapiManifestCallback
	{
		// Token: 0x06007F43 RID: 32579 RVA: 0x0022D9FC File Offset: 0x0022BBFC
		internal IcsCallback(MailboxSyncProvider mailboxSyncProvider, Dictionary<ISyncItemId, ServerManifestEntry> serverManifest, int numOperations, MailboxSyncWatermark syncWatermark)
		{
			this.mailboxSyncProvider = mailboxSyncProvider;
			this.serverManifest = serverManifest;
			this.numOperations = numOperations;
			this.moreAvailable = false;
			this.syncWatermark = syncWatermark;
			this.lastServerManifestEntry = null;
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x06007F44 RID: 32580 RVA: 0x0022DA2F File Offset: 0x0022BC2F
		public static PropTag[] PropTags
		{
			get
			{
				return IcsCallback.propTags;
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x06007F45 RID: 32581 RVA: 0x0022DA36 File Offset: 0x0022BC36
		public ServerManifestEntry ExtraServerManiferEntry
		{
			get
			{
				if (!this.moreAvailable)
				{
					return null;
				}
				return this.lastServerManifestEntry;
			}
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x06007F46 RID: 32582 RVA: 0x0022DA48 File Offset: 0x0022BC48
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x0022DA50 File Offset: 0x0022BC50
		public void Bind(MailboxSyncWatermark syncWatermark, int numOperations, Dictionary<ISyncItemId, ServerManifestEntry> serverManifest)
		{
			this.syncWatermark = syncWatermark;
			this.numOperations = numOperations;
			this.serverManifest = serverManifest;
			this.moreAvailable = false;
			this.lastServerManifestEntry = null;
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x0022DA78 File Offset: 0x0022BC78
		public ManifestCallbackStatus Change(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModifiedTime, ManifestChangeType changeType, bool associated, PropValue[] properties)
		{
			EnumValidator.ThrowIfInvalid<ManifestChangeType>(changeType, "changeType");
			if (ExTraceGlobals.SyncTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				this.TraceChangeChangeCallbackProps(entryId, sourceKey, changeKey, changeList, lastModifiedTime, changeType, associated, properties);
			}
			int? num = null;
			string text = null;
			bool read = false;
			ConversationId conversationId = null;
			bool firstMessageInConversation = false;
			ExDateTime? filterDate = null;
			foreach (PropValue propValue in properties)
			{
				if (!propValue.IsError())
				{
					PropTag propTag = propValue.PropTag;
					if (propTag <= PropTag.MessageDeliveryTime)
					{
						if (propTag != PropTag.MessageClass)
						{
							ConversationIndex index;
							if (propTag != PropTag.ConversationIndex)
							{
								if (propTag == PropTag.MessageDeliveryTime)
								{
									if (propValue.PropType == PropType.SysTime)
									{
										filterDate = new ExDateTime?((ExDateTime)propValue.GetDateTime());
									}
								}
							}
							else if (propValue.PropType == PropType.Binary && ConversationIndex.TryCreate(propValue.GetBytes(), out index) && index != ConversationIndex.Empty && index.Components != null && index.Components.Count == 1)
							{
								firstMessageInConversation = true;
							}
						}
						else if (propValue.PropType == PropType.String)
						{
							text = propValue.GetString();
						}
					}
					else if (propTag != PropTag.MessageFlags)
					{
						if (propTag != PropTag.InternetArticleNumber)
						{
							if (propTag == PropTag.ConversationId)
							{
								if (propValue.PropType == PropType.Binary)
								{
									conversationId = ConversationId.Create(propValue.GetBytes());
								}
							}
						}
						else
						{
							if (propValue.PropType != PropType.Int)
							{
								return ManifestCallbackStatus.Continue;
							}
							num = new int?(propValue.GetInt());
						}
					}
					else if (propValue.PropType == PropType.Int)
					{
						MessageFlags @int = (MessageFlags)propValue.GetInt();
						read = ((@int & MessageFlags.IsRead) == MessageFlags.IsRead);
					}
				}
			}
			if (changeType == ManifestChangeType.Add || changeType == ManifestChangeType.Change)
			{
				if (num == null)
				{
					return ManifestCallbackStatus.Continue;
				}
				StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, (text == null) ? StoreObjectType.Unknown : ObjectClass.GetObjectType(text));
				MailboxSyncItemId mailboxSyncItemId = MailboxSyncItemId.CreateForNewItem(id);
				MailboxSyncWatermark mailboxSyncWatermark = MailboxSyncWatermark.CreateForSingleItem();
				mailboxSyncWatermark.UpdateWithChangeNumber(num.Value, read);
				ServerManifestEntry serverManifestEntry = this.mailboxSyncProvider.CreateItemChangeManifestEntry(mailboxSyncItemId, mailboxSyncWatermark);
				serverManifestEntry.IsNew = (changeType == ManifestChangeType.Add);
				serverManifestEntry.MessageClass = text;
				serverManifestEntry.ConversationId = conversationId;
				serverManifestEntry.FirstMessageInConversation = firstMessageInConversation;
				serverManifestEntry.FilterDate = filterDate;
				mailboxSyncItemId.ChangeKey = changeKey;
				this.lastServerManifestEntry = serverManifestEntry;
			}
			else
			{
				StoreObjectId id2 = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
				MailboxSyncItemId mailboxSyncItemId2 = MailboxSyncItemId.CreateForExistingItem(this.mailboxSyncProvider.FolderSync, id2);
				if (mailboxSyncItemId2 == null)
				{
					return ManifestCallbackStatus.Continue;
				}
				this.lastServerManifestEntry = MailboxSyncProvider.CreateItemDeleteManifestEntry(mailboxSyncItemId2);
				this.lastServerManifestEntry.ConversationId = conversationId;
			}
			return this.CheckYieldOrStop();
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x0022DD18 File Offset: 0x0022BF18
		public ManifestCallbackStatus Delete(byte[] entryId, bool softDelete, bool expiry)
		{
			if (ExTraceGlobals.SyncTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				this.TraceDeleteCallbackProps(entryId, softDelete, expiry);
			}
			if (softDelete)
			{
				return ManifestCallbackStatus.Continue;
			}
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
			MailboxSyncItemId mailboxSyncItemId = MailboxSyncItemId.CreateForExistingItem(this.mailboxSyncProvider.FolderSync, id);
			if (mailboxSyncItemId == null)
			{
				return ManifestCallbackStatus.Continue;
			}
			this.lastServerManifestEntry = MailboxSyncProvider.CreateItemDeleteManifestEntry(mailboxSyncItemId);
			return this.CheckYieldOrStop();
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x0022DD74 File Offset: 0x0022BF74
		public ManifestCallbackStatus ReadUnread(byte[] entryId, bool read)
		{
			ManifestCallbackStatus result = ManifestCallbackStatus.Continue;
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
			ISyncItemId syncItemId = MailboxSyncItemId.CreateForExistingItem(this.mailboxSyncProvider.FolderSync, id);
			if (syncItemId == null)
			{
				return result;
			}
			ServerManifestEntry serverManifestEntry = this.mailboxSyncProvider.CreateReadFlagChangeManifestEntry(syncItemId, read);
			if (serverManifestEntry != null)
			{
				this.lastServerManifestEntry = serverManifestEntry;
			}
			return this.CheckYieldOrStop();
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x0022DDC0 File Offset: 0x0022BFC0
		public void TraceDeleteCallbackProps(byte[] entryId, bool softDelete, bool expiry)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("ICS Change Callback: ");
			stringBuilder.Append("entryId=");
			stringBuilder.Append((entryId == null) ? "null" : Convert.ToBase64String(entryId));
			stringBuilder.Append(" softDelete=");
			stringBuilder.Append(softDelete.ToString());
			stringBuilder.Append(" expiry=");
			stringBuilder.Append(expiry.ToString());
			ExTraceGlobals.SyncTracer.Information<StringBuilder>((long)this.GetHashCode(), "{0}", stringBuilder);
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x0022DE54 File Offset: 0x0022C054
		private ManifestCallbackStatus CheckYieldOrStop()
		{
			if (this.numOperations != -1)
			{
				if (this.serverManifest.Count >= this.numOperations)
				{
					this.moreAvailable = true;
					return ManifestCallbackStatus.Stop;
				}
				if (this.serverManifest.Count >= this.numOperations - 1)
				{
					if (this.lastServerManifestEntry != null)
					{
						if (this.lastServerManifestEntry.Watermark != null)
						{
							this.syncWatermark.ChangeNumber = ((MailboxSyncWatermark)this.lastServerManifestEntry.Watermark).ChangeNumber;
						}
						this.serverManifest[this.lastServerManifestEntry.Id] = this.lastServerManifestEntry;
						this.lastServerManifestEntry = null;
					}
					return ManifestCallbackStatus.Yield;
				}
			}
			if (this.lastServerManifestEntry != null)
			{
				if (this.lastServerManifestEntry.Watermark != null)
				{
					this.syncWatermark.ChangeNumber = ((MailboxSyncWatermark)this.lastServerManifestEntry.Watermark).ChangeNumber;
				}
				this.serverManifest[this.lastServerManifestEntry.Id] = this.lastServerManifestEntry;
				this.lastServerManifestEntry = null;
			}
			return ManifestCallbackStatus.Continue;
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x0022DF54 File Offset: 0x0022C154
		private void TraceChangeChangeCallbackProps(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModifiedTime, ManifestChangeType changeType, bool associated, PropValue[] properties)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("ICS Change Callback: ");
			stringBuilder.Append("entryId=");
			stringBuilder.Append((entryId == null) ? "null" : Convert.ToBase64String(entryId));
			stringBuilder.Append(" sourceKey=");
			stringBuilder.Append((sourceKey == null) ? "null" : Convert.ToBase64String(sourceKey));
			stringBuilder.Append(" changeKey=");
			stringBuilder.Append((changeKey == null) ? "null" : Convert.ToBase64String(changeKey));
			stringBuilder.Append(" changeList=");
			stringBuilder.Append((changeList == null) ? "null" : Convert.ToBase64String(changeList));
			stringBuilder.Append(" lastModifiedTime=");
			stringBuilder.Append(lastModifiedTime.ToString());
			stringBuilder.Append(" changeType=");
			stringBuilder.Append(changeType.ToString());
			stringBuilder.Append(" associated=");
			stringBuilder.Append(associated.ToString());
			stringBuilder.Append(" properties.Length=");
			stringBuilder.Append((properties == null) ? "null" : properties.Length.ToString());
			stringBuilder.Append("{");
			if (properties != null)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					stringBuilder.Append("{");
					if (properties[i].IsError())
					{
						stringBuilder.Append("Error: " + properties[i].GetErrorValue().ToString());
					}
					else
					{
						stringBuilder.Append(properties[i].PropTag.ToString() + ", ");
						stringBuilder.Append(properties[i].PropType.ToString() + ", ");
						stringBuilder.Append((properties[i].Value == null) ? "null" : properties[i].Value);
					}
					stringBuilder.Append("}");
				}
			}
			stringBuilder.Append("}");
			ExTraceGlobals.SyncTracer.Information<StringBuilder>((long)this.GetHashCode(), "{0}", stringBuilder);
		}

		// Token: 0x0400562D RID: 22061
		private static readonly PropTag[] propTags = new PropTag[]
		{
			PropTag.MessageFlags,
			PropTag.InternetArticleNumber,
			PropTag.MessageClass,
			PropTag.ConversationId,
			PropTag.ConversationIndex,
			PropTag.MessageDeliveryTime
		};

		// Token: 0x0400562E RID: 22062
		private MailboxSyncProvider mailboxSyncProvider;

		// Token: 0x0400562F RID: 22063
		private bool moreAvailable;

		// Token: 0x04005630 RID: 22064
		private int numOperations;

		// Token: 0x04005631 RID: 22065
		private Dictionary<ISyncItemId, ServerManifestEntry> serverManifest;

		// Token: 0x04005632 RID: 22066
		private MailboxSyncWatermark syncWatermark;

		// Token: 0x04005633 RID: 22067
		private ServerManifestEntry lastServerManifestEntry;

		// Token: 0x02000E5D RID: 3677
		private enum PropTagEnum
		{
			// Token: 0x04005635 RID: 22069
			MessageFlags,
			// Token: 0x04005636 RID: 22070
			ArticleId,
			// Token: 0x04005637 RID: 22071
			MessageClass,
			// Token: 0x04005638 RID: 22072
			ConversationId,
			// Token: 0x04005639 RID: 22073
			ConversationIndex,
			// Token: 0x0400563A RID: 22074
			MessageDeliveryTime,
			// Token: 0x0400563B RID: 22075
			Total
		}
	}
}

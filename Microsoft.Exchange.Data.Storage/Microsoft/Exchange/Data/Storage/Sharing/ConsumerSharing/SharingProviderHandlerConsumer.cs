using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Sharing.ConsumerSharing
{
	// Token: 0x02000DF2 RID: 3570
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharingProviderHandlerConsumer : SharingProviderHandler
	{
		// Token: 0x06007AB6 RID: 31414 RVA: 0x0021EC28 File Offset: 0x0021CE28
		protected override bool InternalValidateCompatibility(Folder folderToShare)
		{
			IExchangePrincipal mailboxOwner = folderToShare.Session.MailboxOwner;
			SharingDataType sharingDataType = SharingDataType.FromContainerClass(folderToShare.ClassName);
			return sharingDataType == SharingDataType.Calendar && mailboxOwner is IUserPrincipal && ((IUserPrincipal)mailboxOwner).NetId != null && mailboxOwner.GetConfiguration().DataStorage.XOWAConsumerSharing.Enabled;
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x0021EC8C File Offset: 0x0021CE8C
		protected override PerformInvitationResults InternalPerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator)
		{
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, context.FolderId, CalendarFolderSchema.ConsumerCalendarProperties))
			{
				Guid a = calendarFolder.ConsumerCalendarGuid;
				Guid consumerCalendarPrivateFreeBusyId = calendarFolder.ConsumerCalendarPrivateFreeBusyId;
				Guid consumerCalendarPrivateDetailId = calendarFolder.ConsumerCalendarPrivateDetailId;
				if (a == Guid.Empty)
				{
					a = (calendarFolder.ConsumerCalendarGuid = Guid.NewGuid());
				}
				if (consumerCalendarPrivateFreeBusyId == Guid.Empty)
				{
					Guid guid = calendarFolder.ConsumerCalendarPrivateFreeBusyId = Guid.NewGuid();
				}
				if (consumerCalendarPrivateDetailId == Guid.Empty)
				{
					Guid guid2 = calendarFolder.ConsumerCalendarPrivateDetailId = Guid.NewGuid();
				}
				if (calendarFolder.IsDirty)
				{
					FolderSaveResult folderSaveResult = calendarFolder.Save();
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						throw folderSaveResult.ToException(new LocalizedString("TODO: LOC: Failed to share the calendar."));
					}
				}
				context.FolderEwsId = a.ToString();
				context.MailboxId = ((IUserPrincipal)mailboxSession.MailboxOwner).NetId.ToByteArray();
				context.FolderName = (context.IsPrimary ? string.Format("TODO: LOC: {0}'s Calendar", context.InitiatorName) : calendarFolder.DisplayName);
				context.IsPrimary = false;
			}
			return new PerformInvitationResults(recipients);
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x0021EDC4 File Offset: 0x0021CFC4
		internal override void FillSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			sharingMessageProvider.FolderId = context.FolderEwsId;
			sharingMessageProvider.MailboxId = HexConverter.ByteArrayToHexString(context.MailboxId);
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x0021EDE3 File Offset: 0x0021CFE3
		internal override void ParseSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			context.FolderEwsId = sharingMessageProvider.FolderId;
			context.MailboxId = HexConverter.HexStringToByteArray(sharingMessageProvider.MailboxId);
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x0021EE04 File Offset: 0x0021D004
		protected override SubscribeResults InternalPerformSubscribe(MailboxSession mailboxSession, SharingContext context)
		{
			NetID netID = new NetID(context.MailboxId);
			long num = checked((long)netID.ToUInt64());
			if (num == 0L)
			{
				throw new InvalidOperationException("Invitation does not contain the owner ID.");
			}
			Guid guid;
			if (!Guid.TryParse(context.FolderEwsId, out guid))
			{
				throw new InvalidOperationException("Invitation does not contain the calendar ID.");
			}
			using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Root))
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
				QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, CalendarFolderSchema.ConsumerCalendarGuid, guid),
					new ComparisonFilter(ComparisonOperator.Equal, CalendarFolderSchema.ConsumerCalendarOwnerId, num)
				});
				using (IQueryResult queryResult = folder.IFolderQuery(FolderQueryFlags.DeepTraversal, queryFilter, null, new PropertyDefinition[]
				{
					FolderSchema.Id,
					FolderSchema.DisplayName,
					StoreObjectSchema.ParentItemId
				}))
				{
					bool flag = true;
					while (flag)
					{
						object[][] rows = queryResult.GetRows(100, out flag);
						if (rows == null || rows.Length == 0)
						{
							break;
						}
						foreach (object[] array2 in rows)
						{
							StoreObjectId storeObjectId = StoreId.GetStoreObjectId((StoreId)array2[2]);
							if (!defaultFolderId.Equals(storeObjectId))
							{
								return new SubscribeResults(context.DataType, context.InitiatorSmtpAddress, context.InitiatorName, context.FolderName, StoreId.GetStoreObjectId((StoreId)rows[0][0]), false, (string)rows[0][1]);
							}
						}
					}
				}
			}
			SubscribeResults result;
			using (CalendarFolder calendarFolder = CalendarFolder.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Root), StoreObjectType.CalendarFolder))
			{
				calendarFolder[FolderSchema.ExtendedFolderFlags] = (ExtendedFolderFlags.SharedIn | ExtendedFolderFlags.ExclusivelyBound | ExtendedFolderFlags.ExchangeConsumerShareFolder);
				calendarFolder.DisplayName = (context.FolderName ?? context.InitiatorSmtpAddress);
				calendarFolder.ConsumerCalendarGuid = guid;
				calendarFolder.ConsumerCalendarOwnerId = num;
				calendarFolder.SaveWithUniqueDisplayName(50);
				calendarFolder.Load();
				result = new SubscribeResults(context.DataType, context.InitiatorSmtpAddress, context.InitiatorName, context.FolderName, StoreId.GetStoreObjectId(calendarFolder.Id), true, calendarFolder.DisplayName);
			}
			return result;
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x0021F07C File Offset: 0x0021D27C
		protected override ValidRecipient InternalCheckOneRecipient(ADRecipient mailboxOwner, string recipient, IRecipientSession recipientSession)
		{
			return new ValidRecipient(recipient, null);
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x0021F085 File Offset: 0x0021D285
		protected override void InternalPerformRevocation(MailboxSession mailboxSession, SharingContext context)
		{
		}
	}
}

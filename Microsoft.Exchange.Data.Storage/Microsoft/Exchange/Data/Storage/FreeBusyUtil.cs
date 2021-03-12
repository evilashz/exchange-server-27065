using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C2 RID: 962
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FreeBusyUtil
	{
		// Token: 0x06002BC0 RID: 11200 RVA: 0x000AE45C File Offset: 0x000AC65C
		public static DelegateRuleType GetDelegateRuleType(MailboxSession session)
		{
			DelegateRuleType result = DelegateRuleType.ForwardAndDelete;
			FolderSaveResult folderSaveResult;
			byte[] freeBusyMsgId = FreeBusyUtil.GetFreeBusyMsgId(session, out folderSaveResult);
			if (freeBusyMsgId != null && freeBusyMsgId.Length > 0)
			{
				try
				{
					using (MessageItem messageItem = MessageItem.Bind(session, StoreObjectId.FromProviderSpecificId(freeBusyMsgId), FreeBusyUtil.FreeBusyMessageProperties))
					{
						result = FreeBusyUtil.GetDelegateRuleType(messageItem);
					}
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.CalendarLoggingTracer.TraceDebug((long)session.GetHashCode(), "FreeBusyUtil::GetDelegateRuleType. No FreeBusyMessage");
				}
			}
			return result;
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000AE4DC File Offset: 0x000AC6DC
		public static DelegateRuleType GetDelegateRuleType(MessageItem localFBMessage)
		{
			DelegateRuleType delegateRuleType = DelegateRuleType.ForwardAndDelete;
			bool valueOrDefault = localFBMessage.GetValueOrDefault<bool>(InternalSchema.DelegateDontMail);
			if (valueOrDefault)
			{
				delegateRuleType = DelegateRuleType.NoForward;
			}
			else
			{
				bool valueOrDefault2 = localFBMessage.GetValueOrDefault<bool>(InternalSchema.DelegateBossWantsCopy);
				bool valueOrDefault3 = localFBMessage.GetValueOrDefault<bool>(InternalSchema.DelegateBossWantsInfo);
				if (valueOrDefault2 && !valueOrDefault3)
				{
					delegateRuleType = DelegateRuleType.Forward;
				}
				else if (valueOrDefault2 && valueOrDefault3)
				{
					delegateRuleType = DelegateRuleType.ForwardAndSetAsInformationalUpdate;
				}
				else if (!valueOrDefault2)
				{
					delegateRuleType = DelegateRuleType.ForwardAndDelete;
				}
			}
			ExTraceGlobals.CalendarLoggingTracer.TraceDebug<DelegateRuleType>(0L, "FreeBusyUtil::GetDelegateRuleType. Returned DelegateRuleType: {0}", delegateRuleType);
			return delegateRuleType;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000AE544 File Offset: 0x000AC744
		public static byte[] GetFreeBusyMsgId(MailboxSession session, out FolderSaveResult result)
		{
			result = null;
			byte[] array = null;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Inbox, new PropertyDefinition[]
			{
				InternalSchema.FreeBusyEntryIds
			}))
			{
				byte[][] array2 = folder.TryGetProperty(InternalSchema.FreeBusyEntryIds) as byte[][];
				if (array2 != null)
				{
					if (array2.Length > 1)
					{
						array = array2[1];
						try
						{
							StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(array);
							if (!IdConverter.IsMessageId(storeObjectId))
							{
								ExTraceGlobals.CalendarLoggingTracer.TraceError(0L, "FreeBusyUtil::GetFreeBusyMsgId. The extracted storeObjectId is not a valid MessageId");
								array = null;
							}
						}
						catch (CorruptDataException)
						{
							ExTraceGlobals.CalendarLoggingTracer.TraceError(0L, "FreeBusyUtil::GetFreeBusyMsgId. The localFreeBusyMsgId is not a valid entry id");
							array = null;
						}
					}
					if (array == null || array.Length == 0)
					{
						array = FreeBusyUtil.FindFreeBusyMsgId(session);
						if (array == null)
						{
							ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::GetFreeBusyMsgId. Couldn't find a free busy message. Attempting to create one.");
							array = FreeBusyUtil.CreateFreeBusyMessage(session, out result);
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000AE620 File Offset: 0x000AC820
		private static byte[] FindFreeBusyMsgId(MailboxSession session)
		{
			ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::FindFreeBusyMsgId. Bind to the FreeBusy data folder");
			byte[] result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.FreeBusyData))
			{
				ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::FindFreeBusyMsgId. Looking for fb messages by matching class");
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(InternalSchema.ItemClass),
					new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, FreeBusyUtil.LocalFbMessageClass)
				});
				SortBy[] sortColumns = new SortBy[]
				{
					new SortBy(StoreObjectSchema.CreationTime, SortOrder.Descending)
				};
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, sortColumns, FreeBusyUtil.FindFreeBusyMessageProperties))
				{
					string text;
					for (;;)
					{
						object[][] rows = queryResult.GetRows(5);
						foreach (object[] array2 in rows)
						{
							text = (array2[2] as string);
							ExTraceGlobals.CalendarLoggingTracer.TraceDebug<string>(0L, "FreeBusyUtil::FindFreeBusyMsgId. Searching for message with subject: {0}", text);
							if (text.Equals(FreeBusyUtil.LocalFbSubject, StringComparison.OrdinalIgnoreCase))
							{
								goto Block_4;
							}
						}
						if (rows.Length <= 0)
						{
							goto Block_7;
						}
					}
					Block_4:
					ExTraceGlobals.CalendarLoggingTracer.TraceDebug<string>(0L, "FreeBusyUtil::FindFreeBusyMsgId. Found message with subject: {0}", text);
					object[] array2;
					byte[] array3 = array2[0] as byte[];
					if (array3 != null)
					{
						FreeBusyUtil.StampFreeBusyMsgId(session, array3, DefaultFolderType.Inbox);
						FreeBusyUtil.StampFreeBusyMsgId(session, array3, DefaultFolderType.Configuration);
					}
					return array3;
					Block_7:
					ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::FindFreeBusyMsgId. Nothing found that looks like an FB message, just return null");
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000AE7B0 File Offset: 0x000AC9B0
		private static FolderSaveResult StampFreeBusyMsgId(MailboxSession session, byte[] entryId, DefaultFolderType folderType)
		{
			FolderSaveResult result;
			using (Folder folder = Folder.Bind(session, folderType, new PropertyDefinition[]
			{
				InternalSchema.FreeBusyEntryIds
			}))
			{
				byte[][] array = folder.GetValueOrDefault<byte[][]>(InternalSchema.FreeBusyEntryIds);
				if (array == null)
				{
					array = new byte[][]
					{
						Array<byte>.Empty,
						entryId,
						Array<byte>.Empty
					};
				}
				else
				{
					array[1] = entryId;
				}
				folder[InternalSchema.FreeBusyEntryIds] = array;
				ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::StampFreeBusyMsgId. Saving folder");
				result = folder.Save();
			}
			return result;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000AE850 File Offset: 0x000ACA50
		private static byte[] CreateFreeBusyMessage(MailboxSession session, out FolderSaveResult result)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.FreeBusyData);
			if (defaultFolderId == null)
			{
				ExTraceGlobals.CalendarLoggingTracer.TraceError(0L, "FreeBusyUtil::CreateFreeBusyMessage. No FreeBusyData folder found.");
				throw new DelegateUserNoFreeBusyFolderException(ServerStrings.NoFreeBusyFolder);
			}
			ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::CreateFreeBusyMessage. Creating the message");
			byte[] providerLevelItemId;
			using (MessageItem messageItem = MessageItem.Create(session, defaultFolderId))
			{
				messageItem.Subject = FreeBusyUtil.LocalFbSubject;
				messageItem.ClassName = FreeBusyUtil.LocalFbMessageClass;
				messageItem.Save(SaveMode.ResolveConflicts);
				messageItem.Load(null);
				providerLevelItemId = messageItem.Id.ObjectId.ProviderLevelItemId;
			}
			result = null;
			ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::CreateFreeBusyMessage. Stamping the entry id on the inbox");
			FolderSaveResult folderSaveResult = FreeBusyUtil.StampFreeBusyMsgId(session, providerLevelItemId, DefaultFolderType.Inbox);
			if (folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				result = folderSaveResult;
			}
			ExTraceGlobals.CalendarLoggingTracer.TraceDebug(0L, "FreeBusyUtil::CreateFreeBusyMessage. Stamping the entry id on the configuration folder");
			folderSaveResult = FreeBusyUtil.StampFreeBusyMsgId(session, providerLevelItemId, DefaultFolderType.Configuration);
			if (folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				result = folderSaveResult;
			}
			return providerLevelItemId;
		}

		// Token: 0x04001866 RID: 6246
		internal static readonly string LocalFbSubject = "LocalFreebusy";

		// Token: 0x04001867 RID: 6247
		internal static readonly PropertyDefinition[] FreeBusyMessageProperties = new PropertyDefinition[]
		{
			InternalSchema.DelegateNames,
			InternalSchema.DelegateEntryIds,
			InternalSchema.DelegateFlags,
			InternalSchema.DelegateEntryIds2,
			InternalSchema.DelegateBossWantsCopy,
			InternalSchema.DelegateFlags2,
			InternalSchema.DelegateBossWantsInfo,
			InternalSchema.DelegateDontMail
		};

		// Token: 0x04001868 RID: 6248
		internal static readonly PropertyDefinition[] FindFreeBusyMessageProperties = new PropertyDefinition[]
		{
			InternalSchema.EntryId,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			StoreObjectSchema.CreationTime
		};

		// Token: 0x04001869 RID: 6249
		internal static readonly string LocalFbMessageClass = "IPM.Microsoft.ScheduleData.FreeBusy";
	}
}

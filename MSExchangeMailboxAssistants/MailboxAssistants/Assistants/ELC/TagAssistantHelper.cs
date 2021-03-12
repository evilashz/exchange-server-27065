using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000063 RID: 99
	internal static class TagAssistantHelper
	{
		// Token: 0x06000380 RID: 896 RVA: 0x000187A0 File Offset: 0x000169A0
		internal static bool IsRetainableItem(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, StoreObject item)
		{
			DefaultFolderType parentDefaultFolderType = TagAssistantHelper.GetParentDefaultFolderType(mailboxState, mailboxSession, parentId);
			if (UserRetentionPolicyCache.IsFolderTypeToSkip(parentDefaultFolderType))
			{
				TagAssistantHelper.Tracer.TraceDebug<DefaultFolderType>(0L, "Current item is in folder {0} (or a sub-folder) that should not be processed.", parentDefaultFolderType);
				return false;
			}
			return TagAssistantHelper.IsRetainableItem(item.ClassName);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000187E0 File Offset: 0x000169E0
		internal static bool IsRetainableItem(string itemClass)
		{
			bool flag = !ObjectClass.IsContact(itemClass) && !ObjectClass.IsDistributionList(itemClass);
			if (!flag)
			{
				TagAssistantHelper.Tracer.TraceDebug<string>(0L, "Item is of message class {0} and should not be processed.", itemClass);
			}
			return flag;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00018818 File Offset: 0x00016A18
		internal static bool IsConflictableItemClass(string itemClass)
		{
			bool flag = ObjectClass.IsCalendarItem(itemClass) || ObjectClass.IsTask(itemClass) || ObjectClass.IsCalendarItemSeries(itemClass);
			if (flag)
			{
				TagAssistantHelper.Tracer.TraceDebug<string>(0L, "Item is of message class {0} and is conflictable.", itemClass);
			}
			return flag;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00018855 File Offset: 0x00016A55
		internal static bool IsConflictableItem(string itemClass, byte[] currentFolderId, byte[] deletedItemsFolderId)
		{
			if (TagAssistantHelper.IsConflictableItemClass(itemClass) && !ArrayComparer<byte>.Comparer.Equals(currentFolderId, deletedItemsFolderId))
			{
				TagAssistantHelper.Tracer.TraceDebug<string, byte[]>(0L, "Item is of message class {0}, in folder {1} and is hence conflictable.", itemClass, currentFolderId);
				return true;
			}
			return false;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00018884 File Offset: 0x00016A84
		internal static DefaultFolderType GetParentDefaultFolderType(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId)
		{
			bool flag = false;
			StoreObjectId storeObjectId = parentId;
			DefaultFolderType defaultFolderType = DefaultFolderType.None;
			Folder folder = null;
			try
			{
				while (!flag)
				{
					if (!mailboxState.FolderTypeDictionary.TryGetValue(storeObjectId, out defaultFolderType))
					{
						defaultFolderType = mailboxSession.IsDefaultFolderType(storeObjectId);
						if (defaultFolderType != DefaultFolderType.None || ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, mailboxState.RootFolderId))
						{
							break;
						}
						Exception ex = null;
						try
						{
							folder = Folder.Bind(mailboxSession, storeObjectId);
						}
						catch (ObjectNotFoundException ex2)
						{
							ex = ex2;
						}
						catch (ConversionFailedException ex3)
						{
							ex = ex3;
						}
						catch (VirusMessageDeletedException ex4)
						{
							ex = ex4;
						}
						if (ex != null)
						{
							TagAssistantHelper.Tracer.TraceDebug<Exception>(0L, "Problems loading a folder. It will not be processed. Exception: {0}", ex);
						}
						if (folder == null || ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, folder.ParentId.ProviderLevelItemId))
						{
							break;
						}
						storeObjectId = folder.ParentId;
						folder.Dispose();
						folder = null;
					}
					else
					{
						flag = true;
					}
				}
				if (storeObjectId != null && !ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, parentId.ProviderLevelItemId))
				{
					mailboxState.FolderTypeDictionary.Add(parentId, defaultFolderType);
				}
			}
			finally
			{
				if (folder != null)
				{
					folder.Dispose();
				}
			}
			return defaultFolderType;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000189B8 File Offset: 0x00016BB8
		internal static bool IsTagImplicit(int? retentionPeriod, int? retentionFlags)
		{
			return retentionPeriod == null || FlagsMan.IsAutoTagSet(retentionFlags);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000189D0 File Offset: 0x00016BD0
		internal static bool IsTagNull(Guid? tagGuid)
		{
			return tagGuid == null || tagGuid.Value.Equals(ElcMailboxHelper.BadGuid);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000189FC File Offset: 0x00016BFC
		internal static RetentionAndArchiveFlags UpdatePersonalTagBit(AdTagData tag, RetentionAndArchiveFlags flags)
		{
			if (tag == null || tag.Tag.Type != ElcFolderType.Personal)
			{
				flags = (RetentionAndArchiveFlags)FlagsMan.ClearPersonalTag((int)flags);
			}
			else
			{
				flags = (RetentionAndArchiveFlags)FlagsMan.SetPersonalTag((int)flags);
			}
			return flags;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00018A24 File Offset: 0x00016C24
		internal static bool DateSlushyEquals(DateTime? a, DateTime? b)
		{
			bool result = false;
			if (a != null && b != null)
			{
				double totalDays = a.Value.Subtract(b.Value).TotalDays;
				if (totalDays > -1.0 && totalDays < 1.0)
				{
					result = true;
				}
			}
			else if (a == null && b == null)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x040002E8 RID: 744
		private static readonly Trace Tracer = ExTraceGlobals.EventBasedAssistantTracer;

		// Token: 0x040002E9 RID: 745
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}

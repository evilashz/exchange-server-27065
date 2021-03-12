using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000077 RID: 119
	internal class FolderHelper
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		internal static void SyncFolders(IArchiveProcessor archiveHandler, FolderTuple sourceRoot, FolderTuple targetRoot, string traceId)
		{
			FolderHelper.<>c__DisplayClass2 CS$<>8__locals1 = new FolderHelper.<>c__DisplayClass2();
			CS$<>8__locals1.archiveHandler = archiveHandler;
			CS$<>8__locals1.traceId = traceId;
			if (CS$<>8__locals1.archiveHandler == null)
			{
				return;
			}
			Queue<FolderTuple> queue = new Queue<FolderTuple>(20);
			Queue<FolderTuple> queue2 = new Queue<FolderTuple>(20);
			queue.Enqueue(sourceRoot);
			queue2.Enqueue(targetRoot);
			while (queue2.Count > 0)
			{
				FolderHelper.<>c__DisplayClass6 CS$<>8__locals2 = new FolderHelper.<>c__DisplayClass6();
				CS$<>8__locals2.CS$<>8__locals3 = CS$<>8__locals1;
				CS$<>8__locals2.source = queue.Dequeue();
				CS$<>8__locals2.target = queue2.Dequeue();
				if (!FolderTuple.ArePropTagsSame(CS$<>8__locals2.source, CS$<>8__locals2.target))
				{
					ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<SyncFolders>b__0)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<SyncFolders>b__1)));
				}
				if (CS$<>8__locals2.target.Children != null && CS$<>8__locals2.target.Children.Count > 0 && CS$<>8__locals2.source.Children != null && CS$<>8__locals2.source.Children.Count > 0)
				{
					foreach (FolderTuple folderTuple in CS$<>8__locals2.target.Children.Values)
					{
						if (!string.IsNullOrEmpty(folderTuple.DisplayName))
						{
							FolderTuple folderTuple2 = null;
							if (CS$<>8__locals2.source.Children.TryGetValue(folderTuple.DisplayName, out folderTuple2) && folderTuple2 != null)
							{
								queue.Enqueue(folderTuple2);
								queue2.Enqueue(folderTuple);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001F97C File Offset: 0x0001DB7C
		internal static Dictionary<StoreObjectId, FolderTuple> QueryFolderHierarchy(MailboxSession mailboxSession)
		{
			Dictionary<StoreObjectId, FolderTuple> dictionary = new Dictionary<StoreObjectId, FolderTuple>();
			if (mailboxSession == null)
			{
				return dictionary;
			}
			FolderHelper.Tracer.TraceDebug<IExchangePrincipal, bool>((long)mailboxSession.GetHashCode(), "{0}: QueryFolderHierarchy: Archive mailbox: {1}.", mailboxSession.MailboxOwner, mailboxSession.MailboxOwner.MailboxInfo.IsArchive);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			dictionary[defaultFolderId] = new FolderTuple(defaultFolderId, defaultFolderId, string.Empty, new object[10], true);
			using (Folder folder = Folder.Bind(mailboxSession, defaultFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, FolderHelper.DataColumns))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							break;
						}
						for (int i = 0; i < rows.Length; i++)
						{
							if (rows[i][0] is VersionedId && rows[i][1] is StoreObjectId)
							{
								StoreObjectId objectId = (rows[i][0] as VersionedId).ObjectId;
								StoreObjectId storeObjectId = rows[i][1] as StoreObjectId;
								dictionary[objectId] = new FolderTuple(objectId, storeObjectId, (string)rows[i][2], rows[i]);
								FolderHelper.Tracer.TraceDebug((long)mailboxSession.GetHashCode(), "{0}: QueryFolderHierarchy: Added folder '{1}' to the folderMap. FolderId: '{2}'. ParentId: '{3}'.", new object[]
								{
									mailboxSession.MailboxOwner,
									(string)rows[i][2],
									objectId.ToHexEntryId(),
									storeObjectId.ToHexEntryId()
								});
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001FB30 File Offset: 0x0001DD30
		internal static FolderTuple LinkChildren(Dictionary<StoreObjectId, FolderTuple> folders, string emailAddress)
		{
			FolderTuple folderTuple = null;
			FolderTuple folderTuple2 = null;
			FolderHelper.Tracer.TraceDebug<string>(0L, "{0}: LinkChildren: About to start linking children to parents.", emailAddress);
			foreach (FolderTuple folderTuple3 in folders.Values)
			{
				FolderHelper.Tracer.TraceDebug<string, string, string>(0L, "{0}: LinkChildren: About to link folder '{1}'. FolderId: '{2}'. Looking for parent..", emailAddress, folderTuple3.DisplayName, folderTuple3.FolderId.ToHexEntryId());
				if (folderTuple3.IsRootFolder || ArrayComparer<byte>.Comparer.Equals(folderTuple3.FolderId.ProviderLevelItemId, folderTuple3.ParentId.ProviderLevelItemId))
				{
					if (folderTuple != null)
					{
						FolderHelper.Tracer.TraceError(0L, "{0}: LinkChildren: FolderId and ParentId of folder '{1}' are the same. FolderId: '{2}'. ParentId: '{3}'.", new object[]
						{
							emailAddress,
							folderTuple3.DisplayName,
							folderTuple3.FolderId.ToHexEntryId(),
							folderTuple3.ParentId.ToHexEntryId()
						});
					}
					else
					{
						FolderHelper.Tracer.TraceDebug(0L, "{0}: LinkChildren: Found root folder '{1}'. FolderId: '{2}'. ParentId: '{3}'.", new object[]
						{
							emailAddress,
							folderTuple3.DisplayName,
							folderTuple3.FolderId.ToHexEntryId(),
							folderTuple3.ParentId.ToHexEntryId()
						});
						folderTuple = folderTuple3;
					}
				}
				else if (folders.TryGetValue(folderTuple3.ParentId, out folderTuple2))
				{
					folderTuple2.AddChild(folderTuple3.DisplayName, folderTuple3);
					FolderHelper.Tracer.TraceDebug<string, string, string>(0L, "{0}: LinkChildren: Adding child folder {1} to parent folder {2}.", emailAddress, folderTuple3.DisplayName, folderTuple2.DisplayName);
				}
				else
				{
					FolderHelper.Tracer.TraceDebug(0L, "{0}: LinkChildren: Could not find parent for folder {1} in table. FolderId: '{2}'. ParentId: '{3}'.", new object[]
					{
						emailAddress,
						folderTuple3.DisplayName,
						folderTuple3.FolderId.ToHexEntryId(),
						folderTuple3.ParentId.ToHexEntryId()
					});
				}
			}
			return folderTuple;
		}

		// Token: 0x04000376 RID: 886
		private static readonly Trace Tracer = ExTraceGlobals.FolderProvisionerTracer;

		// Token: 0x04000377 RID: 887
		internal static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.DisplayName,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			FolderSchema.RetentionTagEntryId,
			StoreObjectSchema.ContainerClass
		};

		// Token: 0x02000078 RID: 120
		internal enum DataColumnIndex
		{
			// Token: 0x04000379 RID: 889
			folderIdIndex,
			// Token: 0x0400037A RID: 890
			parentIdIndex,
			// Token: 0x0400037B RID: 891
			displayNameIndex,
			// Token: 0x0400037C RID: 892
			startOfTagPropsIndex,
			// Token: 0x0400037D RID: 893
			policyTagIndex = 3,
			// Token: 0x0400037E RID: 894
			retentionPeriodIndex,
			// Token: 0x0400037F RID: 895
			retentionFlagsIndex,
			// Token: 0x04000380 RID: 896
			archiveTagIndex,
			// Token: 0x04000381 RID: 897
			archivePeriodIndex,
			// Token: 0x04000382 RID: 898
			retentionTagEntryId,
			// Token: 0x04000383 RID: 899
			containerClassIndex,
			// Token: 0x04000384 RID: 900
			endOfTagPropsIndex = 9,
			// Token: 0x04000385 RID: 901
			dataColumnCount
		}
	}
}

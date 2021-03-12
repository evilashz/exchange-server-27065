using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200000D RID: 13
	public class FolderTypeCheckTask : IntegrityCheckTaskBase
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00004504 File Offset: 0x00002704
		public FolderTypeCheckTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000450D File Offset: 0x0000270D
		public override string TaskName
		{
			get
			{
				return "FolderTypeCheckTask";
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004514 File Offset: 0x00002714
		public override ErrorCode ExecuteOneFolder(Mailbox mailbox, MailboxEntry mailboxEntry, FolderEntry folderEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			Context currentOperationContext = mailbox.CurrentOperationContext;
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug(0L, "Mailbox {0}: Executing task {1} with detectOnly=={2} on folder {3}", new object[]
				{
					mailboxEntry.MailboxOwnerName,
					this.TaskName,
					detectOnly,
					folderEntry.ToString()
				});
			}
			Folder folder = Folder.OpenFolder(currentOperationContext, mailbox, folderEntry.FolderId);
			if (folder != null && !(folder is SearchFolder))
			{
				Folder parentFolder = folder.GetParentFolder(currentOperationContext);
				if (parentFolder != null && (folderEntry.NameStartsWith("Restriction-") || folderEntry.NameStartsWith("Restriciton-")) && parentFolder.GetSpecialFolderNumber(currentOperationContext) == SpecialFolders.Finder)
				{
					if (!detectOnly)
					{
						FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(currentOperationContext.Database);
						folder.SetColumn(currentOperationContext, folderTable.QueryCriteria, new RestrictionFalse().Serialize());
						folder.Save(currentOperationContext);
					}
					base.ReportCorruption("Incorrect folder type detected", mailboxEntry, folderEntry, null, CorruptionType.WrongFolderTypeOnRestrictionFolder, !detectOnly);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004608 File Offset: 0x00002808
		protected internal override bool IgnoreFolder(Context context, Mailbox mailbox, ExchangeId folderId, ExchangeId parentFolderId, bool isSearchFolder, short specialFolderNumber)
		{
			return false;
		}
	}
}

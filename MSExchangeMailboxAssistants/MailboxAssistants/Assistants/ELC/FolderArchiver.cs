using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderArchiver : IDisposable
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		internal FolderArchiver(ElcUserInformation elcUserInformation, IArchiveProcessor archiveProcessor)
		{
			this.archiveProcessor = archiveProcessor;
			this.elcUserInformation = elcUserInformation;
			this.mailboxSession = elcUserInformation.MailboxSession;
			this.mailboxDN = this.mailboxSession.MailboxOwner.LegacyDn.ToString();
			this.mailboxSmtpAddress = this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001F161 File Offset: 0x0001D361
		internal ElcUserInformation ElcUserInformation
		{
			get
			{
				return this.elcUserInformation;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001F169 File Offset: 0x0001D369
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001F171 File Offset: 0x0001D371
		internal string MailboxDN
		{
			get
			{
				return this.mailboxDN;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001F179 File Offset: 0x0001D379
		internal string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddress;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001F181 File Offset: 0x0001D381
		internal bool SuspendExpiration
		{
			get
			{
				return this.elcUserInformation.SuspendExpiration;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001F18E File Offset: 0x0001D38E
		internal DateTime Now
		{
			get
			{
				return this.elcUserInformation.Now;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001F19B File Offset: 0x0001D39B
		internal DateTime UtcNow
		{
			get
			{
				return this.elcUserInformation.UtcNow;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		internal FolderTuple GetArchiveFolderTuple(StoreObjectId sourceFolderId)
		{
			FolderTuple folderTuple = null;
			FolderTuple folderTuple2 = null;
			FolderTuple[] targetFolderPathFromSourceFolder = this.GetTargetFolderPathFromSourceFolder(sourceFolderId);
			DefaultFolderType defaultFolderType;
			if (this.ElcUserInformation.ProcessEhaMigratedMessages && (targetFolderPathFromSourceFolder == null || targetFolderPathFromSourceFolder.Length == 0) && this.IsMigrationFolder(sourceFolderId, out defaultFolderType))
			{
				StoreObjectId defaultFolderId = this.MailboxSession.GetDefaultFolderId(defaultFolderType);
				targetFolderPathFromSourceFolder = this.GetTargetFolderPathFromSourceFolder(defaultFolderId);
			}
			this.QueryHierarchies();
			if (this.archiveParent != null && this.archiveHierarchy != null && targetFolderPathFromSourceFolder != null && targetFolderPathFromSourceFolder.Length > 0)
			{
				folderTuple = this.archiveParent;
				FolderTuple folderTuple3 = null;
				foreach (FolderTuple folderTuple4 in targetFolderPathFromSourceFolder)
				{
					FolderArchiver.Tracer.TraceDebug<IExchangePrincipal, string>((long)this.GetHashCode(), "{0}: GetArchiveFolder: The current pathPart in sourceFolderPath is {1}", this.mailboxSession.MailboxOwner, folderTuple4.DisplayName);
					if (folderTuple.TryGetChild(folderTuple4.DisplayName, out folderTuple3))
					{
						FolderArchiver.Tracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: GetArchiveFolder: The current source folder {1} was found in archive. Current folder in archive is {2}.", this.mailboxSession.MailboxOwner, folderTuple4.DisplayName, folderTuple.DisplayName);
						folderTuple = folderTuple3;
					}
					else
					{
						FolderArchiver.Tracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: GetArchiveFolder: The current source folder {1} was not found in archive. Current folder in archive is {2}.", this.mailboxSession.MailboxOwner, folderTuple4.DisplayName, folderTuple.DisplayName);
						folderTuple2 = this.archiveProcessor.CreateAndUpdateFolderInArchive(folderTuple, folderTuple4);
						if (!this.archiveHierarchy.ContainsKey(folderTuple2.FolderId))
						{
							FolderArchiver.Tracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: GetArchiveFolder: The current source folder {1} was not found in archiveHierarchy table. Adding it to table. Id of corresp folder in archive: {2}", this.mailboxSession.MailboxOwner, folderTuple4.DisplayName, folderTuple2.FolderId.ToHexEntryId());
							this.archiveHierarchy.Add(folderTuple2.FolderId, folderTuple2);
						}
						else
						{
							FolderArchiver.Tracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: GetArchiveFolder: The current source folder {1} was already present in archiveHierarchy table. Id of corresp folder in archive: {2}", this.mailboxSession.MailboxOwner, folderTuple4.DisplayName, folderTuple2.FolderId.ToHexEntryId());
							folderTuple2 = this.archiveHierarchy[folderTuple2.FolderId];
						}
						folderTuple.AddChild(folderTuple4.DisplayName, folderTuple2);
						folderTuple = folderTuple2;
					}
				}
			}
			if (folderTuple2 == null && folderTuple != null)
			{
				return folderTuple;
			}
			return folderTuple2;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		private bool IsMigrationFolder(StoreObjectId sourceFolderId, out DefaultFolderType folderType)
		{
			bool result = false;
			folderType = DefaultFolderType.None;
			using (Folder folder = Folder.Bind(this.MailboxSession, sourceFolderId, new PropertyDefinition[]
			{
				FolderSchema.DisplayName,
				StoreObjectSchema.ParentItemId,
				FolderSchema.Id
			}))
			{
				if (string.Compare(folder.DisplayName, DefaultFolderType.Inbox.ToString(), true) == 0 || string.Compare(folder.DisplayName, DefaultFolderType.SentItems.ToString(), true) == 0)
				{
					using (Folder folder2 = Folder.Bind(this.MailboxSession, folder.ParentId, new PropertyDefinition[]
					{
						FolderSchema.DisplayName,
						StoreObjectSchema.ParentItemId
					}))
					{
						StoreId defaultFolderId = this.MailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration);
						if (defaultFolderId.Equals(folder2.ParentId) && string.Compare(folder2.DisplayName, MigrateToArchiveEnforcer.MigrationFolderName, true) == 0)
						{
							result = true;
							folderType = ((string.Compare(folder.DisplayName, DefaultFolderType.Inbox.ToString(), true) == 0) ? DefaultFolderType.Inbox : DefaultFolderType.SentItems);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		internal FolderTuple[] GetTargetFolderPathFromSourceFolder(StoreObjectId sourceFolderId)
		{
			List<FolderTuple> list = new List<FolderTuple>();
			if (this.mailboxHierarchy == null)
			{
				this.mailboxHierarchy = FolderHelper.QueryFolderHierarchy(this.MailboxSession);
			}
			StoreObjectId key = sourceFolderId;
			while (key != null)
			{
				FolderTuple folderTuple = null;
				bool flag = this.mailboxHierarchy.TryGetValue(key, out folderTuple);
				if (!flag || string.IsNullOrEmpty(folderTuple.DisplayName))
				{
					break;
				}
				list.Insert(0, folderTuple);
				key = null;
				if (!ArrayComparer<byte>.Comparer.Equals(folderTuple.FolderId.ProviderLevelItemId, folderTuple.ParentId.ProviderLevelItemId))
				{
					key = folderTuple.ParentId;
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001F584 File Offset: 0x0001D784
		internal void SyncHierarchies()
		{
			this.QueryHierarchies();
			if (this.archiveParent == null)
			{
				FolderArchiver.Tracer.TraceError<string>((long)this.GetHashCode(), "Archive folder is not supposed to be null since mailbox {0} has an archive guid", this.mailboxParent.DisplayName);
				return;
			}
			if (this.archiveProcessor == null)
			{
				FolderArchiver.Tracer.TraceError<string>((long)this.GetHashCode(), "Archive processor is not supposed to be null since mailbox {0} has an archive guid", this.mailboxParent.DisplayName);
				return;
			}
			FolderHelper.SyncFolders(this.archiveProcessor, this.mailboxParent, this.archiveParent, this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() + ", archive mailbox");
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001F630 File Offset: 0x0001D830
		internal void QueryHierarchies()
		{
			if (this.archiveHierarchy == null || this.archiveHierarchy.Count == 0)
			{
				if (this.archiveProcessor != null)
				{
					this.archiveHierarchy = this.archiveProcessor.GetFolderHierarchyInArchive();
				}
				else
				{
					FolderArchiver.Tracer.TraceWarning<FolderArchiver>((long)this.GetHashCode(), "{0}: Archive hierarchy cannot be retrieved because archive processor is null.", this);
				}
			}
			if (this.archiveParent == null && this.archiveHierarchy != null)
			{
				string emailAddress = this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() + " , archive mailbox";
				this.archiveParent = FolderHelper.LinkChildren(this.archiveHierarchy, emailAddress);
			}
			if (this.mailboxHierarchy == null)
			{
				this.mailboxHierarchy = FolderHelper.QueryFolderHierarchy(this.MailboxSession);
			}
			if (this.mailboxParent == null)
			{
				string emailAddress2 = this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
				this.mailboxParent = FolderHelper.LinkChildren(this.mailboxHierarchy, emailAddress2);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001F72B File Offset: 0x0001D92B
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.archiveSession != null)
				{
					this.archiveSession.Dispose();
					this.archiveSession = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000368 RID: 872
		internal static readonly int MaxErrorsAllowed = 10;

		// Token: 0x04000369 RID: 873
		internal static readonly int MaxRetryCount = 3;

		// Token: 0x0400036A RID: 874
		private static readonly Trace Tracer = ExTraceGlobals.FolderProvisionerTracer;

		// Token: 0x0400036B RID: 875
		private bool disposed;

		// Token: 0x0400036C RID: 876
		private ElcUserInformation elcUserInformation;

		// Token: 0x0400036D RID: 877
		private MailboxSession mailboxSession;

		// Token: 0x0400036E RID: 878
		private MailboxSession archiveSession;

		// Token: 0x0400036F RID: 879
		private string mailboxDN;

		// Token: 0x04000370 RID: 880
		private string mailboxSmtpAddress;

		// Token: 0x04000371 RID: 881
		private Dictionary<StoreObjectId, FolderTuple> mailboxHierarchy;

		// Token: 0x04000372 RID: 882
		private Dictionary<StoreObjectId, FolderTuple> archiveHierarchy;

		// Token: 0x04000373 RID: 883
		private FolderTuple archiveParent;

		// Token: 0x04000374 RID: 884
		private FolderTuple mailboxParent;

		// Token: 0x04000375 RID: 885
		private IArchiveProcessor archiveProcessor;
	}
}

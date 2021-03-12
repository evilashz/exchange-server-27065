using System;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000013 RID: 19
	internal class MailboxTargetFolderProvider : TargetFolderProvider<string, BaseFolderType, ITargetMailbox>
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000085DA File Offset: 0x000067DA
		public MailboxTargetFolderProvider(IExportContext exportContext, ITargetMailbox targetMailbox)
		{
			Util.ThrowIfNull(exportContext, "exportContext");
			Util.ThrowIfNull(targetMailbox, "targetMailbox");
			this.exportContext = exportContext;
			this.targetMailbox = targetMailbox;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008608 File Offset: 0x00006808
		protected override BaseFolderType CreateFolder(ITargetMailbox targetSession, BaseFolderType parentFolder, string folderName)
		{
			BaseFolderType baseFolderType = targetSession.GetFolderByName(parentFolder.FolderId, folderName);
			if (baseFolderType == null)
			{
				baseFolderType = targetSession.CreateFolder(parentFolder.FolderId, folderName, false);
			}
			return baseFolderType;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008636 File Offset: 0x00006836
		protected override string GenerateTopLevelFolderName(bool isArchive)
		{
			return isArchive ? Strings.ArchiveMailbox : Strings.PrimaryMailbox;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000864C File Offset: 0x0000684C
		protected override BaseFolderType GetFolder(ITargetMailbox targetSession, string folderId)
		{
			return targetSession.GetFolder(folderId);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008655 File Offset: 0x00006855
		protected override string GetFolderId(BaseFolderType folder)
		{
			return folder.FolderId.Id;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008664 File Offset: 0x00006864
		protected override void InitializeTargetFolderHierarchy()
		{
			string resultFolderName;
			if (this.exportContext.ExportMetadata.IncludeDuplicates)
			{
				string arg = base.DataContext.IsPublicFolder ? "Public Folders" : base.DataContext.SourceName;
				resultFolderName = string.Format("{0}-{1}", arg, this.targetMailbox.ExportSettings.ExportTime);
			}
			else
			{
				resultFolderName = string.Format("{0}-{1}", "Results", this.targetMailbox.ExportSettings.ExportTime);
			}
			string text = this.targetMailbox.CreateResultFolder(resultFolderName);
			if (base.DataContext.IsUnsearchable)
			{
				FolderIdType parentFolderId = new FolderIdType
				{
					Id = text
				};
				BaseFolderType baseFolderType = this.targetMailbox.GetFolderByName(parentFolderId, Strings.Unsearchable);
				if (baseFolderType == null)
				{
					baseFolderType = this.targetMailbox.CreateFolder(parentFolderId, Strings.Unsearchable, false);
				}
				text = baseFolderType.FolderId.Id;
			}
			base.FolderMapping.Add("", text);
		}

		// Token: 0x04000084 RID: 132
		private const string Results = "Results";

		// Token: 0x04000085 RID: 133
		private readonly ITargetMailbox targetMailbox;

		// Token: 0x04000086 RID: 134
		private readonly IExportContext exportContext;
	}
}

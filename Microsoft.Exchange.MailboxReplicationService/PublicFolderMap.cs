using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000059 RID: 89
	internal class PublicFolderMap : FolderMap
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0001C7D5 File Offset: 0x0001A9D5
		public PublicFolderMap(List<FolderRecWrapper> folders) : base(folders)
		{
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001C7DE File Offset: 0x0001A9DE
		public override FolderRecWrapper RootRec
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		protected override void InsertFolderInternal(FolderRecWrapper rec)
		{
			if (this.folders.ContainsKey(rec.EntryId))
			{
				FolderRecWrapper folderRecWrapper = this.folders[rec.EntryId];
				MrsTracer.Service.Error("Folder {0} is listed more than once in the input folder list", new object[]
				{
					rec.FolderRec.ToString()
				});
				throw new FolderHierarchyContainsDuplicatesPermanentException(rec.FolderRec.ToString(), folderRecWrapper.FolderRec.ToString());
			}
			this.folders[rec.EntryId] = rec;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001C869 File Offset: 0x0001AA69
		protected override void ValidateMap()
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001C86C File Offset: 0x0001AA6C
		public override void EnumerateSubtree(EnumHierarchyFlags flags, FolderRecWrapper root, FolderMap.EnumFolderCallback callback)
		{
			FolderMap.EnumFolderContext enumFolderContext = new FolderMap.EnumFolderContext();
			enumFolderContext.Result = EnumHierarchyResult.Continue;
			foreach (KeyValuePair<byte[], FolderRecWrapper> keyValuePair in this.folders)
			{
				callback(keyValuePair.Value, enumFolderContext);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001CA60 File Offset: 0x0001AC60
		protected override IEnumerable<FolderRecWrapper> GetFolderList(EnumHierarchyFlags flags, FolderRecWrapper folderRec)
		{
			foreach (KeyValuePair<byte[], FolderRecWrapper> kvp in this.folders)
			{
				KeyValuePair<byte[], FolderRecWrapper> keyValuePair = kvp;
				yield return keyValuePair.Value;
			}
			yield break;
		}
	}
}

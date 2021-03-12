using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A7 RID: 2471
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MockTeamMailboxClientOperations : TeamMailboxClientOperations
	{
		// Token: 0x06005B39 RID: 23353 RVA: 0x0017D3F7 File Offset: 0x0017B5F7
		public MockTeamMailboxClientOperations(MiniRecipient miniRecipient) : base(null, false, miniRecipient, TimeSpan.MinValue, false)
		{
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0017D408 File Offset: 0x0017B608
		protected override void SharePointCreateFolder(string siteUrl, string parentFolderUrl, Guid listId, string folderName, out Guid? uniqueId, out string folderUrl)
		{
			MockTeamMailboxClientOperations.SharePointServer.CreateFolder(parentFolderUrl, folderName, out uniqueId, out folderUrl);
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0017D41B File Offset: 0x0017B61B
		protected override void SharePointlDeleteFolder(string siteUrl, string folderUrl)
		{
			MockTeamMailboxClientOperations.SharePointServer.DeleteFolder(folderUrl);
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x0017D428 File Offset: 0x0017B628
		protected override void SharePointCreateFile(string siteUrl, string folderUrl, string fileName, Stream contentStream, out Guid uniqueId, out string fileUrl, out int fileSize)
		{
			MockTeamMailboxClientOperations.SharePointServer.CreateFile(folderUrl, fileName, out uniqueId, out fileUrl, out fileSize);
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0017D43C File Offset: 0x0017B63C
		protected override void SharePointDeleteFile(string siteUrl, string fileUrl, bool isSharePointRecycleBinEnabled)
		{
			MockTeamMailboxClientOperations.SharePointServer.DeleteFile(fileUrl);
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x0017D449 File Offset: 0x0017B649
		protected override void SharePointMoveOrCopyFile(bool isCopy, string siteUrl, string sourceFileUrl, string destinationFolderUrl, out Guid uniqueId, out string destinationFileUrl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x0017D450 File Offset: 0x0017B650
		protected override bool SharePointIsRecycleBinEnabled(string siteUrl)
		{
			return true;
		}

		// Token: 0x04003259 RID: 12889
		public const string MockFailedCreateFileName = "6e2e937a-238f-4362-b4a1-51b5acc04c2f";

		// Token: 0x0400325A RID: 12890
		private static readonly MockTeamMailboxClientOperations.MockSharePointServer SharePointServer = new MockTeamMailboxClientOperations.MockSharePointServer();

		// Token: 0x020009A8 RID: 2472
		private class SharePointItem
		{
			// Token: 0x1700190A RID: 6410
			// (get) Token: 0x06005B41 RID: 23361 RVA: 0x0017D45F File Offset: 0x0017B65F
			// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0017D467 File Offset: 0x0017B667
			public Guid UniqueId { get; private set; }

			// Token: 0x1700190B RID: 6411
			// (get) Token: 0x06005B43 RID: 23363 RVA: 0x0017D470 File Offset: 0x0017B670
			// (set) Token: 0x06005B44 RID: 23364 RVA: 0x0017D478 File Offset: 0x0017B678
			public string Url { get; private set; }

			// Token: 0x06005B45 RID: 23365 RVA: 0x0017D481 File Offset: 0x0017B681
			public SharePointItem(string url)
			{
				this.Url = url;
				this.UniqueId = Guid.NewGuid();
			}
		}

		// Token: 0x020009A9 RID: 2473
		private sealed class SharePointFile : MockTeamMailboxClientOperations.SharePointItem
		{
			// Token: 0x1700190C RID: 6412
			// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0017D49B File Offset: 0x0017B69B
			// (set) Token: 0x06005B47 RID: 23367 RVA: 0x0017D4A3 File Offset: 0x0017B6A3
			public int Size { get; private set; }

			// Token: 0x06005B48 RID: 23368 RVA: 0x0017D4AC File Offset: 0x0017B6AC
			public SharePointFile(string url) : base(url)
			{
				Random random = new Random();
				this.Size = random.Next(1024, 10485760);
			}
		}

		// Token: 0x020009AA RID: 2474
		private sealed class SharePointFolder : MockTeamMailboxClientOperations.SharePointItem
		{
			// Token: 0x06005B49 RID: 23369 RVA: 0x0017D4DC File Offset: 0x0017B6DC
			public SharePointFolder(string url) : base(url)
			{
			}

			// Token: 0x06005B4A RID: 23370 RVA: 0x0017D4F0 File Offset: 0x0017B6F0
			public bool CreateFile(string url, out MockTeamMailboxClientOperations.SharePointFile file)
			{
				file = null;
				if (this.files.ContainsKey(url))
				{
					return false;
				}
				file = new MockTeamMailboxClientOperations.SharePointFile(url);
				this.files.Add(url, file);
				return true;
			}

			// Token: 0x06005B4B RID: 23371 RVA: 0x0017D51C File Offset: 0x0017B71C
			public void DeleteFile(string url)
			{
				if (this.files.ContainsKey(url))
				{
					this.files.Remove(url);
				}
			}

			// Token: 0x0400325E RID: 12894
			private readonly Dictionary<string, MockTeamMailboxClientOperations.SharePointFile> files = new Dictionary<string, MockTeamMailboxClientOperations.SharePointFile>();
		}

		// Token: 0x020009AB RID: 2475
		private sealed class MockSharePointServer
		{
			// Token: 0x06005B4D RID: 23373 RVA: 0x0017D558 File Offset: 0x0017B758
			public void CreateFile(string folderUrl, string fileName, out Guid uniqueId, out string fileUrl, out int fileSize)
			{
				MockTeamMailboxClientOperations.SharePointFolder sharePointFolder = null;
				uniqueId = Guid.Empty;
				fileUrl = null;
				fileSize = 0;
				if (fileName.Equals("6e2e937a-238f-4362-b4a1-51b5acc04c2f", StringComparison.OrdinalIgnoreCase))
				{
					throw new SharePointException(folderUrl, new LocalizedString("Failed to create file"));
				}
				lock (this.syncObject)
				{
					if (!this.folders.TryGetValue(folderUrl, out sharePointFolder))
					{
						sharePointFolder = new MockTeamMailboxClientOperations.SharePointFolder(folderUrl);
						this.folders.Add(folderUrl, sharePointFolder);
					}
					fileUrl = folderUrl + "/" + fileName;
					MockTeamMailboxClientOperations.SharePointFile sharePointFile = null;
					if (!sharePointFolder.CreateFile(fileUrl, out sharePointFile))
					{
						fileUrl = null;
						throw new SharePointException(fileUrl, new LocalizedString(string.Format("CreateFile:File {0} already existed in {1}", fileName, folderUrl)));
					}
					uniqueId = sharePointFile.UniqueId;
					fileSize = sharePointFile.Size;
				}
			}

			// Token: 0x06005B4E RID: 23374 RVA: 0x0017D63C File Offset: 0x0017B83C
			public void DeleteFile(string fileUrl)
			{
				lock (this.syncObject)
				{
					string key = fileUrl.Substring(0, fileUrl.LastIndexOf("/"));
					if (this.folders.ContainsKey(key))
					{
						this.folders[key].DeleteFile(fileUrl);
					}
				}
			}

			// Token: 0x06005B4F RID: 23375 RVA: 0x0017D6AC File Offset: 0x0017B8AC
			public void CreateFolder(string parentFolderUrl, string folderName, out Guid? uniqueId, out string folderUrl)
			{
				lock (this.syncObject)
				{
					uniqueId = new Guid?(Guid.Empty);
					folderUrl = parentFolderUrl + "/" + folderName;
					if (this.folders.ContainsKey(folderUrl))
					{
						folderUrl = null;
						throw new SharePointException(folderUrl, new LocalizedString(string.Format("CreateFolder:Folder {0} already under in {1}", folderName, parentFolderUrl)));
					}
					MockTeamMailboxClientOperations.SharePointFolder sharePointFolder = new MockTeamMailboxClientOperations.SharePointFolder(folderUrl);
					this.folders.Add(folderUrl, sharePointFolder);
					uniqueId = new Guid?(sharePointFolder.UniqueId);
				}
			}

			// Token: 0x06005B50 RID: 23376 RVA: 0x0017D75C File Offset: 0x0017B95C
			public void DeleteFolder(string folderUrl)
			{
				lock (this.syncObject)
				{
					if (this.folders.ContainsKey(folderUrl))
					{
						this.folders.Remove(folderUrl);
					}
				}
			}

			// Token: 0x0400325F RID: 12895
			private readonly object syncObject = new object();

			// Token: 0x04003260 RID: 12896
			private readonly Dictionary<string, MockTeamMailboxClientOperations.SharePointFolder> folders = new Dictionary<string, MockTeamMailboxClientOperations.SharePointFolder>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000025 RID: 37
	internal abstract class TargetFolderProvider<TFolderId, TFolder, TTargetSession> : TargetFolderProviderBase where TFolder : class
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000540F File Offset: 0x0000360F
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005417 File Offset: 0x00003617
		private protected DataContext DataContext { protected get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005420 File Offset: 0x00003620
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005428 File Offset: 0x00003628
		private protected Dictionary<string, TFolderId> FolderMapping { protected get; private set; }

		// Token: 0x06000150 RID: 336 RVA: 0x00005431 File Offset: 0x00003631
		public void Reset(DataContext dataContext)
		{
			this.DataContext = dataContext;
			this.FolderMapping = new Dictionary<string, TFolderId>();
			this.folderCache = new Dictionary<TFolderId, TFolder>();
			this.InitializeTargetFolderHierarchy();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005458 File Offset: 0x00003658
		public TFolder GetParentFolder(TTargetSession targetSession, string parentFolderPath, bool mapFolderHierarchy)
		{
			if (parentFolderPath == null)
			{
				return default(TFolder);
			}
			if (!mapFolderHierarchy)
			{
				parentFolderPath = "";
			}
			return this.CreateMappedFolder(targetSession, parentFolderPath);
		}

		// Token: 0x06000152 RID: 338
		protected abstract void InitializeTargetFolderHierarchy();

		// Token: 0x06000153 RID: 339
		protected abstract TFolder GetFolder(TTargetSession targetSession, TFolderId folderId);

		// Token: 0x06000154 RID: 340
		protected abstract string GenerateTopLevelFolderName(bool isArchive);

		// Token: 0x06000155 RID: 341
		protected abstract TFolder CreateFolder(TTargetSession targetSession, TFolder parentFolder, string folderName);

		// Token: 0x06000156 RID: 342
		protected abstract TFolderId GetFolderId(TFolder folder);

		// Token: 0x06000157 RID: 343 RVA: 0x00005484 File Offset: 0x00003684
		private TFolder CreateMappedFolder(TTargetSession targetSession, string serverFolderPath)
		{
			string text = serverFolderPath;
			TFolderId tfolderId;
			while (!this.FolderMapping.TryGetValue(text, out tfolderId))
			{
				text = text.Substring(0, text.LastIndexOf('\\'));
			}
			TFolder tfolder = default(TFolder);
			if (!this.folderCache.TryGetValue(tfolderId, out tfolder))
			{
				tfolder = this.GetFolder(targetSession, tfolderId);
				this.folderCache.Add(tfolderId, tfolder);
			}
			string text2 = serverFolderPath.Substring(text.Length, serverFolderPath.Length - text.Length);
			if (text2.Length > 0)
			{
				string[] array = text2.Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				StringBuilder stringBuilder = new StringBuilder(text);
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text3 = array2[i];
					stringBuilder.Append("\\");
					stringBuilder.Append(text3);
					text = stringBuilder.ToString();
					string a;
					if ((a = text) == null)
					{
						goto IL_105;
					}
					string folderName;
					if (!(a == "\\root"))
					{
						if (!(a == "\\archive"))
						{
							goto IL_105;
						}
						folderName = this.GenerateTopLevelFolderName(true);
					}
					else
					{
						folderName = this.GenerateTopLevelFolderName(false);
					}
					IL_109:
					tfolder = this.CreateFolder(targetSession, tfolder, folderName);
					TFolderId folderId = this.GetFolderId(tfolder);
					this.FolderMapping.Add(text, folderId);
					this.folderCache.Add(folderId, tfolder);
					i++;
					continue;
					IL_105:
					folderName = text3;
					goto IL_109;
				}
			}
			return tfolder;
		}

		// Token: 0x04000092 RID: 146
		private Dictionary<TFolderId, TFolder> folderCache;
	}
}

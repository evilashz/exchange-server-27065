using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x02000962 RID: 2402
	[DataContract]
	[Serializable]
	public sealed class FolderInfo
	{
		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x060058F7 RID: 22775 RVA: 0x0016DD78 File Offset: 0x0016BF78
		// (set) Token: 0x060058F8 RID: 22776 RVA: 0x0016DD80 File Offset: 0x0016BF80
		[DataMember]
		public string FolderPath { get; private set; }

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x060058F9 RID: 22777 RVA: 0x0016DD89 File Offset: 0x0016BF89
		// (set) Token: 0x060058FA RID: 22778 RVA: 0x0016DD91 File Offset: 0x0016BF91
		[DataMember]
		public ulong Size { get; private set; }

		// Token: 0x060058FB RID: 22779 RVA: 0x0016DD9A File Offset: 0x0016BF9A
		public FolderInfo(string path, ulong size)
		{
			this.FolderPath = path;
			this.Size = size;
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0016DDB0 File Offset: 0x0016BFB0
		public static Dictionary<string, FolderInfo> BuildFolderHierarchyMap(HierarchyFolderNode rootFolder, List<FolderInfo> foldersToProcess, string folderPathSeparator, char[] folderPathSplitCharacters)
		{
			Dictionary<string, FolderInfo> dictionary = new Dictionary<string, FolderInfo>();
			foreach (FolderInfo folderInfo in foldersToProcess)
			{
				StringBuilder stringBuilder = new StringBuilder();
				HierarchyFolderNode hierarchyFolderNode = rootFolder;
				string[] array = folderInfo.FolderPath.Split(folderPathSplitCharacters, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string value = array[i];
					stringBuilder.Append(folderPathSeparator);
					stringBuilder.Append(value);
					string text = stringBuilder.ToString();
					HierarchyFolderNode hierarchyFolderNode2 = hierarchyFolderNode.ChildNodes[text];
					if (hierarchyFolderNode2 == null)
					{
						hierarchyFolderNode2 = new HierarchyFolderNode(text);
						if (i == array.Length - 1)
						{
							hierarchyFolderNode2.TotalItemSize = folderInfo.Size;
							hierarchyFolderNode2.AggregateTotalItemSize = folderInfo.Size;
							dictionary[text] = folderInfo;
						}
						hierarchyFolderNode.ChildNodes.Add(text, hierarchyFolderNode2);
					}
					hierarchyFolderNode.AggregateTotalItemSize += folderInfo.Size;
					hierarchyFolderNode = hierarchyFolderNode2;
				}
			}
			return dictionary;
		}
	}
}

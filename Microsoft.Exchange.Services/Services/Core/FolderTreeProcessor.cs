using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderTreeProcessor
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00013108 File Offset: 0x00011308
		internal static T[] ShiftDefaultFoldersToTop<T>(T[] folderCollection, Func<T, DistinguishedFolderIdName> distinguishedFolderIdNameExtractor, Func<T, string> parentFolderIdExtractor, DistinguishedFolderIdName[] foldersToMoveToTop)
		{
			try
			{
				List<T> list = new List<T>(folderCollection.Length);
				FolderTreeNode[] requiredFoldersSubTrees = FolderTreeProcessor.GetRequiredFoldersSubTrees<T>(folderCollection, distinguishedFolderIdNameExtractor, parentFolderIdExtractor, foldersToMoveToTop);
				FolderTreeProcessor.AddRequiredFolderSubTree<T>(list, folderCollection, requiredFoldersSubTrees);
				FolderTreeProcessor.AddRestOfTheFolderTree<T>(list, folderCollection, requiredFoldersSubTrees);
				return list.ToArray();
			}
			catch (Exception ex)
			{
				if (!ExWatson.IsWatsonReportAlreadySent(ex))
				{
					ExWatson.SendReport(ex, ReportOptions.ReportTerminateAfterSend, null);
					ExWatson.SetWatsonReportAlreadySent(ex);
				}
			}
			return folderCollection;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00013170 File Offset: 0x00011370
		private static void AddRestOfTheFolderTree<T>(List<T> reOrderedList, T[] folderCollection, FolderTreeNode[] requiredFoldersSubTrees)
		{
			int i = 0;
			while (i < folderCollection.Length)
			{
				int num = FolderTreeProcessor.IsIndexSubFolderOfRequiredFolders(i, requiredFoldersSubTrees);
				if (num == -1)
				{
					reOrderedList.Add(folderCollection[i]);
					i++;
				}
				else
				{
					i += num;
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000131AC File Offset: 0x000113AC
		private static int IsIndexSubFolderOfRequiredFolders(int folderIndex, FolderTreeNode[] requiredFoldersSubTrees)
		{
			foreach (FolderTreeNode folderTreeNode in requiredFoldersSubTrees)
			{
				if (folderTreeNode != null && folderIndex == folderTreeNode.Index)
				{
					return folderTreeNode.DescendantCount;
				}
			}
			return -1;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000131E8 File Offset: 0x000113E8
		private static void AddRequiredFolderSubTree<T>(List<T> reOrderedList, T[] folderCollection, FolderTreeNode[] requiredFoldersSubTrees)
		{
			foreach (FolderTreeNode folderTreeNode in requiredFoldersSubTrees)
			{
				if (folderTreeNode != null)
				{
					for (int j = folderTreeNode.Index; j < folderTreeNode.Index + folderTreeNode.DescendantCount; j++)
					{
						reOrderedList.Add(folderCollection[j]);
					}
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013238 File Offset: 0x00011438
		private static FolderTreeNode[] GetRequiredFoldersSubTrees<T>(T[] folderCollection, Func<T, DistinguishedFolderIdName> distinguishedFolderIdNameExtractor, Func<T, string> parentFolderIdExtractor, DistinguishedFolderIdName[] foldersToMoveToTop)
		{
			FolderTreeNode[] array = new FolderTreeNode[foldersToMoveToTop.Length];
			int num = 0;
			int num2 = 0;
			while (num2 < folderCollection.Length && num <= array.Length)
			{
				T arg = folderCollection[num2];
				DistinguishedFolderIdName folderIdName = distinguishedFolderIdNameExtractor(arg);
				int indexToInsertTreeNode = FolderTreeProcessor.GetIndexToInsertTreeNode(folderIdName, foldersToMoveToTop);
				if (indexToInsertTreeNode == -1)
				{
					num2++;
				}
				else
				{
					int numberOfSubFolders = FolderTreeProcessor.GetNumberOfSubFolders<T>(folderCollection, num2, parentFolderIdExtractor);
					FolderTreeNode folderTreeNode = new FolderTreeNode(num2, numberOfSubFolders);
					array[indexToInsertTreeNode] = folderTreeNode;
					num2 += numberOfSubFolders;
					num++;
				}
			}
			return array;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000132AC File Offset: 0x000114AC
		private static int GetIndexToInsertTreeNode(DistinguishedFolderIdName folderIdName, DistinguishedFolderIdName[] foldersToMoveToTop)
		{
			for (int i = 0; i < foldersToMoveToTop.Length; i++)
			{
				DistinguishedFolderIdName distinguishedFolderIdName = foldersToMoveToTop[i];
				if (folderIdName == distinguishedFolderIdName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000132D4 File Offset: 0x000114D4
		private static int GetNumberOfSubFolders<T>(T[] folderCollection, int index, Func<T, string> parentFolderIdExtractor)
		{
			int num = 1;
			T arg = folderCollection[index];
			for (int i = index + 1; i < folderCollection.Length; i++)
			{
				T arg2 = folderCollection[i];
				if (!(parentFolderIdExtractor(arg2) != parentFolderIdExtractor(arg)))
				{
					break;
				}
				num++;
			}
			return num;
		}
	}
}

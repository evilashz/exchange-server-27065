using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AB RID: 171
	internal class FileSearchUtil
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00019B75 File Offset: 0x00017D75
		public FileSearchUtil(string dirRoot, string searchPattern)
		{
			if (string.IsNullOrEmpty(dirRoot))
			{
				throw new ArgumentException("Argument 'path' was null or emtpy.");
			}
			if (string.IsNullOrEmpty(searchPattern))
			{
				throw new ArgumentException("Argument 'searchPattern' was null or emtpy.");
			}
			this.path = dirRoot;
			this.searchPattern = searchPattern;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00019BB1 File Offset: 0x00017DB1
		public List<string> GetFilesRecurse()
		{
			this.fileList = new List<string>();
			this.TraverseDirectoryTree(this.path, 10);
			return this.fileList;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00019BD4 File Offset: 0x00017DD4
		private void TraverseDirectoryTree(string root, int maxDirectoryDepth)
		{
			KeyValuePair<string, int> item = new KeyValuePair<string, int>(root, 0);
			Stack<KeyValuePair<string, int>> stack = new Stack<KeyValuePair<string, int>>();
			if (!Directory.Exists(root))
			{
				throw new ArgumentException("Directory 'root' does not exist", "root");
			}
			stack.Push(item);
			while (stack.Count > 0)
			{
				KeyValuePair<string, int> keyValuePair = stack.Pop();
				string[] array = null;
				try
				{
					array = Directory.GetDirectories(keyValuePair.Key);
				}
				catch (UnauthorizedAccessException)
				{
					continue;
				}
				catch (DirectoryNotFoundException)
				{
					continue;
				}
				if (keyValuePair.Value < maxDirectoryDepth)
				{
					foreach (string key in array)
					{
						stack.Push(new KeyValuePair<string, int>(key, keyValuePair.Value + 1));
					}
				}
				FileInfo[] array3 = null;
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(keyValuePair.Key);
					array3 = directoryInfo.GetFiles(this.searchPattern);
				}
				catch (UnauthorizedAccessException)
				{
					continue;
				}
				catch (DirectoryNotFoundException)
				{
					continue;
				}
				if (array3 != null)
				{
					foreach (FileInfo fileInfo in array3)
					{
						this.fileList.Add(fileInfo.FullName);
					}
				}
			}
		}

		// Token: 0x04000181 RID: 385
		private const int recurseDepthLimit = 10;

		// Token: 0x04000182 RID: 386
		private string path;

		// Token: 0x04000183 RID: 387
		private string searchPattern;

		// Token: 0x04000184 RID: 388
		private List<string> fileList;
	}
}

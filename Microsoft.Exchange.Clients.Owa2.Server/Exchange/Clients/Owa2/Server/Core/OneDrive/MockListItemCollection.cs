using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000025 RID: 37
	public class MockListItemCollection : MockClientObject<ListItemCollection>, IListItemCollection, IClientObjectCollection<IListItem, ListItemCollection>, IClientObject<ListItemCollection>, IEnumerable<IListItem>, IEnumerable
	{
		// Token: 0x17000049 RID: 73
		public IListItem this[int index]
		{
			get
			{
				return this.interalList[index];
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000362A File Offset: 0x0000182A
		public MockListItemCollection(MockClientContext context, string listTitle, string relativePath, List<KeyValuePair<string, bool>> orderBy, List<string> viewFields, int rowLimit, ListItemCollectionPosition listItemCollectionPosition)
		{
			this.context = context;
			this.listTitle = listTitle;
			this.relativePath = relativePath;
			this.orderBy = orderBy;
			this.viewFields = viewFields;
			this.rowLimit = rowLimit;
			this.listItemCollectionPosition = listItemCollectionPosition;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003674 File Offset: 0x00001874
		public override void LoadMockData()
		{
			string text = string.IsNullOrEmpty(this.relativePath) ? this.listTitle : this.relativePath;
			string path = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, text);
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			List<string> list = new List<string>(this.viewFields);
			foreach (KeyValuePair<string, bool> keyValuePair in this.orderBy)
			{
				list.Add(keyValuePair.Key);
			}
			List<MockListItem> list2 = new List<MockListItem>();
			foreach (DirectoryInfo dirInfo in directoryInfo.GetDirectories())
			{
				list2.Add(new MockListItem(dirInfo, text, this.context));
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				list2.Add(new MockListItem(fileInfo, text, this.context));
			}
			if (this.orderBy != null && this.orderBy.Count > 0)
			{
				list2.Sort((MockListItem item1, MockListItem item2) => this.Compare(item1, item2, 0));
			}
			ListItemCollectionPosition listItemCollectionPosition = this.listItemCollectionPosition;
			if (this.rowLimit > 0 && this.rowLimit < list2.Count)
			{
				MockListItem[] array = new MockListItem[this.rowLimit];
				list2.CopyTo(0, array, 0, this.rowLimit);
				list2 = new List<MockListItem>(array);
			}
			this.interalList = list2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003804 File Offset: 0x00001A04
		public int Count()
		{
			return this.interalList.Count;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003811 File Offset: 0x00001A11
		public IEnumerator<IListItem> GetEnumerator()
		{
			return this.interalList.GetEnumerator();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003823 File Offset: 0x00001A23
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000382C File Offset: 0x00001A2C
		private int Compare(MockListItem item1, MockListItem item2, int level)
		{
			if (level == this.orderBy.Count)
			{
				return 0;
			}
			string key = this.orderBy[level].Key;
			bool value = this.orderBy[level].Value;
			int num = item1[key].ToString().CompareTo(item2[key].ToString()) * (value ? 1 : -1);
			if (num != 0)
			{
				return num;
			}
			return this.Compare(item1, item2, level + 1);
		}

		// Token: 0x04000047 RID: 71
		private readonly ListItemCollectionPosition listItemCollectionPosition;

		// Token: 0x04000048 RID: 72
		private readonly string listTitle;

		// Token: 0x04000049 RID: 73
		private readonly string relativePath;

		// Token: 0x0400004A RID: 74
		private readonly MockClientContext context;

		// Token: 0x0400004B RID: 75
		private readonly List<KeyValuePair<string, bool>> orderBy;

		// Token: 0x0400004C RID: 76
		private readonly List<string> viewFields;

		// Token: 0x0400004D RID: 77
		private readonly int rowLimit;

		// Token: 0x0400004E RID: 78
		private List<MockListItem> interalList;
	}
}

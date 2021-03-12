using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200002F RID: 47
	internal static class BookmarkHelper
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000060E4 File Offset: 0x000042E4
		public static void RebuildBookmarksWithNewText(List<Bookmark> bookmarks, string newText)
		{
			List<Bookmark> list = new List<Bookmark>(bookmarks.Count);
			int num = 0;
			while (bookmarks.Count > num)
			{
				list.Insert(list.Count, new Bookmark(newText, bookmarks[num].PartType, bookmarks[num].PartNumber, bookmarks[num].CodingScheme, bookmarks[num].BeginLocation, bookmarks[num].EndLocation, bookmarks[num].IncompleteBegin, bookmarks[num].IncompleteEnd));
				num++;
			}
			bookmarks.Clear();
			bookmarks.AddRange(list);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000061A4 File Offset: 0x000043A4
		public static void RebuildBookmarksWithTrailingEllipsis(List<Bookmark> bookmarks)
		{
			string text = bookmarks[bookmarks.Count - 1].FullText.Substring(0, bookmarks[bookmarks.Count - 1].EndLocation + 1);
			text = new EllipsisTrailer(bookmarks[bookmarks.Count - 1].CodingScheme).Trail(text);
			bookmarks[bookmarks.Count - 1] = new Bookmark(text, bookmarks[bookmarks.Count - 1].PartType, bookmarks[bookmarks.Count - 1].PartNumber, bookmarks[bookmarks.Count - 1].CodingScheme, bookmarks[bookmarks.Count - 1].BeginLocation, text.Length - 1, bookmarks[bookmarks.Count - 1].IncompleteBegin, bookmarks[bookmarks.Count - 1].IncompleteEnd);
			BookmarkHelper.RebuildBookmarksWithNewText(bookmarks, text);
		}
	}
}

using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	public class QueueViewerSortOrderEntry
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002783B File Offset: 0x00025A3B
		public QueueViewerSortOrderEntry(string property, ListSortDirection sortDirection)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.propertyName = property;
			this.sortDirection = sortDirection;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002785F File Offset: 0x00025A5F
		public string Property
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00027867 File Offset: 0x00025A67
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00027870 File Offset: 0x00025A70
		public static QueueViewerSortOrderEntry Parse(string s)
		{
			if (s == null || s.Trim().Length < 2)
			{
				throw new FormatException(string.Format("The sort order format {0} is invalid. The format is: [+/-]PropertyName, where '+' indicates ascending sort; '-' indicates descending sort, and PropertyName is the name of the property to sort by.", s));
			}
			ListSortDirection listSortDirection;
			switch (s[0])
			{
			case '+':
				listSortDirection = ListSortDirection.Ascending;
				goto IL_5A;
			case '-':
				listSortDirection = ListSortDirection.Descending;
				goto IL_5A;
			}
			throw new FormatException(string.Format("The sort order format {0} is invalid. The format is: [+/-]PropertyName, where '+' indicates ascending sort; '-' indicates descending sort, and PropertyName is the name of the property to sort by.", s));
			IL_5A:
			string property = s.Substring(1);
			return new QueueViewerSortOrderEntry(property, listSortDirection);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000278E8 File Offset: 0x00025AE8
		public override string ToString()
		{
			string arg = string.Empty;
			switch (this.SortDirection)
			{
			case ListSortDirection.Ascending:
				arg = "+";
				break;
			case ListSortDirection.Descending:
				arg = "-";
				break;
			}
			return string.Format("{0}{1}", arg, this.Property);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00027934 File Offset: 0x00025B34
		public override bool Equals(object obj)
		{
			QueueViewerSortOrderEntry queueViewerSortOrderEntry = obj as QueueViewerSortOrderEntry;
			return queueViewerSortOrderEntry != null && StringComparer.Ordinal.Compare(this.Property, queueViewerSortOrderEntry.Property) == 0 && this.SortDirection == queueViewerSortOrderEntry.SortDirection;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002797C File Offset: 0x00025B7C
		public override int GetHashCode()
		{
			return this.Property.GetHashCode() ^ this.SortDirection.GetHashCode();
		}

		// Token: 0x040007CB RID: 1995
		private string propertyName;

		// Token: 0x040007CC RID: 1996
		private ListSortDirection sortDirection;
	}
}

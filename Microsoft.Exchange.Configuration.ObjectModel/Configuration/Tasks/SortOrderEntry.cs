using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public class SortOrderEntry
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000169FD File Offset: 0x00014BFD
		public SortOrderEntry(string property, ListSortDirection sortDirection)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.propertyName = property;
			this.sortDirection = sortDirection;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00016A21 File Offset: 0x00014C21
		public string Property
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00016A29 File Offset: 0x00014C29
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00016A34 File Offset: 0x00014C34
		public static SortOrderEntry Parse(string s)
		{
			if (s == null || s.Trim().Length < 2)
			{
				throw new SortOrderFormatException(s);
			}
			ListSortDirection listSortDirection;
			switch (s[0])
			{
			case '+':
				listSortDirection = ListSortDirection.Ascending;
				goto IL_46;
			case '-':
				listSortDirection = ListSortDirection.Descending;
				goto IL_46;
			}
			throw new SortOrderFormatException(s);
			IL_46:
			string property = s.Substring(1);
			return new SortOrderEntry(property, listSortDirection);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00016A98 File Offset: 0x00014C98
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

		// Token: 0x06000606 RID: 1542 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public override bool Equals(object obj)
		{
			SortOrderEntry sortOrderEntry = obj as SortOrderEntry;
			return sortOrderEntry != null && string.Compare(this.Property, sortOrderEntry.Property, StringComparison.Ordinal) == 0 && this.SortDirection == sortOrderEntry.SortDirection;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016B28 File Offset: 0x00014D28
		public override int GetHashCode()
		{
			return this.Property.GetHashCode() ^ this.SortDirection.GetHashCode();
		}

		// Token: 0x04000138 RID: 312
		private string propertyName;

		// Token: 0x04000139 RID: 313
		private ListSortDirection sortDirection;
	}
}

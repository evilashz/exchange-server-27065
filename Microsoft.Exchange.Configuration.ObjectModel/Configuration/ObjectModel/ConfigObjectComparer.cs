using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000019 RID: 25
	internal class ConfigObjectComparer : IComparer
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004D48 File Offset: 0x00002F48
		public ConfigObjectComparer(string property, ListSortDirection direction)
		{
			if (string.IsNullOrEmpty(property))
			{
				throw new ArgumentException("Argument 'property' was null or emtpy.");
			}
			this.propertyToSort = property;
			this.sortDirection = direction;
			this.sorts = null;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004D78 File Offset: 0x00002F78
		public ConfigObjectComparer(ListSortDescriptionCollection sorts)
		{
			if (sorts == null)
			{
				throw new ArgumentNullException("sorts");
			}
			this.sorts = sorts;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004D98 File Offset: 0x00002F98
		public ConfigObjectComparer(ListSortDescription sort)
		{
			if (sort == null)
			{
				throw new ArgumentNullException("sort");
			}
			this.sorts = new ListSortDescriptionCollection(new ListSortDescription[]
			{
				sort
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004DD0 File Offset: 0x00002FD0
		int IComparer.Compare(object x, object y)
		{
			int num = 0;
			if (!typeof(ConfigObject).IsAssignableFrom(x.GetType()) || !typeof(ConfigObject).IsAssignableFrom(x.GetType()))
			{
				throw new ArgumentException();
			}
			if (x != null && y != null)
			{
				if (this.sorts != null)
				{
					using (IEnumerator enumerator = ((IEnumerable)this.sorts).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							ListSortDescription listSortDescription = (ListSortDescription)obj;
							string name = listSortDescription.PropertyDescriptor.Name;
							num = this.CompareObjectProperties(x, y, listSortDescription.PropertyDescriptor.Name, listSortDescription.SortDirection == ListSortDirection.Descending);
							if (num != 0)
							{
								break;
							}
						}
						return num;
					}
				}
				num = this.CompareObjectProperties(x, y, this.propertyToSort, this.sortDirection == ListSortDirection.Descending);
			}
			else if (x == null && y != null)
			{
				num = -1;
			}
			else if (y == null && x != null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004ECC File Offset: 0x000030CC
		private int CompareObjectProperties(object x, object y, string property, bool reverseResult)
		{
			object obj = (x as ConfigObject).Fields[property];
			object obj2 = (y as ConfigObject).Fields[property];
			if (!(obj is IComparable) || !(obj2 is IComparable))
			{
				throw new ArgumentException();
			}
			int num = (obj as IComparable).CompareTo(obj2);
			if (reverseResult)
			{
				num = -1 * num;
			}
			return num;
		}

		// Token: 0x04000051 RID: 81
		private ListSortDescriptionCollection sorts;

		// Token: 0x04000052 RID: 82
		private string propertyToSort;

		// Token: 0x04000053 RID: 83
		private ListSortDirection sortDirection;
	}
}

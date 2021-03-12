using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000021 RID: 33
	public class ObjectComparer : IObjectComparer, ISupportTextComparer
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000079BA File Offset: 0x00005BBA
		public ObjectComparer(ITextComparer textComparer)
		{
			if (textComparer == null)
			{
				throw new ArgumentNullException("textComparer");
			}
			this.TextComparer = textComparer;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000079D7 File Offset: 0x00005BD7
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000079DF File Offset: 0x00005BDF
		public ITextComparer TextComparer { get; private set; }

		// Token: 0x060001BD RID: 445 RVA: 0x000079E8 File Offset: 0x00005BE8
		public SortMode GetSortMode(Type type)
		{
			if (typeof(IComparable).IsAssignableFrom(type) && !typeof(Enum).IsAssignableFrom(type))
			{
				return SortMode.Standard;
			}
			return SortMode.Custom;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007A14 File Offset: 0x00005C14
		public int Compare(object x, object y, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			if (this.IsNullValue(x))
			{
				if (!this.IsNullValue(y))
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this.IsNullValue(y))
				{
					return 1;
				}
				IComparable comparable = this.ConverToIComparable(x, customFormatter, formatProvider, formatString, defaultEmptyText);
				IComparable obj = this.ConverToIComparable(y, customFormatter, formatProvider, formatString, defaultEmptyText);
				return comparable.CompareTo(obj);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007A67 File Offset: 0x00005C67
		private bool IsNullValue(object item)
		{
			return item == null || DBNull.Value.Equals(item);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007A7C File Offset: 0x00005C7C
		private IComparable ConverToIComparable(object item, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			IComparable comparable = this.ToComparable(item);
			if (comparable == null)
			{
				comparable = this.TextComparer.Format(item, customFormatter, formatProvider, formatString, string.Empty);
			}
			return comparable;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007AAC File Offset: 0x00005CAC
		private IComparable ToComparable(object item)
		{
			if (this.GetSortMode(item.GetType()) == SortMode.Standard)
			{
				return item as IComparable;
			}
			if (item is Enum)
			{
				return new EnumObject(item as Enum);
			}
			if (item.GetType().IsGenericType && item.GetType().GetGenericTypeDefinition() == typeof(IComparable<>))
			{
				return new ObjectComparer.GenericComparableAdapter(item);
			}
			if (item.GetType().IsGenericType && item.GetType().GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return this.ToComparable(ObjectComparer.GetPropertyValue(typeof(Nullable<>), item, "Value"));
			}
			return null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007B59 File Offset: 0x00005D59
		private static object GetPropertyValue(Type t, object obj, string propertyName)
		{
			return t.GetProperty(propertyName).GetValue(obj, null);
		}

		// Token: 0x04000070 RID: 112
		public static ObjectComparer DefaultObjectComparer = new ObjectComparer(Microsoft.Exchange.Management.SystemManager.TextComparer.DefaultTextComparer);

		// Token: 0x02000022 RID: 34
		private class GenericComparableAdapter : IComparable
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x00007B7A File Offset: 0x00005D7A
			public GenericComparableAdapter(object genericComparable)
			{
				if (genericComparable == null)
				{
					throw new ArgumentNullException();
				}
				this.genericComparable = genericComparable;
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00007B94 File Offset: 0x00005D94
			public int CompareTo(object obj)
			{
				if (DBNull.Value.Equals(obj))
				{
					obj = null;
				}
				if (obj != null && !obj.GetType().Equals(this.genericComparable.GetType()))
				{
					return 0;
				}
				return (int)typeof(IComparable<>).GetMethod("CompareTo").Invoke(this.genericComparable, new object[]
				{
					obj
				});
			}

			// Token: 0x04000072 RID: 114
			private object genericComparable;
		}
	}
}

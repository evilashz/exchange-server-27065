using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000024 RID: 36
	public class ComparableTypeConverter : TypeConverter
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00007CA7 File Offset: 0x00005EA7
		public ComparableTypeConverter(IComparer comparer)
		{
			this.Comparer = comparer;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007CB6 File Offset: 0x00005EB6
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007CBE File Offset: 0x00005EBE
		public IComparer Comparer { get; private set; }

		// Token: 0x060001D5 RID: 469 RVA: 0x00007CC7 File Offset: 0x00005EC7
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007CCA File Offset: 0x00005ECA
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return new ComparableTypeConverter.ObjectWithComparerAdapter(value, this.Comparer);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007CDF File Offset: 0x00005EDF
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x02000025 RID: 37
		private class ObjectWithComparerAdapter : IComparable
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00007CE6 File Offset: 0x00005EE6
			public ObjectWithComparerAdapter(object item, IComparer comparer)
			{
				this.Item = item;
				this.Comparer = comparer;
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060001DA RID: 474 RVA: 0x00007CFC File Offset: 0x00005EFC
			// (set) Token: 0x060001DB RID: 475 RVA: 0x00007D04 File Offset: 0x00005F04
			public object Item { get; private set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060001DC RID: 476 RVA: 0x00007D0D File Offset: 0x00005F0D
			// (set) Token: 0x060001DD RID: 477 RVA: 0x00007D15 File Offset: 0x00005F15
			public IComparer Comparer { get; private set; }

			// Token: 0x060001DE RID: 478 RVA: 0x00007D1E File Offset: 0x00005F1E
			public int CompareTo(object obj)
			{
				if (obj == null || DBNull.Value.Equals(obj))
				{
					return 1;
				}
				return this.Comparer.Compare(this.Item, (obj as ComparableTypeConverter.ObjectWithComparerAdapter).Item);
			}
		}
	}
}

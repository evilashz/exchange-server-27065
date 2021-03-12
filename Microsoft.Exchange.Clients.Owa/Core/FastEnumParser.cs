using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200010E RID: 270
	public class FastEnumParser : IComparer
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x00040EA8 File Offset: 0x0003F0A8
		public FastEnumParser(Type enumType, bool ignoreCase)
		{
			this.names = Enum.GetNames(enumType);
			Array values = Enum.GetValues(enumType);
			this.items = new FastEnumParser.EnumItem[this.names.Length];
			for (int i = 0; i < this.names.Length; i++)
			{
				this.items[i] = new FastEnumParser.EnumItem(this.names[i], values.GetValue(i));
			}
			if (ignoreCase)
			{
				this.stringComparer = StringComparer.OrdinalIgnoreCase;
			}
			else
			{
				this.stringComparer = StringComparer.Ordinal;
			}
			Array.Sort(this.items, this);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00040F37 File Offset: 0x0003F137
		public FastEnumParser(Type enumType) : this(enumType, false)
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00040F44 File Offset: 0x0003F144
		int IComparer.Compare(object x, object y)
		{
			FastEnumParser.EnumItem enumItem = x as FastEnumParser.EnumItem;
			FastEnumParser.EnumItem enumItem2 = y as FastEnumParser.EnumItem;
			string x2;
			if (enumItem != null)
			{
				x2 = enumItem.Name;
			}
			else
			{
				x2 = (string)x;
			}
			string y2;
			if (enumItem2 != null)
			{
				y2 = enumItem2.Name;
			}
			else
			{
				y2 = (string)y;
			}
			return this.stringComparer.Compare(x2, y2);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00040F96 File Offset: 0x0003F196
		public string GetString(int value)
		{
			return this.names[value];
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00040FA0 File Offset: 0x0003F1A0
		public object Parse(string value)
		{
			int num = Array.BinarySearch(this.items, value, this);
			if (num < 0)
			{
				return null;
			}
			return this.items[num].Value;
		}

		// Token: 0x0400065D RID: 1629
		private FastEnumParser.EnumItem[] items;

		// Token: 0x0400065E RID: 1630
		private string[] names;

		// Token: 0x0400065F RID: 1631
		private StringComparer stringComparer;

		// Token: 0x0200010F RID: 271
		private class EnumItem
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x00040FCE File Offset: 0x0003F1CE
			public EnumItem(string name, object value)
			{
				this.Name = name;
				this.Value = value;
			}

			// Token: 0x04000660 RID: 1632
			public string Name;

			// Token: 0x04000661 RID: 1633
			public object Value;
		}
	}
}

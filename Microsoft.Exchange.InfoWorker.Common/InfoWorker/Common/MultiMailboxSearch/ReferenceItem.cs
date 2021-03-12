using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200020D RID: 525
	internal class ReferenceItem
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x0003DE50 File Offset: 0x0003C050
		public ReferenceItem(SortBy column, object value, long secondarySortValue)
		{
			Util.ThrowOnNull(column, "column");
			Util.ThrowOnNull(value, "value");
			this.sortBy = column;
			Type type = Nullable.GetUnderlyingType(column.ColumnDefinition.Type) ?? column.ColumnDefinition.Type;
			if (type.Equals(typeof(ExDateTime)))
			{
				this.sortColumnValue = ExDateTime.Parse(((value is ExDateTime) ? ((ExDateTime)value).UniversalTime : ExDateTime.MinValue.UniversalTime).ToString(ReferenceItem.DateTimeWithoutMillisecondsFormatString, DateTimeFormatInfo.InvariantInfo));
			}
			else
			{
				this.sortColumnValue = value;
			}
			this.secondarySortValue = secondarySortValue;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0003DF09 File Offset: 0x0003C109
		public long SecondarySortValue
		{
			get
			{
				return this.secondarySortValue;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0003DF11 File Offset: 0x0003C111
		public PropertyDefinition SortColumn
		{
			get
			{
				return this.sortBy.ColumnDefinition;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0003DF1E File Offset: 0x0003C11E
		public SortBy SortBy
		{
			get
			{
				return this.sortBy;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003DF26 File Offset: 0x0003C126
		public object SortColumnValue
		{
			get
			{
				return this.sortColumnValue;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003DF2E File Offset: 0x0003C12E
		public int MailboxIdHash
		{
			get
			{
				return (int)(this.secondarySortValue >> 32);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0003DF3A File Offset: 0x0003C13A
		public int DocId
		{
			get
			{
				return (int)(this.secondarySortValue & (long)((ulong)-1));
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003DF48 File Offset: 0x0003C148
		public static ReferenceItem Parse(SortBy sortBy, string serializedItem)
		{
			int num = -1;
			if (!int.TryParse(serializedItem.Substring(0, ReferenceItem.LengthOfSortColumnValueLengthPart), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
			{
				throw new ArgumentException("Invalid serialized item as the sort column value length is invalid");
			}
			if (num < 0 || num > ReferenceItem.MaximumStringPropertyLength)
			{
				throw new ArgumentException("Invalid serialized item as the sort column value length is negative or greater than 256");
			}
			if (serializedItem.Length != ReferenceItem.LengthOfSortColumnValueLengthPart + num + ReferenceItem.LengthOfSecondarySortValuePart)
			{
				throw new ArgumentException("Invalid serialized item as the length is not consistent");
			}
			object value = ReferenceItem.ConvertStringToSortColumnValue(sortBy.ColumnDefinition, serializedItem.Substring(ReferenceItem.LengthOfSortColumnValueLengthPart, num));
			long num2 = 0L;
			if (!long.TryParse(serializedItem.Substring(ReferenceItem.LengthOfSortColumnValueLengthPart + num), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
			{
				throw new ArgumentException("Invalid serialized item as the reference id is invalid");
			}
			if (num2 < 0L)
			{
				throw new ArgumentException("Invalid serialized item as the secondary sort value is negative");
			}
			return new ReferenceItem(sortBy, value, num2);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003E018 File Offset: 0x0003C218
		public override bool Equals(object obj)
		{
			ReferenceItem referenceItem = obj as ReferenceItem;
			return referenceItem != null && this.SortBy.ColumnDefinition.Equals(referenceItem.SortBy.ColumnDefinition) && this.SortBy.SortOrder == referenceItem.SortBy.SortOrder && this.SortColumnValue.Equals(referenceItem.SortColumnValue) && this.SecondarySortValue == referenceItem.SecondarySortValue;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003E08A File Offset: 0x0003C28A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003E094 File Offset: 0x0003C294
		public override string ToString()
		{
			StringBuilder stringBuilder = ReferenceItem.ConvertSortValueToString(this.sortBy.ColumnDefinition, this.sortColumnValue);
			stringBuilder.AppendFormat("{0:X16}", this.SecondarySortValue);
			return stringBuilder.ToString();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003E0D8 File Offset: 0x0003C2D8
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			ReferenceItem referenceItem = obj as ReferenceItem;
			if (referenceItem == null)
			{
				throw new ArgumentException("Object is not a ReferenceItem");
			}
			if (!this.SortBy.ColumnDefinition.Equals(referenceItem.SortBy.ColumnDefinition))
			{
				throw new ArgumentException("Cannot compare two reference items with different sort columns");
			}
			if (this.SortBy.SortOrder != referenceItem.SortBy.SortOrder)
			{
				throw new ArgumentException("Cannot compare two reference items with different sort order");
			}
			if (this.SortColumnValue == null && referenceItem.SortColumnValue != null)
			{
				return -1;
			}
			if (this.SortColumnValue != null && referenceItem.SortColumnValue == null)
			{
				return 1;
			}
			if (this.SortColumnValue.Equals(referenceItem.SortColumnValue))
			{
				return this.SecondarySortValue.CompareTo(referenceItem.SecondarySortValue);
			}
			IComparable comparable = this.SortColumnValue as IComparable;
			IComparable obj2 = referenceItem.SortColumnValue as IComparable;
			return comparable.CompareTo(obj2);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		private static StringBuilder ConvertSortValueToString(PropertyDefinition column, object value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type left = Nullable.GetUnderlyingType(column.Type) ?? column.Type;
			if (left != typeof(int) && left != typeof(long) && left != typeof(ExDateTime))
			{
				throw new ArgumentException("Invalid sort column. We support only sort property with type int, long, ExDateTime");
			}
			if (left == typeof(int))
			{
				stringBuilder.AppendFormat("{0:X4}{1:X8}", 8, value);
			}
			if (left == typeof(long))
			{
				stringBuilder.AppendFormat("{0:X4}{1:X16}", 16, value);
			}
			if (left == typeof(ExDateTime))
			{
				string text = ((ExDateTime)value).UniversalTime.ToString(ReferenceItem.DateTimeFormatString, DateTimeFormatInfo.InvariantInfo);
				stringBuilder.AppendFormat("{0:X4}{1}", text.Length, text);
			}
			return stringBuilder;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		private static object ConvertStringToSortColumnValue(PropertyDefinition column, string stringValue)
		{
			Type left = Nullable.GetUnderlyingType(column.Type) ?? column.Type;
			object result = null;
			if (left != typeof(int) && left != typeof(long) && left != typeof(ExDateTime))
			{
				throw new ArgumentException("Invalid sort column. We support only sort property with type int, long, ExDateTime");
			}
			if (left == typeof(int))
			{
				int num;
				if (!int.TryParse(stringValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
				{
					throw new ArgumentException("Invalid string value as int");
				}
				result = num;
			}
			if (left == typeof(long))
			{
				long num2;
				if (!long.TryParse(stringValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
				{
					throw new ArgumentException("Invalid string value as long");
				}
				result = num2;
			}
			if (left == typeof(ExDateTime))
			{
				ExDateTime exDateTime;
				if (!ExDateTime.TryParseExact(ExTimeZone.UtcTimeZone, stringValue, ReferenceItem.DateTimeFormatString, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out exDateTime))
				{
					throw new ArgumentException("Invalid string value as ExDateTime");
				}
				result = exDateTime;
			}
			return result;
		}

		// Token: 0x040009BE RID: 2494
		private static readonly int LengthOfSortColumnValueLengthPart = 4;

		// Token: 0x040009BF RID: 2495
		private static readonly int LengthOfSecondarySortValuePart = 16;

		// Token: 0x040009C0 RID: 2496
		private static readonly string DateTimeFormatString = "yyyy-MM-ddTHH:mm:ss.fffffff";

		// Token: 0x040009C1 RID: 2497
		private static readonly string DateTimeWithoutMillisecondsFormatString = "yyyy-MM-ddTHH:mm:ss";

		// Token: 0x040009C2 RID: 2498
		private static readonly int MaximumStringPropertyLength = 256;

		// Token: 0x040009C3 RID: 2499
		private readonly SortBy sortBy;

		// Token: 0x040009C4 RID: 2500
		private readonly long secondarySortValue;

		// Token: 0x040009C5 RID: 2501
		private readonly object sortColumnValue;
	}
}

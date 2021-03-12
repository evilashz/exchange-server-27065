using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000028 RID: 40
	public struct ValueRange
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00012D2C File Offset: 0x00010F2C
		public ValueRange(MinValue minValue, MaxValue maxValue, Column column, CompareInfo compareInfo)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00012D3C File Offset: 0x00010F3C
		public bool IsEmpty
		{
			get
			{
				return !this.MaxValue.IsValid;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00012D5C File Offset: 0x00010F5C
		public bool IsFull
		{
			get
			{
				return this.MinValue.IsInfinity && this.MaxValue.IsInfinity;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00012D8C File Offset: 0x00010F8C
		public static bool Equal(ValueRange range1, ValueRange range2, CompareInfo compareInfo)
		{
			return (range1.IsEmpty && range2.IsEmpty) || (!range1.IsEmpty && !range2.IsEmpty && MinValue.Equal(range1.MinValue, range2.MinValue, compareInfo) && MaxValue.Equal(range1.MaxValue, range2.MaxValue, compareInfo));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00012DEC File Offset: 0x00010FEC
		public static ValueRange Intersect(ValueRange range1, ValueRange range2, Column column, CompareInfo compareInfo)
		{
			if (!ValueRange.AreOverlapping(range1, range2, column, compareInfo))
			{
				return ValueRange.Empty;
			}
			MinValue minValue = (MinValue.Compare(range1.MinValue, range2.MinValue, column, compareInfo) < 0) ? range2.MinValue : range1.MinValue;
			MaxValue maxValue = (MaxValue.Compare(range1.MaxValue, range2.MaxValue, column, compareInfo) > 0) ? range2.MaxValue : range1.MaxValue;
			return new ValueRange(minValue, maxValue, column, compareInfo);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00012E68 File Offset: 0x00011068
		public static bool AreOverlappingOrAdjacent(ValueRange range1, ValueRange range2, Column column, CompareInfo compareInfo)
		{
			return range1.IsEmpty || range2.IsEmpty || (ValueRange.CompareMinMax(range1.MinValue, range2.MaxValue, column, compareInfo, true) <= 0 && ValueRange.CompareMinMax(range2.MinValue, range1.MaxValue, column, compareInfo, true) <= 0);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00012EC0 File Offset: 0x000110C0
		public static ValueRange UnionOverlappingOrAdjacent(ValueRange range1, ValueRange range2, Column column, CompareInfo compareInfo)
		{
			if (range1.IsEmpty)
			{
				return range2;
			}
			if (range2.IsEmpty)
			{
				return range1;
			}
			MinValue minValue = (MinValue.Compare(range1.MinValue, range2.MinValue, column, compareInfo) < 0) ? range1.MinValue : range2.MinValue;
			MaxValue maxValue = (MaxValue.Compare(range1.MaxValue, range2.MaxValue, column, compareInfo) > 0) ? range1.MaxValue : range2.MaxValue;
			return new ValueRange(minValue, maxValue, column, compareInfo);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00012F40 File Offset: 0x00011140
		private static bool AreOverlapping(ValueRange range1, ValueRange range2, Column column, CompareInfo compareInfo)
		{
			return !range1.IsEmpty && !range2.IsEmpty && ValueRange.CompareMinMax(range1.MinValue, range2.MaxValue, column, compareInfo, false) < 0 && ValueRange.CompareMinMax(range2.MinValue, range1.MaxValue, column, compareInfo, false) < 0;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00012F98 File Offset: 0x00011198
		private static int CompareMinMax(MinValue min, MaxValue max, Column column, CompareInfo compareInfo, bool adjacentSameAsOverlapping)
		{
			if (min.IsInfinity || max.IsInfinity)
			{
				return -1;
			}
			int num = ValueHelper.ValuesCompare(min.Value, max.Value, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
			if (num < 0)
			{
				if (!adjacentSameAsOverlapping && !min.IsInclusive && !max.IsInclusive && ValueRange.AdjacentValues(min.Value, max.Value, column, compareInfo))
				{
					return 0;
				}
				return -1;
			}
			else if (num == 0)
			{
				if (!min.IsInclusive)
				{
					if (!max.IsInclusive)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (!max.IsInclusive)
					{
						return 0;
					}
					return -1;
				}
			}
			else
			{
				if (adjacentSameAsOverlapping && min.IsInclusive && max.IsInclusive && ValueRange.AdjacentValues(max.Value, min.Value, column, compareInfo))
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001305C File Offset: 0x0001125C
		private static bool AdjacentValues(object lhs, object rhs, Column column, CompareInfo compareInfo)
		{
			switch (column.ExtendedTypeCode)
			{
			case ExtendedTypeCode.Boolean:
				if (lhs != null)
				{
					return !(bool)lhs && (bool)rhs;
				}
				return !(bool)rhs;
			case ExtendedTypeCode.Int16:
				if (lhs != null)
				{
					return (short)lhs + 1 == (short)rhs;
				}
				return (short)rhs == short.MinValue;
			case ExtendedTypeCode.Int32:
				if (lhs != null)
				{
					return (int)lhs + 1 == (int)rhs;
				}
				return (int)rhs == int.MinValue;
			case ExtendedTypeCode.Int64:
				if (lhs != null)
				{
					return (long)lhs + 1L == (long)rhs;
				}
				return (long)rhs == long.MinValue;
			case ExtendedTypeCode.Single:
				return lhs == null && (float)rhs == float.MinValue;
			case ExtendedTypeCode.Double:
				return lhs == null && (double)rhs == double.MinValue;
			case ExtendedTypeCode.DateTime:
				if (lhs != null)
				{
					return ((DateTime)lhs).Ticks + 1L == ((DateTime)rhs).Ticks;
				}
				return (DateTime)rhs == DateTime.MinValue;
			case ExtendedTypeCode.Guid:
				return lhs == null && (Guid)rhs == Guid.Empty;
			case ExtendedTypeCode.String:
				return lhs == null && ((string)rhs).Length == 0;
			case ExtendedTypeCode.Binary:
				return lhs == null && ((Array)rhs).Length == 0;
			case ExtendedTypeCode.MVInt16:
			case ExtendedTypeCode.MVInt32:
			case ExtendedTypeCode.MVInt64:
			case ExtendedTypeCode.MVSingle:
			case ExtendedTypeCode.MVDouble:
			case ExtendedTypeCode.MVDateTime:
			case ExtendedTypeCode.MVGuid:
			case ExtendedTypeCode.MVString:
			case ExtendedTypeCode.MVBinary:
				return lhs == null && ((Array)rhs).Length == 0;
			}
			return false;
		}

		// Token: 0x04000202 RID: 514
		public static readonly ValueRange Empty = default(ValueRange);

		// Token: 0x04000203 RID: 515
		public static readonly ValueRange Full = new ValueRange(MinValue.Infinity, MaxValue.Infinity, null, null);

		// Token: 0x04000204 RID: 516
		public readonly MinValue MinValue;

		// Token: 0x04000205 RID: 517
		public readonly MaxValue MaxValue;
	}
}

using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000026 RID: 38
	public struct MinValue
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00012B3F File Offset: 0x00010D3F
		public MinValue(bool inclusive, object value)
		{
			this.exclusive = !inclusive;
			this.value = value;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00012B52 File Offset: 0x00010D52
		public bool IsInclusive
		{
			get
			{
				return !this.exclusive;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00012B5D File Offset: 0x00010D5D
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00012B65 File Offset: 0x00010D65
		public bool IsInfinity
		{
			get
			{
				return !this.exclusive && this.value == null;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00012B7A File Offset: 0x00010D7A
		public static bool Equal(MinValue value1, MinValue value2, CompareInfo compareInfo)
		{
			return value1.IsInclusive == value2.IsInclusive && ValueHelper.ValuesEqual(value1.Value, value2.Value, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public static int Compare(MinValue lhs, MinValue rhs, Column column, CompareInfo compareInfo)
		{
			int num = ValueHelper.ValuesCompare(lhs.Value, rhs.Value, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
			if (num < 0)
			{
				return -1;
			}
			if (num != 0)
			{
				return 1;
			}
			if (!lhs.IsInclusive)
			{
				if (!rhs.IsInclusive)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (!rhs.IsInclusive)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x040001FC RID: 508
		public static readonly MinValue Infinity = default(MinValue);

		// Token: 0x040001FD RID: 509
		private readonly bool exclusive;

		// Token: 0x040001FE RID: 510
		private readonly object value;
	}
}

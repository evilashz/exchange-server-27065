using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000027 RID: 39
	public struct MaxValue
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00012C02 File Offset: 0x00010E02
		public MaxValue(bool inclusive, object value)
		{
			this.inclusive = inclusive;
			this.value = value;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00012C12 File Offset: 0x00010E12
		public bool IsValid
		{
			get
			{
				return this.inclusive || this.value != null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00012C2A File Offset: 0x00010E2A
		public bool IsInclusive
		{
			get
			{
				return this.inclusive;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00012C32 File Offset: 0x00010E32
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00012C3A File Offset: 0x00010E3A
		public bool IsInfinity
		{
			get
			{
				return object.ReferenceEquals(this.value, MaxValue.Infinity.value);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00012C54 File Offset: 0x00010E54
		public static bool Equal(MaxValue value1, MaxValue value2, CompareInfo compareInfo)
		{
			return value1.IsInfinity == value2.IsInfinity && (value1.IsInfinity || (value1.IsInclusive == value2.IsInclusive && ValueHelper.ValuesEqual(value1.Value, value2.Value, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00012CA8 File Offset: 0x00010EA8
		public static int Compare(MaxValue lhs, MaxValue rhs, Column column, CompareInfo compareInfo)
		{
			if (lhs.IsInfinity)
			{
				if (!rhs.IsInfinity)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (rhs.IsInfinity)
				{
					return -1;
				}
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
					return -1;
				}
				else
				{
					if (!rhs.IsInclusive)
					{
						return 1;
					}
					return 0;
				}
			}
		}

		// Token: 0x040001FF RID: 511
		public static readonly MaxValue Infinity = new MaxValue(false, new object());

		// Token: 0x04000200 RID: 512
		private readonly bool inclusive;

		// Token: 0x04000201 RID: 513
		private readonly object value;
	}
}

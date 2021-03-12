using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class IntRange : IComparable<IntRange>, IComparable
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		public IntRange(int singleInteger)
		{
			this.UpperBound = singleInteger;
			this.LowerBound = singleInteger;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E40F File Offset: 0x0000C60F
		public IntRange(int lowerBound, int upperBound)
		{
			if (lowerBound > upperBound)
			{
				throw new ArgumentException("Lower bound cannot be higher than upper.", "lowerBound");
			}
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E439 File Offset: 0x0000C639
		public static bool operator ==(IntRange value1, IntRange value2)
		{
			return value1 == value2 || (value1 != null && value2 != null && value1.Equals(value2));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000E450 File Offset: 0x0000C650
		public static bool operator !=(IntRange value1, IntRange value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000E45C File Offset: 0x0000C65C
		public bool Contains(int value)
		{
			return value >= this.LowerBound && value <= this.UpperBound;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000E478 File Offset: 0x0000C678
		public static IntRange Parse(string expression)
		{
			IntRange result = null;
			if (!IntRange.TryParse(expression, out result))
			{
				throw new ArgumentException(DataStrings.InvalidIntRangeArgument(expression), "expression");
			}
			return result;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		public static bool TryParse(string expression, out IntRange range)
		{
			range = null;
			string[] array = (expression[0] == '-') ? expression.Substring(1).Split(new char[]
			{
				'-'
			}, 2) : expression.Split(new char[]
			{
				'-'
			}, 2);
			if (expression[0] == '-')
			{
				array[0] = "-" + array[0];
			}
			int num = 0;
			int num2 = 0;
			if (array.Length == 1)
			{
				if (int.TryParse(expression, out num))
				{
					range = new IntRange(num, num);
				}
			}
			else if (int.TryParse(array[0], out num) && int.TryParse(array[1], out num2) && num2 >= num)
			{
				range = new IntRange(num, num2);
			}
			return null != range;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E560 File Offset: 0x0000C760
		public override string ToString()
		{
			if (this.LowerBound == this.UpperBound)
			{
				return this.LowerBound.ToString();
			}
			return this.LowerBound.ToString() + "-" + this.UpperBound.ToString();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public int LowerBound { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000E5C1 File Offset: 0x0000C7C1
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public int UpperBound { get; private set; }

		// Token: 0x060003FB RID: 1019 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public override bool Equals(object obj)
		{
			IntRange intRange = obj as IntRange;
			return intRange != null && intRange.LowerBound == this.LowerBound && intRange.UpperBound == this.UpperBound;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000E60F File Offset: 0x0000C80F
		public override int GetHashCode()
		{
			return this.LowerBound ^ this.UpperBound;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000E620 File Offset: 0x0000C820
		public int CompareTo(IntRange x)
		{
			if (x == null)
			{
				return 1;
			}
			int num = this.LowerBound.CompareTo(x.LowerBound);
			if (num != 0)
			{
				return num;
			}
			return this.UpperBound.CompareTo(x.UpperBound);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E668 File Offset: 0x0000C868
		int IComparable.CompareTo(object obj)
		{
			IntRange x = obj as IntRange;
			return this.CompareTo(x);
		}

		// Token: 0x040001FF RID: 511
		public const int NoUpperBound = 2147483647;
	}
}

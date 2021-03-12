using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000B2 RID: 178
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00020F71 File Offset: 0x0001F171
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00020F7A File Offset: 0x0001F17A
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00020F8B File Offset: 0x0001F18B
		public string ToString(IFormatProvider provider)
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00020F9C File Offset: 0x0001F19C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00020FB2 File Offset: 0x0001F1B2
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00020FB9 File Offset: 0x0001F1B9
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeBoolean"));
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00020FEB File Offset: 0x0001F1EB
		[__DynamicallyInvokable]
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00020FFC File Offset: 0x0001F1FC
		[__DynamicallyInvokable]
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			bool result = false;
			if (!bool.TryParse(value, out result))
			{
				throw new FormatException(Environment.GetResourceString("Format_BadBoolean"));
			}
			return result;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00021034 File Offset: 0x0001F234
		[__DynamicallyInvokable]
		public static bool TryParse(string value, out bool result)
		{
			result = false;
			if (value == null)
			{
				return false;
			}
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000210A0 File Offset: 0x0001F2A0
		private static string TrimWhiteSpaceAndNull(string value)
		{
			int i = 0;
			int num = value.Length - 1;
			char c = '\0';
			while (i < value.Length)
			{
				if (!char.IsWhiteSpace(value[i]) && value[i] != c)
				{
					IL_52:
					while (num >= i && (char.IsWhiteSpace(value[num]) || value[num] == c))
					{
						num--;
					}
					return value.Substring(i, num - i + 1);
				}
				i++;
			}
			goto IL_52;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002110F File Offset: 0x0001F30F
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00021112 File Offset: 0x0001F312
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00021116 File Offset: 0x0001F316
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Boolean",
				"Char"
			}));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002113D File Offset: 0x0001F33D
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00021146 File Offset: 0x0001F346
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002114F File Offset: 0x0001F34F
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00021158 File Offset: 0x0001F358
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00021161 File Offset: 0x0001F361
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002116A File Offset: 0x0001F36A
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00021173 File Offset: 0x0001F373
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002117C File Offset: 0x0001F37C
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00021185 File Offset: 0x0001F385
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002118E File Offset: 0x0001F38E
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00021197 File Offset: 0x0001F397
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000211A0 File Offset: 0x0001F3A0
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Boolean",
				"DateTime"
			}));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000211C7 File Offset: 0x0001F3C7
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003ED RID: 1005
		private bool m_value;

		// Token: 0x040003EE RID: 1006
		internal const int True = 1;

		// Token: 0x040003EF RID: 1007
		internal const int False = 0;

		// Token: 0x040003F0 RID: 1008
		internal const string TrueLiteral = "True";

		// Token: 0x040003F1 RID: 1009
		internal const string FalseLiteral = "False";

		// Token: 0x040003F2 RID: 1010
		[__DynamicallyInvokable]
		public static readonly string TrueString = "True";

		// Token: 0x040003F3 RID: 1011
		[__DynamicallyInvokable]
		public static readonly string FalseString = "False";
	}
}

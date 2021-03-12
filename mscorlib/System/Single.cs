using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x0200013F RID: 319
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x000383CC File Offset: 0x000365CC
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsInfinity(float f)
		{
			return (*(int*)(&f) & int.MaxValue) == 2139095040;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000383DF File Offset: 0x000365DF
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsPositiveInfinity(float f)
		{
			return *(int*)(&f) == 2139095040;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000383EC File Offset: 0x000365EC
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNegativeInfinity(float f)
		{
			return *(int*)(&f) == -8388608;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000383F9 File Offset: 0x000365F9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNaN(float f)
		{
			return (*(int*)(&f) & int.MaxValue) > 2139095040;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003840C File Offset: 0x0003660C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSingle"));
			}
			float num = (float)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00038468 File Offset: 0x00036668
		[__DynamicallyInvokable]
		public int CompareTo(float value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00038495 File Offset: 0x00036695
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003849B File Offset: 0x0003669B
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000384A4 File Offset: 0x000366A4
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000384AA File Offset: 0x000366AA
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000384B0 File Offset: 0x000366B0
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000384B9 File Offset: 0x000366B9
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000384C4 File Offset: 0x000366C4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000384FA File Offset: 0x000366FA
		[__DynamicallyInvokable]
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00038514 File Offset: 0x00036714
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			float num = this;
			if (num == 0f)
			{
				return 0;
			}
			return *(int*)(&num);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00038534 File Offset: 0x00036734
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00038543 File Offset: 0x00036743
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00038553 File Offset: 0x00036753
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00038562 File Offset: 0x00036762
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00038572 File Offset: 0x00036772
		[__DynamicallyInvokable]
		public static float Parse(string s)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00038584 File Offset: 0x00036784
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00038598 File Offset: 0x00036798
		[__DynamicallyInvokable]
		public static float Parse(string s, IFormatProvider provider)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000385AB File Offset: 0x000367AB
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000385C0 File Offset: 0x000367C0
		private static float Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseSingle(s, style, info);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000385CA File Offset: 0x000367CA
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000385DD File Offset: 0x000367DD
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000385F4 File Offset: 0x000367F4
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			if (!Number.TryParseSingle(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = float.NaN;
				}
			}
			return true;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00038669 File Offset: 0x00036869
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003866D File Offset: 0x0003686D
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00038676 File Offset: 0x00036876
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Single",
				"Char"
			}));
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0003869D File Offset: 0x0003689D
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000386A6 File Offset: 0x000368A6
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000386AF File Offset: 0x000368AF
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000386B8 File Offset: 0x000368B8
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000386C1 File Offset: 0x000368C1
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000386CA File Offset: 0x000368CA
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000386D3 File Offset: 0x000368D3
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000386DC File Offset: 0x000368DC
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000386E5 File Offset: 0x000368E5
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000386E9 File Offset: 0x000368E9
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000386F2 File Offset: 0x000368F2
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000386FB File Offset: 0x000368FB
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Single",
				"DateTime"
			}));
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00038722 File Offset: 0x00036922
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000688 RID: 1672
		internal float m_value;

		// Token: 0x04000689 RID: 1673
		[__DynamicallyInvokable]
		public const float MinValue = -3.4028235E+38f;

		// Token: 0x0400068A RID: 1674
		[__DynamicallyInvokable]
		public const float Epsilon = 1E-45f;

		// Token: 0x0400068B RID: 1675
		[__DynamicallyInvokable]
		public const float MaxValue = 3.4028235E+38f;

		// Token: 0x0400068C RID: 1676
		[__DynamicallyInvokable]
		public const float PositiveInfinity = float.PositiveInfinity;

		// Token: 0x0400068D RID: 1677
		[__DynamicallyInvokable]
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x0400068E RID: 1678
		[__DynamicallyInvokable]
		public const float NaN = float.NaN;
	}
}

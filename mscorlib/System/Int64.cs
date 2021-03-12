using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000FC RID: 252
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Int64 : IComparable, IFormattable, IConvertible, IComparable<long>, IEquatable<long>
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt64"));
			}
			long num = (long)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002F6E0 File Offset: 0x0002D8E0
		[__DynamicallyInvokable]
		public int CompareTo(long value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002F6F1 File Offset: 0x0002D8F1
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is long && this == (long)obj;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002F707 File Offset: 0x0002D907
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(long obj)
		{
			return this == obj;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002F70E File Offset: 0x0002D90E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002F71A File Offset: 0x0002D91A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002F729 File Offset: 0x0002D929
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002F739 File Offset: 0x0002D939
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002F748 File Offset: 0x0002D948
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002F758 File Offset: 0x0002D958
		[__DynamicallyInvokable]
		public static long Parse(string s)
		{
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002F766 File Offset: 0x0002D966
		[__DynamicallyInvokable]
		public static long Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002F77A File Offset: 0x0002D97A
		[__DynamicallyInvokable]
		public static long Parse(string s, IFormatProvider provider)
		{
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002F789 File Offset: 0x0002D989
		[__DynamicallyInvokable]
		public static long Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002F79E File Offset: 0x0002D99E
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out long result)
		{
			return Number.TryParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002F7AD File Offset: 0x0002D9AD
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002F7C3 File Offset: 0x0002D9C3
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002F7C7 File Offset: 0x0002D9C7
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002F7D9 File Offset: 0x0002D9D9
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002F7E2 File Offset: 0x0002D9E2
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002F7EB File Offset: 0x0002D9EB
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0002F7FD File Offset: 0x0002D9FD
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002F806 File Offset: 0x0002DA06
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002F80F File Offset: 0x0002DA0F
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002F813 File Offset: 0x0002DA13
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002F81C File Offset: 0x0002DA1C
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0002F825 File Offset: 0x0002DA25
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002F82E File Offset: 0x0002DA2E
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002F837 File Offset: 0x0002DA37
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Int64",
				"DateTime"
			}));
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002F85E File Offset: 0x0002DA5E
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005A5 RID: 1445
		internal long m_value;

		// Token: 0x040005A6 RID: 1446
		[__DynamicallyInvokable]
		public const long MaxValue = 9223372036854775807L;

		// Token: 0x040005A7 RID: 1447
		[__DynamicallyInvokable]
		public const long MinValue = -9223372036854775808L;
	}
}

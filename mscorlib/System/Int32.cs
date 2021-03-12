using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000FB RID: 251
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"));
			}
			int num = (int)value;
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

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002F518 File Offset: 0x0002D718
		[__DynamicallyInvokable]
		public int CompareTo(int value)
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002F529 File Offset: 0x0002D729
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002F53F File Offset: 0x0002D73F
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(int obj)
		{
			return this == obj;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002F546 File Offset: 0x0002D746
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002F54A File Offset: 0x0002D74A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002F559 File Offset: 0x0002D759
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002F568 File Offset: 0x0002D768
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002F578 File Offset: 0x0002D778
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002F588 File Offset: 0x0002D788
		[__DynamicallyInvokable]
		public static int Parse(string s)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002F596 File Offset: 0x0002D796
		[__DynamicallyInvokable]
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002F5AA File Offset: 0x0002D7AA
		[__DynamicallyInvokable]
		public static int Parse(string s, IFormatProvider provider)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002F5B9 File Offset: 0x0002D7B9
		[__DynamicallyInvokable]
		public static int Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002F5CE File Offset: 0x0002D7CE
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out int result)
		{
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002F5DD File Offset: 0x0002D7DD
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002F5F3 File Offset: 0x0002D7F3
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002F5F7 File Offset: 0x0002D7F7
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002F600 File Offset: 0x0002D800
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002F609 File Offset: 0x0002D809
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002F612 File Offset: 0x0002D812
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002F61B File Offset: 0x0002D81B
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002F624 File Offset: 0x0002D824
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002F62D File Offset: 0x0002D82D
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002F631 File Offset: 0x0002D831
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002F63A File Offset: 0x0002D83A
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002F643 File Offset: 0x0002D843
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002F64C File Offset: 0x0002D84C
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002F655 File Offset: 0x0002D855
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002F65E File Offset: 0x0002D85E
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002F667 File Offset: 0x0002D867
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Int32",
				"DateTime"
			}));
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002F68E File Offset: 0x0002D88E
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005A2 RID: 1442
		internal int m_value;

		// Token: 0x040005A3 RID: 1443
		[__DynamicallyInvokable]
		public const int MaxValue = 2147483647;

		// Token: 0x040005A4 RID: 1444
		[__DynamicallyInvokable]
		public const int MinValue = -2147483648;
	}
}

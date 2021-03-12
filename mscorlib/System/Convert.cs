using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020000CC RID: 204
	[__DynamicallyInvokable]
	public static class Convert
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000250B4 File Offset: 0x000232B4
		[__DynamicallyInvokable]
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000250D8 File Offset: 0x000232D8
		public static bool IsDBNull(object value)
		{
			if (value == System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00025104 File Offset: 0x00023304
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00025118 File Offset: 0x00023318
		[__DynamicallyInvokable]
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_UnknownTypeCode"));
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00025298 File Offset: 0x00023498
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			RuntimeType left = targetType as RuntimeType;
			if (left != null)
			{
				if (value.GetType() == targetType)
				{
					return value;
				}
				if (left == Convert.ConvertTypes[3])
				{
					return value.ToBoolean(provider);
				}
				if (left == Convert.ConvertTypes[4])
				{
					return value.ToChar(provider);
				}
				if (left == Convert.ConvertTypes[5])
				{
					return value.ToSByte(provider);
				}
				if (left == Convert.ConvertTypes[6])
				{
					return value.ToByte(provider);
				}
				if (left == Convert.ConvertTypes[7])
				{
					return value.ToInt16(provider);
				}
				if (left == Convert.ConvertTypes[8])
				{
					return value.ToUInt16(provider);
				}
				if (left == Convert.ConvertTypes[9])
				{
					return value.ToInt32(provider);
				}
				if (left == Convert.ConvertTypes[10])
				{
					return value.ToUInt32(provider);
				}
				if (left == Convert.ConvertTypes[11])
				{
					return value.ToInt64(provider);
				}
				if (left == Convert.ConvertTypes[12])
				{
					return value.ToUInt64(provider);
				}
				if (left == Convert.ConvertTypes[13])
				{
					return value.ToSingle(provider);
				}
				if (left == Convert.ConvertTypes[14])
				{
					return value.ToDouble(provider);
				}
				if (left == Convert.ConvertTypes[15])
				{
					return value.ToDecimal(provider);
				}
				if (left == Convert.ConvertTypes[16])
				{
					return value.ToDateTime(provider);
				}
				if (left == Convert.ConvertTypes[18])
				{
					return value.ToString(provider);
				}
				if (left == Convert.ConvertTypes[1])
				{
					return value;
				}
				if (left == Convert.EnumType)
				{
					return (Enum)value;
				}
				if (left == Convert.ConvertTypes[2])
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
				}
				if (left == Convert.ConvertTypes[0])
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
				}
			}
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				value.GetType().FullName,
				targetType.FullName
			}));
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00025513 File Offset: 0x00023713
		[__DynamicallyInvokable]
		public static object ChangeType(object value, Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00025528 File Offset: 0x00023728
		[__DynamicallyInvokable]
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCastNullToValueType"));
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
				}
				else
				{
					RuntimeType left = conversionType as RuntimeType;
					if (left == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (left == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (left == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (left == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (left == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (left == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (left == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (left == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (left == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (left == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (left == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (left == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (left == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (left == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (left == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (left == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00025758 File Offset: 0x00023958
		[__DynamicallyInvokable]
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002576B File Offset: 0x0002396B
		[__DynamicallyInvokable]
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002577E File Offset: 0x0002397E
		[__DynamicallyInvokable]
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00025781 File Offset: 0x00023981
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00025787 File Offset: 0x00023987
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00025795 File Offset: 0x00023995
		[__DynamicallyInvokable]
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002579B File Offset: 0x0002399B
		[__DynamicallyInvokable]
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000257A1 File Offset: 0x000239A1
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000257A7 File Offset: 0x000239A7
		[__DynamicallyInvokable]
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000257AD File Offset: 0x000239AD
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000257B3 File Offset: 0x000239B3
		[__DynamicallyInvokable]
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000257BA File Offset: 0x000239BA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000257C1 File Offset: 0x000239C1
		[__DynamicallyInvokable]
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000257CE File Offset: 0x000239CE
		[__DynamicallyInvokable]
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000257DB File Offset: 0x000239DB
		[__DynamicallyInvokable]
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000257E8 File Offset: 0x000239E8
		[__DynamicallyInvokable]
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000257F9 File Offset: 0x000239F9
		[__DynamicallyInvokable]
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00025806 File Offset: 0x00023A06
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00025814 File Offset: 0x00023A14
		[__DynamicallyInvokable]
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00025827 File Offset: 0x00023A27
		[__DynamicallyInvokable]
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002583A File Offset: 0x00023A3A
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00025848 File Offset: 0x00023A48
		public static char ToChar(char value)
		{
			return value;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002584B File Offset: 0x00023A4B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00025863 File Offset: 0x00023A63
		[__DynamicallyInvokable]
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00025866 File Offset: 0x00023A66
		[__DynamicallyInvokable]
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002587E File Offset: 0x00023A7E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00025881 File Offset: 0x00023A81
		[__DynamicallyInvokable]
		public static char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000258A1 File Offset: 0x00023AA1
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000258BD File Offset: 0x00023ABD
		[__DynamicallyInvokable]
		public static char ToChar(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000258DF File Offset: 0x00023ADF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
			}
			return (char)value;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000258FC File Offset: 0x00023AFC
		[__DynamicallyInvokable]
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00025905 File Offset: 0x00023B05
		[__DynamicallyInvokable]
		public static char ToChar(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
			}
			return value[0];
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00025935 File Offset: 0x00023B35
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00025943 File Offset: 0x00023B43
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00025951 File Offset: 0x00023B51
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002595F File Offset: 0x00023B5F
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002596D File Offset: 0x00023B6D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00025980 File Offset: 0x00023B80
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00025993 File Offset: 0x00023B93
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002599B File Offset: 0x00023B9B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002599E File Offset: 0x00023B9E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000259B7 File Offset: 0x00023BB7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000259D0 File Offset: 0x00023BD0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000259EE File Offset: 0x00023BEE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00025A07 File Offset: 0x00023C07
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00025A25 File Offset: 0x00023C25
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00025A40 File Offset: 0x00023C40
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00025A60 File Offset: 0x00023C60
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)value;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00025A7A File Offset: 0x00023C7A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00025A83 File Offset: 0x00023C83
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00025A90 File Offset: 0x00023C90
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00025A9E File Offset: 0x00023C9E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00025AB0 File Offset: 0x00023CB0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			return sbyte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00025ABA File Offset: 0x00023CBA
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00025AC8 File Offset: 0x00023CC8
		[__DynamicallyInvokable]
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00025ADB File Offset: 0x00023CDB
		[__DynamicallyInvokable]
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00025AEE File Offset: 0x00023CEE
		[__DynamicallyInvokable]
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00025AF6 File Offset: 0x00023CF6
		[__DynamicallyInvokable]
		public static byte ToByte(byte value)
		{
			return value;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00025AF9 File Offset: 0x00023CF9
		[__DynamicallyInvokable]
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00025B15 File Offset: 0x00023D15
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00025B2D File Offset: 0x00023D2D
		[__DynamicallyInvokable]
		public static byte ToByte(short value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00025B4D File Offset: 0x00023D4D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00025B69 File Offset: 0x00023D69
		[__DynamicallyInvokable]
		public static byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00025B89 File Offset: 0x00023D89
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00025BA5 File Offset: 0x00023DA5
		[__DynamicallyInvokable]
		public static byte ToByte(long value)
		{
			if (value < 0L || value > 255L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00025BC7 File Offset: 0x00023DC7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)value;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00025BE4 File Offset: 0x00023DE4
		[__DynamicallyInvokable]
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00025BED File Offset: 0x00023DED
		[__DynamicallyInvokable]
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00025BFA File Offset: 0x00023DFA
		[__DynamicallyInvokable]
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00025C08 File Offset: 0x00023E08
		[__DynamicallyInvokable]
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00025C1A File Offset: 0x00023E1A
		[__DynamicallyInvokable]
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00025C29 File Offset: 0x00023E29
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00025C37 File Offset: 0x00023E37
		[__DynamicallyInvokable]
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00025C4A File Offset: 0x00023E4A
		[__DynamicallyInvokable]
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00025C5D File Offset: 0x00023E5D
		[__DynamicallyInvokable]
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00025C65 File Offset: 0x00023E65
		[__DynamicallyInvokable]
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00025C81 File Offset: 0x00023E81
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00025C84 File Offset: 0x00023E84
		[__DynamicallyInvokable]
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00025C87 File Offset: 0x00023E87
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00025CA3 File Offset: 0x00023EA3
		[__DynamicallyInvokable]
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00025CC7 File Offset: 0x00023EC7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00025CE5 File Offset: 0x00023EE5
		[__DynamicallyInvokable]
		public static short ToInt16(short value)
		{
			return value;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00025CE8 File Offset: 0x00023EE8
		[__DynamicallyInvokable]
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00025D0E File Offset: 0x00023F0E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)value;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00025D2B File Offset: 0x00023F2B
		[__DynamicallyInvokable]
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00025D34 File Offset: 0x00023F34
		[__DynamicallyInvokable]
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00025D41 File Offset: 0x00023F41
		[__DynamicallyInvokable]
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00025D4F File Offset: 0x00023F4F
		[__DynamicallyInvokable]
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00025D61 File Offset: 0x00023F61
		[__DynamicallyInvokable]
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00025D70 File Offset: 0x00023F70
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00025D7E File Offset: 0x00023F7E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00025D91 File Offset: 0x00023F91
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00025DA4 File Offset: 0x00023FA4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00025DAC File Offset: 0x00023FAC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00025DAF File Offset: 0x00023FAF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00025DC7 File Offset: 0x00023FC7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00025DCA File Offset: 0x00023FCA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00025DE2 File Offset: 0x00023FE2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00025E02 File Offset: 0x00024002
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00025E05 File Offset: 0x00024005
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00025E21 File Offset: 0x00024021
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00025E43 File Offset: 0x00024043
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)value;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00025E60 File Offset: 0x00024060
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00025E69 File Offset: 0x00024069
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00025E76 File Offset: 0x00024076
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00025E84 File Offset: 0x00024084
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00025E96 File Offset: 0x00024096
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00025EA5 File Offset: 0x000240A5
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00025EB3 File Offset: 0x000240B3
		[__DynamicallyInvokable]
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00025EC6 File Offset: 0x000240C6
		[__DynamicallyInvokable]
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00025ED9 File Offset: 0x000240D9
		[__DynamicallyInvokable]
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00025EE1 File Offset: 0x000240E1
		[__DynamicallyInvokable]
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00025EE4 File Offset: 0x000240E4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00025EE7 File Offset: 0x000240E7
		[__DynamicallyInvokable]
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00025EEA File Offset: 0x000240EA
		[__DynamicallyInvokable]
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00025EED File Offset: 0x000240ED
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00025EF0 File Offset: 0x000240F0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00025F0B File Offset: 0x0002410B
		[__DynamicallyInvokable]
		public static int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00025F0E File Offset: 0x0002410E
		[__DynamicallyInvokable]
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00025F34 File Offset: 0x00024134
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
			}
			return (int)value;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00025F51 File Offset: 0x00024151
		[__DynamicallyInvokable]
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00025F5C File Offset: 0x0002415C
		[__DynamicallyInvokable]
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00025FF2 File Offset: 0x000241F2
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int ToInt32(decimal value)
		{
			return decimal.FCallToInt32(value);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00025FFA File Offset: 0x000241FA
		[__DynamicallyInvokable]
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002600C File Offset: 0x0002420C
		[__DynamicallyInvokable]
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002601B File Offset: 0x0002421B
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00026029 File Offset: 0x00024229
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002603C File Offset: 0x0002423C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002604F File Offset: 0x0002424F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00026057 File Offset: 0x00024257
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002605A File Offset: 0x0002425A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00026071 File Offset: 0x00024271
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00026074 File Offset: 0x00024274
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002608B File Offset: 0x0002428B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002608E File Offset: 0x0002428E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000260A5 File Offset: 0x000242A5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000260A8 File Offset: 0x000242A8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)-1))
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000260C6 File Offset: 0x000242C6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)-1)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
			}
			return (uint)value;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000260DF File Offset: 0x000242DF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000260E8 File Offset: 0x000242E8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00026148 File Offset: 0x00024348
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00026156 File Offset: 0x00024356
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00026168 File Offset: 0x00024368
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00026177 File Offset: 0x00024377
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00026185 File Offset: 0x00024385
		[__DynamicallyInvokable]
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00026199 File Offset: 0x00024399
		[__DynamicallyInvokable]
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000261AD File Offset: 0x000243AD
		[__DynamicallyInvokable]
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000261B7 File Offset: 0x000243B7
		[__DynamicallyInvokable]
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000261BB File Offset: 0x000243BB
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000261BF File Offset: 0x000243BF
		[__DynamicallyInvokable]
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000261C3 File Offset: 0x000243C3
		[__DynamicallyInvokable]
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000261C7 File Offset: 0x000243C7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000261CB File Offset: 0x000243CB
		[__DynamicallyInvokable]
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000261CF File Offset: 0x000243CF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000261D3 File Offset: 0x000243D3
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
			}
			return (long)value;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000261F2 File Offset: 0x000243F2
		[__DynamicallyInvokable]
		public static long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000261F5 File Offset: 0x000243F5
		[__DynamicallyInvokable]
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000261FE File Offset: 0x000243FE
		[__DynamicallyInvokable]
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00026207 File Offset: 0x00024407
		[__DynamicallyInvokable]
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00026215 File Offset: 0x00024415
		[__DynamicallyInvokable]
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00026228 File Offset: 0x00024428
		[__DynamicallyInvokable]
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00026238 File Offset: 0x00024438
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00026246 File Offset: 0x00024446
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002625A File Offset: 0x0002445A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002626E File Offset: 0x0002446E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00026278 File Offset: 0x00024478
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002627C File Offset: 0x0002447C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00026294 File Offset: 0x00024494
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00026298 File Offset: 0x00024498
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000262B0 File Offset: 0x000244B0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000262B4 File Offset: 0x000244B4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000262CC File Offset: 0x000244CC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000262D0 File Offset: 0x000244D0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
			}
			return (ulong)value;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000262E8 File Offset: 0x000244E8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000262EB File Offset: 0x000244EB
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000262F4 File Offset: 0x000244F4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000262FD File Offset: 0x000244FD
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002630B File Offset: 0x0002450B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002631E File Offset: 0x0002451E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002632E File Offset: 0x0002452E
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002633C File Offset: 0x0002453C
		[__DynamicallyInvokable]
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00026353 File Offset: 0x00024553
		[__DynamicallyInvokable]
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002636A File Offset: 0x0002456A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002636E File Offset: 0x0002456E
		[__DynamicallyInvokable]
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00026372 File Offset: 0x00024572
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00026380 File Offset: 0x00024580
		[__DynamicallyInvokable]
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00026384 File Offset: 0x00024584
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00026388 File Offset: 0x00024588
		[__DynamicallyInvokable]
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002638C File Offset: 0x0002458C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(uint value)
		{
			return value;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00026391 File Offset: 0x00024591
		[__DynamicallyInvokable]
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00026395 File Offset: 0x00024595
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002639A File Offset: 0x0002459A
		[__DynamicallyInvokable]
		public static float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002639D File Offset: 0x0002459D
		[__DynamicallyInvokable]
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000263A1 File Offset: 0x000245A1
		[__DynamicallyInvokable]
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000263AA File Offset: 0x000245AA
		[__DynamicallyInvokable]
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000263C0 File Offset: 0x000245C0
		[__DynamicallyInvokable]
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000263D7 File Offset: 0x000245D7
		[__DynamicallyInvokable]
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000263E1 File Offset: 0x000245E1
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000263EF File Offset: 0x000245EF
		[__DynamicallyInvokable]
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002640A File Offset: 0x0002460A
		[__DynamicallyInvokable]
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00026425 File Offset: 0x00024625
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00026429 File Offset: 0x00024629
		[__DynamicallyInvokable]
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002642D File Offset: 0x0002462D
		[__DynamicallyInvokable]
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00026431 File Offset: 0x00024631
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002643F File Offset: 0x0002463F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00026443 File Offset: 0x00024643
		[__DynamicallyInvokable]
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00026447 File Offset: 0x00024647
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(uint value)
		{
			return value;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002644C File Offset: 0x0002464C
		[__DynamicallyInvokable]
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00026450 File Offset: 0x00024650
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00026455 File Offset: 0x00024655
		[__DynamicallyInvokable]
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00026459 File Offset: 0x00024659
		[__DynamicallyInvokable]
		public static double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002645C File Offset: 0x0002465C
		[__DynamicallyInvokable]
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00026465 File Offset: 0x00024665
		[__DynamicallyInvokable]
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002647F File Offset: 0x0002467F
		[__DynamicallyInvokable]
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002649A File Offset: 0x0002469A
		[__DynamicallyInvokable]
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000264A4 File Offset: 0x000246A4
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000264B2 File Offset: 0x000246B2
		[__DynamicallyInvokable]
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000264C9 File Offset: 0x000246C9
		[__DynamicallyInvokable]
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000264E0 File Offset: 0x000246E0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000264E8 File Offset: 0x000246E8
		[__DynamicallyInvokable]
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000264F0 File Offset: 0x000246F0
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000264FE File Offset: 0x000246FE
		[__DynamicallyInvokable]
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00026506 File Offset: 0x00024706
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002650E File Offset: 0x0002470E
		[__DynamicallyInvokable]
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00026516 File Offset: 0x00024716
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002651E File Offset: 0x0002471E
		[__DynamicallyInvokable]
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00026526 File Offset: 0x00024726
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002652E File Offset: 0x0002472E
		[__DynamicallyInvokable]
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00026536 File Offset: 0x00024736
		[__DynamicallyInvokable]
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002653E File Offset: 0x0002473E
		[__DynamicallyInvokable]
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00026554 File Offset: 0x00024754
		[__DynamicallyInvokable]
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, NumberStyles.Number, provider);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00026568 File Offset: 0x00024768
		[__DynamicallyInvokable]
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002656B File Offset: 0x0002476B
		[__DynamicallyInvokable]
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00026579 File Offset: 0x00024779
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00026587 File Offset: 0x00024787
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002658A File Offset: 0x0002478A
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000265A1 File Offset: 0x000247A1
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000265B8 File Offset: 0x000247B8
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000265D0 File Offset: 0x000247D0
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000265E4 File Offset: 0x000247E4
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000265F2 File Offset: 0x000247F2
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00026600 File Offset: 0x00024800
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002660E File Offset: 0x0002480E
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002661C File Offset: 0x0002481C
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002662A File Offset: 0x0002482A
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00026638 File Offset: 0x00024838
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00026646 File Offset: 0x00024846
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00026654 File Offset: 0x00024854
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00026662 File Offset: 0x00024862
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00026670 File Offset: 0x00024870
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002667E File Offset: 0x0002487E
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002668C File Offset: 0x0002488C
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002669A File Offset: 0x0002489A
		[__DynamicallyInvokable]
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000266A4 File Offset: 0x000248A4
		[__DynamicallyInvokable]
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000266E5 File Offset: 0x000248E5
		[__DynamicallyInvokable]
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000266EE File Offset: 0x000248EE
		[__DynamicallyInvokable]
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000266F8 File Offset: 0x000248F8
		[__DynamicallyInvokable]
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00026700 File Offset: 0x00024900
		[__DynamicallyInvokable]
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002670A File Offset: 0x0002490A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(sbyte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00026718 File Offset: 0x00024918
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00026722 File Offset: 0x00024922
		[__DynamicallyInvokable]
		public static string ToString(byte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00026730 File Offset: 0x00024930
		[__DynamicallyInvokable]
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002673A File Offset: 0x0002493A
		[__DynamicallyInvokable]
		public static string ToString(short value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00026748 File Offset: 0x00024948
		[__DynamicallyInvokable]
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00026752 File Offset: 0x00024952
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ushort value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00026760 File Offset: 0x00024960
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002676A File Offset: 0x0002496A
		[__DynamicallyInvokable]
		public static string ToString(int value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00026778 File Offset: 0x00024978
		[__DynamicallyInvokable]
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00026782 File Offset: 0x00024982
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(uint value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00026790 File Offset: 0x00024990
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002679A File Offset: 0x0002499A
		[__DynamicallyInvokable]
		public static string ToString(long value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000267A8 File Offset: 0x000249A8
		[__DynamicallyInvokable]
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000267B2 File Offset: 0x000249B2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ulong value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000267C0 File Offset: 0x000249C0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000267CA File Offset: 0x000249CA
		[__DynamicallyInvokable]
		public static string ToString(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000267D8 File Offset: 0x000249D8
		[__DynamicallyInvokable]
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000267E2 File Offset: 0x000249E2
		[__DynamicallyInvokable]
		public static string ToString(double value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000267F0 File Offset: 0x000249F0
		[__DynamicallyInvokable]
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000267FA File Offset: 0x000249FA
		[__DynamicallyInvokable]
		public static string ToString(decimal value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00026808 File Offset: 0x00024A08
		[__DynamicallyInvokable]
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00026812 File Offset: 0x00024A12
		[__DynamicallyInvokable]
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002681B File Offset: 0x00024A1B
		[__DynamicallyInvokable]
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00026825 File Offset: 0x00024A25
		public static string ToString(string value)
		{
			return value;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00026828 File Offset: 0x00024A28
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002682C File Offset: 0x00024A2C
		[__DynamicallyInvokable]
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)num;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00026888 File Offset: 0x00024A88
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)num;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000268F0 File Offset: 0x00024AF0
		[__DynamicallyInvokable]
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)num;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00026960 File Offset: 0x00024B60
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)num;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000269BA File Offset: 0x00024BBA
		[__DynamicallyInvokable]
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.StringToInt(value, fromBase, 4096);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000269EA File Offset: 0x00024BEA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return (uint)ParseNumbers.StringToInt(value, fromBase, 4608);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00026A1A File Offset: 0x00024C1A
		[__DynamicallyInvokable]
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.StringToLong(value, fromBase, 4096);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00026A4A File Offset: 0x00024C4A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return (ulong)ParseNumbers.StringToLong(value, fromBase, 4608);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00026A7A File Offset: 0x00024C7A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00026AAA File Offset: 0x00024CAA
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00026ADD File Offset: 0x00024CDD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00026B0C File Offset: 0x00024D0C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00026B3B File Offset: 0x00024D3B
		[__DynamicallyInvokable]
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, Base64FormattingOptions.None);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00026B56 File Offset: 0x00024D56
		[ComVisible(false)]
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, options);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00026B71 File Offset: 0x00024D71
		[__DynamicallyInvokable]
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00026B7C File Offset: 0x00024D7C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public unsafe static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)options
				}));
			}
			int num = inArray.Length;
			if (offset > num - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			if (num == 0)
			{
				return string.Empty;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			int length2 = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
			string text = string.FastAllocateString(length2);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (byte* ptr2 = inArray)
				{
					int num2 = Convert.ConvertToBase64Array(ptr, ptr2, offset, length, insertLineBreaks);
					return text;
				}
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00026C6E File Offset: 0x00024E6E
		[__DynamicallyInvokable]
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00026C7C File Offset: 0x00024E7C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)options
				}));
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			if (num == 0)
			{
				return 0;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
			}
			int result;
			fixed (char* ptr = &outArray[offsetOut])
			{
				fixed (byte* ptr2 = inArray)
				{
					result = Convert.ConvertToBase64Array(ptr, ptr2, offsetIn, length, insertLineBreaks);
				}
			}
			return result;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00026DB0 File Offset: 0x00024FB0
		[SecurityCritical]
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			fixed (char* ptr = Convert.base64Table)
			{
				int i;
				for (i = offset; i < num2; i += 3)
				{
					if (insertLineBreaks)
					{
						if (num4 == 76)
						{
							outChars[num3++] = '\r';
							outChars[num3++] = '\n';
							num4 = 0;
						}
						num4 += 4;
					}
					outChars[num3] = ptr[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
					outChars[num3 + 2] = ptr[(int)(inData[i + 1] & 15) << 2 | (inData[i + 2] & 192) >> 6];
					outChars[num3 + 3] = ptr[inData[i + 2] & 63];
					num3 += 4;
				}
				i = num2;
				if (insertLineBreaks && num != 0 && num4 == 76)
				{
					outChars[num3++] = '\r';
					outChars[num3++] = '\n';
				}
				if (num != 1)
				{
					if (num == 2)
					{
						outChars[num3] = ptr[(inData[i] & 252) >> 2];
						outChars[num3 + 1] = ptr[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
						outChars[num3 + 2] = ptr[(inData[i + 1] & 15) << 2];
						outChars[num3 + 3] = ptr[64];
						num3 += 4;
					}
				}
				else
				{
					outChars[num3] = ptr[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr[(inData[i] & 3) << 4];
					outChars[num3 + 2] = ptr[64];
					outChars[num3 + 3] = ptr[64];
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00026FD4 File Offset: 0x000251D4
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002702C File Offset: 0x0002522C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Convert.FromBase64CharPtr(ptr, s.Length);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00027064 File Offset: 0x00025264
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
			}
			fixed (char* ptr = inArray)
			{
				return Convert.FromBase64CharPtr(ptr + offset, length);
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000270F4 File Offset: 0x000252F4
		[SecurityCritical]
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			int num2 = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
			byte[] array = new byte[num2];
			fixed (byte* ptr = array)
			{
				int num3 = Convert.FromBase64_Decode(inputPtr, inputLength, ptr, num2);
			}
			return array;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00027168 File Offset: 0x00025368
		[SecurityCritical]
		private unsafe static int FromBase64_Decode(char* startInputPtr, int inputLength, byte* startDestPtr, int destLength)
		{
			char* ptr = startInputPtr;
			byte* ptr2 = startDestPtr;
			char* ptr3 = ptr + inputLength;
			byte* ptr4 = ptr2 + destLength;
			uint num = 255U;
			while (ptr < ptr3)
			{
				uint num2 = (uint)(*ptr);
				ptr++;
				if (num2 - 65U <= 25U)
				{
					num2 -= 65U;
				}
				else if (num2 - 97U <= 25U)
				{
					num2 -= 71U;
				}
				else
				{
					if (num2 - 48U > 9U)
					{
						if (num2 <= 32U)
						{
							if (num2 - 9U <= 1U || num2 == 13U || num2 == 32U)
							{
								continue;
							}
						}
						else
						{
							if (num2 == 43U)
							{
								num2 = 62U;
								goto IL_A7;
							}
							if (num2 == 47U)
							{
								num2 = 63U;
								goto IL_A7;
							}
							if (num2 == 61U)
							{
								if (ptr == ptr3)
								{
									num <<= 6;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
									}
									if ((int)((long)(ptr4 - ptr2)) < 2)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									*(ptr2++) = (byte)(num >> 8);
									num = 255U;
									break;
								}
								else
								{
									while (ptr < ptr3 - 1)
									{
										int num3 = (int)(*ptr);
										if (num3 != 32 && num3 != 10 && num3 != 13 && num3 != 9)
										{
											break;
										}
										ptr++;
									}
									if (ptr != ptr3 - 1 || *ptr != '=')
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
									}
									num <<= 12;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
									}
									if ((int)((long)(ptr4 - ptr2)) < 1)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									num = 255U;
									break;
								}
							}
						}
						throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
					}
					num2 -= 4294967292U;
				}
				IL_A7:
				num = (num << 6 | num2);
				if ((num & 2147483648U) != 0U)
				{
					if ((int)((long)(ptr4 - ptr2)) < 3)
					{
						return -1;
					}
					*ptr2 = (byte)(num >> 16);
					ptr2[1] = (byte)(num >> 8);
					ptr2[2] = (byte)num;
					ptr2 += 3;
					num = 255U;
				}
			}
			if (num != 255U)
			{
				throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
			}
			return (int)((long)(ptr2 - startDestPtr));
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00027360 File Offset: 0x00025560
		[SecurityCritical]
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x04000534 RID: 1332
		internal static readonly RuntimeType[] ConvertTypes = new RuntimeType[]
		{
			(RuntimeType)typeof(Empty),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(DBNull),
			(RuntimeType)typeof(bool),
			(RuntimeType)typeof(char),
			(RuntimeType)typeof(sbyte),
			(RuntimeType)typeof(byte),
			(RuntimeType)typeof(short),
			(RuntimeType)typeof(ushort),
			(RuntimeType)typeof(int),
			(RuntimeType)typeof(uint),
			(RuntimeType)typeof(long),
			(RuntimeType)typeof(ulong),
			(RuntimeType)typeof(float),
			(RuntimeType)typeof(double),
			(RuntimeType)typeof(decimal),
			(RuntimeType)typeof(DateTime),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(string)
		};

		// Token: 0x04000535 RID: 1333
		private static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x04000536 RID: 1334
		internal static readonly char[] base64Table = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'+',
			'/',
			'='
		};

		// Token: 0x04000537 RID: 1335
		private const int base64LineBreakPosition = 76;

		// Token: 0x04000538 RID: 1336
		public static readonly object DBNull = System.DBNull.Value;
	}
}

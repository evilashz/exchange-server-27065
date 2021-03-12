using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x02000150 RID: 336
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt16 : IComparable, IFormattable, IConvertible, IComparable<ushort>, IEquatable<ushort>
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x0003DDF9 File Offset: 0x0003BFF9
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is ushort)
			{
				return (int)(this - (ushort)value);
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt16"));
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0003DE21 File Offset: 0x0003C021
		[__DynamicallyInvokable]
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0003DE27 File Offset: 0x0003C027
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ushort && this == (ushort)obj;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0003DE3D File Offset: 0x0003C03D
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(ushort obj)
		{
			return this == obj;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003DE44 File Offset: 0x0003C044
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003DE48 File Offset: 0x0003C048
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0003DE57 File Offset: 0x0003C057
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0003DE67 File Offset: 0x0003C067
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0003DE76 File Offset: 0x0003C076
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0003DE86 File Offset: 0x0003C086
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0003DE94 File Offset: 0x0003C094
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, IFormatProvider provider)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0003DEB7 File Offset: 0x0003C0B7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0003DECC File Offset: 0x0003C0CC
		private static ushort Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			uint num = 0U;
			try
			{
				num = Number.ParseUInt32(s, style, info);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), innerException);
			}
			if (num > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)num;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0003DF24 File Offset: 0x0003C124
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0003DF33 File Offset: 0x0003C133
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003DF4C File Offset: 0x0003C14C
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out ushort result)
		{
			result = 0;
			uint num;
			if (!Number.TryParseUInt32(s, style, info, out num))
			{
				return false;
			}
			if (num > 65535U)
			{
				return false;
			}
			result = (ushort)num;
			return true;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003DF79 File Offset: 0x0003C179
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0003DF7C File Offset: 0x0003C17C
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0003DF85 File Offset: 0x0003C185
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0003DF8E File Offset: 0x0003C18E
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0003DF97 File Offset: 0x0003C197
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003DFA0 File Offset: 0x0003C1A0
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003DFA9 File Offset: 0x0003C1A9
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003DFAD File Offset: 0x0003C1AD
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003DFB6 File Offset: 0x0003C1B6
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0003DFBF File Offset: 0x0003C1BF
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0003DFC8 File Offset: 0x0003C1C8
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0003DFD1 File Offset: 0x0003C1D1
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0003DFDA File Offset: 0x0003C1DA
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003DFE3 File Offset: 0x0003C1E3
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0003DFEC File Offset: 0x0003C1EC
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"UInt16",
				"DateTime"
			}));
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0003E013 File Offset: 0x0003C213
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F1 RID: 1777
		private ushort m_value;

		// Token: 0x040006F2 RID: 1778
		[__DynamicallyInvokable]
		public const ushort MaxValue = 65535;

		// Token: 0x040006F3 RID: 1779
		[__DynamicallyInvokable]
		public const ushort MinValue = 0;
	}
}

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x02000152 RID: 338
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt64 : IComparable, IFormattable, IConvertible, IComparable<ulong>, IEquatable<ulong>
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0003E1EC File Offset: 0x0003C3EC
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt64"));
			}
			ulong num = (ulong)value;
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

		// Token: 0x06001523 RID: 5411 RVA: 0x0003E22C File Offset: 0x0003C42C
		[__DynamicallyInvokable]
		public int CompareTo(ulong value)
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

		// Token: 0x06001524 RID: 5412 RVA: 0x0003E23D File Offset: 0x0003C43D
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003E253 File Offset: 0x0003C453
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0003E25A File Offset: 0x0003C45A
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0003E266 File Offset: 0x0003C466
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003E275 File Offset: 0x0003C475
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0003E285 File Offset: 0x0003C485
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0003E294 File Offset: 0x0003C494
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003E2B2 File Offset: 0x0003C4B2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003E2C6 File Offset: 0x0003C4C6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0003E2EA File Offset: 0x0003C4EA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out ulong result)
		{
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003E2F9 File Offset: 0x0003C4F9
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003E30F File Offset: 0x0003C50F
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003E313 File Offset: 0x0003C513
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003E31C File Offset: 0x0003C51C
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003E325 File Offset: 0x0003C525
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003E32E File Offset: 0x0003C52E
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003E337 File Offset: 0x0003C537
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003E340 File Offset: 0x0003C540
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0003E349 File Offset: 0x0003C549
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003E352 File Offset: 0x0003C552
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003E35B File Offset: 0x0003C55B
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003E364 File Offset: 0x0003C564
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0003E368 File Offset: 0x0003C568
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0003E371 File Offset: 0x0003C571
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003E37A File Offset: 0x0003C57A
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003E383 File Offset: 0x0003C583
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"UInt64",
				"DateTime"
			}));
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003E3AA File Offset: 0x0003C5AA
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F7 RID: 1783
		private ulong m_value;

		// Token: 0x040006F8 RID: 1784
		[__DynamicallyInvokable]
		public const ulong MaxValue = 18446744073709551615UL;

		// Token: 0x040006F9 RID: 1785
		[__DynamicallyInvokable]
		public const ulong MinValue = 0UL;
	}
}

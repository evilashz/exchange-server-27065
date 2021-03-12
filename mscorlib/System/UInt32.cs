using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x02000151 RID: 337
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt32 : IComparable, IFormattable, IConvertible, IComparable<uint>, IEquatable<uint>
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x0003E024 File Offset: 0x0003C224
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is uint))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt32"));
			}
			uint num = (uint)value;
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

		// Token: 0x06001504 RID: 5380 RVA: 0x0003E064 File Offset: 0x0003C264
		[__DynamicallyInvokable]
		public int CompareTo(uint value)
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

		// Token: 0x06001505 RID: 5381 RVA: 0x0003E075 File Offset: 0x0003C275
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is uint && this == (uint)obj;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0003E08B File Offset: 0x0003C28B
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(uint obj)
		{
			return this == obj;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0003E092 File Offset: 0x0003C292
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0003E096 File Offset: 0x0003C296
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt32(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0003E0A5 File Offset: 0x0003C2A5
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0003E0B5 File Offset: 0x0003C2B5
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt32(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0003E0C4 File Offset: 0x0003C2C4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Parse(string s)
		{
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0003E0E2 File Offset: 0x0003C2E2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0003E0F6 File Offset: 0x0003C2F6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Parse(string s, IFormatProvider provider)
		{
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0003E105 File Offset: 0x0003C305
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0003E11A File Offset: 0x0003C31A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out uint result)
		{
			return Number.TryParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0003E129 File Offset: 0x0003C329
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out uint result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0003E13F File Offset: 0x0003C33F
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt32;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003E143 File Offset: 0x0003C343
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0003E14C File Offset: 0x0003C34C
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0003E155 File Offset: 0x0003C355
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0003E15E File Offset: 0x0003C35E
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0003E167 File Offset: 0x0003C367
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0003E170 File Offset: 0x0003C370
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0003E179 File Offset: 0x0003C379
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0003E182 File Offset: 0x0003C382
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0003E186 File Offset: 0x0003C386
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0003E18F File Offset: 0x0003C38F
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0003E198 File Offset: 0x0003C398
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0003E1A1 File Offset: 0x0003C3A1
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003E1AA File Offset: 0x0003C3AA
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0003E1B3 File Offset: 0x0003C3B3
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"UInt32",
				"DateTime"
			}));
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0003E1DA File Offset: 0x0003C3DA
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F4 RID: 1780
		private uint m_value;

		// Token: 0x040006F5 RID: 1781
		[__DynamicallyInvokable]
		public const uint MaxValue = 4294967295U;

		// Token: 0x040006F6 RID: 1782
		[__DynamicallyInvokable]
		public const uint MinValue = 0U;
	}
}

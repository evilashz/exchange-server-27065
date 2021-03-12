using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x0200013C RID: 316
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct SByte : IComparable, IFormattable, IConvertible, IComparable<sbyte>, IEquatable<sbyte>
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00037F4F File Offset: 0x0003614F
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSByte"));
			}
			return (int)(this - (sbyte)obj);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00037F77 File Offset: 0x00036177
		[__DynamicallyInvokable]
		public int CompareTo(sbyte value)
		{
			return (int)(this - value);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00037F7D File Offset: 0x0003617D
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is sbyte && this == (sbyte)obj;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00037F93 File Offset: 0x00036193
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(sbyte obj)
		{
			return this == obj;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00037F9A File Offset: 0x0003619A
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)this << 8;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00037FA3 File Offset: 0x000361A3
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00037FB2 File Offset: 0x000361B2
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00037FC2 File Offset: 0x000361C2
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return this.ToString(format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00037FD0 File Offset: 0x000361D0
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00037FE0 File Offset: 0x000361E0
		[SecuritySafeCritical]
		private string ToString(string format, NumberFormatInfo info)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				uint value = (uint)this & 255U;
				return Number.FormatUInt32(value, format, info);
			}
			return Number.FormatInt32((int)this, format, info);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003802F File Offset: 0x0003622F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0003803D File Offset: 0x0003623D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00038051 File Offset: 0x00036251
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, IFormatProvider provider)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00038060 File Offset: 0x00036260
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00038078 File Offset: 0x00036278
		private static sbyte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), innerException);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
				}
				return (sbyte)num;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
				}
				return (sbyte)num;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000380F8 File Offset: 0x000362F8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out sbyte result)
		{
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00038107 File Offset: 0x00036307
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00038120 File Offset: 0x00036320
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out sbyte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0003816C File Offset: 0x0003636C
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003816F File Offset: 0x0003636F
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00038178 File Offset: 0x00036378
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00038181 File Offset: 0x00036381
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00038185 File Offset: 0x00036385
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0003818E File Offset: 0x0003638E
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00038197 File Offset: 0x00036397
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000381A0 File Offset: 0x000363A0
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000381A4 File Offset: 0x000363A4
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000381AD File Offset: 0x000363AD
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000381B6 File Offset: 0x000363B6
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000381BF File Offset: 0x000363BF
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000381C8 File Offset: 0x000363C8
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000381D1 File Offset: 0x000363D1
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000381DA File Offset: 0x000363DA
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"SByte",
				"DateTime"
			}));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00038201 File Offset: 0x00036401
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000680 RID: 1664
		private sbyte m_value;

		// Token: 0x04000681 RID: 1665
		[__DynamicallyInvokable]
		public const sbyte MaxValue = 127;

		// Token: 0x04000682 RID: 1666
		[__DynamicallyInvokable]
		public const sbyte MinValue = -128;
	}
}

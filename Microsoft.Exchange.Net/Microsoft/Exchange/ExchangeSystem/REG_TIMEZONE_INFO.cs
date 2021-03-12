using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000078 RID: 120
	internal struct REG_TIMEZONE_INFO : IEquatable<REG_TIMEZONE_INFO>
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public static bool operator ==(REG_TIMEZONE_INFO v1, REG_TIMEZONE_INFO v2)
		{
			return v1.Equals(v2);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000113B2 File Offset: 0x0000F5B2
		public static bool operator !=(REG_TIMEZONE_INFO v1, REG_TIMEZONE_INFO v2)
		{
			return !v1.Equals(v2);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public static REG_TIMEZONE_INFO Parse(ArraySegment<byte> buffer)
		{
			if (buffer.Count < REG_TIMEZONE_INFO.Size)
			{
				throw new ArgumentOutOfRangeException();
			}
			REG_TIMEZONE_INFO result;
			result.Bias = BitConverter.ToInt32(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.BiasOffset);
			result.StandardBias = BitConverter.ToInt32(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.StandardBiasOffset);
			result.DaylightBias = BitConverter.ToInt32(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.DaylightBiasOffset);
			result.StandardDate = NativeMethods.SystemTime.Parse(new ArraySegment<byte>(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.StandardDateOffset, NativeMethods.SystemTime.Size));
			result.DaylightDate = NativeMethods.SystemTime.Parse(new ArraySegment<byte>(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.DaylightDateOffset, NativeMethods.SystemTime.Size));
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011496 File Offset: 0x0000F696
		public override bool Equals(object o)
		{
			return o is REG_TIMEZONE_INFO && this.Equals((REG_TIMEZONE_INFO)o);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public bool Equals(REG_TIMEZONE_INFO v)
		{
			return this.Bias == v.Bias && this.StandardBias == v.StandardBias && this.DaylightBias == v.DaylightBias && this.StandardDate == v.StandardDate && this.DaylightDate == v.DaylightDate;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00011512 File Offset: 0x0000F712
		public override int GetHashCode()
		{
			return this.StandardDate.GetHashCode() ^ this.DaylightDate.GetHashCode() ^ this.Bias;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00011540 File Offset: 0x0000F740
		public int Write(ArraySegment<byte> buffer)
		{
			if (buffer.Count < REG_TIMEZONE_INFO.Size)
			{
				throw new ArgumentOutOfRangeException();
			}
			ExBitConverter.Write(this.Bias, buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.BiasOffset);
			ExBitConverter.Write(this.StandardBias, buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.StandardBiasOffset);
			ExBitConverter.Write(this.DaylightBias, buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.DaylightBiasOffset);
			this.StandardDate.Write(new ArraySegment<byte>(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.StandardDateOffset, NativeMethods.SystemTime.Size));
			this.DaylightDate.Write(new ArraySegment<byte>(buffer.Array, buffer.Offset + REG_TIMEZONE_INFO.DaylightDateOffset, NativeMethods.SystemTime.Size));
			return REG_TIMEZONE_INFO.Size;
		}

		// Token: 0x040001FE RID: 510
		public static readonly int Size = Marshal.SizeOf(typeof(REG_TIMEZONE_INFO));

		// Token: 0x040001FF RID: 511
		public int Bias;

		// Token: 0x04000200 RID: 512
		public int StandardBias;

		// Token: 0x04000201 RID: 513
		public int DaylightBias;

		// Token: 0x04000202 RID: 514
		public NativeMethods.SystemTime StandardDate;

		// Token: 0x04000203 RID: 515
		public NativeMethods.SystemTime DaylightDate;

		// Token: 0x04000204 RID: 516
		private static readonly int BiasOffset = (int)Marshal.OffsetOf(typeof(REG_TIMEZONE_INFO), "Bias");

		// Token: 0x04000205 RID: 517
		private static readonly int StandardBiasOffset = (int)Marshal.OffsetOf(typeof(REG_TIMEZONE_INFO), "StandardBias");

		// Token: 0x04000206 RID: 518
		private static readonly int DaylightBiasOffset = (int)Marshal.OffsetOf(typeof(REG_TIMEZONE_INFO), "DaylightBias");

		// Token: 0x04000207 RID: 519
		private static readonly int StandardDateOffset = (int)Marshal.OffsetOf(typeof(REG_TIMEZONE_INFO), "StandardDate");

		// Token: 0x04000208 RID: 520
		private static readonly int DaylightDateOffset = (int)Marshal.OffsetOf(typeof(REG_TIMEZONE_INFO), "DaylightDate");
	}
}

using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000411 RID: 1041
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class O11TimeZoneFormatter
	{
		// Token: 0x06002F11 RID: 12049 RVA: 0x000C1620 File Offset: 0x000BF820
		public static byte[] GetTimeZoneBlob(ExTimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			if (timeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				throw new ArgumentException("timeZone should not be UnspecifiedTimeZone");
			}
			REG_TIMEZONE_INFO timeZoneInfo = TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(timeZone);
			return O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.ToBytesFromRegistryFormat(timeZoneInfo);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000C1660 File Offset: 0x000BF860
		public static bool TryParseTimeZoneBlob(byte[] bytes, string displayName, out ExTimeZone timeZone)
		{
			timeZone = null;
			if (bytes == null || displayName == null)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "O11TimeZoneParser, time zone blob or display name is null");
				return false;
			}
			if (bytes.Length < O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.Size)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>(0L, "O11TimeZoneParser, corrupted TimeZoneBlob. Length {0} less than standard blob", bytes.Length);
				return false;
			}
			if (bytes.Length > O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.Size)
			{
				ExTraceGlobals.StorageTracer.TraceWarning<int>(0L, "O11TimeZoneParser, corrupted TimeZoneBlob Length {0}, going to trim extra bytes", bytes.Length);
			}
			REG_TIMEZONE_INFO regInfo = O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.ParseToRegistryFormat(bytes);
			try
			{
				timeZone = TimeZoneHelper.CreateCustomExTimeZoneFromRegTimeZoneInfo(regInfo, string.Empty, displayName);
			}
			catch (InvalidTimeZoneException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "O11TimeZoneParser, corrupted time zone, blob is not valid registry format. Inner message is {0}", ex.Message);
			}
			return timeZone != null;
		}

		// Token: 0x02000412 RID: 1042
		private struct OUTLOOK_TIMEZONE_INFO
		{
			// Token: 0x06002F13 RID: 12051 RVA: 0x000C1710 File Offset: 0x000BF910
			public static REG_TIMEZONE_INFO ParseToRegistryFormat(byte[] bytes)
			{
				if (bytes.Length < O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.Size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return new REG_TIMEZONE_INFO
				{
					Bias = BitConverter.ToInt32(bytes, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.BiasOffset),
					StandardBias = BitConverter.ToInt32(bytes, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.StandardBiasOffset),
					DaylightBias = BitConverter.ToInt32(bytes, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.DaylightBiasOffset),
					StandardDate = NativeMethods.SystemTime.Parse(new ArraySegment<byte>(bytes, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.StandardDateOffset, NativeMethods.SystemTime.Size)),
					DaylightDate = NativeMethods.SystemTime.Parse(new ArraySegment<byte>(bytes, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.DaylightDateOffset, NativeMethods.SystemTime.Size))
				};
			}

			// Token: 0x06002F14 RID: 12052 RVA: 0x000C17A4 File Offset: 0x000BF9A4
			public static byte[] ToBytesFromRegistryFormat(REG_TIMEZONE_INFO timeZoneInfo)
			{
				byte[] array = new byte[O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.Size];
				ExBitConverter.Write(timeZoneInfo.Bias, array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.BiasOffset);
				ExBitConverter.Write(timeZoneInfo.StandardBias, array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.StandardBiasOffset);
				ExBitConverter.Write(timeZoneInfo.DaylightBias, array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.DaylightBiasOffset);
				ExBitConverter.Write(0, array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.StandardYearOffset);
				timeZoneInfo.StandardDate.Write(new ArraySegment<byte>(array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.StandardDateOffset, NativeMethods.SystemTime.Size));
				ExBitConverter.Write(0, array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.DaylightYearOffset);
				timeZoneInfo.DaylightDate.Write(new ArraySegment<byte>(array, O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO.DaylightDateOffset, NativeMethods.SystemTime.Size));
				return array;
			}

			// Token: 0x040019A5 RID: 6565
			private const int DefaultStandardYear = 0;

			// Token: 0x040019A6 RID: 6566
			private const int DefaultDaylightYear = 0;

			// Token: 0x040019A7 RID: 6567
			public static readonly int Size = Marshal.SizeOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO));

			// Token: 0x040019A8 RID: 6568
			private static readonly int BiasOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "bias");

			// Token: 0x040019A9 RID: 6569
			private static readonly int StandardBiasOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "standardBias");

			// Token: 0x040019AA RID: 6570
			private static readonly int DaylightBiasOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "daylightBias");

			// Token: 0x040019AB RID: 6571
			private static readonly int StandardYearOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "standardYear");

			// Token: 0x040019AC RID: 6572
			private static readonly int StandardDateOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "standardDate");

			// Token: 0x040019AD RID: 6573
			private static readonly int DaylightYearOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "daylightYear");

			// Token: 0x040019AE RID: 6574
			private static readonly int DaylightDateOffset = (int)Marshal.OffsetOf(typeof(O11TimeZoneFormatter.OUTLOOK_TIMEZONE_INFO), "daylightDate");

			// Token: 0x040019AF RID: 6575
			private int bias;

			// Token: 0x040019B0 RID: 6576
			private int standardBias;

			// Token: 0x040019B1 RID: 6577
			private int daylightBias;

			// Token: 0x040019B2 RID: 6578
			private short standardYear;

			// Token: 0x040019B3 RID: 6579
			private NativeMethods.SystemTime standardDate;

			// Token: 0x040019B4 RID: 6580
			private short daylightYear;

			// Token: 0x040019B5 RID: 6581
			private NativeMethods.SystemTime daylightDate;
		}
	}
}

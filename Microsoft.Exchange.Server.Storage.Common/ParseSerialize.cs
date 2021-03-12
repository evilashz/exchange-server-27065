using System;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000071 RID: 113
	public static class ParseSerialize
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000109D6 File Offset: 0x0000EBD6
		public static void CheckBounds(int pos, int posMax, int sizeNeeded)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, sizeNeeded))
			{
				throw new BufferTooSmall((LID)42104U, "Request would overflow buffer");
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000109F7 File Offset: 0x0000EBF7
		public static bool TryCheckBounds(int pos, int posMax, int sizeNeeded)
		{
			return ParseSerialize.CheckOffsetLength(posMax, pos, sizeNeeded);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00010A01 File Offset: 0x0000EC01
		public static void CheckBounds(int pos, byte[] buffer, int sizeNeeded)
		{
			if (buffer != null)
			{
				ParseSerialize.CheckBounds(pos, buffer.Length, sizeNeeded);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00010A10 File Offset: 0x0000EC10
		internal static void CheckCount(uint count, int elementSize, int availableSize)
		{
			if (!ParseSerialize.TryCheckCount(count, elementSize, availableSize))
			{
				throw new BufferTooSmall((LID)58488U, "TryCheckCount failed");
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00010A34 File Offset: 0x0000EC34
		internal static bool TryCheckCount(uint count, int elementSize, int availableSize)
		{
			if (count < 0U)
			{
				DiagnosticContext.TraceLocation((LID)39900U);
				return false;
			}
			if ((ulong)count * (ulong)((long)elementSize) > (ulong)((long)availableSize))
			{
				DiagnosticContext.TraceLocation((LID)56284U);
				return false;
			}
			if ((ulong)count * (ulong)((long)elementSize) > 536870911UL)
			{
				DiagnosticContext.TraceLocation((LID)43996U);
				return false;
			}
			return true;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00010A90 File Offset: 0x0000EC90
		public static byte GetByte(byte[] buff, ref int pos, int posMax)
		{
			byte result;
			if (!ParseSerialize.TryGetByte(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)44604U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00010ABF File Offset: 0x0000ECBF
		public static bool TryGetByte(byte[] buff, ref int pos, int posMax, out byte result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 1))
			{
				DiagnosticContext.TraceLocation((LID)60380U);
				result = 0;
				return false;
			}
			result = buff[pos];
			pos++;
			return true;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public static uint GetDword(byte[] buff, ref int pos, int posMax)
		{
			uint result;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)49628U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00010B1F File Offset: 0x0000ED1F
		public static bool TryGetDword(byte[] buff, ref int pos, int posMax, out uint result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 4))
			{
				DiagnosticContext.TraceLocation((LID)35804U);
				result = 0U;
				return false;
			}
			result = (uint)ParseSerialize.ParseInt32(buff, pos);
			pos += 4;
			return true;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00010B54 File Offset: 0x0000ED54
		public static ushort GetWord(byte[] buff, ref int pos, int posMax)
		{
			ushort result;
			if (!ParseSerialize.TryGetWord(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)55772U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00010B83 File Offset: 0x0000ED83
		public static bool TryGetWord(byte[] buff, ref int pos, int posMax, out ushort result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 2))
			{
				DiagnosticContext.TraceLocation((LID)52188U);
				result = 0;
				return false;
			}
			result = (ushort)ParseSerialize.ParseInt16(buff, pos);
			pos += 2;
			return true;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public static float GetFloat(byte[] buff, ref int pos, int posMax)
		{
			float result;
			if (!ParseSerialize.TryGetFloat(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)53724U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010BE7 File Offset: 0x0000EDE7
		public static bool TryGetFloat(byte[] buff, ref int pos, int posMax, out float result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 4))
			{
				DiagnosticContext.TraceLocation((LID)46044U);
				result = 0f;
				return false;
			}
			result = ParseSerialize.ParseSingle(buff, pos);
			pos += 4;
			return true;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00010C20 File Offset: 0x0000EE20
		public static ulong GetQword(byte[] buff, ref int pos, int posMax)
		{
			ulong result;
			if (!ParseSerialize.TryGetQword(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)34268U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00010C4F File Offset: 0x0000EE4F
		public static bool TryGetQword(byte[] buff, ref int pos, int posMax, out ulong result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 8))
			{
				DiagnosticContext.TraceLocation((LID)62428U);
				result = 0UL;
				return false;
			}
			result = (ulong)ParseSerialize.ParseInt64(buff, pos);
			pos += 8;
			return true;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00010C84 File Offset: 0x0000EE84
		public static double GetDouble(byte[] buff, ref int pos, int posMax)
		{
			double result;
			if (!ParseSerialize.TryGetDouble(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)50652U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00010CB3 File Offset: 0x0000EEB3
		public static bool TryGetDouble(byte[] buff, ref int pos, int posMax, out double result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 8))
			{
				DiagnosticContext.TraceLocation((LID)37852U);
				result = 0.0;
				return false;
			}
			result = ParseSerialize.ParseDouble(buff, pos);
			pos += 8;
			return true;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public static DateTime GetSysTime(byte[] buff, ref int pos, int posMax)
		{
			DateTime result;
			if (!ParseSerialize.TryGetSysTime(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)47580U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010D1F File Offset: 0x0000EF1F
		public static bool TryGetSysTime(byte[] buff, ref int pos, int posMax, out DateTime result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 8))
			{
				DiagnosticContext.TraceLocation((LID)54236U);
				result = default(DateTime);
				return false;
			}
			result = ParseSerialize.ParseFileTime(buff, pos);
			pos += 8;
			return true;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public static bool GetBoolean(byte[] buff, ref int pos, int posMax)
		{
			bool result;
			if (!ParseSerialize.TryGetBoolean(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)59868U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public static bool TryGetBoolean(byte[] buff, ref int pos, int posMax, out bool result)
		{
			byte b;
			if (!ParseSerialize.TryGetByte(buff, ref pos, posMax, out b))
			{
				DiagnosticContext.TraceLocation((LID)41948U);
				result = false;
				return false;
			}
			result = (b != 0);
			return true;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		public static Guid GetGuid(byte[] buff, ref int pos, int posMax)
		{
			Guid result;
			if (!ParseSerialize.TryGetGuid(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)33244U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00010DF3 File Offset: 0x0000EFF3
		public static bool TryGetGuid(byte[] buff, ref int pos, int posMax, out Guid result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 16))
			{
				DiagnosticContext.TraceLocation((LID)58332U);
				result = default(Guid);
				return false;
			}
			result = ParseSerialize.ParseGuid(buff, pos);
			pos += 16;
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00010E30 File Offset: 0x0000F030
		public static byte[][] GetMVBinary(byte[] buff, ref int pos, int posMax)
		{
			byte[][] result;
			if (!ParseSerialize.TryGetMVBinary(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)57820U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010E60 File Offset: 0x0000F060
		public static bool TryGetMVBinary(byte[] buff, ref int pos, int posMax, out byte[][] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)33756U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 2, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)50140U);
				result = null;
				return false;
			}
			byte[][] array = new byte[num][];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetByteArray(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)48348U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public static short[] GetMVInt16(byte[] buff, ref int pos, int posMax)
		{
			short[] result;
			if (!ParseSerialize.TryGetMVInt16(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)37340U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010F20 File Offset: 0x0000F120
		public static bool TryGetMVInt16(byte[] buff, ref int pos, int posMax, out short[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)64732U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 2, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)40156U);
				result = null;
				return false;
			}
			short[] array = new short[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				ushort num3;
				if (!ParseSerialize.TryGetWord(buff, ref pos, posMax, out num3))
				{
					DiagnosticContext.TraceLocation((LID)56540U);
					result = null;
					return false;
				}
				array[num2] = (short)num3;
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public static int[] GetMVInt32(byte[] buff, ref int pos, int posMax)
		{
			int[] result;
			if (!ParseSerialize.TryGetMVInt32(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)39388U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public static bool TryGetMVInt32(byte[] buff, ref int pos, int posMax, out int[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)44252U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 4, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)60636U);
				result = null;
				return false;
			}
			int[] array = new int[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				uint num3;
				if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num3))
				{
					DiagnosticContext.TraceLocation((LID)36060U);
					result = null;
					return false;
				}
				array[num2] = (int)num3;
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001106C File Offset: 0x0000F26C
		public static float[] GetMVReal32(byte[] buff, ref int pos, int posMax)
		{
			float[] result;
			if (!ParseSerialize.TryGetMVReal32(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)63964U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001109C File Offset: 0x0000F29C
		public static bool TryGetMVReal32(byte[] buff, ref int pos, int posMax, out float[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)52444U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 4, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)46300U);
				result = null;
				return false;
			}
			float[] array = new float[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetFloat(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)62684U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001112C File Offset: 0x0000F32C
		public static double[] GetMVR8(byte[] buff, ref int pos, int posMax)
		{
			double[] result;
			if (!ParseSerialize.TryGetMVR8(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)43484U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001115C File Offset: 0x0000F35C
		public static bool TryGetMVR8(byte[] buff, ref int pos, int posMax, out double[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)38108U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 8, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)54492U);
				result = null;
				return false;
			}
			double[] array = new double[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetDouble(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)42204U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static long[] GetMVInt64(byte[] buff, ref int pos, int posMax)
		{
			long[] result;
			if (!ParseSerialize.TryGetMVInt64(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)35292U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001121C File Offset: 0x0000F41C
		public static bool TryGetMVInt64(byte[] buff, ref int pos, int posMax, out long[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)58588U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 8, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)34012U);
				result = null;
				return false;
			}
			long[] array = new long[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				ulong num3;
				if (!ParseSerialize.TryGetQword(buff, ref pos, posMax, out num3))
				{
					DiagnosticContext.TraceLocation((LID)50396U);
					result = null;
					return false;
				}
				array[num2] = (long)num3;
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public static DateTime[] GetMVSysTime(byte[] buff, ref int pos, int posMax)
		{
			DateTime[] result;
			if (!ParseSerialize.TryGetMVSysTime(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)51676U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public static bool TryGetMVSysTime(byte[] buff, ref int pos, int posMax, out DateTime[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)47324U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 8, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)63708U);
				result = null;
				return false;
			}
			DateTime[] array = new DateTime[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetSysTime(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)39132U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00011368 File Offset: 0x0000F568
		public static Guid[] GetMVGuid(byte[] buff, ref int pos, int posMax)
		{
			Guid[] result;
			if (!ParseSerialize.TryGetMVGuid(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)45532U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00011398 File Offset: 0x0000F598
		public static bool TryGetMVGuid(byte[] buff, ref int pos, int posMax, out Guid[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)55516U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 16, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)43228U);
				result = null;
				return false;
			}
			Guid[] array = new Guid[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetGuid(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)59612U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00011428 File Offset: 0x0000F628
		public static string GetStringFromUnicode(byte[] buff, ref int pos, int posMax)
		{
			string result;
			if (!ParseSerialize.TryGetStringFromUnicode(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)52700U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00011458 File Offset: 0x0000F658
		public static bool TryGetStringFromUnicode(byte[] buff, ref int pos, int posMax, out string result)
		{
			int num = 0;
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 2))
			{
				DiagnosticContext.TraceLocation((LID)35036U);
				result = null;
				return false;
			}
			while (buff[pos + num] != 0 || buff[pos + num + 1] != 0)
			{
				num += 2;
				if (!ParseSerialize.TryCheckBounds(pos + num, posMax, 2))
				{
					DiagnosticContext.TraceLocation((LID)51420U);
					result = null;
					return false;
				}
			}
			return ParseSerialize.TryGetStringFromUnicode(buff, ref pos, posMax, num + 2, out result);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000114CC File Offset: 0x0000F6CC
		public static string GetStringFromUnicode(byte[] buff, ref int pos, int posMax, int byteCount)
		{
			string result;
			if (!ParseSerialize.TryGetStringFromUnicode(buff, ref pos, posMax, byteCount, out result))
			{
				throw new BufferTooSmall((LID)46556U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000114FC File Offset: 0x0000F6FC
		public static bool TryGetStringFromUnicode(byte[] buff, ref int pos, int posMax, int byteCount, out string result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, byteCount))
			{
				DiagnosticContext.TraceLocation((LID)45276U);
				result = null;
				return false;
			}
			result = Encoding.Unicode.GetString(buff, pos, byteCount - 2);
			pos += byteCount;
			return true;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00011538 File Offset: 0x0000F738
		public static byte PeekByte(byte[] buff, int pos, int posMax)
		{
			byte result;
			if (!ParseSerialize.TryPeekByte(buff, pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)61916U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00011567 File Offset: 0x0000F767
		public static bool TryPeekByte(byte[] buff, int pos, int posMax, out byte result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, 1))
			{
				DiagnosticContext.TraceLocation((LID)65500U);
				result = 0;
				return false;
			}
			result = buff[pos];
			return true;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00011590 File Offset: 0x0000F790
		public static string GetStringFromASCII(byte[] buff, ref int pos, int posMax)
		{
			string result;
			if (!ParseSerialize.TryGetStringFromASCII(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)41436U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000115C0 File Offset: 0x0000F7C0
		public static bool TryGetStringFromASCII(byte[] buff, ref int pos, int posMax, out string result)
		{
			int num = 0;
			while (pos + num < posMax && buff[pos + num] != 0)
			{
				num++;
			}
			if (pos + num >= posMax)
			{
				DiagnosticContext.TraceLocation((LID)61404U);
				result = null;
				return false;
			}
			return ParseSerialize.TryGetStringFromASCII(buff, ref pos, posMax, num + 1, out result);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001160C File Offset: 0x0000F80C
		public static string GetStringFromASCII(byte[] buff, ref int pos, int posMax, int charCount)
		{
			string result;
			if (!ParseSerialize.TryGetStringFromASCII(buff, ref pos, posMax, charCount, out result))
			{
				throw new BufferTooSmall((LID)36316U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001163C File Offset: 0x0000F83C
		public static bool TryGetStringFromASCII(byte[] buff, ref int pos, int posMax, int charCount, out string result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, charCount))
			{
				DiagnosticContext.TraceLocation((LID)36828U);
				result = null;
				return false;
			}
			result = CTSGlobals.AsciiEncoding.GetString(buff, pos, charCount - 1);
			pos += charCount;
			return true;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00011678 File Offset: 0x0000F878
		public static string GetStringFromASCIINoNull(byte[] buff, ref int pos, int posMax, int charCount)
		{
			string result;
			if (!ParseSerialize.TryGetStringFromASCIINoNull(buff, ref pos, posMax, charCount, out result))
			{
				throw new BufferTooSmall((LID)49116U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public static bool TryGetStringFromASCIINoNull(byte[] buff, ref int pos, int posMax, int charCount, out string result)
		{
			if (!ParseSerialize.TryCheckBounds(pos, posMax, charCount))
			{
				DiagnosticContext.TraceLocation((LID)32988U);
				result = null;
				return false;
			}
			result = CTSGlobals.AsciiEncoding.GetString(buff, pos, charCount);
			pos += charCount;
			return true;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000116E4 File Offset: 0x0000F8E4
		public static string[] GetMVUnicode(byte[] buff, ref int pos, int posMax)
		{
			string[] result;
			if (!ParseSerialize.TryGetMVUnicode(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)45020U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00011714 File Offset: 0x0000F914
		public static bool TryGetMVUnicode(byte[] buff, ref int pos, int posMax, out string[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)65244U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 2, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)57564U);
				result = null;
				return false;
			}
			string[] array = new string[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetStringFromUnicode(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)48860U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public static byte[] GetByteArray(byte[] buff, ref int pos, int posMax)
		{
			byte[] result;
			if (!ParseSerialize.TryGetByteArray(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)57308U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public static bool TryGetByteArray(byte[] buff, ref int pos, int posMax, out byte[] result)
		{
			ushort num;
			if (!ParseSerialize.TryGetWord(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)49372U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckBounds(pos, posMax, (int)num))
			{
				DiagnosticContext.TraceLocation((LID)57052U);
				result = null;
				return false;
			}
			result = ParseSerialize.ParseBinary(buff, pos, (int)num);
			pos += (int)num;
			return true;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00011834 File Offset: 0x0000FA34
		public static string[] GetMVString8(byte[] buff, ref int pos, int posMax)
		{
			string[] result;
			if (!ParseSerialize.TryGetMVString8(buff, ref pos, posMax, out result))
			{
				throw new BufferTooSmall((LID)40924U, "Request would overflow buffer");
			}
			return result;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00011864 File Offset: 0x0000FA64
		public static bool TryGetMVString8(byte[] buff, ref int pos, int posMax, out string[] result)
		{
			uint num;
			if (!ParseSerialize.TryGetDword(buff, ref pos, posMax, out num))
			{
				DiagnosticContext.TraceLocation((LID)40668U);
				result = null;
				return false;
			}
			if (!ParseSerialize.TryCheckCount(num, 1, posMax - pos))
			{
				DiagnosticContext.TraceLocation((LID)36572U);
				result = null;
				return false;
			}
			string[] array = new string[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				if (!ParseSerialize.TryGetStringFromASCII(buff, ref pos, posMax, out array[num2]))
				{
					DiagnosticContext.TraceLocation((LID)34524U);
					result = null;
					return false;
				}
				num2++;
			}
			result = array;
			return true;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000118F1 File Offset: 0x0000FAF1
		public static bool CheckOffsetLength(int maxOffset, int offset, int length)
		{
			return offset >= 0 && length >= 0 && offset <= maxOffset && length <= maxOffset - offset;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001190A File Offset: 0x0000FB0A
		public static bool CheckOffsetLength(byte[] buffer, int offset, int length)
		{
			return ParseSerialize.CheckOffsetLength(buffer.Length, offset, length);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00011916 File Offset: 0x0000FB16
		public static short ParseInt16(byte[] buffer, int offset)
		{
			return (short)((int)buffer[offset] | (int)buffer[offset + 1] << 8);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00011924 File Offset: 0x0000FB24
		public static int ParseInt32(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00011944 File Offset: 0x0000FB44
		public static long ParseInt64(byte[] buffer, int offset)
		{
			uint num = (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
			uint num2 = (uint)((int)buffer[offset + 4] | (int)buffer[offset + 5] << 8 | (int)buffer[offset + 6] << 16 | (int)buffer[offset + 7] << 24);
			return (long)((ulong)num | (ulong)num2 << 32);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00011997 File Offset: 0x0000FB97
		public static float ParseSingle(byte[] buffer, int offset)
		{
			return BitConverter.ToSingle(buffer, offset);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public static double ParseDouble(byte[] buffer, int offset)
		{
			return BitConverter.ToDouble(buffer, offset);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000119A9 File Offset: 0x0000FBA9
		public static Guid ParseGuid(byte[] buffer, int offset)
		{
			return ExBitConverter.ReadGuid(buffer, offset);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public static byte[] ParseBinary(byte[] buffer, int offset, int length)
		{
			if (length == 0)
			{
				return ParseSerialize.emptyByteArray;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(buffer, offset, array, 0, length);
			return array;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000119DC File Offset: 0x0000FBDC
		public static string ParseUcs16String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.Unicode.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000119F4 File Offset: 0x0000FBF4
		public static string ParseUtf8String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.UTF8.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00011A0C File Offset: 0x0000FC0C
		public static int GetLengthOfUtf8String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.UTF8.GetCharCount(buffer, offset, length);
			}
			return 0;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00011A20 File Offset: 0x0000FC20
		public static string ParseAsciiString(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return CTSGlobals.AsciiEncoding.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00011A38 File Offset: 0x0000FC38
		public static DateTime ParseFileTime(byte[] buffer, int offset)
		{
			DateTime result;
			ParseSerialize.TryParseFileTime(buffer, offset, out result);
			return result;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00011A50 File Offset: 0x0000FC50
		public static bool TryParseFileTime(byte[] buffer, int offset, out DateTime dateTime)
		{
			long fileTime = ParseSerialize.ParseInt64(buffer, offset);
			return ParseSerialize.TryConvertFileTime(fileTime, out dateTime);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public static bool TryConvertFileTime(long fileTime, out DateTime dateTime)
		{
			bool result;
			if (fileTime < ParseSerialize.MinFileTime || fileTime >= ParseSerialize.MaxFileTime)
			{
				dateTime = DateTime.MaxValue;
				result = (fileTime == long.MaxValue);
			}
			else if (fileTime == 0L)
			{
				dateTime = DateTime.MinValue;
				result = true;
			}
			else
			{
				dateTime = DateTime.FromFileTimeUtc(fileTime);
				result = true;
			}
			return result;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public static int SerializeInt16(short value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			return 2;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00011AD9 File Offset: 0x0000FCD9
		public static int SerializeInt32(int value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 4;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public static int SerializeInt64(long value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00011AF1 File Offset: 0x0000FCF1
		public static int SerializeSingle(float value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 4;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00011AFE File Offset: 0x0000FCFE
		public static int SerializeDouble(double value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00011B0B File Offset: 0x0000FD0B
		public static int SerializeGuid(Guid value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 16;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00011B18 File Offset: 0x0000FD18
		public static int SerializeFileTime(DateTime dateTime, byte[] buffer, int offset)
		{
			long value;
			if (dateTime < ParseSerialize.MinFileTimeDateTime)
			{
				value = 0L;
			}
			else if (dateTime == DateTime.MaxValue)
			{
				value = long.MaxValue;
			}
			else
			{
				value = dateTime.ToFileTimeUtc();
			}
			ParseSerialize.SerializeInt64(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00011B62 File Offset: 0x0000FD62
		public static int SerializeAsciiString(string value, byte[] buffer, int offset)
		{
			CTSGlobals.AsciiEncoding.GetBytes(value, 0, value.Length, buffer, offset);
			return value.Length;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00011B7F File Offset: 0x0000FD7F
		public static void SetWord(byte[] buff, ref int pos, ushort w)
		{
			ParseSerialize.SetWord(buff, ref pos, (short)w);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00011B8A File Offset: 0x0000FD8A
		public static void SetWord(byte[] buff, ref int pos, short w)
		{
			ParseSerialize.CheckBounds(pos, buff, 2);
			if (buff != null)
			{
				ParseSerialize.SerializeInt16(w, buff, pos);
			}
			pos += 2;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		public static void SetDword(byte[] buff, ref int pos, uint dw)
		{
			ParseSerialize.SetDword(buff, ref pos, (int)dw);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		public static void SetDword(byte[] buff, ref int pos, int dw)
		{
			ParseSerialize.CheckBounds(pos, buff, 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(dw, buff, pos);
			}
			pos += 4;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public static void SetQword(byte[] buff, ref int pos, ulong qw)
		{
			ParseSerialize.SetQword(buff, ref pos, (long)qw);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00011BDA File Offset: 0x0000FDDA
		public static void SetQword(byte[] buff, ref int pos, long qw)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt64(qw, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00011BF8 File Offset: 0x0000FDF8
		public static void SetSysTime(byte[] buff, ref int pos, DateTime value)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeFileTime(value, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00011C16 File Offset: 0x0000FE16
		public static void SetBoolean(byte[] buff, ref int pos, bool value)
		{
			ParseSerialize.SetByte(buff, ref pos, value ? 1 : 0);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00011C26 File Offset: 0x0000FE26
		public static void SetByte(byte[] buff, ref int pos, byte b)
		{
			ParseSerialize.CheckBounds(pos, buff, 1);
			if (buff != null)
			{
				buff[pos] = b;
			}
			pos++;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00011C40 File Offset: 0x0000FE40
		public static void SetUnicodeString(byte[] buff, ref int pos, string str)
		{
			ParseSerialize.CheckBounds(pos, buff, (str.Length + 1) * 2);
			if (buff != null)
			{
				Encoding.Unicode.GetBytes(str, 0, str.Length, buff, pos);
				buff[pos + str.Length * 2] = 0;
				buff[pos + str.Length * 2 + 1] = 0;
			}
			pos += (str.Length + 1) * 2;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public static void SetASCIIString(byte[] buff, ref int pos, string str)
		{
			ParseSerialize.CheckBounds(pos, buff, str.Length + 1);
			if (buff != null)
			{
				CTSGlobals.AsciiEncoding.GetBytes(str, 0, str.Length, buff, pos);
				buff[pos + str.Length] = 0;
			}
			pos += str.Length + 1;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		public static void SetByteArray(byte[] buff, ref int pos, byte[] byteArray)
		{
			ParseSerialize.CheckBounds(pos, buff, 2 + byteArray.Length);
			if (buff != null)
			{
				ParseSerialize.SerializeInt16((short)byteArray.Length, buff, pos);
				Buffer.BlockCopy(byteArray, 0, buff, pos + 2, byteArray.Length);
			}
			pos += 2 + byteArray.Length;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00011D2F File Offset: 0x0000FF2F
		public static void SetRestrictionByteArray(byte[] buff, ref int pos, byte[] serializedRestriction)
		{
			ParseSerialize.CheckBounds(pos, buff, serializedRestriction.Length);
			if (buff != null)
			{
				Buffer.BlockCopy(serializedRestriction, 0, buff, pos, serializedRestriction.Length);
			}
			pos += serializedRestriction.Length;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00011D54 File Offset: 0x0000FF54
		public static void SetFloat(byte[] buff, ref int pos, float fl)
		{
			ParseSerialize.CheckBounds(pos, buff, 4);
			if (buff != null)
			{
				ParseSerialize.SerializeSingle(fl, buff, pos);
			}
			pos += 4;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00011D72 File Offset: 0x0000FF72
		public static void SetDouble(byte[] buff, ref int pos, double dbl)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeDouble(dbl, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00011D90 File Offset: 0x0000FF90
		public static void SetGuid(byte[] buff, ref int pos, Guid guid)
		{
			ParseSerialize.CheckBounds(pos, buff, 16);
			if (buff != null)
			{
				ParseSerialize.SerializeGuid(guid, buff, pos);
			}
			pos += 16;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public static void SetMVInt16(byte[] buff, ref int pos, short[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 2);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt16(values[i], buff, pos + 4 + i * 2);
				}
			}
			pos += 4 + values.Length * 2;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00011E08 File Offset: 0x00010008
		public static void SetMVInt32(byte[] buff, ref int pos, int[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt32(values[i], buff, pos + 4 + i * 4);
				}
			}
			pos += 4 + values.Length * 4;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00011E60 File Offset: 0x00010060
		public static void SetMVInt64(byte[] buff, ref int pos, long[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt64(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00011EB8 File Offset: 0x000100B8
		public static void SetMVReal32(byte[] buff, ref int pos, float[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeSingle(values[i], buff, pos + 4 + i * 4);
				}
			}
			pos += 4 + values.Length * 4;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00011F10 File Offset: 0x00010110
		public static void SetMVReal64(byte[] buff, ref int pos, double[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeDouble(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00011F68 File Offset: 0x00010168
		public static void SetMVGuid(byte[] buff, ref int pos, Guid[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 16);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeGuid(values[i], buff, pos + 4 + i * 16);
				}
			}
			pos += 4 + values.Length * 16;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00011FCC File Offset: 0x000101CC
		public static void SetMVSystime(byte[] buff, ref int pos, DateTime[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeFileTime(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001202C File Offset: 0x0001022C
		public static void SetMVUnicode(byte[] buff, ref int pos, string[] values)
		{
			ParseSerialize.SetDword(buff, ref pos, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				ParseSerialize.SetUnicodeString(buff, ref pos, values[i]);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001205C File Offset: 0x0001025C
		public static void SetMVBinary(byte[] buff, ref int pos, byte[][] values)
		{
			ParseSerialize.SetDword(buff, ref pos, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				ParseSerialize.SetByteArray(buff, ref pos, values[i]);
			}
		}

		// Token: 0x04000605 RID: 1541
		public const int SizeOfByte = 1;

		// Token: 0x04000606 RID: 1542
		public const int SizeOfInt16 = 2;

		// Token: 0x04000607 RID: 1543
		public const int SizeOfInt32 = 4;

		// Token: 0x04000608 RID: 1544
		public const int SizeOfInt64 = 8;

		// Token: 0x04000609 RID: 1545
		public const int SizeOfSingle = 4;

		// Token: 0x0400060A RID: 1546
		public const int SizeOfDouble = 8;

		// Token: 0x0400060B RID: 1547
		public const int SizeOfGuid = 16;

		// Token: 0x0400060C RID: 1548
		public const int SizeOfFileTime = 8;

		// Token: 0x0400060D RID: 1549
		public const int SizeOfUnicodeChar = 2;

		// Token: 0x0400060E RID: 1550
		public static readonly long MinFileTime = 0L;

		// Token: 0x0400060F RID: 1551
		public static readonly long MaxFileTime = DateTime.MaxValue.ToFileTimeUtc();

		// Token: 0x04000610 RID: 1552
		public static readonly DateTime MinFileTimeDateTime = DateTime.FromFileTimeUtc(ParseSerialize.MinFileTime);

		// Token: 0x04000611 RID: 1553
		private static readonly byte[] emptyByteArray = new byte[0];
	}
}

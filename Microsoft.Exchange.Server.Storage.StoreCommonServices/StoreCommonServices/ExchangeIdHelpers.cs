using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200005F RID: 95
	public static class ExchangeIdHelpers
	{
		// Token: 0x0600035D RID: 861 RVA: 0x000187A0 File Offset: 0x000169A0
		public static void From26ByteArray(byte[] buff, out Guid guid, out ushort replid, out ulong counter)
		{
			if (buff == null)
			{
				guid = Guid.Empty;
				replid = 0;
				counter = 0UL;
				return;
			}
			int a = ParseSerialize.ParseInt32(buff, 0);
			short b = ParseSerialize.ParseInt16(buff, 4);
			short c = ParseSerialize.ParseInt16(buff, 6);
			guid = new Guid(a, b, c, buff[8], buff[9], buff[10], buff[11], buff[12], buff[13], buff[14], buff[15]);
			counter = ExchangeIdHelpers.GlobcntFromByteArray(buff, 16U);
			replid = (ushort)ParseSerialize.ParseInt16(buff, 24);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001881D File Offset: 0x00016A1D
		public static void From8ByteArray(byte[] buff, out ushort replid, out ulong counter)
		{
			if (buff == null)
			{
				replid = 0;
				counter = 0UL;
				return;
			}
			ExchangeIdHelpers.FromLong(ParseSerialize.ParseInt64(buff, 0), out replid, out counter);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00018838 File Offset: 0x00016A38
		public static void From9ByteArray(byte[] buff, out ushort replid, out ulong counter)
		{
			if (buff == null)
			{
				replid = 0;
				counter = 0UL;
				return;
			}
			ExchangeIdHelpers.FromLong(ParseSerialize.ParseInt64(buff, 1), out replid, out counter);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00018854 File Offset: 0x00016A54
		public static void FromLong(long shortTermId, out ushort replid, out ulong counter)
		{
			replid = (ushort)shortTermId;
			counter = (ulong)((shortTermId & 16711680L) << 24 | (shortTermId & (long)((ulong)-16777216)) << 8 | (long)((ulong)(shortTermId & 1095216660480L) >> 8) | (long)((ulong)(shortTermId & 280375465082880L) >> 24) | (long)((ulong)(shortTermId & 71776119061217280L) >> 40) | (long)((ulong)(shortTermId & -72057594037927936L) >> 56));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000188BC File Offset: 0x00016ABC
		public static ulong CounterFromLong(long shortTermId)
		{
			return (ulong)((shortTermId & 16711680L) << 24 | (shortTermId & (long)((ulong)-16777216)) << 8 | (long)((ulong)(shortTermId & 1095216660480L) >> 8) | (long)((ulong)(shortTermId & 280375465082880L) >> 24) | (long)((ulong)(shortTermId & 71776119061217280L) >> 40) | (long)((ulong)(shortTermId & -72057594037927936L) >> 56));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001891C File Offset: 0x00016B1C
		public static byte[] Convert26ByteTo22Byte(byte[] bytes)
		{
			byte[] array = new byte[22];
			Buffer.BlockCopy(bytes, 0, array, 0, 22);
			return array;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00018940 File Offset: 0x00016B40
		public static byte[] Convert26ByteTo24Byte(byte[] bytes)
		{
			byte[] array = new byte[24];
			Buffer.BlockCopy(bytes, 0, array, 0, 24);
			return array;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00018964 File Offset: 0x00016B64
		public static long Convert26ByteToLong(byte[] bytes)
		{
			ulong counter = ExchangeIdHelpers.GlobcntFromByteArray(bytes, 16U);
			ushort replid = (ushort)ParseSerialize.ParseInt16(bytes, 24);
			return ExchangeIdHelpers.ToLong(replid, counter);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001898C File Offset: 0x00016B8C
		public static long Convert9ByteToLong(byte[] bytes)
		{
			ushort replid = (ushort)ParseSerialize.ParseInt16(bytes, 1);
			ulong counter = ExchangeIdHelpers.GlobcntFromByteArray(bytes, 3U);
			return ExchangeIdHelpers.ToLong(replid, counter);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000189B4 File Offset: 0x00016BB4
		public static byte[] Convert26ByteTo9Byte(byte[] bytes)
		{
			ulong counter = ExchangeIdHelpers.GlobcntFromByteArray(bytes, 16U);
			ushort replid = (ushort)ParseSerialize.ParseInt16(bytes, 24);
			return ExchangeIdHelpers.To9ByteArray(replid, counter);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000189DC File Offset: 0x00016BDC
		public static byte[] ConvertLongTo9Byte(long shortTermId)
		{
			ushort replid;
			ulong counter;
			ExchangeIdHelpers.FromLong(shortTermId, out replid, out counter);
			return ExchangeIdHelpers.To9ByteArray(replid, counter);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000189FC File Offset: 0x00016BFC
		public static byte[] Convert26ByteToFolderSvrEid(byte[] fid)
		{
			Guid guid;
			ushort replid;
			ulong counter;
			ExchangeIdHelpers.From26ByteArray(fid, out guid, out replid, out counter);
			return ExchangeIdHelpers.BuildOursServerEntryId(ExchangeIdHelpers.ToLong(replid, counter), 0L, 0);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00018A24 File Offset: 0x00016C24
		public static byte[] BuildOursServerEntryId(byte[] fid, byte[] mid, int instanceNum)
		{
			Guid guid;
			ushort replid;
			ulong counter;
			ExchangeIdHelpers.From26ByteArray(fid, out guid, out replid, out counter);
			ushort replid2;
			ulong counter2;
			ExchangeIdHelpers.From26ByteArray(mid, out guid, out replid2, out counter2);
			return ExchangeIdHelpers.BuildOursServerEntryId(ExchangeIdHelpers.ToLong(replid, counter), ExchangeIdHelpers.ToLong(replid2, counter2), instanceNum);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00018A60 File Offset: 0x00016C60
		public static byte[] BuildOursServerEntryId(long fid, long mid, int instanceNum)
		{
			byte[] array = new byte[21];
			int num = 0;
			array[num++] = 1;
			num += ParseSerialize.SerializeInt64(fid, array, num);
			num += ParseSerialize.SerializeInt64(mid, array, num);
			ParseSerialize.SerializeInt32(instanceNum, array, num);
			return array;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00018AA0 File Offset: 0x00016CA0
		public static bool ParseOursServerEntryId(byte[] entryId, out long fid, out long mid, out int instanceNum)
		{
			if (entryId == null || entryId.Length != 21 || entryId[0] != 1)
			{
				fid = 0L;
				mid = 0L;
				instanceNum = 0;
				DiagnosticContext.TraceLocation((LID)57320U);
				return false;
			}
			fid = ParseSerialize.ParseInt64(entryId, 1);
			mid = ParseSerialize.ParseInt64(entryId, 9);
			instanceNum = ParseSerialize.ParseInt32(entryId, 17);
			return true;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00018AF7 File Offset: 0x00016CF7
		public static ulong GlobcntFromByteArray(byte[] src, uint offset)
		{
			return (ulong)src[(int)((UIntPtr)offset)] << 40 | (ulong)src[(int)((UIntPtr)(offset + 1U))] << 32 | (ulong)src[(int)((UIntPtr)(offset + 2U))] << 24 | (ulong)src[(int)((UIntPtr)(offset + 3U))] << 16 | (ulong)src[(int)((UIntPtr)(offset + 4U))] << 8 | (ulong)src[(int)((UIntPtr)(offset + 5U))];
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00018B34 File Offset: 0x00016D34
		public static int GlobcntIntoByteArray(ulong globCnt, byte[] dst, int offset)
		{
			dst[offset] = (byte)(globCnt >> 40);
			dst[offset + 1] = (byte)(globCnt >> 32);
			dst[offset + 2] = (byte)(globCnt >> 24);
			dst[offset + 3] = (byte)(globCnt >> 16);
			dst[offset + 4] = (byte)(globCnt >> 8);
			dst[offset + 5] = (byte)globCnt;
			return 6;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00018B70 File Offset: 0x00016D70
		public static long ToLong(ushort replid, ulong counter)
		{
			return (long)((counter & 255UL) << 56 | (counter & 65280UL) << 40 | (counter & 16711680UL) << 24 | (counter & (ulong)-16777216) << 8 | (counter & 1095216660480UL) >> 8 | (counter & 280375465082880UL) >> 24 | (ulong)replid);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00018BCC File Offset: 0x00016DCC
		public static byte[] To8ByteArray(ushort replid, ulong counter)
		{
			byte[] array = new byte[8];
			ExchangeIdHelpers.To8ByteArray(replid, counter, array, 0);
			return array;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00018BEC File Offset: 0x00016DEC
		public static int To8ByteArray(ushort replid, ulong counter, byte[] buffer, int offset)
		{
			int num = offset;
			offset += ParseSerialize.SerializeInt16((short)replid, buffer, offset);
			offset += ExchangeIdHelpers.GlobcntIntoByteArray(counter, buffer, offset);
			return offset - num;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00018C18 File Offset: 0x00016E18
		public static byte[] To9ByteArray(ushort replid, ulong counter)
		{
			byte[] array = new byte[9];
			ExchangeIdHelpers.To9ByteArray(replid, counter, array, 0);
			return array;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00018C38 File Offset: 0x00016E38
		public static int To9ByteArray(ushort replid, ulong counter, byte[] buffer, int offset)
		{
			int num = offset;
			buffer[offset++] = ((replid == 1) ? 1 : 0);
			offset += ParseSerialize.SerializeInt16((short)replid, buffer, offset);
			offset += ExchangeIdHelpers.GlobcntIntoByteArray(counter, buffer, offset);
			return offset - num;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00018C74 File Offset: 0x00016E74
		public static byte[] To22ByteArray(Guid guid, ulong counter)
		{
			byte[] array = new byte[22];
			ExchangeIdHelpers.To22ByteArray(guid, counter, array, 0);
			return array;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00018C94 File Offset: 0x00016E94
		public static int To22ByteArray(Guid guid, ulong counter, byte[] buffer, int offset)
		{
			int num = offset;
			offset += ParseSerialize.SerializeGuid(guid, buffer, offset);
			offset += ExchangeIdHelpers.GlobcntIntoByteArray(counter, buffer, offset);
			return offset - num;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public static byte[] To24ByteArray(Guid guid, ulong counter)
		{
			byte[] array = new byte[24];
			ExchangeIdHelpers.To22ByteArray(guid, counter, array, 0);
			return array;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00018CE0 File Offset: 0x00016EE0
		public static byte[] To26ByteArray(ushort replid, Guid guid, ulong counter)
		{
			byte[] array = new byte[26];
			ExchangeIdHelpers.To26ByteArray(replid, guid, counter, array, 0);
			return array;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00018D04 File Offset: 0x00016F04
		public static int To26ByteArray(ushort replid, Guid guid, ulong counter, byte[] buffer, int offset)
		{
			int num = offset;
			offset += ParseSerialize.SerializeGuid(guid, buffer, offset);
			offset += ExchangeIdHelpers.GlobcntIntoByteArray(counter, buffer, offset);
			offset += 2;
			offset += ParseSerialize.SerializeInt16((short)replid, buffer, offset);
			return offset - num;
		}
	}
}

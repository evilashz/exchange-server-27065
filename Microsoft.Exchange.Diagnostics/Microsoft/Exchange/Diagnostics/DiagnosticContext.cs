using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200000A RID: 10
	public class DiagnosticContext
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000251E File Offset: 0x0000071E
		public static DiagnosticContext.IDiagnosticContextTest DiagnosticContextTest
		{
			get
			{
				if (DiagnosticContext.diagnosticContextTestFactory != null)
				{
					if (DiagnosticContext.diagnosticContextTest == null)
					{
						DiagnosticContext.diagnosticContextTest = DiagnosticContext.diagnosticContextTestFactory();
					}
					return DiagnosticContext.diagnosticContextTest;
				}
				return null;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002544 File Offset: 0x00000744
		public static bool HasData
		{
			get
			{
				BipBuffer buffer = DiagnosticContext.GetBuffer();
				return buffer.AllocatedSize > 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002560 File Offset: 0x00000760
		public static int Size
		{
			get
			{
				BipBuffer buffer = DiagnosticContext.GetBuffer();
				return buffer.AllocatedSize + DiagnosticContext.ContextBuffer.StructSize;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000257F File Offset: 0x0000077F
		public static uint ContextSignatureMask
		{
			get
			{
				return 4293918720U;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002586 File Offset: 0x00000786
		public static uint ContextLidMask
		{
			get
			{
				return 1048575U;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000258D File Offset: 0x0000078D
		public static void SetTestHook(DiagnosticContext.DiagnosticContextTestFactory diagnosticContextFactory)
		{
			DiagnosticContext.diagnosticContextTestFactory = diagnosticContextFactory;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002595 File Offset: 0x00000795
		public static void SetOnLIDCallback(Action<LID> callback)
		{
			DiagnosticContext.onLIDCallback = callback;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025A0 File Offset: 0x000007A0
		public unsafe static void TraceLocation(LID lid)
		{
			DiagnosticContext.TraceTestLocation(lid.Value);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(268435456U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationRecord* ptr2 = (DiagnosticContext.LocationRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 268435456U);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002604 File Offset: 0x00000804
		public unsafe static void TraceDword(LID lid, uint info)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, 0U, info);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(269484032U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndDwordRecord* ptr2 = (DiagnosticContext.LocationAndDwordRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 269484032U);
				ptr2->Info = info;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002670 File Offset: 0x00000870
		public unsafe static void TraceLong(LID lid, ulong info)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, (uint)(info >> 32), (uint)(info & (ulong)-1));
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(544210944U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndLongRecord* ptr2 = (DiagnosticContext.LocationAndLongRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 544210944U);
				ptr2->Info = info;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026E4 File Offset: 0x000008E4
		public unsafe static void TraceGenericError(LID lid, uint error)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, error);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(270532608U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndGenericErrorRecord* ptr2 = (DiagnosticContext.LocationAndGenericErrorRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 270532608U);
				ptr2->Error = error;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002750 File Offset: 0x00000950
		public unsafe static void TraceWin32Error(LID lid, uint error)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, error);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(271581184U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndWin32ErrorRecord* ptr2 = (DiagnosticContext.LocationAndWin32ErrorRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 271581184U);
				ptr2->WinError = error;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027BC File Offset: 0x000009BC
		public unsafe static void TraceStoreError(LID lid, uint storeError)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, storeError);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(272629760U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndStoreErrorRecord* ptr2 = (DiagnosticContext.LocationAndStoreErrorRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 272629760U);
				ptr2->StoreError = storeError;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002828 File Offset: 0x00000A28
		public unsafe static void TracePropTagError(LID lid, uint storeError, uint propTag)
		{
			DiagnosticContext.TraceTestLocation(lid.Value, storeError, propTag);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(543162368U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndStoreErrorAndProptagRecord* ptr2 = (DiagnosticContext.LocationAndStoreErrorAndProptagRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 543162368U);
				ptr2->StoreError = storeError;
				ptr2->PropTag = propTag;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000289C File Offset: 0x00000A9C
		public unsafe static void TraceDwordAndString(LID lid, uint info, string str)
		{
			DiagnosticContext.TraceTestLocation(lid.Value);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			DiagnosticContext.GetBuffer();
			int bytes = DiagnosticContext.asciiEncoding.GetBytes(str, 0, Math.Min(103, str.Length), DiagnosticContext.stringBuffer, 0);
			int num = Math.Min(bytes, 103) + 1;
			int num2 = 1 + (num + 7) / 8;
			uint num3 = (uint)((uint)num2 << 28);
			num3 |= 5242880U;
			byte[] array;
			int num4;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(num3), out array, out num4);
			fixed (byte* ptr = &array[num4])
			{
				DiagnosticContext.LocationAndDwordAndStringRecord* ptr2 = (DiagnosticContext.LocationAndDwordAndStringRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, num3);
				ptr2->Info = info;
			}
			Array.Copy(DiagnosticContext.stringBuffer, 0, array, num4 + DiagnosticContext.LocationAndDwordAndStringRecord.StructSize, num - 1);
			array[num4 + DiagnosticContext.LocationAndDwordAndStringRecord.StructSize + num - 1] = 0;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002974 File Offset: 0x00000B74
		public unsafe static void TraceGuid(LID lid, Guid info)
		{
			DiagnosticContext.TraceTestLocation(lid.Value);
			if (DiagnosticContext.onLIDCallback != null)
			{
				DiagnosticContext.onLIDCallback(lid);
			}
			byte[] array;
			int num;
			DiagnosticContext.GetBufferPointer(DiagnosticContext.SizeOfRecordFromSignature(813694976U), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				DiagnosticContext.LocationAndGuidRecord* ptr2 = (DiagnosticContext.LocationAndGuidRecord*)ptr;
				ptr2->Lid = DiagnosticContext.AdjustLID(lid.Value, 813694976U);
				ptr2->Info = info;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029DE File Offset: 0x00000BDE
		public static DiagnosticContext.MeasuredScope TraceLatency(LID lid)
		{
			return new DiagnosticContext.MeasuredScope(lid);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029E8 File Offset: 0x00000BE8
		public static void ExtractInfo(int maxSize, out byte flags, out byte[] info)
		{
			BipBuffer buffer = DiagnosticContext.GetBuffer();
			DiagnosticContext.Shared.ExtractInfo(buffer, maxSize, out info);
			flags = (byte)(2 | (DiagnosticContext.overFlow ? 1 : 0));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A14 File Offset: 0x00000C14
		public unsafe static int PackInfo(byte[] destinationBuffer, ref int pos, int maxSize)
		{
			BipBuffer buffer = DiagnosticContext.GetBuffer();
			if (maxSize < DiagnosticContext.ContextBuffer.StructSize)
			{
				return 0;
			}
			while (maxSize < DiagnosticContext.Size)
			{
				DiagnosticContext.FlushHeadRecord(buffer);
			}
			fixed (byte* ptr = &destinationBuffer[pos])
			{
				DiagnosticContext.ContextBuffer* ptr2 = (DiagnosticContext.ContextBuffer*)ptr;
				ptr2->Format = byte.MaxValue;
				ptr2->ThreadID = (uint)Environment.CurrentManagedThreadId;
				ptr2->RequestID = 0U;
				ptr2->Flags = (byte)(2 | (DiagnosticContext.overFlow ? 1 : 0));
				ptr2->Length = (uint)buffer.AllocatedSize;
			}
			pos += DiagnosticContext.ContextBuffer.StructSize;
			maxSize -= DiagnosticContext.ContextBuffer.StructSize;
			int allocatedSize = buffer.AllocatedSize;
			buffer.Extract(destinationBuffer, pos, allocatedSize);
			pos += allocatedSize;
			return allocatedSize + DiagnosticContext.ContextBuffer.StructSize;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002ABC File Offset: 0x00000CBC
		public static byte[] PackInfo()
		{
			BipBuffer buffer = DiagnosticContext.GetBuffer();
			byte[] array = new byte[DiagnosticContext.ContextBuffer.StructSize + buffer.AllocatedSize];
			int num = 0;
			DiagnosticContext.PackInfo(array, ref num, array.Length);
			return array;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static void Reset()
		{
			if (DiagnosticContext.DiagnosticContextTest != null)
			{
				DiagnosticContext.DiagnosticContextTest.Reset();
			}
			BipBuffer buffer = DiagnosticContext.GetBuffer();
			DiagnosticContext.Shared.Reset(buffer);
			DiagnosticContext.overFlow = false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B20 File Offset: 0x00000D20
		public static void AppendToBuffer(byte[] sourceBuffer)
		{
			int size = sourceBuffer.Length - DiagnosticContext.ContextBuffer.StructSize;
			byte[] destinationArray;
			int destinationIndex;
			DiagnosticContext.GetBufferPointer(size, out destinationArray, out destinationIndex);
			Array.Copy(sourceBuffer, DiagnosticContext.ContextBuffer.StructSize, destinationArray, destinationIndex, sourceBuffer.Length - DiagnosticContext.ContextBuffer.StructSize);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B57 File Offset: 0x00000D57
		private static void TraceTestLocation(uint testLid)
		{
			if (DiagnosticContext.DiagnosticContextTest != null)
			{
				DiagnosticContext.DiagnosticContextTest.TraceTestLocation(testLid, 0U, 0U);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B6D File Offset: 0x00000D6D
		private static void TraceTestLocation(uint testLid, uint error)
		{
			if (DiagnosticContext.DiagnosticContextTest != null)
			{
				DiagnosticContext.DiagnosticContextTest.TraceTestLocation(testLid, error, 0U);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B83 File Offset: 0x00000D83
		private static void TraceTestLocation(uint testLid, uint error, uint info)
		{
			if (DiagnosticContext.DiagnosticContextTest != null)
			{
				DiagnosticContext.DiagnosticContextTest.TraceTestLocation(testLid, error, info);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B9C File Offset: 0x00000D9C
		private static BipBuffer GetBuffer()
		{
			BipBuffer bipBuffer = DiagnosticContext.bipBuffer;
			if (bipBuffer == null)
			{
				bipBuffer = (DiagnosticContext.bipBuffer = new BipBuffer(2048));
				DiagnosticContext.overFlow = false;
				DiagnosticContext.stringBuffer = new byte[104];
			}
			return bipBuffer;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BD6 File Offset: 0x00000DD6
		private static uint AdjustLID(uint lid, uint signature)
		{
			return DiagnosticContext.Shared.AdjustLID(lid, signature);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BDF File Offset: 0x00000DDF
		private static int SizeOfRecordFromSignature(uint signature)
		{
			return DiagnosticContext.Shared.SizeOfRecordFromSignature(signature);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private static void FlushHeadRecord(BipBuffer buf)
		{
			byte b;
			buf.Extract(3, out b);
			int num = (b >> 4) * 8;
			buf.Release(num - 4);
			DiagnosticContext.overFlow = true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C14 File Offset: 0x00000E14
		private static void GetBufferPointer(int size, out byte[] buffer, out int index)
		{
			BipBuffer buffer2 = DiagnosticContext.GetBuffer();
			DiagnosticContext.Shared.GetBufferPointer(buffer2, size, out buffer, out index);
		}

		// Token: 0x0400003C RID: 60
		private const uint ContextRecordLengthMask = 4026531840U;

		// Token: 0x0400003D RID: 61
		private const uint ContextRecordTypeMask = 267386880U;

		// Token: 0x0400003E RID: 62
		private const uint ContextRecordLidMask = 1048575U;

		// Token: 0x0400003F RID: 63
		private const int RecordLengthBitShift = 28;

		// Token: 0x04000040 RID: 64
		private static readonly Encoding asciiEncoding = Encoding.GetEncoding("us-ascii");

		// Token: 0x04000041 RID: 65
		[ThreadStatic]
		private static DiagnosticContext.IDiagnosticContextTest diagnosticContextTest;

		// Token: 0x04000042 RID: 66
		private static DiagnosticContext.DiagnosticContextTestFactory diagnosticContextTestFactory;

		// Token: 0x04000043 RID: 67
		[ThreadStatic]
		private static BipBuffer bipBuffer;

		// Token: 0x04000044 RID: 68
		[ThreadStatic]
		private static bool overFlow;

		// Token: 0x04000045 RID: 69
		[ThreadStatic]
		private static byte[] stringBuffer;

		// Token: 0x04000046 RID: 70
		[ThreadStatic]
		private static Stopwatch cachedStopwatch;

		// Token: 0x04000047 RID: 71
		private static Action<LID> onLIDCallback;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x0600003F RID: 63
		public delegate DiagnosticContext.IDiagnosticContextTest DiagnosticContextTestFactory();

		// Token: 0x0200000C RID: 12
		public interface IDiagnosticContextTest
		{
			// Token: 0x06000042 RID: 66
			void TraceTestLocation(uint testLid, uint error = 0U, uint info = 0U);

			// Token: 0x06000043 RID: 67
			void Reset();
		}

		// Token: 0x0200000D RID: 13
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationRecord
		{
			// Token: 0x04000048 RID: 72
			public const uint Signature = 268435456U;

			// Token: 0x04000049 RID: 73
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationRecord));

			// Token: 0x0400004A RID: 74
			public uint Lid;
		}

		// Token: 0x0200000E RID: 14
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndDwordRecord
		{
			// Token: 0x0400004B RID: 75
			public const uint Signature = 269484032U;

			// Token: 0x0400004C RID: 76
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndDwordRecord));

			// Token: 0x0400004D RID: 77
			public uint Lid;

			// Token: 0x0400004E RID: 78
			public uint Info;
		}

		// Token: 0x0200000F RID: 15
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndGenericErrorRecord
		{
			// Token: 0x0400004F RID: 79
			public const uint Signature = 270532608U;

			// Token: 0x04000050 RID: 80
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndGenericErrorRecord));

			// Token: 0x04000051 RID: 81
			public uint Lid;

			// Token: 0x04000052 RID: 82
			public uint Error;
		}

		// Token: 0x02000010 RID: 16
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndWin32ErrorRecord
		{
			// Token: 0x04000053 RID: 83
			public const uint Signature = 271581184U;

			// Token: 0x04000054 RID: 84
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndWin32ErrorRecord));

			// Token: 0x04000055 RID: 85
			public uint Lid;

			// Token: 0x04000056 RID: 86
			public uint WinError;
		}

		// Token: 0x02000011 RID: 17
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndStoreErrorRecord
		{
			// Token: 0x04000057 RID: 87
			public const uint Signature = 272629760U;

			// Token: 0x04000058 RID: 88
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndStoreErrorRecord));

			// Token: 0x04000059 RID: 89
			public uint Lid;

			// Token: 0x0400005A RID: 90
			public uint StoreError;
		}

		// Token: 0x02000012 RID: 18
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndDwordAndStringRecord
		{
			// Token: 0x0400005B RID: 91
			public const uint Signature = 5242880U;

			// Token: 0x0400005C RID: 92
			public const int MaxStringLength = 103;

			// Token: 0x0400005D RID: 93
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndDwordAndStringRecord));

			// Token: 0x0400005E RID: 94
			public uint Lid;

			// Token: 0x0400005F RID: 95
			public uint Info;
		}

		// Token: 0x02000013 RID: 19
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndGuidRecord
		{
			// Token: 0x04000060 RID: 96
			public const uint Signature = 813694976U;

			// Token: 0x04000061 RID: 97
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndGuidRecord));

			// Token: 0x04000062 RID: 98
			public uint Lid;

			// Token: 0x04000063 RID: 99
			public Guid Info;
		}

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndStoreErrorAndProptagRecord
		{
			// Token: 0x04000064 RID: 100
			public const uint Signature = 543162368U;

			// Token: 0x04000065 RID: 101
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndStoreErrorAndProptagRecord));

			// Token: 0x04000066 RID: 102
			public uint Lid;

			// Token: 0x04000067 RID: 103
			public uint StoreError;

			// Token: 0x04000068 RID: 104
			public uint PropTag;
		}

		// Token: 0x02000015 RID: 21
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct LocationAndLongRecord
		{
			// Token: 0x04000069 RID: 105
			public const uint Signature = 544210944U;

			// Token: 0x0400006A RID: 106
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.LocationAndLongRecord));

			// Token: 0x0400006B RID: 107
			public uint Lid;

			// Token: 0x0400006C RID: 108
			public ulong Info;
		}

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct ContextBuffer
		{
			// Token: 0x0400006D RID: 109
			public static readonly int StructSize = Marshal.SizeOf(typeof(DiagnosticContext.ContextBuffer));

			// Token: 0x0400006E RID: 110
			public byte Format;

			// Token: 0x0400006F RID: 111
			public uint ThreadID;

			// Token: 0x04000070 RID: 112
			public uint RequestID;

			// Token: 0x04000071 RID: 113
			public byte Flags;

			// Token: 0x04000072 RID: 114
			public uint Length;
		}

		// Token: 0x02000017 RID: 23
		public static class Shared
		{
			// Token: 0x0600004E RID: 78 RVA: 0x00002D28 File Offset: 0x00000F28
			public static void ExtractInfo(BipBuffer buf, int maxSize, out byte[] info)
			{
				while (maxSize < buf.AllocatedSize)
				{
					DiagnosticContext.FlushHeadRecord(buf);
				}
				int allocatedSize = buf.AllocatedSize;
				info = new byte[allocatedSize];
				buf.Extract(info, 0, allocatedSize);
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00002D5F File Offset: 0x00000F5F
			public static void GetBufferPointer(BipBuffer buf, int size, out byte[] buffer, out int index)
			{
				do
				{
					index = buf.Allocate(size);
					if (-1 == index)
					{
						DiagnosticContext.FlushHeadRecord(buf);
					}
				}
				while (-1 == index);
				buffer = buf.Buffer;
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002D82 File Offset: 0x00000F82
			public static void Reset(BipBuffer buf)
			{
				buf.Release(buf.AllocatedSize);
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00002D90 File Offset: 0x00000F90
			public static uint AdjustLID(uint lid, uint signature)
			{
				return lid | signature;
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00002D95 File Offset: 0x00000F95
			public static int SizeOfRecordFromSignature(uint signature)
			{
				return (int)(8U * (signature >> 28));
			}
		}

		// Token: 0x02000018 RID: 24
		public struct MeasuredScope : IDisposable
		{
			// Token: 0x06000053 RID: 83 RVA: 0x00002D9D File Offset: 0x00000F9D
			public MeasuredScope(LID lid)
			{
				if (DiagnosticContext.cachedStopwatch != null)
				{
					this.stopwatch = DiagnosticContext.cachedStopwatch;
					DiagnosticContext.cachedStopwatch = null;
				}
				else
				{
					this.stopwatch = new Stopwatch();
				}
				this.lid = lid;
				this.stopwatch.Restart();
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00002DD6 File Offset: 0x00000FD6
			public void Dispose()
			{
				this.stopwatch.Stop();
				DiagnosticContext.TraceDword(this.lid, (uint)this.stopwatch.ElapsedMilliseconds);
				if (DiagnosticContext.cachedStopwatch == null)
				{
					DiagnosticContext.cachedStopwatch = this.stopwatch;
				}
			}

			// Token: 0x04000073 RID: 115
			private Stopwatch stopwatch;

			// Token: 0x04000074 RID: 116
			private LID lid;
		}
	}
}

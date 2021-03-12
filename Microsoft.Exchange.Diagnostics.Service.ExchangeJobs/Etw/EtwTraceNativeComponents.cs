using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x0200000F RID: 15
	public static class EtwTraceNativeComponents
	{
		// Token: 0x06000066 RID: 102
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenTraceW", SetLastError = true)]
		internal static extern ulong OpenTrace([In] [Out] ref EtwTraceNativeComponents.EVENT_TRACE_LOGFILEW logfile);

		// Token: 0x06000067 RID: 103
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern int ProcessTrace([In] ulong[] handleArray, [In] uint handleCount, [In] IntPtr startTime, [In] IntPtr endTime);

		// Token: 0x06000068 RID: 104
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern int CloseTrace([In] ulong traceHandle);

		// Token: 0x04000082 RID: 130
		internal const ushort EventHeaderFlag32BitHeader = 32;

		// Token: 0x04000083 RID: 131
		internal const ushort EventHeaderFlag64BitHeader = 64;

		// Token: 0x04000084 RID: 132
		internal const ushort EventHeaderFlagClassicHeader = 256;

		// Token: 0x04000085 RID: 133
		internal const uint ProcessTraceModeEventRecord = 268435456U;

		// Token: 0x04000086 RID: 134
		internal const uint ProcessTraceModeRawTimestamp = 4096U;

		// Token: 0x04000087 RID: 135
		internal const ulong InvalidHeaderValue = 18446744073709551615UL;

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x0600006A RID: 106
		internal delegate bool EventTraceBufferCallback([In] IntPtr logfile);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x0600006E RID: 110
		internal unsafe delegate void EventTraceEventCallback([In] EtwTraceNativeComponents.EVENT_RECORD* rawData);

		// Token: 0x02000012 RID: 18
		internal enum TraceMessageFlags
		{
			// Token: 0x04000089 RID: 137
			Sequence = 1,
			// Token: 0x0400008A RID: 138
			Guid,
			// Token: 0x0400008B RID: 139
			ComponentId = 4,
			// Token: 0x0400008C RID: 140
			Timestamp = 8,
			// Token: 0x0400008D RID: 141
			PerformanceTimestamp = 16,
			// Token: 0x0400008E RID: 142
			SystemInfo = 32,
			// Token: 0x0400008F RID: 143
			FlagMask = 65535
		}

		// Token: 0x02000013 RID: 19
		internal enum EventIndex : uint
		{
			// Token: 0x04000091 RID: 145
			Invalid = 4294967295U
		}

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 172)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x04000092 RID: 146
			public uint Bias;

			// Token: 0x04000093 RID: 147
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string StandardName;

			// Token: 0x04000094 RID: 148
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U2)]
			public ushort[] StandardDate;

			// Token: 0x04000095 RID: 149
			public uint StandardBias;

			// Token: 0x04000096 RID: 150
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DaylightName;

			// Token: 0x04000097 RID: 151
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U2)]
			public ushort[] DaylightDate;

			// Token: 0x04000098 RID: 152
			public uint DaylightBias;
		}

		// Token: 0x02000015 RID: 21
		internal struct EVENT_TRACE_HEADER
		{
			// Token: 0x04000099 RID: 153
			public ushort Size;

			// Token: 0x0400009A RID: 154
			public ushort FieldTypeFlags;

			// Token: 0x0400009B RID: 155
			public byte Type;

			// Token: 0x0400009C RID: 156
			public byte Level;

			// Token: 0x0400009D RID: 157
			public ushort Version;

			// Token: 0x0400009E RID: 158
			public int ThreadId;

			// Token: 0x0400009F RID: 159
			public int ProcessId;

			// Token: 0x040000A0 RID: 160
			public long TimeStamp;

			// Token: 0x040000A1 RID: 161
			public Guid Guid;

			// Token: 0x040000A2 RID: 162
			public int KernelTime;

			// Token: 0x040000A3 RID: 163
			public int UserTime;
		}

		// Token: 0x02000016 RID: 22
		internal struct EVENT_TRACE
		{
			// Token: 0x040000A4 RID: 164
			public EtwTraceNativeComponents.EVENT_TRACE_HEADER Header;

			// Token: 0x040000A5 RID: 165
			public uint InstanceId;

			// Token: 0x040000A6 RID: 166
			public uint ParentInstanceId;

			// Token: 0x040000A7 RID: 167
			public Guid ParentGuid;

			// Token: 0x040000A8 RID: 168
			public IntPtr MofData;

			// Token: 0x040000A9 RID: 169
			public int MofLength;

			// Token: 0x040000AA RID: 170
			public EtwTraceNativeComponents.ETW_BUFFER_CONTEXT BufferContext;
		}

		// Token: 0x02000017 RID: 23
		internal struct TRACE_LOGFILE_HEADER
		{
			// Token: 0x040000AB RID: 171
			public uint BufferSize;

			// Token: 0x040000AC RID: 172
			public uint Version;

			// Token: 0x040000AD RID: 173
			public uint ProviderVersion;

			// Token: 0x040000AE RID: 174
			public uint NumberOfProcessors;

			// Token: 0x040000AF RID: 175
			public long EndTime;

			// Token: 0x040000B0 RID: 176
			public uint TimerResolution;

			// Token: 0x040000B1 RID: 177
			public uint MaximumFileSize;

			// Token: 0x040000B2 RID: 178
			public uint LogFileMode;

			// Token: 0x040000B3 RID: 179
			public uint BuffersWritten;

			// Token: 0x040000B4 RID: 180
			public uint StartBuffers;

			// Token: 0x040000B5 RID: 181
			public uint PointerSize;

			// Token: 0x040000B6 RID: 182
			public uint EventsLost;

			// Token: 0x040000B7 RID: 183
			public uint CpuSpeedInMHz;

			// Token: 0x040000B8 RID: 184
			public IntPtr LoggerName;

			// Token: 0x040000B9 RID: 185
			public IntPtr LogFileName;

			// Token: 0x040000BA RID: 186
			public EtwTraceNativeComponents.TIME_ZONE_INFORMATION TimeZone;

			// Token: 0x040000BB RID: 187
			public long BootTime;

			// Token: 0x040000BC RID: 188
			public long PerfFreq;

			// Token: 0x040000BD RID: 189
			public long StartTime;

			// Token: 0x040000BE RID: 190
			public uint ReservedFlags;

			// Token: 0x040000BF RID: 191
			public uint BuffersLost;
		}

		// Token: 0x02000018 RID: 24
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct EVENT_TRACE_LOGFILEW
		{
			// Token: 0x040000C0 RID: 192
			[MarshalAs(UnmanagedType.LPWStr)]
			public string LogFileName;

			// Token: 0x040000C1 RID: 193
			[MarshalAs(UnmanagedType.LPWStr)]
			public string LoggerName;

			// Token: 0x040000C2 RID: 194
			public long CurrentTime;

			// Token: 0x040000C3 RID: 195
			public uint BuffersRead;

			// Token: 0x040000C4 RID: 196
			public uint LogFileMode;

			// Token: 0x040000C5 RID: 197
			public EtwTraceNativeComponents.EVENT_TRACE CurrentEvent;

			// Token: 0x040000C6 RID: 198
			public EtwTraceNativeComponents.TRACE_LOGFILE_HEADER LogfileHeader;

			// Token: 0x040000C7 RID: 199
			public EtwTraceNativeComponents.EventTraceBufferCallback BufferCallback;

			// Token: 0x040000C8 RID: 200
			public int BufferSize;

			// Token: 0x040000C9 RID: 201
			public int Filled;

			// Token: 0x040000CA RID: 202
			public int EventsLost;

			// Token: 0x040000CB RID: 203
			public EtwTraceNativeComponents.EventTraceEventCallback EventCallback;

			// Token: 0x040000CC RID: 204
			public int IsKernelTrace;

			// Token: 0x040000CD RID: 205
			public IntPtr Context;
		}

		// Token: 0x02000019 RID: 25
		internal struct EVENT_HEADER
		{
			// Token: 0x040000CE RID: 206
			public ushort Size;

			// Token: 0x040000CF RID: 207
			public ushort HeaderType;

			// Token: 0x040000D0 RID: 208
			public ushort Flags;

			// Token: 0x040000D1 RID: 209
			public ushort EventProperty;

			// Token: 0x040000D2 RID: 210
			public int ThreadId;

			// Token: 0x040000D3 RID: 211
			public int ProcessId;

			// Token: 0x040000D4 RID: 212
			public long TimeStamp;

			// Token: 0x040000D5 RID: 213
			public Guid ProviderId;

			// Token: 0x040000D6 RID: 214
			public ushort Id;

			// Token: 0x040000D7 RID: 215
			public byte Version;

			// Token: 0x040000D8 RID: 216
			public byte Channel;

			// Token: 0x040000D9 RID: 217
			public byte Level;

			// Token: 0x040000DA RID: 218
			public byte Opcode;

			// Token: 0x040000DB RID: 219
			public ushort Task;

			// Token: 0x040000DC RID: 220
			public ulong Keyword;

			// Token: 0x040000DD RID: 221
			public int KernelTime;

			// Token: 0x040000DE RID: 222
			public int UserTime;

			// Token: 0x040000DF RID: 223
			public Guid ActivityId;
		}

		// Token: 0x0200001A RID: 26
		internal struct ETW_BUFFER_CONTEXT
		{
			// Token: 0x040000E0 RID: 224
			public byte ProcessorNumber;

			// Token: 0x040000E1 RID: 225
			public byte Alignment;

			// Token: 0x040000E2 RID: 226
			public ushort LoggerId;
		}

		// Token: 0x0200001B RID: 27
		internal struct EVENT_RECORD
		{
			// Token: 0x040000E3 RID: 227
			public EtwTraceNativeComponents.EVENT_HEADER EventHeader;

			// Token: 0x040000E4 RID: 228
			public EtwTraceNativeComponents.ETW_BUFFER_CONTEXT BufferContext;

			// Token: 0x040000E5 RID: 229
			public ushort ExtendedDataCount;

			// Token: 0x040000E6 RID: 230
			public ushort UserDataLength;

			// Token: 0x040000E7 RID: 231
			public unsafe EtwTraceNativeComponents.EVENT_HEADER_EXTENDED_DATA_ITEM* ExtendedData;

			// Token: 0x040000E8 RID: 232
			public IntPtr UserData;

			// Token: 0x040000E9 RID: 233
			public IntPtr UserContext;
		}

		// Token: 0x0200001C RID: 28
		internal struct EVENT_HEADER_EXTENDED_DATA_ITEM
		{
			// Token: 0x040000EA RID: 234
			public ushort Reserved1;

			// Token: 0x040000EB RID: 235
			public ushort ExtType;

			// Token: 0x040000EC RID: 236
			public ushort Reserved2;

			// Token: 0x040000ED RID: 237
			public ushort DataSize;

			// Token: 0x040000EE RID: 238
			public ulong DataPtr;
		}
	}
}

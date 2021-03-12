using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Microsoft
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("ManagedWPP", "1.0.0.0")]
	[Guid("748004CA-4959-409a-887C-6546438CF48E")]
	internal class TraceProvider
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000037DC File Offset: 0x000019DC
		internal TraceProvider(string applicationName, Guid controlGuid, bool useSequenceNumbers)
		{
			this.applicationName = applicationName;
			this.controlGuid = controlGuid;
			this.useSequenceNumbers = useSequenceNumbers;
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle("advapi32.dll");
			IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, "GetTraceLoggerHandle");
			this.getTraceLoggerHandle = (TraceProvider.GetTraceLoggerHandle)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(TraceProvider.GetTraceLoggerHandle));
			procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, "GetTraceEnableFlags");
			this.getTraceEnableFlags = (TraceProvider.GetTraceEnableFlags)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(TraceProvider.GetTraceEnableFlags));
			procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, "GetTraceEnableLevel");
			this.getTraceEnableLevel = (TraceProvider.GetTraceEnableLevel)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(TraceProvider.GetTraceEnableLevel));
			this.Register();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000388C File Offset: 0x00001A8C
		private unsafe uint Register()
		{
			this.etwCallback = new UnsafeNativeMethods.WMIDPREQUEST(this.MyCallback);
			UnsafeNativeMethods.TRACE_GUID_REGISTRATION trace_GUID_REGISTRATION = default(UnsafeNativeMethods.TRACE_GUID_REGISTRATION);
			Guid guid = new Guid("{b4955bf0-3af1-4740-b475-99055d3fe9aa}");
			trace_GUID_REGISTRATION.Guid = &guid;
			trace_GUID_REGISTRATION.RegHandle = IntPtr.Zero;
			return UnsafeNativeMethods.RegisterTraceGuids(this.etwCallback, null, ref this.controlGuid, 1U, ref trace_GUID_REGISTRATION, null, null, out this.registrationHandle);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000038F8 File Offset: 0x00001AF8
		private unsafe uint MyCallback(UnsafeNativeMethods.WMIDPREQUESTCODE requestCode, IntPtr context, uint* bufferSize, byte* byteBuffer)
		{
			switch (requestCode)
			{
			case UnsafeNativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS:
				this.traceHandle = this.getTraceLoggerHandle((UnsafeNativeMethods.WNODE_HEADER*)byteBuffer);
				this.flags = this.getTraceEnableFlags(this.traceHandle);
				this.level = this.getTraceEnableLevel(this.traceHandle);
				this.enabled = true;
				break;
			case UnsafeNativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS:
				this.enabled = false;
				this.traceHandle = 0UL;
				this.level = 0;
				this.flags = 0U;
				break;
			default:
				this.enabled = false;
				this.traceHandle = 0UL;
				break;
			}
			return 0U;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003994 File Offset: 0x00001B94
		~TraceProvider()
		{
			UnsafeNativeMethods.UnregisterTraceGuids(this.registrationHandle);
			GC.KeepAlive(this.etwCallback);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000039D4 File Offset: 0x00001BD4
		internal int Flags
		{
			get
			{
				return (int)this.flags;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000039DC File Offset: 0x00001BDC
		internal byte Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000039E4 File Offset: 0x00001BE4
		internal bool IsEnabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000039EC File Offset: 0x00001BEC
		internal string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000039F4 File Offset: 0x00001BF4
		internal static string MakeStringArg(object arg)
		{
			if (arg != null)
			{
				return arg.ToString();
			}
			return "NULL";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003A08 File Offset: 0x00001C08
		internal static long MakeTimeStampArg(object arg)
		{
			if (arg != null)
			{
				if (arg is DateTime)
				{
					return ((DateTime)arg).ToFileTime();
				}
				try
				{
					return Convert.ToInt64(arg);
				}
				catch (InvalidCastException)
				{
					return 0L;
				}
			}
			return 0L;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A54 File Offset: 0x00001C54
		internal static int GetBufferSize(int numberOfFields, int totalParameterSize)
		{
			return sizeof(UnsafeNativeMethods.VALIST_HEADER) + (numberOfFields + 1) * sizeof(UnsafeNativeMethods.VALIST_FIELD) + totalParameterSize;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003A6C File Offset: 0x00001C6C
		internal unsafe static void InitializeTraceBuffer(void* buffer, Guid messageGuid, int messageNumber)
		{
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageGuid = messageGuid;
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageNumber = (ushort)messageNumber;
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->NumberOfFields = 0;
			UnsafeNativeMethods.VALIST_FIELD* ptr = (UnsafeNativeMethods.VALIST_FIELD*)((byte*)buffer + sizeof(UnsafeNativeMethods.VALIST_HEADER));
			ptr->DataPointer = null;
			ptr->DataLength = 0U;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003AAC File Offset: 0x00001CAC
		internal unsafe static void* InitializeTraceBuffer(void* buffer, Guid messageGuid, int messageNumber, int numberOfFields, ref int dataBufferOffset)
		{
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageGuid = messageGuid;
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageNumber = (ushort)messageNumber;
			((UnsafeNativeMethods.VALIST_HEADER*)buffer)->NumberOfFields = (ushort)numberOfFields;
			UnsafeNativeMethods.VALIST_FIELD* ptr = (UnsafeNativeMethods.VALIST_FIELD*)((byte*)buffer + sizeof(UnsafeNativeMethods.VALIST_HEADER));
			UnsafeNativeMethods.VALIST_FIELD* ptr2 = ptr + numberOfFields;
			ptr2->DataPointer = null;
			ptr2->DataLength = 0U;
			byte* ptr3 = (byte*)(ptr2 + 1);
			dataBufferOffset = (int)((long)((byte*)ptr3 - (byte*)buffer));
			return (void*)ptr;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003B0C File Offset: 0x00001D0C
		internal unsafe static void* InitializeTraceField(void* field, string value, void* buffer, ref int dataBufferOffset, char* charBuffer)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			int num = (value == null) ? 0 : (value.Length * 2);
			*(short*)ptr = (short)((ushort)num);
			dataBufferOffset += 2;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 2U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			UnsafeNativeMethods.VALIST_FIELD* ptr2 = (UnsafeNativeMethods.VALIST_FIELD*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
			ptr2->DataLength = (uint)num;
			ptr2->DataPointer = (void*)charBuffer;
			return (void*)(ptr2 + 1);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B68 File Offset: 0x00001D68
		internal unsafe static void* InitializeTraceField(void* field, byte[] value, void* buffer, ref int dataBufferOffset, byte* byteBuffer)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			int num = (value == null) ? 0 : value.Length;
			*(short*)ptr = (short)((ushort)num);
			dataBufferOffset += 2;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 2U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			UnsafeNativeMethods.VALIST_FIELD* ptr2 = (UnsafeNativeMethods.VALIST_FIELD*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
			ptr2->DataLength = (uint)num;
			ptr2->DataPointer = (void*)byteBuffer;
			return (void*)(ptr2 + 1);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal unsafe static void* InitializeTraceField(void* field, char value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(short*)ptr = (short)value;
			dataBufferOffset += 2;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 2U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003BF4 File Offset: 0x00001DF4
		internal unsafe static void* InitializeTraceField(void* field, byte value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*ptr = value;
			dataBufferOffset++;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 1U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C28 File Offset: 0x00001E28
		internal unsafe static void* InitializeTraceField(void* field, short value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(short*)ptr = value;
			dataBufferOffset += 2;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 2U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003C5C File Offset: 0x00001E5C
		internal unsafe static void* InitializeTraceField(void* field, ushort value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(short*)ptr = (short)value;
			dataBufferOffset += 2;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 2U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003C90 File Offset: 0x00001E90
		internal unsafe static void* InitializeTraceField(void* field, int value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(int*)ptr = value;
			dataBufferOffset += 4;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 4U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal unsafe static void* InitializeTraceField(void* field, uint value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(int*)ptr = (int)value;
			dataBufferOffset += 4;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 4U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003CF8 File Offset: 0x00001EF8
		internal unsafe static void* InitializeTraceField(void* field, IntPtr value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(IntPtr*)ptr = value;
			dataBufferOffset += sizeof(IntPtr);
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = (uint)sizeof(IntPtr);
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003D3C File Offset: 0x00001F3C
		internal unsafe static void* InitializeTraceField(void* field, long value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(long*)ptr = value;
			dataBufferOffset += 8;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 8U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003D70 File Offset: 0x00001F70
		internal unsafe static void* InitializeTraceField(void* field, ulong value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(long*)ptr = (long)value;
			dataBufferOffset += 8;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 8U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003DA4 File Offset: 0x00001FA4
		internal unsafe static void* InitializeTraceField(void* field, double value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(double*)ptr = value;
			dataBufferOffset += 8;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 8U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003DD8 File Offset: 0x00001FD8
		internal unsafe static void* InitializeTraceField(void* field, Guid value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(Guid*)ptr = value;
			dataBufferOffset += sizeof(Guid);
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = (uint)sizeof(Guid);
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003E1C File Offset: 0x0000201C
		internal unsafe static void* InitializeTraceField(void* field, DateTime value, void* buffer, ref int dataBufferOffset)
		{
			byte* ptr = (byte*)buffer + dataBufferOffset;
			*(long*)ptr = value.Ticks;
			dataBufferOffset += 8;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataLength = 8U;
			((UnsafeNativeMethods.VALIST_FIELD*)field)->DataPointer = (void*)ptr;
			return (void*)((byte*)field + sizeof(UnsafeNativeMethods.VALIST_FIELD));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003E58 File Offset: 0x00002058
		internal unsafe uint TraceEvent(void* buffer)
		{
			UnsafeNativeMethods.TRACE_MESSAGE_FLAGS trace_MESSAGE_FLAGS = UnsafeNativeMethods.TRACE_MESSAGE_FLAGS.TRACE_MESSAGE_GUID | UnsafeNativeMethods.TRACE_MESSAGE_FLAGS.TRACE_MESSAGE_TIMESTAMP | UnsafeNativeMethods.TRACE_MESSAGE_FLAGS.TRACE_MESSAGE_SYSTEMINFO;
			if (this.useSequenceNumbers)
			{
				trace_MESSAGE_FLAGS |= UnsafeNativeMethods.TRACE_MESSAGE_FLAGS.TRACE_MESSAGE_SEQUENCE;
			}
			return UnsafeNativeMethods.TraceMessageVa(this.traceHandle, (uint)trace_MESSAGE_FLAGS, &((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageGuid, ((UnsafeNativeMethods.VALIST_HEADER*)buffer)->MessageNumber, (void*)((byte*)buffer + sizeof(UnsafeNativeMethods.VALIST_HEADER)));
		}

		// Token: 0x0400003D RID: 61
		internal UnsafeNativeMethods.WMIDPREQUEST etwCallback;

		// Token: 0x0400003E RID: 62
		private readonly bool useSequenceNumbers;

		// Token: 0x0400003F RID: 63
		private ulong registrationHandle;

		// Token: 0x04000040 RID: 64
		private ulong traceHandle;

		// Token: 0x04000041 RID: 65
		private byte level;

		// Token: 0x04000042 RID: 66
		private uint flags;

		// Token: 0x04000043 RID: 67
		private bool enabled;

		// Token: 0x04000044 RID: 68
		private Guid controlGuid;

		// Token: 0x04000045 RID: 69
		private readonly string applicationName;

		// Token: 0x04000046 RID: 70
		private TraceProvider.GetTraceLoggerHandle getTraceLoggerHandle;

		// Token: 0x04000047 RID: 71
		private TraceProvider.GetTraceEnableFlags getTraceEnableFlags;

		// Token: 0x04000048 RID: 72
		private TraceProvider.GetTraceEnableLevel getTraceEnableLevel;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000054 RID: 84
		private unsafe delegate ulong GetTraceLoggerHandle(UnsafeNativeMethods.WNODE_HEADER* Buffer);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000058 RID: 88
		private delegate uint GetTraceEnableFlags(ulong TraceHandle);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x0600005C RID: 92
		private delegate byte GetTraceEnableLevel(ulong TraceHandle);

		// Token: 0x02000012 RID: 18
		internal enum TraceLevel : byte
		{
			// Token: 0x0400004A RID: 74
			TRACE_LEVEL_NOISE = 6,
			// Token: 0x0400004B RID: 75
			TRACE_LEVEL_INFORMATION = 4,
			// Token: 0x0400004C RID: 76
			TRACE_LEVEL_FATAL = 1,
			// Token: 0x0400004D RID: 77
			TRACE_LEVEL_ERROR,
			// Token: 0x0400004E RID: 78
			TRACE_LEVEL_WARNING,
			// Token: 0x0400004F RID: 79
			TRACE_LEVEL_VERBOSE = 5
		}
	}
}

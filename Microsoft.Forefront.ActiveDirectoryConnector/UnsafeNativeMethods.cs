using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft
{
	// Token: 0x02000013 RID: 19
	[GeneratedCode("ManagedWPP", "1.0.0.0")]
	internal sealed class UnsafeNativeMethods
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003E97 File Offset: 0x00002097
		internal UnsafeNativeMethods()
		{
		}

		// Token: 0x06000060 RID: 96
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW")]
		internal unsafe static extern uint RegisterTraceGuids([In] UnsafeNativeMethods.WMIDPREQUEST RequestAddress, [In] void* RequestContext, [In] ref Guid ControlGuid, [In] uint GuidCount, ref UnsafeNativeMethods.TRACE_GUID_REGISTRATION TraceGuidReg, [In] string MofImagePath, [In] string MofResourceName, out ulong RegistrationHandle);

		// Token: 0x06000061 RID: 97
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal static extern int UnregisterTraceGuids(ulong RegistrationHandle);

		// Token: 0x06000062 RID: 98
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal unsafe static extern uint TraceEvent(ulong TraceHandle, UnsafeNativeMethods.EVENT_TRACE_BUFFER* EventTrace);

		// Token: 0x06000063 RID: 99
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal unsafe static extern uint TraceMessageVa(ulong TraceHandle, uint MessageFlags, Guid* MessageGuid, ushort MessageNumber, void* MessageArgList);

		// Token: 0x06000064 RID: 100
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetModuleHandleW")]
		internal static extern IntPtr GetModuleHandle([In] string lpModuleName);

		// Token: 0x06000065 RID: 101
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		internal static extern IntPtr GetProcAddress([In] IntPtr hModule, [In] string lpProcName);

		// Token: 0x04000050 RID: 80
		internal const int TRACE_MESSAGE_MAXIMUM_SIZE = 8192;

		// Token: 0x04000051 RID: 81
		internal const uint ERROR_INVALID_PARAMETER = 87U;

		// Token: 0x02000014 RID: 20
		internal struct VALIST_FIELD
		{
			// Token: 0x04000052 RID: 82
			internal unsafe void* DataPointer;

			// Token: 0x04000053 RID: 83
			internal uint DataLength;
		}

		// Token: 0x02000015 RID: 21
		[StructLayout(LayoutKind.Explicit, Size = 20)]
		internal struct VALIST_HEADER
		{
			// Token: 0x04000054 RID: 84
			[FieldOffset(0)]
			internal Guid MessageGuid;

			// Token: 0x04000055 RID: 85
			[FieldOffset(16)]
			internal ushort MessageNumber;

			// Token: 0x04000056 RID: 86
			[FieldOffset(18)]
			internal ushort NumberOfFields;
		}

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		internal struct MOF_FIELD
		{
			// Token: 0x04000057 RID: 87
			[FieldOffset(0)]
			internal ulong ZeroInit;

			// Token: 0x04000058 RID: 88
			[FieldOffset(0)]
			internal unsafe void* DataPointer;

			// Token: 0x04000059 RID: 89
			[FieldOffset(8)]
			internal uint DataLength;

			// Token: 0x0400005A RID: 90
			[FieldOffset(12)]
			internal uint DataType;
		}

		// Token: 0x02000017 RID: 23
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		internal struct WNODE_HEADER
		{
			// Token: 0x0400005B RID: 91
			[FieldOffset(0)]
			internal uint BufferSize;

			// Token: 0x0400005C RID: 92
			[FieldOffset(4)]
			internal uint ProviderId;

			// Token: 0x0400005D RID: 93
			[FieldOffset(8)]
			internal IntPtr HistoricalContext;

			// Token: 0x0400005E RID: 94
			[FieldOffset(16)]
			internal long TimeStamp;

			// Token: 0x0400005F RID: 95
			[FieldOffset(24)]
			internal Guid Guid;

			// Token: 0x04000060 RID: 96
			[FieldOffset(40)]
			internal uint ClientContext;

			// Token: 0x04000061 RID: 97
			[FieldOffset(44)]
			internal uint Flags;
		}

		// Token: 0x02000018 RID: 24
		internal struct TRACE_GUID_REGISTRATION
		{
			// Token: 0x04000062 RID: 98
			internal unsafe Guid* Guid;

			// Token: 0x04000063 RID: 99
			internal IntPtr RegHandle;
		}

		// Token: 0x02000019 RID: 25
		[StructLayout(LayoutKind.Explicit, Size = 336)]
		internal struct EVENT_TRACE_BUFFER
		{
			// Token: 0x04000064 RID: 100
			[FieldOffset(0)]
			internal uint BufferSize;

			// Token: 0x04000065 RID: 101
			[FieldOffset(4)]
			internal uint ProviderId;

			// Token: 0x04000066 RID: 102
			[FieldOffset(8)]
			internal uint ThreadId;

			// Token: 0x04000067 RID: 103
			[FieldOffset(12)]
			internal uint ProcessId;

			// Token: 0x04000068 RID: 104
			[FieldOffset(16)]
			internal long TimeStamp;

			// Token: 0x04000069 RID: 105
			[FieldOffset(24)]
			internal Guid Guid;

			// Token: 0x0400006A RID: 106
			[FieldOffset(40)]
			internal uint ClientContext;

			// Token: 0x0400006B RID: 107
			[FieldOffset(44)]
			internal uint Flags;

			// Token: 0x0400006C RID: 108
			[FieldOffset(48)]
			internal UnsafeNativeMethods.MOF_FIELD UserData;
		}

		// Token: 0x0200001A RID: 26
		internal enum WNODE_FLAGS : uint
		{
			// Token: 0x0400006E RID: 110
			WNODE_FLAG_TRACED_GUID = 131072U,
			// Token: 0x0400006F RID: 111
			WNODE_FLAG_USE_GUID_PTR = 524288U,
			// Token: 0x04000070 RID: 112
			WNODE_FLAG_USE_MOF_PTR = 1048576U
		}

		// Token: 0x0200001B RID: 27
		[Flags]
		internal enum TRACE_MESSAGE_FLAGS : uint
		{
			// Token: 0x04000072 RID: 114
			TRACE_MESSAGE_SEQUENCE = 1U,
			// Token: 0x04000073 RID: 115
			TRACE_MESSAGE_GUID = 2U,
			// Token: 0x04000074 RID: 116
			TRACE_MESSAGE_COMPONENTID = 4U,
			// Token: 0x04000075 RID: 117
			TRACE_MESSAGE_TIMESTAMP = 8U,
			// Token: 0x04000076 RID: 118
			TRACE_MESSAGE_PERFORMANCE_TIMESTAMP = 16U,
			// Token: 0x04000077 RID: 119
			TRACE_MESSAGE_SYSTEMINFO = 32U
		}

		// Token: 0x0200001C RID: 28
		internal enum WMIDPREQUESTCODE : uint
		{
			// Token: 0x04000079 RID: 121
			WMI_ENABLE_EVENTS = 4U,
			// Token: 0x0400007A RID: 122
			WMI_DISABLE_EVENTS
		}

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x06000067 RID: 103
		internal unsafe delegate uint WMIDPREQUEST(UnsafeNativeMethods.WMIDPREQUESTCODE RequestCode, IntPtr RequestContext, uint* BufferSize, byte* Buffer);
	}
}

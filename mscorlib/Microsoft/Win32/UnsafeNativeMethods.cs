using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000018 RID: 24
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600014F RID: 335
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int GetTimeZoneInformation(out Win32Native.TimeZoneInformation lpTimeZoneInformation);

		// Token: 0x06000150 RID: 336
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int GetDynamicTimeZoneInformation(out Win32Native.DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

		// Token: 0x06000151 RID: 337
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetFileMUIPath(int flags, [MarshalAs(UnmanagedType.LPWStr)] string filePath, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder language, ref int languageLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileMuiPath, ref int fileMuiPathLength, ref long enumerator);

		// Token: 0x06000152 RID: 338
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "LoadStringW", ExactSpelling = true, SetLastError = true)]
		internal static extern int LoadString(SafeLibraryHandle handle, int id, StringBuilder buffer, int bufferLength);

		// Token: 0x06000153 RID: 339
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x06000154 RID: 340
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000155 RID: 341
		[SecurityCritical]
		[DllImport("combase.dll")]
		internal static extern int RoGetActivationFactory([MarshalAs(UnmanagedType.HString)] string activatableClassId, [In] ref Guid iid, [MarshalAs(UnmanagedType.IInspectable)] out object factory);

		// Token: 0x02000A91 RID: 2705
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static class ManifestEtw
		{
			// Token: 0x06006876 RID: 26742
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

			// Token: 0x06006877 RID: 26743
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern uint EventUnregister([In] long registrationHandle);

			// Token: 0x06006878 RID: 26744
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x06006879 RID: 26745
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventWriteString([In] long registrationHandle, [In] byte level, [In] long keyword, [In] string msg);

			// Token: 0x0600687A RID: 26746 RVA: 0x00166DC0 File Offset: 0x00164FC0
			internal unsafe static int EventWriteTransferWrapper(long registrationHandle, ref EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
			{
				int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
				if (num == 87 && relatedActivityId == null)
				{
					Guid empty = Guid.Empty;
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, &empty, userDataCount, userData);
				}
				return num;
			}

			// Token: 0x0600687B RID: 26747
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			private unsafe static extern int EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x0600687C RID: 26748
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventActivityIdControl([In] UnsafeNativeMethods.ManifestEtw.ActivityControl ControlCode, [In] [Out] ref Guid ActivityId);

			// Token: 0x0600687D RID: 26749
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventSetInformation([In] long registrationHandle, [In] UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS informationClass, [In] void* eventInformation, [In] int informationLength);

			// Token: 0x0600687E RID: 26750
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS TraceQueryInfoClass, void* InBuffer, int InBufferSize, void* OutBuffer, int OutBufferSize, ref int ReturnLength);

			// Token: 0x04002FE4 RID: 12260
			internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

			// Token: 0x04002FE5 RID: 12261
			internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

			// Token: 0x04002FE6 RID: 12262
			internal const int ERROR_MORE_DATA = 234;

			// Token: 0x04002FE7 RID: 12263
			internal const int ERROR_NOT_SUPPORTED = 50;

			// Token: 0x04002FE8 RID: 12264
			internal const int ERROR_INVALID_PARAMETER = 87;

			// Token: 0x04002FE9 RID: 12265
			internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;

			// Token: 0x04002FEA RID: 12266
			internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;

			// Token: 0x04002FEB RID: 12267
			internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

			// Token: 0x02000CBB RID: 3259
			// (Invoke) Token: 0x060070A0 RID: 28832
			[SecurityCritical]
			internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext);

			// Token: 0x02000CBC RID: 3260
			internal struct EVENT_FILTER_DESCRIPTOR
			{
				// Token: 0x04003809 RID: 14345
				public long Ptr;

				// Token: 0x0400380A RID: 14346
				public int Size;

				// Token: 0x0400380B RID: 14347
				public int Type;
			}

			// Token: 0x02000CBD RID: 3261
			internal enum ActivityControl : uint
			{
				// Token: 0x0400380D RID: 14349
				EVENT_ACTIVITY_CTRL_GET_ID = 1U,
				// Token: 0x0400380E RID: 14350
				EVENT_ACTIVITY_CTRL_SET_ID,
				// Token: 0x0400380F RID: 14351
				EVENT_ACTIVITY_CTRL_CREATE_ID,
				// Token: 0x04003810 RID: 14352
				EVENT_ACTIVITY_CTRL_GET_SET_ID,
				// Token: 0x04003811 RID: 14353
				EVENT_ACTIVITY_CTRL_CREATE_SET_ID
			}

			// Token: 0x02000CBE RID: 3262
			internal enum EVENT_INFO_CLASS
			{
				// Token: 0x04003813 RID: 14355
				BinaryTrackInfo,
				// Token: 0x04003814 RID: 14356
				SetEnableAllKeywords,
				// Token: 0x04003815 RID: 14357
				SetTraits
			}

			// Token: 0x02000CBF RID: 3263
			internal enum TRACE_QUERY_INFO_CLASS
			{
				// Token: 0x04003817 RID: 14359
				TraceGuidQueryList,
				// Token: 0x04003818 RID: 14360
				TraceGuidQueryInfo,
				// Token: 0x04003819 RID: 14361
				TraceGuidQueryProcess,
				// Token: 0x0400381A RID: 14362
				TraceStackTracingInfo,
				// Token: 0x0400381B RID: 14363
				MaxTraceSetInfoClass
			}

			// Token: 0x02000CC0 RID: 3264
			internal struct TRACE_GUID_INFO
			{
				// Token: 0x0400381C RID: 14364
				public int InstanceCount;

				// Token: 0x0400381D RID: 14365
				public int Reserved;
			}

			// Token: 0x02000CC1 RID: 3265
			internal struct TRACE_PROVIDER_INSTANCE_INFO
			{
				// Token: 0x0400381E RID: 14366
				public int NextOffset;

				// Token: 0x0400381F RID: 14367
				public int EnableCount;

				// Token: 0x04003820 RID: 14368
				public int Pid;

				// Token: 0x04003821 RID: 14369
				public int Flags;
			}

			// Token: 0x02000CC2 RID: 3266
			internal struct TRACE_ENABLE_INFO
			{
				// Token: 0x04003822 RID: 14370
				public int IsEnabled;

				// Token: 0x04003823 RID: 14371
				public byte Level;

				// Token: 0x04003824 RID: 14372
				public byte Reserved1;

				// Token: 0x04003825 RID: 14373
				public ushort LoggerId;

				// Token: 0x04003826 RID: 14374
				public int EnableProperty;

				// Token: 0x04003827 RID: 14375
				public int Reserved2;

				// Token: 0x04003828 RID: 14376
				public long MatchAnyKeyword;

				// Token: 0x04003829 RID: 14377
				public long MatchAllKeyword;
			}
		}
	}
}

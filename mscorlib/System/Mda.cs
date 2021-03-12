using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200010C RID: 268
	internal static class Mda
	{
		// Token: 0x0600104B RID: 4171
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportStreamWriterBufferedDataLost(string text);

		// Token: 0x0600104C RID: 4172
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostEnabled();

		// Token: 0x0600104D RID: 4173
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostCaptureAllocatedCallStack();

		// Token: 0x0600104E RID: 4174
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MemberInfoCacheCreation();

		// Token: 0x0600104F RID: 4175
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DateTimeInvalidLocalFormat();

		// Token: 0x06001050 RID: 4176
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInvalidGCHandleCookieProbeEnabled();

		// Token: 0x06001051 RID: 4177
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FireInvalidGCHandleCookieProbe(IntPtr cookie);

		// Token: 0x06001052 RID: 4178
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportErrorSafeHandleRelease(Exception ex);

		// Token: 0x02000AC3 RID: 2755
		internal static class StreamWriterBufferedDataLost
		{
			// Token: 0x170011D8 RID: 4568
			// (get) Token: 0x060068F3 RID: 26867 RVA: 0x001688AE File Offset: 0x00166AAE
			internal static bool Enabled
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._enabledState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostEnabled())
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._enabledState == 1;
				}
			}

			// Token: 0x170011D9 RID: 4569
			// (get) Token: 0x060068F4 RID: 26868 RVA: 0x001688DC File Offset: 0x00166ADC
			internal static bool CaptureAllocatedCallStack
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostCaptureAllocatedCallStack())
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 1;
				}
			}

			// Token: 0x060068F5 RID: 26869 RVA: 0x0016890A File Offset: 0x00166B0A
			[SecuritySafeCritical]
			internal static void ReportError(string text)
			{
				Mda.ReportStreamWriterBufferedDataLost(text);
			}

			// Token: 0x040030D7 RID: 12503
			private static volatile int _enabledState;

			// Token: 0x040030D8 RID: 12504
			private static volatile int _captureAllocatedCallStackState;
		}
	}
}

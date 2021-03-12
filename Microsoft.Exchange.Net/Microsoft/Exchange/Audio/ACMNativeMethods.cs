using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000607 RID: 1543
	internal class ACMNativeMethods
	{
		// Token: 0x06001BA8 RID: 7080
		[DllImport("winmm.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr OpenDriver([In] string szDriverName, [In] string szSectionName, [In] int lParam2);

		// Token: 0x06001BA9 RID: 7081
		[DllImport("winmm.dll", SetLastError = true)]
		internal static extern IntPtr GetDriverModuleHandle([In] IntPtr hdrvr);

		// Token: 0x06001BAA RID: 7082
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetProcAddress([In] IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x06001BAB RID: 7083
		[DllImport("msacm32.dll", EntryPoint = "acmDriverAdd")]
		internal static extern int AcmDriverAdd(out IntPtr phadid, [In] IntPtr hModule, [In] IntPtr lParam, [In] int dwPriority, [In] int fdwAdd);

		// Token: 0x06001BAC RID: 7084
		[DllImport("msacm32.dll", EntryPoint = "acmDriverOpen")]
		internal static extern int AcmDriverOpen(out ACMNativeMethods.AcmInstanceHandle phad, [In] IntPtr hadid, [In] int fdwOpen);

		// Token: 0x06001BAD RID: 7085
		[DllImport("msacm32.dll", EntryPoint = "acmDriverClose")]
		internal static extern int AcmDriverClose([In] IntPtr phad, [In] int fdwClose);

		// Token: 0x06001BAE RID: 7086
		[DllImport("msacm32.dll", EntryPoint = "acmStreamOpen")]
		internal static extern int AcmStreamOpen(out ACMNativeMethods.SafeStreamHandle phStream, ACMNativeMethods.AcmInstanceHandle hStream, WaveFormat pwfxSrc, WaveFormat pwfxDst, IntPtr pwfltr, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

		// Token: 0x06001BAF RID: 7087
		[DllImport("msacm32.dll", EntryPoint = "acmStreamSize")]
		internal static extern int AcmStreamSize(ACMNativeMethods.SafeStreamHandle hStream, int cbInput, out int cbOutput, int fdwSize);

		// Token: 0x06001BB0 RID: 7088
		[DllImport("msacm32.dll", EntryPoint = "acmStreamPrepareHeader")]
		internal static extern int AcmStreamPrepareHeader(ACMNativeMethods.SafeStreamHandle hStream, [In] [Out] ref ACMNativeMethods.ACMSTREAMHEADER streamHeader, int fdwPrepare);

		// Token: 0x06001BB1 RID: 7089
		[DllImport("msacm32.dll", EntryPoint = "acmStreamConvert")]
		internal static extern int AcmStreamConvert(ACMNativeMethods.SafeStreamHandle hStream, [In] [Out] ref ACMNativeMethods.ACMSTREAMHEADER streamHeader, int fdwConvert);

		// Token: 0x06001BB2 RID: 7090
		[DllImport("msacm32.dll", EntryPoint = "acmStreamUnprepareHeader")]
		internal static extern int AcmStreamUnprepareHeader(ACMNativeMethods.SafeStreamHandle hStream, [In] [Out] ref ACMNativeMethods.ACMSTREAMHEADER pash, int fdwUnprepare);

		// Token: 0x06001BB3 RID: 7091
		[DllImport("msacm32.dll", EntryPoint = "acmStreamClose")]
		internal static extern int AcmStreamClose(IntPtr hStream, int fdwClose);

		// Token: 0x04001C96 RID: 7318
		internal const int ACM_STREAMSIZEF_SOURCE = 0;

		// Token: 0x04001C97 RID: 7319
		internal const int ACM_STREAMSIZEF_DESTINATION = 1;

		// Token: 0x04001C98 RID: 7320
		internal const int ACM_STREAMOPENF_QUERY = 1;

		// Token: 0x04001C99 RID: 7321
		internal const int ACM_STREAMOPENF_ASYNC = 2;

		// Token: 0x04001C9A RID: 7322
		internal const int ACM_STREAMOPENF_NONREALTIME = 4;

		// Token: 0x04001C9B RID: 7323
		internal const int ACM_STREAMCONVERTF_BLOCKALIGN = 4;

		// Token: 0x04001C9C RID: 7324
		internal const int ACM_STREAMCONVERTF_END = 32;

		// Token: 0x04001C9D RID: 7325
		internal const int ACM_STREAMCONVERTF_START = 16;

		// Token: 0x04001C9E RID: 7326
		internal const int ACMSTREAMHEADER_STATUSF_PREPARED = 131072;

		// Token: 0x04001C9F RID: 7327
		internal const int WAVE_FORMAT_GSM610 = 49;

		// Token: 0x04001CA0 RID: 7328
		internal const int WAVE_FORMAT_PCM = 1;

		// Token: 0x04001CA1 RID: 7329
		internal const string MP3CODEC = "l3codecp.acm";

		// Token: 0x04001CA2 RID: 7330
		internal const string DRIVER_ENTRY_POINT = "DriverProc";

		// Token: 0x04001CA3 RID: 7331
		private const string MSACM32 = "msacm32.dll";

		// Token: 0x04001CA4 RID: 7332
		private const string WINMM = "winmm.dll";

		// Token: 0x04001CA5 RID: 7333
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x02000608 RID: 1544
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct ACMSTREAMHEADER
		{
			// Token: 0x06001BB5 RID: 7093 RVA: 0x000321D0 File Offset: 0x000303D0
			public void Zero()
			{
				this.cbStruct = 0;
				this.fdwStatus = 0;
				this.dwUser = IntPtr.Zero;
				this.pbSrc = IntPtr.Zero;
				this.cbSrcLength = 0;
				this.cbSrcLengthUsed = 0;
				this.dwSrcUser = IntPtr.Zero;
				this.pbDst = IntPtr.Zero;
				this.cbDstLength = 0;
				this.cbDstLengthUsed = 0;
				this.dwDstUser = IntPtr.Zero;
			}

			// Token: 0x04001CA6 RID: 7334
			public int cbStruct;

			// Token: 0x04001CA7 RID: 7335
			public int fdwStatus;

			// Token: 0x04001CA8 RID: 7336
			public IntPtr dwUser;

			// Token: 0x04001CA9 RID: 7337
			public IntPtr pbSrc;

			// Token: 0x04001CAA RID: 7338
			public int cbSrcLength;

			// Token: 0x04001CAB RID: 7339
			public int cbSrcLengthUsed;

			// Token: 0x04001CAC RID: 7340
			public IntPtr dwSrcUser;

			// Token: 0x04001CAD RID: 7341
			public IntPtr pbDst;

			// Token: 0x04001CAE RID: 7342
			public int cbDstLength;

			// Token: 0x04001CAF RID: 7343
			public int cbDstLengthUsed;

			// Token: 0x04001CB0 RID: 7344
			public IntPtr dwDstUser;

			// Token: 0x04001CB1 RID: 7345
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
			public int[] dwReservedDriver;
		}

		// Token: 0x02000609 RID: 1545
		internal sealed class SafeStreamHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06001BB6 RID: 7094 RVA: 0x0003223E File Offset: 0x0003043E
			internal SafeStreamHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
			{
				base.SetHandle(preexistingHandle);
			}

			// Token: 0x06001BB7 RID: 7095 RVA: 0x0003224E File Offset: 0x0003044E
			private SafeStreamHandle() : base(true)
			{
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x00032257 File Offset: 0x00030457
			protected override bool ReleaseHandle()
			{
				return 0 == ACMNativeMethods.AcmStreamClose(this.handle, 0);
			}
		}

		// Token: 0x0200060A RID: 1546
		internal sealed class AcmInstanceHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06001BB9 RID: 7097 RVA: 0x00032268 File Offset: 0x00030468
			internal AcmInstanceHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
			{
				base.SetHandle(preexistingHandle);
			}

			// Token: 0x06001BBA RID: 7098 RVA: 0x00032278 File Offset: 0x00030478
			private AcmInstanceHandle() : base(true)
			{
			}

			// Token: 0x06001BBB RID: 7099 RVA: 0x00032281 File Offset: 0x00030481
			protected override bool ReleaseHandle()
			{
				return 0 == ACMNativeMethods.AcmDriverClose(this.handle, 0);
			}
		}
	}
}

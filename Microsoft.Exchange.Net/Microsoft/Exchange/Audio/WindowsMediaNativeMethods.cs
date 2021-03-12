using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000631 RID: 1585
	internal static class WindowsMediaNativeMethods
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00033F64 File Offset: 0x00032164
		internal static IWMSyncReader CreateSyncReader()
		{
			IWMSyncReader result = null;
			Marshal.ThrowExceptionForHR(WindowsMediaNativeMethods.WMCreateSyncReader(IntPtr.Zero, 0U, out result));
			return result;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00033F88 File Offset: 0x00032188
		internal static IWMWriter CreateWriter()
		{
			IWMWriter result = null;
			Marshal.ThrowExceptionForHR(WindowsMediaNativeMethods.WMCreateWriter(IntPtr.Zero, out result));
			return result;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00033FAC File Offset: 0x000321AC
		internal static IWMProfileManager CreateProfileManager()
		{
			IWMProfileManager result = null;
			Marshal.ThrowExceptionForHR(WindowsMediaNativeMethods.WMCreateProfileManager(out result));
			return result;
		}

		// Token: 0x06001CD2 RID: 7378
		[DllImport("WMVCore.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern int WMCreateSyncReader(IntPtr pUnkCert, uint dwRights, [MarshalAs(UnmanagedType.Interface)] out IWMSyncReader ppSyncReader);

		// Token: 0x06001CD3 RID: 7379
		[DllImport("WMVCore.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern int WMCreateWriter(IntPtr pUnkReserved, [MarshalAs(UnmanagedType.Interface)] out IWMWriter ppWriter);

		// Token: 0x06001CD4 RID: 7380
		[DllImport("WMVCore.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern int WMCreateProfileManager([MarshalAs(UnmanagedType.Interface)] out IWMProfileManager ppProfileManager);

		// Token: 0x04001D10 RID: 7440
		private const string WMVCoreDll = "WMVCore.dll";

		// Token: 0x04001D11 RID: 7441
		private const CallingConvention WMCallingConvention = CallingConvention.StdCall;

		// Token: 0x04001D12 RID: 7442
		private const CharSet WMCharSet = CharSet.Unicode;

		// Token: 0x02000632 RID: 1586
		internal enum WMT_VERSION
		{
			// Token: 0x04001D14 RID: 7444
			WMT_VER_4_0 = 262144,
			// Token: 0x04001D15 RID: 7445
			WMT_VER_7_0 = 458752,
			// Token: 0x04001D16 RID: 7446
			WMT_VER_8_0 = 524288,
			// Token: 0x04001D17 RID: 7447
			WMT_VER_9_0 = 589824
		}

		// Token: 0x02000633 RID: 1587
		internal enum WMT_ATTR_DATATYPE
		{
			// Token: 0x04001D19 RID: 7449
			WMT_TYPE_DWORD,
			// Token: 0x04001D1A RID: 7450
			WMT_TYPE_STRING,
			// Token: 0x04001D1B RID: 7451
			WMT_TYPE_BINARY,
			// Token: 0x04001D1C RID: 7452
			WMT_TYPE_BOOL,
			// Token: 0x04001D1D RID: 7453
			WMT_TYPE_QWORD,
			// Token: 0x04001D1E RID: 7454
			WMT_TYPE_WORD,
			// Token: 0x04001D1F RID: 7455
			WMT_TYPE_GUID
		}

		// Token: 0x02000634 RID: 1588
		internal struct WM_MEDIA_TYPE
		{
			// Token: 0x04001D20 RID: 7456
			internal Guid majortype;

			// Token: 0x04001D21 RID: 7457
			internal Guid subtype;

			// Token: 0x04001D22 RID: 7458
			[MarshalAs(UnmanagedType.Bool)]
			internal bool bFixedSizeSamples;

			// Token: 0x04001D23 RID: 7459
			[MarshalAs(UnmanagedType.Bool)]
			internal bool bTemporalCompression;

			// Token: 0x04001D24 RID: 7460
			internal uint lSampleSize;

			// Token: 0x04001D25 RID: 7461
			internal Guid formattype;

			// Token: 0x04001D26 RID: 7462
			internal IntPtr pUnk;

			// Token: 0x04001D27 RID: 7463
			internal uint cbFormat;

			// Token: 0x04001D28 RID: 7464
			internal IntPtr pbFormat;
		}

		// Token: 0x02000635 RID: 1589
		internal static class MediaTypes
		{
			// Token: 0x170007E2 RID: 2018
			// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00033FC8 File Offset: 0x000321C8
			internal static Guid WMFORMAT_WaveFormatEx
			{
				get
				{
					return new Guid("05589F81-C356-11CE-BF01-00AA0055595A");
				}
			}

			// Token: 0x170007E3 RID: 2019
			// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00033FD4 File Offset: 0x000321D4
			internal static Guid WMMEDIASUBTYPE_PCM
			{
				get
				{
					return new Guid("00000001-0000-0010-8000-00AA00389B71");
				}
			}

			// Token: 0x170007E4 RID: 2020
			// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00033FE0 File Offset: 0x000321E0
			internal static Guid WMMEDIATYPE_Audio
			{
				get
				{
					return new Guid("73647561-0000-0010-8000-00AA00389B71");
				}
			}
		}
	}
}

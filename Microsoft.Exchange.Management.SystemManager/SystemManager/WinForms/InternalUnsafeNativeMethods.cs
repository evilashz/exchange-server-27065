using System;
using System.Runtime.InteropServices;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000212 RID: 530
	internal static class InternalUnsafeNativeMethods
	{
		// Token: 0x060017FC RID: 6140
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, [In] [Out] ref InternalNativeMethods.HDITEM lparam);

		// Token: 0x040008F5 RID: 2293
		public const string User32 = "user32.dll";

		// Token: 0x040008F6 RID: 2294
		public const string Shell32Dll = "shell32.dll";

		// Token: 0x02000213 RID: 531
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class BROWSEINFO
		{
			// Token: 0x040008F7 RID: 2295
			public IntPtr hwndOwner;

			// Token: 0x040008F8 RID: 2296
			public IntPtr pidlRoot;

			// Token: 0x040008F9 RID: 2297
			public IntPtr pszDisplayName;

			// Token: 0x040008FA RID: 2298
			public string lpszTitle;

			// Token: 0x040008FB RID: 2299
			public int ulFlags;

			// Token: 0x040008FC RID: 2300
			public InternalUnsafeNativeMethods.BrowseCallbackProc lpfn;

			// Token: 0x040008FD RID: 2301
			public IntPtr lParam;

			// Token: 0x040008FE RID: 2302
			public int iImage;
		}

		// Token: 0x02000214 RID: 532
		public class Shell32
		{
			// Token: 0x060017FE RID: 6142
			[DllImport("shell32.dll")]
			public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);

			// Token: 0x060017FF RID: 6143
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

			// Token: 0x06001800 RID: 6144
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			public static extern IntPtr SHBrowseForFolder([In] InternalUnsafeNativeMethods.BROWSEINFO lpbi);

			// Token: 0x06001801 RID: 6145
			[DllImport("shell32.dll")]
			public static extern int SHGetMalloc([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IMalloc[] ppMalloc);
		}

		// Token: 0x02000215 RID: 533
		// (Invoke) Token: 0x06001804 RID: 6148
		public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);
	}
}

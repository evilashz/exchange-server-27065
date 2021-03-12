using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CEB RID: 3307
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class NativeMethods
	{
		// Token: 0x06007F28 RID: 32552
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

		// Token: 0x06007F29 RID: 32553
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int FormatMessage(uint dwFlags, SafeLibraryHandle lpSource, uint dwMessageId, uint dwLanguageId, out StringBuilder lpBuffer, uint nSize, string[] arguments);

		// Token: 0x04003E4D RID: 15949
		private const string Kernel32 = "kernel32.dll";

		// Token: 0x04003E4E RID: 15950
		internal const uint LOAD_LIBRARY_AS_DATAFILE = 2U;

		// Token: 0x04003E4F RID: 15951
		internal const uint LOAD_LIBRARY_AS_IMAGE_RESOURCE = 32U;

		// Token: 0x04003E50 RID: 15952
		internal const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256U;

		// Token: 0x04003E51 RID: 15953
		internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

		// Token: 0x04003E52 RID: 15954
		internal const uint FORMAT_MESSAGE_FROM_HMODULE = 2048U;

		// Token: 0x04003E53 RID: 15955
		internal const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192U;

		// Token: 0x04003E54 RID: 15956
		internal const uint ERROR_RESOURCE_LANG_NOT_FOUND = 1815U;
	}
}

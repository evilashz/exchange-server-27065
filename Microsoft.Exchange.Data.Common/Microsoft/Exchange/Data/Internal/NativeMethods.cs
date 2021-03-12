using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000133 RID: 307
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal class NativeMethods
	{
		// Token: 0x06000BFD RID: 3069
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", SetLastError = true)]
		internal static extern SafeFileHandle CreateFile([In] string filename, [In] uint accessMode, [In] uint shareMode, ref NativeMethods.SecurityAttributes securityAttributes, [In] uint creationDisposition, [In] uint flags, [In] IntPtr templateFileHandle);

		// Token: 0x04000EA7 RID: 3751
		internal const uint DELETE = 65536U;

		// Token: 0x04000EA8 RID: 3752
		internal const uint READ_CONTROL = 131072U;

		// Token: 0x04000EA9 RID: 3753
		internal const uint WRITE_DAC = 262144U;

		// Token: 0x04000EAA RID: 3754
		internal const uint WRITE_OWNER = 524288U;

		// Token: 0x04000EAB RID: 3755
		internal const uint SYNCHRONIZE = 1048576U;

		// Token: 0x04000EAC RID: 3756
		internal const uint STANDARD_RIGHTS_REQUIRED = 983040U;

		// Token: 0x04000EAD RID: 3757
		internal const uint STANDARD_RIGHTS_READ = 131072U;

		// Token: 0x04000EAE RID: 3758
		internal const uint STANDARD_RIGHTS_WRITE = 131072U;

		// Token: 0x04000EAF RID: 3759
		internal const uint STANDARD_RIGHTS_EXECUTE = 131072U;

		// Token: 0x04000EB0 RID: 3760
		internal const uint STANDARD_RIGHTS_ALL = 2031616U;

		// Token: 0x04000EB1 RID: 3761
		internal const uint SPECIFIC_RIGHTS_ALL = 65535U;

		// Token: 0x04000EB2 RID: 3762
		internal const uint FILE_SHARE_READ = 1U;

		// Token: 0x04000EB3 RID: 3763
		internal const uint FILE_SHARE_WRITE = 2U;

		// Token: 0x04000EB4 RID: 3764
		internal const uint FILE_SHARE_DELETE = 4U;

		// Token: 0x04000EB5 RID: 3765
		internal const uint FILE_READ_DATA = 1U;

		// Token: 0x04000EB6 RID: 3766
		internal const uint FILE_LIST_DIRECTORY = 1U;

		// Token: 0x04000EB7 RID: 3767
		internal const uint FILE_WRITE_DATA = 2U;

		// Token: 0x04000EB8 RID: 3768
		internal const uint FILE_ADD_FILE = 2U;

		// Token: 0x04000EB9 RID: 3769
		internal const uint FILE_APPEND_DATA = 4U;

		// Token: 0x04000EBA RID: 3770
		internal const uint FILE_ADD_SUBDIRECTORY = 4U;

		// Token: 0x04000EBB RID: 3771
		internal const uint FILE_CREATE_PIPE_INSTANCE = 4U;

		// Token: 0x04000EBC RID: 3772
		internal const uint FILE_READ_EA = 8U;

		// Token: 0x04000EBD RID: 3773
		internal const uint FILE_WRITE_EA = 16U;

		// Token: 0x04000EBE RID: 3774
		internal const uint FILE_EXECUTE = 32U;

		// Token: 0x04000EBF RID: 3775
		internal const uint FILE_TRAVERSE = 32U;

		// Token: 0x04000EC0 RID: 3776
		internal const uint FILE_DELETE_CHILD = 64U;

		// Token: 0x04000EC1 RID: 3777
		internal const uint FILE_READ_ATTRIBUTES = 128U;

		// Token: 0x04000EC2 RID: 3778
		internal const uint FILE_WRITE_ATTRIBUTES = 256U;

		// Token: 0x04000EC3 RID: 3779
		internal const uint FILE_ALL_ACCESS = 2032127U;

		// Token: 0x04000EC4 RID: 3780
		internal const uint FILE_GENERIC_READ = 1179785U;

		// Token: 0x04000EC5 RID: 3781
		internal const uint FILE_GENERIC_WRITE = 1179926U;

		// Token: 0x04000EC6 RID: 3782
		internal const uint FILE_GENERIC_EXECUTE = 1179808U;

		// Token: 0x04000EC7 RID: 3783
		internal const uint CREATE_NEW = 1U;

		// Token: 0x04000EC8 RID: 3784
		internal const uint CREATE_ALWAYS = 2U;

		// Token: 0x04000EC9 RID: 3785
		internal const uint OPEN_EXISTING = 3U;

		// Token: 0x04000ECA RID: 3786
		internal const uint OPEN_ALWAYS = 4U;

		// Token: 0x04000ECB RID: 3787
		internal const uint TRUNCATE_EXISTING = 5U;

		// Token: 0x04000ECC RID: 3788
		internal const uint FILE_ATTRIBUTE_READONLY = 1U;

		// Token: 0x04000ECD RID: 3789
		internal const uint FILE_ATTRIBUTE_HIDDEN = 2U;

		// Token: 0x04000ECE RID: 3790
		internal const uint FILE_ATTRIBUTE_SYSTEM = 4U;

		// Token: 0x04000ECF RID: 3791
		internal const uint FILE_ATTRIBUTE_DIRECTORY = 16U;

		// Token: 0x04000ED0 RID: 3792
		internal const uint FILE_ATTRIBUTE_ARCHIVE = 32U;

		// Token: 0x04000ED1 RID: 3793
		internal const uint FILE_ATTRIBUTE_DEVICE = 64U;

		// Token: 0x04000ED2 RID: 3794
		internal const uint FILE_ATTRIBUTE_NORMAL = 128U;

		// Token: 0x04000ED3 RID: 3795
		internal const uint FILE_ATTRIBUTE_TEMPORARY = 256U;

		// Token: 0x04000ED4 RID: 3796
		internal const uint FILE_ATTRIBUTE_SPARSE_FILE = 512U;

		// Token: 0x04000ED5 RID: 3797
		internal const uint FILE_ATTRIBUTE_REPARSE_POuint = 1024U;

		// Token: 0x04000ED6 RID: 3798
		internal const uint FILE_ATTRIBUTE_COMPRESSED = 2048U;

		// Token: 0x04000ED7 RID: 3799
		internal const uint FILE_ATTRIBUTE_OFFLINE = 4096U;

		// Token: 0x04000ED8 RID: 3800
		internal const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 8192U;

		// Token: 0x04000ED9 RID: 3801
		internal const uint FILE_ATTRIBUTE_ENCRYPTED = 16384U;

		// Token: 0x04000EDA RID: 3802
		internal const uint FILE_FLAG_WRITE_THROUGH = 2147483648U;

		// Token: 0x04000EDB RID: 3803
		internal const uint FILE_FLAG_OVERLAPPED = 1073741824U;

		// Token: 0x04000EDC RID: 3804
		internal const uint FILE_FLAG_NO_BUFFERING = 536870912U;

		// Token: 0x04000EDD RID: 3805
		internal const uint FILE_FLAG_RANDOM_ACCESS = 268435456U;

		// Token: 0x04000EDE RID: 3806
		internal const uint FILE_FLAG_SEQUENTIAL_SCAN = 134217728U;

		// Token: 0x04000EDF RID: 3807
		internal const uint FILE_FLAG_DELETE_ON_CLOSE = 67108864U;

		// Token: 0x04000EE0 RID: 3808
		internal const uint FILE_FLAG_BACKUP_SEMANTICS = 33554432U;

		// Token: 0x04000EE1 RID: 3809
		internal const uint FILE_FLAG_POSIX_SEMANTICS = 16777216U;

		// Token: 0x04000EE2 RID: 3810
		internal const uint FILE_FLAG_OPEN_REPARSE_POuint = 2097152U;

		// Token: 0x04000EE3 RID: 3811
		internal const uint FILE_FLAG_OPEN_NO_RECALL = 1048576U;

		// Token: 0x04000EE4 RID: 3812
		internal const uint FILE_FLAG_FIRST_PIPE_INSTANCE = 524288U;

		// Token: 0x04000EE5 RID: 3813
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000EE6 RID: 3814
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000EE7 RID: 3815
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000EE8 RID: 3816
		internal const int MAX_PATH = 260;

		// Token: 0x02000134 RID: 308
		internal struct SecurityAttributes
		{
			// Token: 0x06000BFF RID: 3071 RVA: 0x0006B6E8 File Offset: 0x000698E8
			internal SecurityAttributes(bool inheritHandle)
			{
				this.length = Marshal.SizeOf(typeof(NativeMethods.SecurityAttributes));
				this.securityDescriptor = IntPtr.Zero;
				this.inheritHandle = inheritHandle;
			}

			// Token: 0x04000EE9 RID: 3817
			internal int length;

			// Token: 0x04000EEA RID: 3818
			internal IntPtr securityDescriptor;

			// Token: 0x04000EEB RID: 3819
			[MarshalAs(UnmanagedType.Bool)]
			internal bool inheritHandle;
		}
	}
}

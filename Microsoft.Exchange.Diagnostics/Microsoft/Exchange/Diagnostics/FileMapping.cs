using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000140 RID: 320
	public class FileMapping
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x00022BC8 File Offset: 0x00020DC8
		public FileMapping(string name, bool writable)
		{
			this.fileMappingHandle = FileMapping.OpenFileMapping(name, writable);
			this.MapViewOfFile(this.fileMappingHandle, writable, name);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00022BEB File Offset: 0x00020DEB
		public FileMapping(string name, int size, bool writable)
		{
			this.fileMappingHandle = FileMapping.CreateFileMapping(name, size, writable);
			this.MapViewOfFile(this.fileMappingHandle, writable, name);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00022C0F File Offset: 0x00020E0F
		public IntPtr IntPtr
		{
			get
			{
				return this.mapViewFileHandle;
			}
		}

		// Token: 0x060008F4 RID: 2292
		[DllImport("Kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060008F5 RID: 2293 RVA: 0x00022C17 File Offset: 0x00020E17
		public void Dispose()
		{
			FileMapping.UnmapViewOfFile(this.mapViewFileHandle);
			FileMapping.CloseHandle(this.fileMappingHandle);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00022C31 File Offset: 0x00020E31
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060008F7 RID: 2295
		[DllImport("Kernel32.dll", BestFitMapping = false, SetLastError = true)]
		internal static extern IntPtr CreateFileMapping(IntPtr hFile, FileMapping.SECURITY_ATTRIBUTES lpFileMappingAttributes, int flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, string lpName);

		// Token: 0x060008F8 RID: 2296
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern IntPtr OpenFileMapping(int desiredAccess, bool inheritHandle, string name);

		// Token: 0x060008F9 RID: 2297
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern IntPtr MapViewOfFile(IntPtr fileMappingObject, int desiredAccess, int fileOffsetHigh, int fileOffsetLow, UIntPtr numberOfBytesToMap);

		// Token: 0x060008FA RID: 2298
		[DllImport("Advapi32.dll", SetLastError = true)]
		internal static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string stringSecurityDescriptor, int stringSDRevision, out IntPtr pSecurityDescriptor, IntPtr securityDescriptorSize);

		// Token: 0x060008FB RID: 2299 RVA: 0x00022C3C File Offset: 0x00020E3C
		private static IntPtr CreateFileMapping(string name, int size, bool writable)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int flProtect;
			if (writable)
			{
				flProtect = 4;
			}
			else
			{
				flProtect = 2;
			}
			IntPtr result;
			try
			{
				intPtr = FileMapping.GetSecurityDescriptor(name);
				FileMapping.SECURITY_ATTRIBUTES security_ATTRIBUTES = new FileMapping.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.SecurityDescriptor = intPtr;
				security_ATTRIBUTES.InheritHandle = false;
				intPtr2 = FileMapping.CreateFileMapping((IntPtr)(-1), security_ATTRIBUTES, flProtect, 0, size, name);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not create file mapping where file mapping name is : " + name);
				}
				result = intPtr2;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					FileMapping.LocalFree(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00022CDC File Offset: 0x00020EDC
		private static IntPtr GetSecurityDescriptor(string name)
		{
			string stringSecurityDescriptor = "D:(A;OICI;FRFWGRGW;;;AU)";
			IntPtr zero = IntPtr.Zero;
			if (!FileMapping.ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, 1, out zero, IntPtr.Zero))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), "Not able to get Security Descriptor where file mapping name is " + name);
			}
			return zero;
		}

		// Token: 0x060008FD RID: 2301
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern int LocalFree(IntPtr hMem);

		// Token: 0x060008FE RID: 2302
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern bool UnmapViewOfFile(IntPtr handle);

		// Token: 0x060008FF RID: 2303 RVA: 0x00022D1C File Offset: 0x00020F1C
		private static IntPtr OpenFileMapping(string name, bool writable)
		{
			IntPtr intPtr = IntPtr.Zero;
			int desiredAccess;
			if (writable)
			{
				desiredAccess = 2;
			}
			else
			{
				desiredAccess = 4;
			}
			intPtr = FileMapping.OpenFileMapping(desiredAccess, false, name);
			if (intPtr == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new FileMappingNotFoundException(string.Format(CultureInfo.InvariantCulture, "Cound not open File mapping for name {0}. Error Details: {1}", new object[]
				{
					name,
					lastWin32Error
				}));
			}
			return intPtr;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00022D80 File Offset: 0x00020F80
		private void MapViewOfFile(IntPtr fileMappingHandle, bool writable, string name)
		{
			int desiredAccess;
			if (writable)
			{
				desiredAccess = 2;
			}
			else
			{
				desiredAccess = 4;
			}
			this.mapViewFileHandle = FileMapping.MapViewOfFile(fileMappingHandle, desiredAccess, 0, 0, UIntPtr.Zero);
			if (this.mapViewFileHandle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not Get Map View of File Where file mapping name is : " + name);
			}
		}

		// Token: 0x04000642 RID: 1602
		internal const int SDDLRevision1 = 1;

		// Token: 0x04000643 RID: 1603
		private const int FileMapRead = 4;

		// Token: 0x04000644 RID: 1604
		private const int FileMapWrite = 2;

		// Token: 0x04000645 RID: 1605
		private const int PageReadonly = 2;

		// Token: 0x04000646 RID: 1606
		private const int PageReadWrite = 4;

		// Token: 0x04000647 RID: 1607
		private IntPtr mapViewFileHandle;

		// Token: 0x04000648 RID: 1608
		private IntPtr fileMappingHandle;

		// Token: 0x02000141 RID: 321
		internal struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x04000649 RID: 1609
			internal IntPtr BaseAddress;

			// Token: 0x0400064A RID: 1610
			internal IntPtr AllocationBase;

			// Token: 0x0400064B RID: 1611
			internal uint AllocationProtect;

			// Token: 0x0400064C RID: 1612
			internal UIntPtr RegionSize;

			// Token: 0x0400064D RID: 1613
			internal uint State;

			// Token: 0x0400064E RID: 1614
			internal uint Protect;

			// Token: 0x0400064F RID: 1615
			internal uint Type;
		}

		// Token: 0x02000142 RID: 322
		[StructLayout(LayoutKind.Sequential)]
		internal class SECURITY_ATTRIBUTES
		{
			// Token: 0x04000650 RID: 1616
			public int Length = 12;

			// Token: 0x04000651 RID: 1617
			public IntPtr SecurityDescriptor = IntPtr.Zero;

			// Token: 0x04000652 RID: 1618
			public bool InheritHandle;
		}
	}
}

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000087 RID: 135
	internal sealed class MemoryMappedFile : IDisposable
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001380C File Offset: 0x00011A0C
		static MemoryMappedFile()
		{
			ulong num;
			if (!NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor("D:(A;;0x101fffff;;;SY)(A;;0x101fffff;;;BA)(A;;0x101fffff;;;CO)(D;;WDWO;;;WD)", 1U, out MemoryMappedFile.defaultSd, out num))
			{
				throw new Win32Exception();
			}
			MemoryMappedFile.defaultSecurityAttributes = new NativeMethods.SECURITY_ATTRIBUTES(MemoryMappedFile.defaultSd);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013844 File Offset: 0x00011A44
		public MemoryMappedFile(string name, int size, bool writable)
		{
			this.size = (uint)((size > 0) ? size : 0);
			NativeMethods.MemoryAccessControl memoryAccessControl;
			if (writable)
			{
				memoryAccessControl = NativeMethods.MemoryAccessControl.ReadWrite;
				this.mapMode = NativeMethods.FileMapAccessControl.Write;
			}
			else
			{
				memoryAccessControl = NativeMethods.MemoryAccessControl.Readonly;
				this.mapMode = NativeMethods.FileMapAccessControl.Read;
			}
			this.fileHandle = NativeMethods.CreateFileMapping(new SafeFileHandle(IntPtr.Zero, false), ref MemoryMappedFile.defaultSecurityAttributes, memoryAccessControl, 0U, this.size, name);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this.fileHandle.IsInvalid)
			{
				throw new IOException("CreateFileMapping(" + memoryAccessControl + ") failed", (lastWin32Error == 0) ? null : new Win32Exception(lastWin32Error));
			}
			if (memoryAccessControl == NativeMethods.MemoryAccessControl.Readonly && lastWin32Error != 183)
			{
				this.fileHandle.Dispose();
				throw new IOException("CreateFileMapping(READONLY) failed - '" + name + "' not found", (lastWin32Error == 0) ? null : new Win32Exception(lastWin32Error));
			}
			if (memoryAccessControl == NativeMethods.MemoryAccessControl.ReadWrite && lastWin32Error == 183)
			{
				this.fileHandle.Dispose();
				throw new IOException("CreateFileMapping(READWRITE) failed - '" + name + "' already exists", (lastWin32Error == 0) ? null : new Win32Exception(lastWin32Error));
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001394C File Offset: 0x00011B4C
		public MapFileStream CreateView(int offset, int size)
		{
			if (this.fileHandle == null)
			{
				throw new ObjectDisposedException("MemoryMappedFile");
			}
			if (this.fileHandle.IsInvalid)
			{
				throw new InvalidOperationException("MemoryMappedFile");
			}
			if ((long)(offset + size) > (long)((ulong)this.size))
			{
				throw new ArgumentException("size");
			}
			SafeViewOfFileHandle safeViewOfFileHandle = NativeMethods.MapViewOfFile(this.fileHandle, this.mapMode, 0U, (uint)offset, new UIntPtr((uint)size));
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeViewOfFileHandle.IsInvalid)
			{
				throw new IOException("MapViewOfFile(" + this.mapMode + ") failed", (lastWin32Error == 0) ? null : new Win32Exception(lastWin32Error));
			}
			return new MapFileStream(safeViewOfFileHandle, size, this.mapMode == NativeMethods.FileMapAccessControl.Write);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00013A00 File Offset: 0x00011C00
		public void Close()
		{
			if (this.fileHandle != null)
			{
				this.fileHandle.Dispose();
				this.fileHandle = null;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013A1C File Offset: 0x00011C1C
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000494 RID: 1172
		private const string DefaultDacl = "D:(A;;0x101fffff;;;SY)(A;;0x101fffff;;;BA)(A;;0x101fffff;;;CO)(D;;WDWO;;;WD)";

		// Token: 0x04000495 RID: 1173
		private static readonly SafeHGlobalHandle defaultSd;

		// Token: 0x04000496 RID: 1174
		private static NativeMethods.SECURITY_ATTRIBUTES defaultSecurityAttributes;

		// Token: 0x04000497 RID: 1175
		private uint size;

		// Token: 0x04000498 RID: 1176
		private NativeMethods.FileMapAccessControl mapMode;

		// Token: 0x04000499 RID: 1177
		private SafeFileHandle fileHandle;
	}
}

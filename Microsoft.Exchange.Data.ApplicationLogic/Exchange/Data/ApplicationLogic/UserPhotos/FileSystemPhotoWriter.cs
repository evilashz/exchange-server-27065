using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D8 RID: 472
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSystemPhotoWriter : IFileSystemPhotoWriter
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x00049DDC File Offset: 0x00047FDC
		public FileSystemPhotoWriter(ITracer upstreamTracer)
		{
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00049E10 File Offset: 0x00048010
		public void Write(string photoFullPath, int thumbprint, Stream photo)
		{
			FileSystemPhotoWriter.ThrowIfInvalidPhotoFullPath(photoFullPath);
			if (photoFullPath == null)
			{
				throw new ArgumentNullException("photoFullPath");
			}
			this.CreatePhotosDirectory(photoFullPath);
			this.tracer.TraceDebug<string, int>((long)this.GetHashCode(), "File system photo writer: writing photo file {0} with thumbprint {1:X8}", photoFullPath, thumbprint);
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES(SafeHGlobalHandle.InvalidHandle);
			using (FileStream fileStream = new FileStream(photoFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (SafeFileHandle safeFileHandle = NativeMethods.CreateFile(photoFullPath + ":thumbprint", NativeMethods.CreateFileAccess.GenericWrite, NativeMethods.CreateFileShare.None, ref security_ATTRIBUTES, FileMode.Create, NativeMethods.CreateFileFileAttributes.None, IntPtr.Zero))
				{
					if (safeFileHandle.IsInvalid)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						this.tracer.TraceDebug<int>((long)this.GetHashCode(), "File system photo writer: failed to create file.  Win32 error: {0}", lastWin32Error);
						throw new Win32Exception(lastWin32Error);
					}
					photo.CopyTo(fileStream);
					using (FileStream fileStream2 = new FileStream(safeFileHandle, FileAccess.Write))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(fileStream2))
						{
							binaryWriter.Write(thumbprint);
						}
					}
				}
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00049F44 File Offset: 0x00048144
		private void CreatePhotosDirectory(string photoFullPath)
		{
			string directoryName = Path.GetDirectoryName(photoFullPath);
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo writer: ensuring photos directory {0} exists.", directoryName);
			Directory.CreateDirectory(directoryName);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00049F77 File Offset: 0x00048177
		private static void ThrowIfInvalidPhotoFullPath(string photoFullPath)
		{
			if (string.IsNullOrEmpty(photoFullPath))
			{
				throw new ArgumentNullException("photoFullPath");
			}
			if (!Path.HasExtension(photoFullPath))
			{
				throw new ArgumentException("photoFullPath");
			}
		}

		// Token: 0x04000963 RID: 2403
		internal const string ThumbprintStream = ":thumbprint";

		// Token: 0x04000964 RID: 2404
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;
	}
}

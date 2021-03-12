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
	// Token: 0x020001D5 RID: 469
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSystemPhotoReader : IFileSystemPhotoReader
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x0004974E File Offset: 0x0004794E
		public FileSystemPhotoReader(ITracer upstreamTracer)
		{
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00049780 File Offset: 0x00047980
		public PhotoMetadata Read(string photoFullPath, Stream output)
		{
			if (string.IsNullOrEmpty(photoFullPath))
			{
				throw new ArgumentNullException("photoFullPath");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo reader: reading photo file {0}", photoFullPath);
			PhotoMetadata result;
			using (FileStream fileStream = new FileStream(photoFullPath, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "File system photo reader: writing photo to output stream.");
				fileStream.CopyTo(output);
				result = new PhotoMetadata
				{
					Length = fileStream.Length,
					ContentType = "image/jpeg"
				};
			}
			return result;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004982C File Offset: 0x00047A2C
		public int ReadThumbprint(string photoFullPath)
		{
			if (string.IsNullOrEmpty(photoFullPath))
			{
				throw new ArgumentNullException("photoFullPath");
			}
			string thumbprintFullPath = FileSystemPhotoReader.GetThumbprintFullPath(photoFullPath);
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo reader: reading thumbprint from file {0}", thumbprintFullPath);
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES(SafeHGlobalHandle.InvalidHandle);
			int result;
			using (SafeFileHandle safeFileHandle = NativeMethods.CreateFile(thumbprintFullPath, (NativeMethods.CreateFileAccess)2147483648U, NativeMethods.CreateFileShare.Read | NativeMethods.CreateFileShare.Delete, ref security_ATTRIBUTES, FileMode.Open, NativeMethods.CreateFileFileAttributes.None, IntPtr.Zero))
			{
				if (safeFileHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.tracer.TraceDebug<int>((long)this.GetHashCode(), "File system photo reader: failed to open file.  Win32 error: {0}", lastWin32Error);
					throw new Win32Exception(lastWin32Error);
				}
				using (FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						int num = binaryReader.ReadInt32();
						this.tracer.TraceDebug<int>((long)this.GetHashCode(), "File system photo reader: thumbprint read: {0:X8}", num);
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00049948 File Offset: 0x00047B48
		public DateTime GetLastModificationTimeUtc(string photoFullPath)
		{
			return File.GetLastWriteTimeUtc(photoFullPath);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00049950 File Offset: 0x00047B50
		private static string GetThumbprintFullPath(string photoFullPath)
		{
			return photoFullPath + ":thumbprint";
		}

		// Token: 0x0400095B RID: 2395
		private const string ThumbprintStream = ":thumbprint";

		// Token: 0x0400095C RID: 2396
		private const string PhotoContentType = "image/jpeg";

		// Token: 0x0400095D RID: 2397
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;
	}
}

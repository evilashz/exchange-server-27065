using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Exchange.CtsResources;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000147 RID: 327
	internal class TempFileStream : FileStream
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0006EAB6 File Offset: 0x0006CCB6
		internal static string Path
		{
			get
			{
				return TempFileStream.GetTempPath();
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0006EABD File Offset: 0x0006CCBD
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0006EAC5 File Offset: 0x0006CCC5
		private TempFileStream(SafeFileHandle handle) : base(handle, FileAccess.ReadWrite)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0006EACF File Offset: 0x0006CCCF
		public static TempFileStream CreateInstance()
		{
			return TempFileStream.CreateInstance("Cts");
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0006EADB File Offset: 0x0006CCDB
		public static TempFileStream CreateInstance(string prefix)
		{
			return TempFileStream.CreateInstance(prefix, true);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0006EAE4 File Offset: 0x0006CCE4
		public static TempFileStream CreateInstance(string prefix, bool deleteOnClose)
		{
			NativeMethods.SecurityAttributes securityAttributes = new NativeMethods.SecurityAttributes(false);
			string path = TempFileStream.Path;
			new FileIOPermission(FileIOPermissionAccess.Write, path).Demand();
			int num = 0;
			int num2 = 10;
			string filename;
			SafeFileHandle safeFileHandle;
			for (;;)
			{
				filename = System.IO.Path.Combine(path, prefix + ((uint)Interlocked.Increment(ref TempFileStream.nextId)).ToString("X5") + ".tmp");
				uint num3 = deleteOnClose ? 67108864U : 0U;
				safeFileHandle = NativeMethods.CreateFile(filename, 1180063U, 0U, ref securityAttributes, 1U, 256U | num3 | 8192U, IntPtr.Zero);
				num2--;
				if (safeFileHandle.IsInvalid)
				{
					num = Marshal.GetLastWin32Error();
					if (num == 80)
					{
						num2++;
					}
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						Interlocked.Add(ref TempFileStream.nextId, currentProcess.Id);
						goto IL_CB;
					}
					goto IL_C8;
				}
				goto IL_C8;
				IL_CB:
				if (num2 <= 0)
				{
					break;
				}
				continue;
				IL_C8:
				num2 = 0;
				goto IL_CB;
			}
			if (safeFileHandle.IsInvalid)
			{
				string message = SharedStrings.CreateFileFailed(filename);
				throw new IOException(message, new Win32Exception(num, message));
			}
			return new TempFileStream(safeFileHandle)
			{
				filePath = filename
			};
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0006EC08 File Offset: 0x0006CE08
		internal static void SetTemporaryPath(string path)
		{
			TempFileStream.tempPath = path;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0006EC10 File Offset: 0x0006CE10
		private static string GetTempPath()
		{
			if (TempFileStream.tempPath == null)
			{
				TempFileStream.tempPath = System.IO.Path.GetTempPath();
			}
			return TempFileStream.tempPath;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0006EC28 File Offset: 0x0006CE28
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x04000F1C RID: 3868
		private static string tempPath;

		// Token: 0x04000F1D RID: 3869
		private static int nextId = Environment.TickCount ^ Process.GetCurrentProcess().Id;

		// Token: 0x04000F1E RID: 3870
		private string filePath;
	}
}

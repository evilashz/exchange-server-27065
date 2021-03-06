using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200080C RID: 2060
	internal class PipeStream : FileStream
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x0005EBD0 File Offset: 0x0005CDD0
		internal PipeStream(SafeFileHandle handle, FileAccess fileAccess, bool isAsync) : base(handle, fileAccess, 1, isAsync)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0005EBDC File Offset: 0x0005CDDC
		public static bool TryCreatePipeHandles(out SafeFileHandle nonInheritableWriteHandle, out SafeFileHandle inheritableReadHandle, Trace tracer)
		{
			string text = null;
			bool flag = false;
			nonInheritableWriteHandle = null;
			inheritableReadHandle = null;
			int num = 0;
			while (num++ < 10)
			{
				text = "\\\\.\\pipe\\" + Guid.NewGuid();
				nonInheritableWriteHandle = NativeMethods.CreateNamedPipe(text, NativeMethods.PipeOpen.AccessOutbound, NativeMethods.PipeModes.TypeMessage | NativeMethods.PipeModes.ReadModeMessage, 1U, 16384U, 0U, uint.MaxValue, IntPtr.Zero);
				if (!nonInheritableWriteHandle.IsInvalid)
				{
					flag = true;
					tracer.TraceDebug<string>(0L, "CreateNamedPipe succeeded for server handle with pipe name <{0}>.", text);
					break;
				}
				tracer.TraceError<int>(0L, "Failed to CreateNamedPipe with Win32Error {0}.", Marshal.GetLastWin32Error());
			}
			if (!flag)
			{
				tracer.TraceError(0L, "All attempts to CreateNamedPipe failed.");
				return false;
			}
			if (!PipeStream.GetClientHandle(text, out inheritableReadHandle, tracer))
			{
				nonInheritableWriteHandle.Close();
				nonInheritableWriteHandle = null;
				return false;
			}
			return true;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0005EC84 File Offset: 0x0005CE84
		private static bool GetClientHandle(string pipeName, out SafeFileHandle readHandle, Trace tracer)
		{
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES(SafeHGlobalHandle.InvalidHandle);
			security_ATTRIBUTES.bInheritHandle = true;
			readHandle = NativeMethods.CreateFile(pipeName, (NativeMethods.CreateFileAccess)2147483904U, NativeMethods.CreateFileShare.None, ref security_ATTRIBUTES, FileMode.Open, NativeMethods.CreateFileFileAttributes.Overlapped, IntPtr.Zero);
			if (readHandle.IsInvalid)
			{
				tracer.TraceError<string, int>(0L, "Cannot get client side of the pipe handle with WriteFile for <{0}>, the Win32 error is {1}.", pipeName, Marshal.GetLastWin32Error());
				return false;
			}
			NativeMethods.PipeModes pipeModes = NativeMethods.PipeModes.ReadModeMessage;
			if (!NativeMethods.SetNamedPipeHandleState(readHandle, ref pipeModes, IntPtr.Zero, IntPtr.Zero))
			{
				tracer.TraceError<string, int>(0L, "Cannot change client pipe handle to message mode pipe name is <{0}>, the Win32 error is {1}.", pipeName, Marshal.GetLastWin32Error());
				return false;
			}
			return true;
		}

		// Token: 0x0400259C RID: 9628
		private const string PipeNamePrefix = "\\\\.\\pipe\\";

		// Token: 0x0400259D RID: 9629
		private const int CreatePipeMaxRetries = 10;
	}
}

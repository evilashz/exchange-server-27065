using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007D RID: 125
	internal sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x000118D8 File Offset: 0x0000FAD8
		private SafeProcessHandle() : base(true)
		{
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000118E4 File Offset: 0x0000FAE4
		internal bool HasExited
		{
			get
			{
				this.ValidateHandle();
				uint num = NativeMethods.WaitForSingleObject(this, 0U);
				if (num == 0U)
				{
					return true;
				}
				if (num == 258U)
				{
					return false;
				}
				throw new Win32Exception();
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011914 File Offset: 0x0000FB14
		internal static SafeProcessHandle CreateProcessAsUser(SafeUserTokenHandle hToken, string lpApplicationName, string lpCommandLine)
		{
			bool flag = false;
			SafeProcessHandle safeProcessHandle = new SafeProcessHandle();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				NativeMethods.STARTUPINFO startupinfo = default(NativeMethods.STARTUPINFO);
				NativeMethods.PROCESS_INFORMATION process_INFORMATION = default(NativeMethods.PROCESS_INFORMATION);
				flag = NativeMethods.CreateProcessAsUser(hToken, lpApplicationName, lpCommandLine, IntPtr.Zero, IntPtr.Zero, true, 0U, IntPtr.Zero, null, ref startupinfo, ref process_INFORMATION);
				if (flag)
				{
					safeProcessHandle.SetHandle(process_INFORMATION.hProcess);
					SafeProcessHandle.CloseHandle(process_INFORMATION.hThread);
				}
			}
			if (!flag)
			{
				safeProcessHandle.Dispose();
				safeProcessHandle = null;
				throw new Win32Exception();
			}
			return safeProcessHandle;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000119A0 File Offset: 0x0000FBA0
		internal static SafeProcessHandle CreateProcess(string lpApplicationName, string lpCommandLine)
		{
			bool flag = false;
			SafeProcessHandle safeProcessHandle = new SafeProcessHandle();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				NativeMethods.STARTUPINFO startupinfo = default(NativeMethods.STARTUPINFO);
				NativeMethods.PROCESS_INFORMATION process_INFORMATION = default(NativeMethods.PROCESS_INFORMATION);
				flag = NativeMethods.CreateProcess(lpApplicationName, lpCommandLine, IntPtr.Zero, IntPtr.Zero, true, 0U, IntPtr.Zero, null, ref startupinfo, ref process_INFORMATION);
				if (flag)
				{
					safeProcessHandle.SetHandle(process_INFORMATION.hProcess);
					SafeProcessHandle.CloseHandle(process_INFORMATION.hThread);
				}
			}
			if (!flag)
			{
				safeProcessHandle.Dispose();
				safeProcessHandle = null;
				throw new Win32Exception();
			}
			return safeProcessHandle;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011A2C File Offset: 0x0000FC2C
		internal void TerminateProcess(uint exitCode)
		{
			this.ValidateHandle();
			if (!NativeMethods.TerminateProcess(this.handle, exitCode))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011A58 File Offset: 0x0000FC58
		internal int GetProcessId()
		{
			this.ValidateHandle();
			uint processId = NativeMethods.GetProcessId(this.handle);
			if (processId == 0U)
			{
				throw new Win32Exception();
			}
			return (int)processId;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011A81 File Offset: 0x0000FC81
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeProcessHandle.CloseHandle(this.handle);
		}

		// Token: 0x06000439 RID: 1081
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600043A RID: 1082 RVA: 0x00011A8E File Offset: 0x0000FC8E
		private void ValidateHandle()
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Process handle is invalid.");
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020001D4 RID: 468
	[Cmdlet("Get", "ProcessModule")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class GetProcessModule : Task
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0004874B File Offset: 0x0004694B
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x00048762 File Offset: 0x00046962
		[Parameter(Mandatory = true)]
		public new int ProcessId
		{
			get
			{
				return (int)base.Fields["ProcessId"];
			}
			set
			{
				base.Fields["ProcessId"] = value;
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00048783 File Offset: 0x00046983
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.ReadModuleList(delegate(string path)
			{
				base.WriteObject(path);
			});
			TaskLogger.LogExit();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000487A4 File Offset: 0x000469A4
		private void ReadModuleList(GetProcessModule.ModuleHandler moduleHandler)
		{
			Process process = this.TryGetProcess();
			if (process == null)
			{
				return;
			}
			using (process)
			{
				IntPtr value = this.TryGetHandle(process);
				if (!(value == IntPtr.Zero))
				{
					IntPtr intPtr = this.CreateProcessSnapshot();
					if (!(intPtr == IntPtr.Zero))
					{
						try
						{
							for (string path = this.GetFirstModule(intPtr); path != null; path = this.GetNextModule(intPtr))
							{
								moduleHandler(path);
							}
						}
						finally
						{
							GetProcessModule.CloseHandle(intPtr);
						}
					}
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004883C File Offset: 0x00046A3C
		private Process TryGetProcess()
		{
			Process result;
			try
			{
				result = Process.GetProcessById(this.ProcessId);
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00048870 File Offset: 0x00046A70
		private IntPtr TryGetHandle(Process process)
		{
			try
			{
				return process.Handle;
			}
			catch (InvalidOperationException)
			{
			}
			catch (Win32Exception ex)
			{
				if (ex.NativeErrorCode != 5)
				{
					throw;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000488BC File Offset: 0x00046ABC
		private IntPtr CreateProcessSnapshot()
		{
			IntPtr intPtr = GetProcessModule.CreateToolhelp32Snapshot(24, this.ProcessId);
			if (intPtr == IntPtr.Zero || intPtr == (IntPtr)(-1))
			{
				if (Marshal.GetLastWin32Error() != 299)
				{
					throw new Win32Exception();
				}
				intPtr = IntPtr.Zero;
			}
			return intPtr;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004890C File Offset: 0x00046B0C
		private string GetFirstModule(IntPtr snapshot)
		{
			GetProcessModule.MODULEENTRY32 entry = GetProcessModule.MODULEENTRY32.NewEntry();
			bool returnValue = GetProcessModule.Module32FirstW(snapshot, ref entry);
			return this.AnalyzeModuleResult(returnValue, entry);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00048930 File Offset: 0x00046B30
		private string GetNextModule(IntPtr snapshot)
		{
			GetProcessModule.MODULEENTRY32 entry = GetProcessModule.MODULEENTRY32.NewEntry();
			bool returnValue = GetProcessModule.Module32NextW(snapshot, ref entry);
			return this.AnalyzeModuleResult(returnValue, entry);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00048954 File Offset: 0x00046B54
		private string AnalyzeModuleResult(bool returnValue, GetProcessModule.MODULEENTRY32 entry)
		{
			if (returnValue)
			{
				return entry.Path;
			}
			if (Marshal.GetLastWin32Error() != 18)
			{
				throw new Win32Exception();
			}
			return null;
		}

		// Token: 0x0600104B RID: 4171
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot(int flags, int processID);

		// Token: 0x0600104C RID: 4172
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool Module32FirstW(IntPtr snapshot, [In] [Out] ref GetProcessModule.MODULEENTRY32 entry);

		// Token: 0x0600104D RID: 4173
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool Module32NextW(IntPtr snapshot, [In] [Out] ref GetProcessModule.MODULEENTRY32 entry);

		// Token: 0x0600104E RID: 4174
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0400076C RID: 1900
		private const int TH32CS_SNAPMODULE = 8;

		// Token: 0x0400076D RID: 1901
		private const int TH32CS_SNAPMODULE32 = 16;

		// Token: 0x0400076E RID: 1902
		private const int MAX_MODULE_NAME32 = 255;

		// Token: 0x0400076F RID: 1903
		private const int MAX_PATH = 260;

		// Token: 0x04000770 RID: 1904
		private const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000771 RID: 1905
		private const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000772 RID: 1906
		private const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000773 RID: 1907
		private const int ERROR_PARTIAL_COPY = 299;

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06001052 RID: 4178
		private delegate void ModuleHandler(string path);

		// Token: 0x020001D6 RID: 470
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct MODULEENTRY32
		{
			// Token: 0x06001055 RID: 4181 RVA: 0x0004897C File Offset: 0x00046B7C
			public static GetProcessModule.MODULEENTRY32 NewEntry()
			{
				GetProcessModule.MODULEENTRY32 result = default(GetProcessModule.MODULEENTRY32);
				if (IntPtr.Size == 4)
				{
					result.dwSize = 1064;
				}
				else
				{
					result.dwSize = 1080;
				}
				return result;
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06001056 RID: 4182 RVA: 0x000489B4 File Offset: 0x00046BB4
			public string Name
			{
				get
				{
					return this.szModule;
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06001057 RID: 4183 RVA: 0x000489BC File Offset: 0x00046BBC
			public IntPtr Handle
			{
				get
				{
					return this.hModule;
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001058 RID: 4184 RVA: 0x000489C4 File Offset: 0x00046BC4
			public string Path
			{
				get
				{
					return this.szExePath;
				}
			}

			// Token: 0x04000774 RID: 1908
			private int dwSize;

			// Token: 0x04000775 RID: 1909
			private int th32ModuleID;

			// Token: 0x04000776 RID: 1910
			private int th32ProcessID;

			// Token: 0x04000777 RID: 1911
			private int GlblcntUsage;

			// Token: 0x04000778 RID: 1912
			private int ProccntUsage;

			// Token: 0x04000779 RID: 1913
			private IntPtr modBaseAddr;

			// Token: 0x0400077A RID: 1914
			private int modBaseSize;

			// Token: 0x0400077B RID: 1915
			private IntPtr hModule;

			// Token: 0x0400077C RID: 1916
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			private string szModule;

			// Token: 0x0400077D RID: 1917
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			private string szExePath;
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200027D RID: 637
	internal class IEHelper
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x00077C98 File Offset: 0x00075E98
		public IEHelper()
		{
			Application.ApplicationExit += delegate(object param0, EventArgs param1)
			{
				if (this.IsIEOpened)
				{
					try
					{
						this.ie.Quit();
					}
					catch (COMException)
					{
					}
					catch (InvalidComObjectException)
					{
					}
					catch (TargetException)
					{
					}
				}
			};
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00077CC4 File Offset: 0x00075EC4
		public void NavigateInSingleIE(string url, IUIService uiService)
		{
			if (!this.IsIEOpened)
			{
				try
				{
					this.ie = (IEHelper.IWebBrowser2)new IEHelper.InternetExplorerClass();
					this.ie.Visible = true;
					this.currentIEHandle = new IntPtr(this.ie.HWND);
				}
				catch (COMException)
				{
					this.StartIEByProcess(url, uiService);
				}
				catch (InvalidComObjectException)
				{
					this.StartIEByProcess(url, uiService);
				}
				catch (TargetException)
				{
					this.StartIEByProcess(url, uiService);
				}
			}
			this.NavigateIE(url);
			this.BringIEToFront();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00077D64 File Offset: 0x00075F64
		private void StartIEByProcess(string url, IUIService uiService)
		{
			bool flag = false;
			try
			{
				this.helpProcess = new Process
				{
					StartInfo = new ProcessStartInfo("iexplore.exe", this.IsIE8OrNewer() ? " -new -nomerge " : " -new ")
				};
				if (this.helpProcess.Start())
				{
					this.helpProcess.WaitForInputIdle(2000);
					flag = true;
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (Win32Exception)
			{
			}
			if (!flag)
			{
				try
				{
					WinformsHelper.OpenUrl(new Uri(url));
				}
				catch (UrlHandlerNotFoundException ex)
				{
					uiService.ShowError(ex.Message);
				}
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00077E14 File Offset: 0x00076014
		private bool IsIE8OrNewer()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Version Vector"))
				{
					string text = (string)registryKey.GetValue("IE");
					if (!string.IsNullOrEmpty(text))
					{
						int num = 0;
						int num2 = 1;
						int num3 = text.IndexOf('.');
						if (num3 > 0)
						{
							if (int.TryParse(text.Substring(0, num3), out num))
							{
								num2 = num;
							}
						}
						else if (int.TryParse(text, out num))
						{
							num2 = num;
						}
						return num2 >= 8;
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			return false;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00077EDC File Offset: 0x000760DC
		private bool NavigateIE(string url)
		{
			bool result = false;
			if (this.ie != null)
			{
				object obj = null;
				try
				{
					this.ie.Navigate(url, ref obj, ref obj, ref obj, ref obj);
					result = true;
				}
				catch (COMException)
				{
				}
				catch (InvalidComObjectException)
				{
				}
				catch (TargetException)
				{
				}
			}
			return result;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00077F40 File Offset: 0x00076140
		private void BringIEToFront()
		{
			if (this.currentIEHandle != IntPtr.Zero)
			{
				if (IEHelper.IsIconic(this.currentIEHandle))
				{
					IEHelper.ShowWindow(this.currentIEHandle, 9);
					return;
				}
				IEHelper.LockSetForegroundWindow(2U);
				IEHelper.BringWindowToTop(this.currentIEHandle);
				IEHelper.SetForegroundWindow(this.currentIEHandle);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00077F9C File Offset: 0x0007619C
		private bool IsIEOpened
		{
			get
			{
				bool flag = false;
				try
				{
					flag = (this.ie != null && this.ie.Visible);
				}
				catch (COMException)
				{
				}
				catch (InvalidComObjectException)
				{
				}
				catch (TargetException)
				{
				}
				return flag || this.FindIEByHandle();
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00078000 File Offset: 0x00076200
		private bool FindIEByHandle()
		{
			bool result = false;
			if (this.helpProcess != null && !this.helpProcess.HasExited)
			{
				this.helpProcess.Refresh();
				this.currentIEHandle = this.helpProcess.MainWindowHandle;
			}
			if (IEHelper.IsWindow(this.currentIEHandle))
			{
				try
				{
					IEnumerable enumerable = (IEnumerable)new IEHelper.ShellWindowsClass();
					foreach (object obj in enumerable)
					{
						IEHelper.IWebBrowser2 webBrowser = obj as IEHelper.IWebBrowser2;
						if (webBrowser != null && this.currentIEHandle == (IntPtr)webBrowser.HWND)
						{
							result = true;
							this.ie = webBrowser;
							break;
						}
					}
				}
				catch (COMException)
				{
				}
				catch (InvalidComObjectException)
				{
				}
				catch (TargetException)
				{
				}
				catch (FileNotFoundException)
				{
				}
			}
			return result;
		}

		// Token: 0x06001B28 RID: 6952
		[DllImport("User32.Dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool IsIconic(IntPtr hWnd);

		// Token: 0x06001B29 RID: 6953
		[DllImport("User32.Dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x06001B2A RID: 6954
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x06001B2B RID: 6955
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool BringWindowToTop(IntPtr hWnd);

		// Token: 0x06001B2C RID: 6956
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x06001B2D RID: 6957
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool LockSetForegroundWindow(uint uLockCode);

		// Token: 0x04000A1A RID: 2586
		private const int LSFW_UNLOCK = 2;

		// Token: 0x04000A1B RID: 2587
		private Process helpProcess;

		// Token: 0x04000A1C RID: 2588
		private IEHelper.IWebBrowser2 ie;

		// Token: 0x04000A1D RID: 2589
		private IntPtr currentIEHandle;

		// Token: 0x0200027E RID: 638
		[Guid("0002DF01-0000-0000-C000-000000000046")]
		[ComImport]
		public class InternetExplorerClass
		{
			// Token: 0x06001B2F RID: 6959
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern InternetExplorerClass();
		}

		// Token: 0x0200027F RID: 639
		[Guid("9BA05972-F6A8-11CF-A442-00A0C90A8F39")]
		[ComImport]
		public class ShellWindowsClass
		{
			// Token: 0x06001B30 RID: 6960
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ShellWindowsClass();
		}

		// Token: 0x02000280 RID: 640
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E")]
		[ComImport]
		public interface IWebBrowser2
		{
			// Token: 0x06001B31 RID: 6961
			[DispId(104)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Navigate([MarshalAs(UnmanagedType.BStr)] [In] string URL, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Flags, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object TargetFrameName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PostData, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Headers);

			// Token: 0x06001B32 RID: 6962
			[DispId(300)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Quit();

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001B33 RID: 6963
			// (set) Token: 0x06001B34 RID: 6964
			[DispId(402)]
			bool Visible { [DispId(402)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(402)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001B35 RID: 6965
			[DispId(-515)]
			int HWND { [DispId(-515)] [MethodImpl(MethodImplOptions.InternalCall)] get; }
		}
	}
}

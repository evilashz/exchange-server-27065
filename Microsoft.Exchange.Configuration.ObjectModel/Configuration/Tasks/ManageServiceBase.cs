using System;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000B4 RID: 180
	public abstract class ManageServiceBase : Task
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0001AC6E File Offset: 0x00018E6E
		public ManageServiceBase()
		{
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001AC78 File Offset: 0x00018E78
		internal static ServiceControllerStatus GetServiceStatus(string serviceName)
		{
			ServiceControllerStatus status;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				status = serviceController.Status;
			}
			return status;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		internal void DoNativeServiceTask(string serviceName, ServiceAccessFlags serviceAccessFlags, ManageServiceBase.NativeServiceTaskDelegate task)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr = NativeMethods.OpenSCManager(null, null, ServiceControlManagerAccessFlags.AllAccess);
				if (IntPtr.Zero == intPtr)
				{
					base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorCannotOpenServiceControllerManager), ErrorCategory.ReadError, null);
				}
				intPtr2 = NativeMethods.OpenService(intPtr, serviceName, serviceAccessFlags);
				if (IntPtr.Zero == intPtr2)
				{
					base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorCannotOpenService(serviceName)), ErrorCategory.ReadError, null);
				}
				task(intPtr2);
			}
			finally
			{
				if (IntPtr.Zero != intPtr2 && !NativeMethods.CloseServiceHandle(intPtr2))
				{
					this.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorCloseServiceHandle), ErrorCategory.InvalidOperation, null, false);
				}
				if (IntPtr.Zero != intPtr && !NativeMethods.CloseServiceHandle(intPtr))
				{
					this.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorCloseServiceHandle), ErrorCategory.InvalidOperation, null, false);
				}
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001AD9C File Offset: 0x00018F9C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		protected void UpdateExecutable(string serviceName, string executablePath)
		{
			TaskLogger.Trace("Updating executable...", new object[0]);
			this.DoNativeServiceTask(serviceName, ServiceAccessFlags.AllAccess, delegate(IntPtr service)
			{
				if (!NativeMethods.ChangeServiceConfig(service, 4294967295U, 4294967295U, 4294967295U, executablePath, null, null, null, null, null, null))
				{
					this.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorChangeServiceConfig2(serviceName)), ErrorCategory.WriteError, null);
				}
			});
		}

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x0600073B RID: 1851
		internal delegate void NativeServiceTaskDelegate(IntPtr serviceHandle);
	}
}

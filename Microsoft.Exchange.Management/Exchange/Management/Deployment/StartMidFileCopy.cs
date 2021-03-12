using System;
using System.Diagnostics;
using System.Management.Automation;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200025E RID: 606
	[Cmdlet("Start", "MidFileCopy", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class StartMidFileCopy : ManageSetupBindingTasks
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x00060A1C File Offset: 0x0005EC1C
		public static bool StartProcessHidden(string fileName, string arguments, int millisecondsTimeout)
		{
			Process process = Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				WindowStyle = ProcessWindowStyle.Hidden
			});
			process.Start();
			process.WaitForExit();
			if (!process.WaitForExit(millisecondsTimeout))
			{
				process.Kill();
				return false;
			}
			return true;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00060A60 File Offset: 0x0005EC60
		public StartMidFileCopy()
		{
			ServiceController serviceController = new ServiceController("WinMgmt");
			if (serviceController.Status != ServiceControllerStatus.Running)
			{
				try
				{
					serviceController.Start();
				}
				catch
				{
					StartMidFileCopy.StartProcessHidden("sc.exe", "config WinMgmt start= auto", 1000);
					int num = 10;
					do
					{
						serviceController.Start();
						Thread.Sleep(100);
						serviceController.Refresh();
					}
					while (serviceController.Status != ServiceControllerStatus.Running && num-- > 0);
				}
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00060AE0 File Offset: 0x0005ECE0
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartMidFileCopyDescription;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00060AE7 File Offset: 0x0005ECE7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (UninstallMsi.RebootRequiredException != null)
			{
				throw UninstallMsi.RebootRequiredException;
			}
			TaskLogger.LogExit();
		}
	}
}

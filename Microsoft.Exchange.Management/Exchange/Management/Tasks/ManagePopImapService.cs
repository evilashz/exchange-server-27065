using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000628 RID: 1576
	public abstract class ManagePopImapService : ManageService
	{
		// Token: 0x060037AF RID: 14255 RVA: 0x000E7188 File Offset: 0x000E5388
		protected ManagePopImapService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Manual;
			base.DisplayName = this.ServiceDisplayName;
			base.Description = this.ServiceDescription;
			this.installPath = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(this.installPath, this.ServiceFile);
			base.ServiceInstallContext = installContext;
			base.EventMessageFile = Path.Combine(this.installPath, this.WorkingProcessEventMessageFile);
			base.CategoryCount = 1;
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060037B0 RID: 14256
		protected abstract string RelativeInstallPath { get; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000E7238 File Offset: 0x000E5438
		protected string RelativeEventLogFilePath
		{
			get
			{
				return "Bin";
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060037B2 RID: 14258
		protected abstract string ServiceDisplayName { get; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060037B3 RID: 14259
		protected abstract string ServiceDescription { get; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060037B4 RID: 14260
		protected abstract string ServiceFile { get; }

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000E723F File Offset: 0x000E543F
		protected string ServiceEventMessageFile
		{
			get
			{
				return "Microsoft.Exchange.Common.ProcessManagerMsg.dll";
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060037B6 RID: 14262
		protected abstract string ServiceCategoryName { get; }

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060037B7 RID: 14263
		protected abstract string WorkingProcessFile { get; }

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060037B8 RID: 14264
		protected abstract string WorkingProcessEventMessageFile { get; }

		// Token: 0x060037B9 RID: 14265 RVA: 0x000E7246 File Offset: 0x000E5446
		protected void InstallPopImapService()
		{
			base.Install();
			this.RegisterServiceEventMessageFile();
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000E7254 File Offset: 0x000E5454
		protected void UninstallPopImapService()
		{
			base.Uninstall();
			this.UnregisterServiceEventMessageFile();
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000E7262 File Offset: 0x000E5462
		protected void ReservePort(int portNumber)
		{
			TcpListener.CreatePersistentTcpPortReservation((ushort)portNumber, 1);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000E726C File Offset: 0x000E546C
		protected void RegisterServiceEventMessageFile()
		{
			string value = Path.Combine(Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeEventLogFilePath), this.ServiceEventMessageFile);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Path.Combine(ManageService.eventLogRegPath, this.ServiceCategoryName)))
			{
				registryKey.SetValue(ManageService.eventMessageFileSubKeyName, value);
				registryKey.SetValue(ManageService.categoryMessageFileSubKeyName, value);
				registryKey.SetValue(ManageService.categoryCountSubKeyName, 1);
				registryKey.SetValue(ManageService.typesSupportedSubKeyName, 7);
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000E7308 File Offset: 0x000E5508
		protected void UnregisterServiceEventMessageFile()
		{
			Registry.LocalMachine.DeleteSubKey(Path.Combine(ManageService.eventLogRegPath, this.ServiceCategoryName), false);
		}

		// Token: 0x040025AF RID: 9647
		protected string installPath;

		// Token: 0x040025B0 RID: 9648
		public bool ForceFailure;
	}
}

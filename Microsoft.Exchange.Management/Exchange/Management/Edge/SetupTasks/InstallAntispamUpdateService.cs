using System;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000308 RID: 776
	[LocDescription(Strings.IDs.InstallAntispamUpdateServiceTask)]
	[Cmdlet("Install", "AntispamUpdateService")]
	public class InstallAntispamUpdateService : ManageAntispamUpdateService
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00074DFE File Offset: 0x00072FFE
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00074E15 File Offset: 0x00073015
		[Parameter(Mandatory = false)]
		public string[] ServicesDependedOnParameter
		{
			get
			{
				return base.Fields["ServicesDependedOn"] as string[];
			}
			set
			{
				base.Fields["ServicesDependedOn"] = value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00074E28 File Offset: 0x00073028
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00074E33 File Offset: 0x00073033
		[Parameter(Mandatory = false)]
		public bool StartAutomatically
		{
			internal get
			{
				return base.StartMode == ServiceStartMode.Automatic;
			}
			set
			{
				base.StartMode = (value ? ServiceStartMode.Automatic : ServiceStartMode.Manual);
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00074E44 File Offset: 0x00073044
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.ServicesDependedOnParameter != null)
			{
				foreach (string serviceName in this.ServicesDependedOnParameter)
				{
					if (!Utils.GetServiceExists(serviceName))
					{
						base.WriteError(new ArgumentException(Strings.InvalidServicesDependedOn(serviceName), "ServicesDependedOn"), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			base.ServicesDependedOn = this.ServicesDependedOnParameter;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00074EB2 File Offset: 0x000730B2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x04000B7B RID: 2939
		public const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

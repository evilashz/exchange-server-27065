using System;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200030A RID: 778
	[Cmdlet("Install", "TransportLogSearchService")]
	public class InstallTransportLogSearchService : ManageTransportLogSearchService
	{
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00074FB0 File Offset: 0x000731B0
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00074FC7 File Offset: 0x000731C7
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00074FDA File Offset: 0x000731DA
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00074FE5 File Offset: 0x000731E5
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

		// Token: 0x06001A62 RID: 6754 RVA: 0x00074FF4 File Offset: 0x000731F4
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

		// Token: 0x06001A63 RID: 6755 RVA: 0x00075062 File Offset: 0x00073262
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x04000B7E RID: 2942
		public const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000306 RID: 774
	[Cmdlet("Install", "FmsService")]
	[LocDescription(Strings.IDs.InstallFmsServiceTask)]
	public class InstallFmsService : ManageFmsService
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00074C8C File Offset: 0x00072E8C
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00074CA3 File Offset: 0x00072EA3
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

		// Token: 0x06001A50 RID: 6736 RVA: 0x00074CB8 File Offset: 0x00072EB8
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

		// Token: 0x06001A51 RID: 6737 RVA: 0x00074D26 File Offset: 0x00072F26
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x04000B78 RID: 2936
		public const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

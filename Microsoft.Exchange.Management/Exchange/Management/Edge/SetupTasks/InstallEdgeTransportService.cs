using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000304 RID: 772
	[LocDescription(Strings.IDs.InstallEdgeTransportServiceTask)]
	[Cmdlet("Install", "EdgeTransportService")]
	public class InstallEdgeTransportService : ManageEdgeTransportService
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00074B4A File Offset: 0x00072D4A
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00074B61 File Offset: 0x00072D61
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

		// Token: 0x06001A49 RID: 6729 RVA: 0x00074B74 File Offset: 0x00072D74
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

		// Token: 0x06001A4A RID: 6730 RVA: 0x00074BE2 File Offset: 0x00072DE2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x04000B74 RID: 2932
		public const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

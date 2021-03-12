using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000764 RID: 1892
	[Cmdlet("Install", "ServiceHost")]
	public class InstallServiceHost : ManageServiceHost
	{
		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x0011416F File Offset: 0x0011236F
		// (set) Token: 0x06004339 RID: 17209 RVA: 0x00114186 File Offset: 0x00112386
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

		// Token: 0x0600433A RID: 17210 RVA: 0x00114199 File Offset: 0x00112399
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.ServicesDependedOnParameter != null)
			{
				base.ServicesDependedOn = this.ServicesDependedOnParameter;
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x001141BE File Offset: 0x001123BE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x040029F0 RID: 10736
		private const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

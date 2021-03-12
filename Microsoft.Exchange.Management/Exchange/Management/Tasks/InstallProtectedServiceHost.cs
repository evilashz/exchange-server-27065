using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000767 RID: 1895
	[Cmdlet("Install", "ProtectedServiceHost")]
	public class InstallProtectedServiceHost : ManageProtectedServiceHost
	{
		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x00114309 File Offset: 0x00112509
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x00114320 File Offset: 0x00112520
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

		// Token: 0x06004345 RID: 17221 RVA: 0x00114333 File Offset: 0x00112533
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

		// Token: 0x06004346 RID: 17222 RVA: 0x00114358 File Offset: 0x00112558
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}

		// Token: 0x040029F2 RID: 10738
		private const string ServicesDependedOnParamName = "ServicesDependedOn";
	}
}

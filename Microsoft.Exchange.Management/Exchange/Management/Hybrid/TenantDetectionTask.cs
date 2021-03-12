using System;
using Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200091E RID: 2334
	internal class TenantDetectionTask : SessionTask
	{
		// Token: 0x060052FC RID: 21244 RVA: 0x00156EE3 File Offset: 0x001550E3
		public TenantDetectionTask() : base(HybridStrings.TenantDetectionTaskName, 1)
		{
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00156EF8 File Offset: 0x001550F8
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				return false;
			}
			base.Logger.LogInformation(HybridStrings.HybridEngineCheckingForUpgradeTenant);
			using (HybridConfigurationDetection hybridConfigurationDetection = new HybridConfigurationDetection(base.Logger))
			{
				if (!hybridConfigurationDetection.RunTenantHybridTest(null, taskContext.TenantSession.GetOrganizationConfig().OrganizationConfigHash))
				{
					base.Logger.LogInformation(HybridStrings.ReturnResultForHybridDetectionWasFalse);
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003032 RID: 12338
		private const string CASRole = "ClientAccess";

		// Token: 0x04003033 RID: 12339
		private const string isCoexistenceDomainKey = "IsCoexistenceDomain";
	}
}

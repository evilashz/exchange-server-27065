using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F5 RID: 501
	internal abstract class MonitoringSiteAlert : MonitoringAlert
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x00050B6F File Offset: 0x0004ED6F
		protected MonitoringSiteAlert(string identity, Guid identityGuid) : base(identity, identityGuid)
		{
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00050B79 File Offset: 0x0004ED79
		protected override bool IsEnabled
		{
			get
			{
				return !RegistryParameters.DatabaseHealthCheckSiteAlertsDisabled;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00050B83 File Offset: 0x0004ED83
		protected override bool IsValidationSuccessful(IHealthValidationResultMinimal result)
		{
			return result.IsSiteValidationSuccessful;
		}
	}
}

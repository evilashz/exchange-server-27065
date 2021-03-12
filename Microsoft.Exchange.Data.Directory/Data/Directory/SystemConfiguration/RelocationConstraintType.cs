using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200055E RID: 1374
	public enum RelocationConstraintType
	{
		// Token: 0x040029E3 RID: 10723
		TenantVersionConstraint,
		// Token: 0x040029E4 RID: 10724
		TenantInTransitionConstraint,
		// Token: 0x040029E5 RID: 10725
		SCTConstraint,
		// Token: 0x040029E6 RID: 10726
		E14MailboxesPresentContraint,
		// Token: 0x040029E7 RID: 10727
		RelocationInProgressConstraint,
		// Token: 0x040029E8 RID: 10728
		ValidationErrorConstraint,
		// Token: 0x040029E9 RID: 10729
		ObsoleteDataConstraint,
		// Token: 0x040029EA RID: 10730
		CNFConstraint,
		// Token: 0x040029EB RID: 10731
		InitialDomainConstraint,
		// Token: 0x040029EC RID: 10732
		OutlookConstraint,
		// Token: 0x040029ED RID: 10733
		FFOUpgradeConstraint,
		// Token: 0x040029EE RID: 10734
		MonitoringConstraint
	}
}

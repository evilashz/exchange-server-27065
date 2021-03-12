using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000099 RID: 153
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IResourceSettings : ISettings
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060003AB RID: 939
		bool Enabled { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060003AC RID: 940
		int MaxConcurrency { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060003AD RID: 941
		int DiscretionaryUnderloaded { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060003AE RID: 942
		int DiscretionaryOverloaded { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060003AF RID: 943
		int DiscretionaryCritical { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060003B0 RID: 944
		int InternalMaintenanceUnderloaded { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060003B1 RID: 945
		int InternalMaintenanceOverloaded { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060003B2 RID: 946
		int InternalMaintenanceCritical { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060003B3 RID: 947
		int CustomerExpectationUnderloaded { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060003B4 RID: 948
		int CustomerExpectationOverloaded { get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060003B5 RID: 949
		int CustomerExpectationCritical { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060003B6 RID: 950
		int UrgentUnderloaded { get; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060003B7 RID: 951
		int UrgentOverloaded { get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060003B8 RID: 952
		int UrgentCritical { get; }
	}
}

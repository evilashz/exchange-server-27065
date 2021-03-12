using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000A4 RID: 164
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface ISystemWorkloadManagerSettings : ISettings
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060003F6 RID: 1014
		int MaxConcurrency { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060003F7 RID: 1015
		TimeSpan RefreshCycle { get; }
	}
}

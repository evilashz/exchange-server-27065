using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000BC RID: 188
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IWorkloadSettings : ISettings
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000454 RID: 1108
		WorkloadClassification Classification { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000455 RID: 1109
		int MaxConcurrency { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000456 RID: 1110
		bool Enabled { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000457 RID: 1111
		bool EnabledDuringBlackout { get; }
	}
}

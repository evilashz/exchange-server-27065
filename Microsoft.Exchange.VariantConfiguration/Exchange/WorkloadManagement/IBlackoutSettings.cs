using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000019 RID: 25
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IBlackoutSettings : ISettings
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118
		TimeSpan StartTime { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119
		TimeSpan EndTime { get; }
	}
}

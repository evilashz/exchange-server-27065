using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessageDepot
{
	// Token: 0x02000089 RID: 137
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IMessageDepotSettings : ISettings
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600036C RID: 876
		bool Enabled { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600036D RID: 877
		IList<DayOfWeek> EnabledOnDaysOfWeek { get; }
	}
}

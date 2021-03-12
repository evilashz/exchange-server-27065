using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000079 RID: 121
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IMailboxAssistantSettings : ISettings
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060002E1 RID: 737
		bool Enabled { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060002E2 RID: 738
		TimeSpan MailboxNotInterestingLogInterval { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060002E3 RID: 739
		bool SpreadLoad { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060002E4 RID: 740
		bool SlaMonitoringEnabled { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060002E5 RID: 741
		bool CompletionMonitoringEnabled { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060002E6 RID: 742
		bool ActiveDatabaseProcessingMonitoringEnabled { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060002E7 RID: 743
		float SlaUrgentThreshold { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060002E8 RID: 744
		float SlaNonUrgentThreshold { get; }
	}
}

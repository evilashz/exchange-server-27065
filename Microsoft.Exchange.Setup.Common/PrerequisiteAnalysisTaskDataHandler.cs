using System;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PrerequisiteAnalysisTaskDataHandler : PrereqBaseTaskDataHandler
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public static PrerequisiteAnalysisTaskDataHandler GetInstance(ISetupContext context, MonadConnection connection)
		{
			return LazyInitializer.EnsureInitialized<PrerequisiteAnalysisTaskDataHandler>(ref PrerequisiteAnalysisTaskDataHandler.instance, () => new PrerequisiteAnalysisTaskDataHandler(context, connection));
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C433 File Offset: 0x0000A633
		private PrerequisiteAnalysisTaskDataHandler(ISetupContext context, MonadConnection connection) : base("test-SetupPrerequisites", Strings.PrerequisiteAnalysis, null, context, connection)
		{
		}

		// Token: 0x040000F0 RID: 240
		private static PrerequisiteAnalysisTaskDataHandler instance;
	}
}

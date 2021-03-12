using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007A RID: 122
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_ : VariantObjectDataAccessorBase<IMailboxAssistantSettings, _DynamicStorageSelection_IMailboxAssistantSettings_Implementation_, _DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_>
	{
		// Token: 0x0400022D RID: 557
		internal string _Name_MaterializedValue_;

		// Token: 0x0400022E RID: 558
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x0400022F RID: 559
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x04000230 RID: 560
		internal TimeSpan _MailboxNotInterestingLogInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000231 RID: 561
		internal ValueProvider<TimeSpan> _MailboxNotInterestingLogInterval_ValueProvider_;

		// Token: 0x04000232 RID: 562
		internal bool _SpreadLoad_MaterializedValue_;

		// Token: 0x04000233 RID: 563
		internal ValueProvider<bool> _SpreadLoad_ValueProvider_;

		// Token: 0x04000234 RID: 564
		internal bool _SlaMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000235 RID: 565
		internal ValueProvider<bool> _SlaMonitoringEnabled_ValueProvider_;

		// Token: 0x04000236 RID: 566
		internal bool _CompletionMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000237 RID: 567
		internal ValueProvider<bool> _CompletionMonitoringEnabled_ValueProvider_;

		// Token: 0x04000238 RID: 568
		internal bool _ActiveDatabaseProcessingMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000239 RID: 569
		internal ValueProvider<bool> _ActiveDatabaseProcessingMonitoringEnabled_ValueProvider_;

		// Token: 0x0400023A RID: 570
		internal float _SlaUrgentThreshold_MaterializedValue_;

		// Token: 0x0400023B RID: 571
		internal ValueProvider<float> _SlaUrgentThreshold_ValueProvider_;

		// Token: 0x0400023C RID: 572
		internal float _SlaNonUrgentThreshold_MaterializedValue_;

		// Token: 0x0400023D RID: 573
		internal ValueProvider<float> _SlaNonUrgentThreshold_ValueProvider_;
	}
}

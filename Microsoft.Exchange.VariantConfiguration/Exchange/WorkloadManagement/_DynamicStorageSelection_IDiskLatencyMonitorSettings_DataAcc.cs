using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000046 RID: 70
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_ : VariantObjectDataAccessorBase<IDiskLatencyMonitorSettings, _DynamicStorageSelection_IDiskLatencyMonitorSettings_Implementation_, _DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_>
	{
		// Token: 0x0400011E RID: 286
		internal string _Name_MaterializedValue_;

		// Token: 0x0400011F RID: 287
		internal TimeSpan _FixedTimeAverageWindowBucket_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000120 RID: 288
		internal ValueProvider<TimeSpan> _FixedTimeAverageWindowBucket_ValueProvider_;

		// Token: 0x04000121 RID: 289
		internal int _FixedTimeAverageNumberOfBuckets_MaterializedValue_;

		// Token: 0x04000122 RID: 290
		internal ValueProvider<int> _FixedTimeAverageNumberOfBuckets_ValueProvider_;

		// Token: 0x04000123 RID: 291
		internal TimeSpan _ResourceHealthPollerInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000124 RID: 292
		internal ValueProvider<TimeSpan> _ResourceHealthPollerInterval_ValueProvider_;
	}
}

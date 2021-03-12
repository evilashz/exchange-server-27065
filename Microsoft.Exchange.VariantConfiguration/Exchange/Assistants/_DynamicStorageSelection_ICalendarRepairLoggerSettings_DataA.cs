using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002A RID: 42
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_ : VariantObjectDataAccessorBase<ICalendarRepairLoggerSettings, _DynamicStorageSelection_ICalendarRepairLoggerSettings_Implementation_, _DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_>
	{
		// Token: 0x0400007B RID: 123
		internal string _Name_MaterializedValue_;

		// Token: 0x0400007C RID: 124
		internal bool _InsightLogEnabled_MaterializedValue_;

		// Token: 0x0400007D RID: 125
		internal ValueProvider<bool> _InsightLogEnabled_ValueProvider_;

		// Token: 0x0400007E RID: 126
		internal string _InsightLogDirectoryName_MaterializedValue_;

		// Token: 0x0400007F RID: 127
		internal ValueProvider<string> _InsightLogDirectoryName_ValueProvider_;

		// Token: 0x04000080 RID: 128
		internal TimeSpan _InsightLogFileAgeInDays_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000081 RID: 129
		internal ValueProvider<TimeSpan> _InsightLogFileAgeInDays_ValueProvider_;

		// Token: 0x04000082 RID: 130
		internal ulong _InsightLogDirectorySizeLimit_MaterializedValue_;

		// Token: 0x04000083 RID: 131
		internal ValueProvider<ulong> _InsightLogDirectorySizeLimit_ValueProvider_;

		// Token: 0x04000084 RID: 132
		internal ulong _InsightLogFileSize_MaterializedValue_;

		// Token: 0x04000085 RID: 133
		internal ValueProvider<ulong> _InsightLogFileSize_ValueProvider_;

		// Token: 0x04000086 RID: 134
		internal ulong _InsightLogCacheSize_MaterializedValue_;

		// Token: 0x04000087 RID: 135
		internal ValueProvider<ulong> _InsightLogCacheSize_ValueProvider_;

		// Token: 0x04000088 RID: 136
		internal TimeSpan _InsightLogFlushIntervalInSeconds_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000089 RID: 137
		internal ValueProvider<TimeSpan> _InsightLogFlushIntervalInSeconds_ValueProvider_;
	}
}

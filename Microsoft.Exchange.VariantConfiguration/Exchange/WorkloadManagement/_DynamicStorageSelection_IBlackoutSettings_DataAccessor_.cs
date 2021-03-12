using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001A RID: 26
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IBlackoutSettings_DataAccessor_ : VariantObjectDataAccessorBase<IBlackoutSettings, _DynamicStorageSelection_IBlackoutSettings_Implementation_, _DynamicStorageSelection_IBlackoutSettings_DataAccessor_>
	{
		// Token: 0x04000053 RID: 83
		internal string _Name_MaterializedValue_;

		// Token: 0x04000054 RID: 84
		internal TimeSpan _StartTime_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000055 RID: 85
		internal ValueProvider<TimeSpan> _StartTime_ValueProvider_;

		// Token: 0x04000056 RID: 86
		internal TimeSpan _EndTime_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000057 RID: 87
		internal ValueProvider<TimeSpan> _EndTime_ValueProvider_;
	}
}

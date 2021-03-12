using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000A5 RID: 165
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_ : VariantObjectDataAccessorBase<ISystemWorkloadManagerSettings, _DynamicStorageSelection_ISystemWorkloadManagerSettings_Implementation_, _DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_>
	{
		// Token: 0x04000301 RID: 769
		internal string _Name_MaterializedValue_;

		// Token: 0x04000302 RID: 770
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x04000303 RID: 771
		internal ValueProvider<int> _MaxConcurrency_ValueProvider_;

		// Token: 0x04000304 RID: 772
		internal TimeSpan _RefreshCycle_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000305 RID: 773
		internal ValueProvider<TimeSpan> _RefreshCycle_ValueProvider_;
	}
}

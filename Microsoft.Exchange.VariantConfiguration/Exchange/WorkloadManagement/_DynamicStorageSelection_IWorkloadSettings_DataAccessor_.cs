using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000BD RID: 189
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IWorkloadSettings_DataAccessor_ : VariantObjectDataAccessorBase<IWorkloadSettings, _DynamicStorageSelection_IWorkloadSettings_Implementation_, _DynamicStorageSelection_IWorkloadSettings_DataAccessor_>
	{
		// Token: 0x0400033A RID: 826
		internal string _Name_MaterializedValue_;

		// Token: 0x0400033B RID: 827
		internal WorkloadClassification _Classification_MaterializedValue_;

		// Token: 0x0400033C RID: 828
		internal ValueProvider<WorkloadClassification> _Classification_ValueProvider_;

		// Token: 0x0400033D RID: 829
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x0400033E RID: 830
		internal ValueProvider<int> _MaxConcurrency_ValueProvider_;

		// Token: 0x0400033F RID: 831
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x04000340 RID: 832
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x04000341 RID: 833
		internal bool _EnabledDuringBlackout_MaterializedValue_;

		// Token: 0x04000342 RID: 834
		internal ValueProvider<bool> _EnabledDuringBlackout_ValueProvider_;
	}
}

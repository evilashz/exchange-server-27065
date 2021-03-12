using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x020000AD RID: 173
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ITransportFlowSettings_DataAccessor_ : VariantObjectDataAccessorBase<ITransportFlowSettings, _DynamicStorageSelection_ITransportFlowSettings_Implementation_, _DynamicStorageSelection_ITransportFlowSettings_DataAccessor_>
	{
		// Token: 0x04000312 RID: 786
		internal string _Name_MaterializedValue_;

		// Token: 0x04000313 RID: 787
		internal bool _SkipTokenInfoGeneration_MaterializedValue_;

		// Token: 0x04000314 RID: 788
		internal ValueProvider<bool> _SkipTokenInfoGeneration_ValueProvider_;

		// Token: 0x04000315 RID: 789
		internal bool _SkipMdmGeneration_MaterializedValue_;

		// Token: 0x04000316 RID: 790
		internal ValueProvider<bool> _SkipMdmGeneration_ValueProvider_;

		// Token: 0x04000317 RID: 791
		internal bool _UseMdmFlow_MaterializedValue_;

		// Token: 0x04000318 RID: 792
		internal ValueProvider<bool> _UseMdmFlow_ValueProvider_;
	}
}

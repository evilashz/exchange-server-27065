using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000052 RID: 82
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IFeature_DataAccessor_ : VariantObjectDataAccessorBase<IFeature, _DynamicStorageSelection_IFeature_Implementation_, _DynamicStorageSelection_IFeature_DataAccessor_>
	{
		// Token: 0x0400013F RID: 319
		internal string _Name_MaterializedValue_;

		// Token: 0x04000140 RID: 320
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x04000141 RID: 321
		internal ValueProvider<bool> _Enabled_ValueProvider_;
	}
}

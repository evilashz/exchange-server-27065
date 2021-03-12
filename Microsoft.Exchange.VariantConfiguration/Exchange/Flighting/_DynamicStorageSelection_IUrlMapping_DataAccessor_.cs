using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B2 RID: 178
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IUrlMapping_DataAccessor_ : VariantObjectDataAccessorBase<IUrlMapping, _DynamicStorageSelection_IUrlMapping_Implementation_, _DynamicStorageSelection_IUrlMapping_DataAccessor_>
	{
		// Token: 0x0400031F RID: 799
		internal string _Name_MaterializedValue_;

		// Token: 0x04000320 RID: 800
		internal string _Url_MaterializedValue_;

		// Token: 0x04000321 RID: 801
		internal ValueProvider<string> _Url_ValueProvider_;

		// Token: 0x04000322 RID: 802
		internal string _RemapTo_MaterializedValue_;

		// Token: 0x04000323 RID: 803
		internal ValueProvider<string> _RemapTo_ValueProvider_;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200005A RID: 90
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IFlight_DataAccessor_ : VariantObjectDataAccessorBase<IFlight, _DynamicStorageSelection_IFlight_Implementation_, _DynamicStorageSelection_IFlight_DataAccessor_>
	{
		// Token: 0x0400014D RID: 333
		internal string _Name_MaterializedValue_;

		// Token: 0x0400014E RID: 334
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x0400014F RID: 335
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x04000150 RID: 336
		internal string _Rotate_MaterializedValue_;

		// Token: 0x04000151 RID: 337
		internal ValueProvider<string> _Rotate_ValueProvider_;

		// Token: 0x04000152 RID: 338
		internal string _Ramp_MaterializedValue_;

		// Token: 0x04000153 RID: 339
		internal ValueProvider<string> _Ramp_ValueProvider_;

		// Token: 0x04000154 RID: 340
		internal string _RampSeed_MaterializedValue_;

		// Token: 0x04000155 RID: 341
		internal ValueProvider<string> _RampSeed_ValueProvider_;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000062 RID: 98
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IIndexStatusSettings_DataAccessor_ : VariantObjectDataAccessorBase<IIndexStatusSettings, _DynamicStorageSelection_IIndexStatusSettings_Implementation_, _DynamicStorageSelection_IIndexStatusSettings_DataAccessor_>
	{
		// Token: 0x04000167 RID: 359
		internal string _Name_MaterializedValue_;

		// Token: 0x04000168 RID: 360
		internal TimeSpan _InvalidateInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000169 RID: 361
		internal ValueProvider<TimeSpan> _InvalidateInterval_ValueProvider_;
	}
}

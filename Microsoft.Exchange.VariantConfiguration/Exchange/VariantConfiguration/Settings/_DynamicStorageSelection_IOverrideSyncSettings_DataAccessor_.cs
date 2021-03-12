using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x02000092 RID: 146
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_ : VariantObjectDataAccessorBase<IOverrideSyncSettings, _DynamicStorageSelection_IOverrideSyncSettings_Implementation_, _DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_>
	{
		// Token: 0x040002B4 RID: 692
		internal string _Name_MaterializedValue_;

		// Token: 0x040002B5 RID: 693
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002B6 RID: 694
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x040002B7 RID: 695
		internal TimeSpan _RefreshInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x040002B8 RID: 696
		internal ValueProvider<TimeSpan> _RefreshInterval_ValueProvider_;
	}
}

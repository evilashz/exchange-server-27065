using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.HolidayCalendars
{
	// Token: 0x0200005E RID: 94
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IHostSettings_DataAccessor_ : VariantObjectDataAccessorBase<IHostSettings, _DynamicStorageSelection_IHostSettings_Implementation_, _DynamicStorageSelection_IHostSettings_DataAccessor_>
	{
		// Token: 0x0400015D RID: 349
		internal string _Name_MaterializedValue_;

		// Token: 0x0400015E RID: 350
		internal string _Endpoint_MaterializedValue_;

		// Token: 0x0400015F RID: 351
		internal ValueProvider<string> _Endpoint_ValueProvider_;

		// Token: 0x04000160 RID: 352
		internal int _Timeout_MaterializedValue_;

		// Token: 0x04000161 RID: 353
		internal ValueProvider<int> _Timeout_ValueProvider_;
	}
}

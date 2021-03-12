using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000056 RID: 86
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IFeederSettings_DataAccessor_ : VariantObjectDataAccessorBase<IFeederSettings, _DynamicStorageSelection_IFeederSettings_Implementation_, _DynamicStorageSelection_IFeederSettings_DataAccessor_>
	{
		// Token: 0x04000146 RID: 326
		internal string _Name_MaterializedValue_;

		// Token: 0x04000147 RID: 327
		internal int _QueueSize_MaterializedValue_;

		// Token: 0x04000148 RID: 328
		internal ValueProvider<int> _QueueSize_ValueProvider_;
	}
}

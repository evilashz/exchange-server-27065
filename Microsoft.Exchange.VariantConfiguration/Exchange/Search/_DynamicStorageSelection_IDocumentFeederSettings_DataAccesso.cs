using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200004A RID: 74
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_ : VariantObjectDataAccessorBase<IDocumentFeederSettings, _DynamicStorageSelection_IDocumentFeederSettings_Implementation_, _DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_>
	{
		// Token: 0x0400012B RID: 299
		internal string _Name_MaterializedValue_;

		// Token: 0x0400012C RID: 300
		internal TimeSpan _BatchTimeout_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400012D RID: 301
		internal ValueProvider<TimeSpan> _BatchTimeout_ValueProvider_;

		// Token: 0x0400012E RID: 302
		internal TimeSpan _LostCallbackTimeout_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400012F RID: 303
		internal ValueProvider<TimeSpan> _LostCallbackTimeout_ValueProvider_;
	}
}

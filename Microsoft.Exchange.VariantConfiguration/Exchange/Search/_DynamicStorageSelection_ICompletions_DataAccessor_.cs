using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200003E RID: 62
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ICompletions_DataAccessor_ : VariantObjectDataAccessorBase<ICompletions, _DynamicStorageSelection_ICompletions_Implementation_, _DynamicStorageSelection_ICompletions_DataAccessor_>
	{
		// Token: 0x0400010A RID: 266
		internal string _Name_MaterializedValue_;

		// Token: 0x0400010B RID: 267
		internal int _MaxCompletionTraversalCount_MaterializedValue_;

		// Token: 0x0400010C RID: 268
		internal ValueProvider<int> _MaxCompletionTraversalCount_ValueProvider_;

		// Token: 0x0400010D RID: 269
		internal string _TopNExclusionCharacters_MaterializedValue_;

		// Token: 0x0400010E RID: 270
		internal ValueProvider<string> _TopNExclusionCharacters_ValueProvider_;

		// Token: 0x0400010F RID: 271
		internal bool _FinalWordSuggestionsEnabled_MaterializedValue_;

		// Token: 0x04000110 RID: 272
		internal ValueProvider<bool> _FinalWordSuggestionsEnabled_ValueProvider_;
	}
}

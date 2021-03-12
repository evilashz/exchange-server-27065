using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200003D RID: 61
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface ICompletions : ISettings
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000163 RID: 355
		int MaxCompletionTraversalCount { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000164 RID: 356
		string TopNExclusionCharacters { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000165 RID: 357
		bool FinalWordSuggestionsEnabled { get; }
	}
}

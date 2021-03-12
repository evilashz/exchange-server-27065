using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000040 RID: 64
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ICompletions_Implementation_ : ICompletions, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000045CE File Offset: 0x000027CE
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000045D1 File Offset: 0x000027D1
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000045D4 File Offset: 0x000027D4
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000045DC File Offset: 0x000027DC
		public int MaxCompletionTraversalCount
		{
			get
			{
				return this._MaxCompletionTraversalCount_MaterializedValue_;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000045E4 File Offset: 0x000027E4
		public string TopNExclusionCharacters
		{
			get
			{
				return this._TopNExclusionCharacters_MaterializedValue_;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000045EC File Offset: 0x000027EC
		public bool FinalWordSuggestionsEnabled
		{
			get
			{
				return this._FinalWordSuggestionsEnabled_MaterializedValue_;
			}
		}

		// Token: 0x04000113 RID: 275
		internal string _Name_MaterializedValue_;

		// Token: 0x04000114 RID: 276
		internal int _MaxCompletionTraversalCount_MaterializedValue_;

		// Token: 0x04000115 RID: 277
		internal string _TopNExclusionCharacters_MaterializedValue_;

		// Token: 0x04000116 RID: 278
		internal bool _FinalWordSuggestionsEnabled_MaterializedValue_;
	}
}

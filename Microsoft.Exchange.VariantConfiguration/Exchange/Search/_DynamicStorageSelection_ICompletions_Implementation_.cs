using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200003F RID: 63
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ICompletions_Implementation_ : ICompletions, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICompletions_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004506 File Offset: 0x00002706
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000450E File Offset: 0x0000270E
		_DynamicStorageSelection_ICompletions_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICompletions_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004516 File Offset: 0x00002716
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICompletions_DataAccessor_>.Initialize(_DynamicStorageSelection_ICompletions_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00004526 File Offset: 0x00002726
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00004533 File Offset: 0x00002733
		public int MaxCompletionTraversalCount
		{
			get
			{
				if (this.dataAccessor._MaxCompletionTraversalCount_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxCompletionTraversalCount_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxCompletionTraversalCount_MaterializedValue_;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00004564 File Offset: 0x00002764
		public string TopNExclusionCharacters
		{
			get
			{
				if (this.dataAccessor._TopNExclusionCharacters_ValueProvider_ != null)
				{
					return this.dataAccessor._TopNExclusionCharacters_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._TopNExclusionCharacters_MaterializedValue_;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00004595 File Offset: 0x00002795
		public bool FinalWordSuggestionsEnabled
		{
			get
			{
				if (this.dataAccessor._FinalWordSuggestionsEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._FinalWordSuggestionsEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._FinalWordSuggestionsEnabled_MaterializedValue_;
			}
		}

		// Token: 0x04000111 RID: 273
		private _DynamicStorageSelection_ICompletions_DataAccessor_ dataAccessor;

		// Token: 0x04000112 RID: 274
		private VariantContextSnapshot context;
	}
}

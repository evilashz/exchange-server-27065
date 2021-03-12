using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200006B RID: 107
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IInlineExploreSettings_Implementation_ : IInlineExploreSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IInlineExploreSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000554E File Offset: 0x0000374E
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00005556 File Offset: 0x00003756
		_DynamicStorageSelection_IInlineExploreSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IInlineExploreSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000555E File Offset: 0x0000375E
		void IDataAccessorBackedObject<_DynamicStorageSelection_IInlineExploreSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IInlineExploreSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000556E File Offset: 0x0000376E
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000557B File Offset: 0x0000377B
		public string Content
		{
			get
			{
				if (this.dataAccessor._Content_ValueProvider_ != null)
				{
					return this.dataAccessor._Content_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Content_MaterializedValue_;
			}
		}

		// Token: 0x040001DE RID: 478
		private _DynamicStorageSelection_IInlineExploreSettings_DataAccessor_ dataAccessor;

		// Token: 0x040001DF RID: 479
		private VariantContextSnapshot context;
	}
}

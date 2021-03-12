using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200006F RID: 111
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IInstantSearch_Implementation_ : IInstantSearch, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IInstantSearch_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000055DA File Offset: 0x000037DA
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000055E2 File Offset: 0x000037E2
		_DynamicStorageSelection_IInstantSearch_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IInstantSearch_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000055EA File Offset: 0x000037EA
		void IDataAccessorBackedObject<_DynamicStorageSelection_IInstantSearch_DataAccessor_>.Initialize(_DynamicStorageSelection_IInstantSearch_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000055FA File Offset: 0x000037FA
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00005607 File Offset: 0x00003807
		public int FastQueryPath
		{
			get
			{
				if (this.dataAccessor._FastQueryPath_ValueProvider_ != null)
				{
					return this.dataAccessor._FastQueryPath_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._FastQueryPath_MaterializedValue_;
			}
		}

		// Token: 0x040001E5 RID: 485
		private _DynamicStorageSelection_IInstantSearch_DataAccessor_ dataAccessor;

		// Token: 0x040001E6 RID: 486
		private VariantContextSnapshot context;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000053 RID: 83
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IFeature_Implementation_ : IFeature, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IFeature_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004978 File Offset: 0x00002B78
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004980 File Offset: 0x00002B80
		_DynamicStorageSelection_IFeature_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IFeature_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004988 File Offset: 0x00002B88
		void IDataAccessorBackedObject<_DynamicStorageSelection_IFeature_DataAccessor_>.Initialize(_DynamicStorageSelection_IFeature_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004998 File Offset: 0x00002B98
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000049A5 File Offset: 0x00002BA5
		public bool Enabled
		{
			get
			{
				if (this.dataAccessor._Enabled_ValueProvider_ != null)
				{
					return this.dataAccessor._Enabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x04000142 RID: 322
		private _DynamicStorageSelection_IFeature_DataAccessor_ dataAccessor;

		// Token: 0x04000143 RID: 323
		private VariantContextSnapshot context;
	}
}

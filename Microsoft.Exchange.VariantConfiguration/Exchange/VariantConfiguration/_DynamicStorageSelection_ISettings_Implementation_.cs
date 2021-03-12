using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x020000A2 RID: 162
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ISettings_Implementation_ : ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ISettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00006CA1 File Offset: 0x00004EA1
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00006CA9 File Offset: 0x00004EA9
		_DynamicStorageSelection_ISettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ISettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00006CB1 File Offset: 0x00004EB1
		void IDataAccessorBackedObject<_DynamicStorageSelection_ISettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ISettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00006CC1 File Offset: 0x00004EC1
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x040002FE RID: 766
		private _DynamicStorageSelection_ISettings_DataAccessor_ dataAccessor;

		// Token: 0x040002FF RID: 767
		private VariantContextSnapshot context;
	}
}

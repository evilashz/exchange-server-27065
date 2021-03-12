using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000043 RID: 67
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_Implementation_ : IDelegatedSetupRoleGroupSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004604 File Offset: 0x00002804
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000460C File Offset: 0x0000280C
		_DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004614 File Offset: 0x00002814
		void IDataAccessorBackedObject<_DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00004624 File Offset: 0x00002824
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004631 File Offset: 0x00002831
		public DelegatedSetupRoleGroupValueEnum DelegatedSetupRoleGroupValue
		{
			get
			{
				if (this.dataAccessor._DelegatedSetupRoleGroupValue_ValueProvider_ != null)
				{
					return this.dataAccessor._DelegatedSetupRoleGroupValue_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DelegatedSetupRoleGroupValue_MaterializedValue_;
			}
		}

		// Token: 0x0400011A RID: 282
		private _DynamicStorageSelection_IDelegatedSetupRoleGroupSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400011B RID: 283
		private VariantContextSnapshot context;
	}
}

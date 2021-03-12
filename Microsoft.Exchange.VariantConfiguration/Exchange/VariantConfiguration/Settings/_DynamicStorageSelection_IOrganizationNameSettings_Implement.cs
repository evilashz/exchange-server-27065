using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x0200008F RID: 143
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IOrganizationNameSettings_Implementation_ : IOrganizationNameSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000663D File Offset: 0x0000483D
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00006645 File Offset: 0x00004845
		_DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000664D File Offset: 0x0000484D
		void IDataAccessorBackedObject<_DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000665D File Offset: 0x0000485D
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000666A File Offset: 0x0000486A
		public IList<string> OrgNames
		{
			get
			{
				if (this.dataAccessor._OrgNames_ValueProvider_ != null)
				{
					return this.dataAccessor._OrgNames_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._OrgNames_MaterializedValue_;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000669B File Offset: 0x0000489B
		public IList<string> DogfoodOrgNames
		{
			get
			{
				if (this.dataAccessor._DogfoodOrgNames_ValueProvider_ != null)
				{
					return this.dataAccessor._DogfoodOrgNames_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DogfoodOrgNames_MaterializedValue_;
			}
		}

		// Token: 0x040002AF RID: 687
		private _DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_ dataAccessor;

		// Token: 0x040002B0 RID: 688
		private VariantContextSnapshot context;
	}
}

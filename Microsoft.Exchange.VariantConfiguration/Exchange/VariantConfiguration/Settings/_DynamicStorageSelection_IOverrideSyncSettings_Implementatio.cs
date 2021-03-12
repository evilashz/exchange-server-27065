using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x02000093 RID: 147
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IOverrideSyncSettings_Implementation_ : IOverrideSyncSettings, IFeature, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000670E File Offset: 0x0000490E
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00006716 File Offset: 0x00004916
		_DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000671E File Offset: 0x0000491E
		void IDataAccessorBackedObject<_DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000672E File Offset: 0x0000492E
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000673B File Offset: 0x0000493B
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

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000676C File Offset: 0x0000496C
		public TimeSpan RefreshInterval
		{
			get
			{
				if (this.dataAccessor._RefreshInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._RefreshInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RefreshInterval_MaterializedValue_;
			}
		}

		// Token: 0x040002B9 RID: 697
		private _DynamicStorageSelection_IOverrideSyncSettings_DataAccessor_ dataAccessor;

		// Token: 0x040002BA RID: 698
		private VariantContextSnapshot context;
	}
}

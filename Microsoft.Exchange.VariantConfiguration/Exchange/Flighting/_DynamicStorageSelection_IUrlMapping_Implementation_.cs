using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B3 RID: 179
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IUrlMapping_Implementation_ : IUrlMapping, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IUrlMapping_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00006F5B File Offset: 0x0000515B
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00006F63 File Offset: 0x00005163
		_DynamicStorageSelection_IUrlMapping_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IUrlMapping_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00006F6B File Offset: 0x0000516B
		void IDataAccessorBackedObject<_DynamicStorageSelection_IUrlMapping_DataAccessor_>.Initialize(_DynamicStorageSelection_IUrlMapping_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00006F7B File Offset: 0x0000517B
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00006F88 File Offset: 0x00005188
		public string Url
		{
			get
			{
				if (this.dataAccessor._Url_ValueProvider_ != null)
				{
					return this.dataAccessor._Url_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Url_MaterializedValue_;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00006FB9 File Offset: 0x000051B9
		public string RemapTo
		{
			get
			{
				if (this.dataAccessor._RemapTo_ValueProvider_ != null)
				{
					return this.dataAccessor._RemapTo_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RemapTo_MaterializedValue_;
			}
		}

		// Token: 0x04000324 RID: 804
		private _DynamicStorageSelection_IUrlMapping_DataAccessor_ dataAccessor;

		// Token: 0x04000325 RID: 805
		private VariantContextSnapshot context;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AutoDiscover
{
	// Token: 0x02000097 RID: 151
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IOWAUrl_Implementation_ : IOWAUrl, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IOWAUrl_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000067DF File Offset: 0x000049DF
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000067E7 File Offset: 0x000049E7
		_DynamicStorageSelection_IOWAUrl_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IOWAUrl_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000067EF File Offset: 0x000049EF
		void IDataAccessorBackedObject<_DynamicStorageSelection_IOWAUrl_DataAccessor_>.Initialize(_DynamicStorageSelection_IOWAUrl_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000067FF File Offset: 0x000049FF
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000680C File Offset: 0x00004A0C
		public string OwaInternalAuthMethods
		{
			get
			{
				if (this.dataAccessor._OwaInternalAuthMethods_ValueProvider_ != null)
				{
					return this.dataAccessor._OwaInternalAuthMethods_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._OwaInternalAuthMethods_MaterializedValue_;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000683D File Offset: 0x00004A3D
		public string OwaExternalAuthMethods
		{
			get
			{
				if (this.dataAccessor._OwaExternalAuthMethods_ValueProvider_ != null)
				{
					return this.dataAccessor._OwaExternalAuthMethods_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._OwaExternalAuthMethods_MaterializedValue_;
			}
		}

		// Token: 0x040002C3 RID: 707
		private _DynamicStorageSelection_IOWAUrl_DataAccessor_ dataAccessor;

		// Token: 0x040002C4 RID: 708
		private VariantContextSnapshot context;
	}
}

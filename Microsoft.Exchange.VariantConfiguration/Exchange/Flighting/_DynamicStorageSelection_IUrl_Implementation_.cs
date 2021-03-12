using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B6 RID: 182
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IUrl_Implementation_ : IUrl, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IUrl_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00007020 File Offset: 0x00005220
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00007028 File Offset: 0x00005228
		_DynamicStorageSelection_IUrl_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IUrl_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00007030 File Offset: 0x00005230
		void IDataAccessorBackedObject<_DynamicStorageSelection_IUrl_DataAccessor_>.Initialize(_DynamicStorageSelection_IUrl_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00007040 File Offset: 0x00005240
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000704D File Offset: 0x0000524D
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

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000707E File Offset: 0x0000527E
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

		// Token: 0x0400032E RID: 814
		private _DynamicStorageSelection_IUrl_DataAccessor_ dataAccessor;

		// Token: 0x0400032F RID: 815
		private VariantContextSnapshot context;
	}
}

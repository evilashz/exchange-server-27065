using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200009F RID: 159
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ISettingsValue_Implementation_ : ISettingsValue, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ISettingsValue_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00006C15 File Offset: 0x00004E15
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00006C1D File Offset: 0x00004E1D
		_DynamicStorageSelection_ISettingsValue_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ISettingsValue_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00006C25 File Offset: 0x00004E25
		void IDataAccessorBackedObject<_DynamicStorageSelection_ISettingsValue_DataAccessor_>.Initialize(_DynamicStorageSelection_ISettingsValue_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00006C35 File Offset: 0x00004E35
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00006C42 File Offset: 0x00004E42
		public string Value
		{
			get
			{
				if (this.dataAccessor._Value_ValueProvider_ != null)
				{
					return this.dataAccessor._Value_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Value_MaterializedValue_;
			}
		}

		// Token: 0x040002F9 RID: 761
		private _DynamicStorageSelection_ISettingsValue_DataAccessor_ dataAccessor;

		// Token: 0x040002FA RID: 762
		private VariantContextSnapshot context;
	}
}
